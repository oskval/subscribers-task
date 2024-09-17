import { Component, OnInit, ViewChild } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { SubscriberApiService } from '../../services/subscriberApiService';
import { Subscriber } from '../../../../models/subscriber';
import { MatSort } from '@angular/material/sort';
import { MatPaginator, PageEvent } from '@angular/material/paginator';

@Component({
  selector: 'subscriber-list',
  templateUrl: './subscriber-list.component.html',
  styleUrls: ['./subscriber-list.component.css'],
})
export class SubscriberListComponent implements OnInit {
  displayedColumns: string[] = ['id', 'email', 'subscriptionDate', 'actions'];
  dataSource = new MatTableDataSource<Subscriber>();
  totalItems = 0;
  pageSize = 5;
  pageIndex = 0;
  sortColumn = 'id';
  sortDirection = 'asc';

  importErrors: string[] = [];

  dataLoaded: boolean = false;

  @ViewChild(MatSort) sort!: MatSort;
  @ViewChild(MatPaginator) paginator!: MatPaginator;

  constructor(private subscriberApiService: SubscriberApiService) {}

  ngOnInit() {
    this.loadSubscribers();
  }

  loadSubscribers() {
    this.subscriberApiService
      .getSubscribers(
        this.pageIndex + 1,
        this.pageSize,
        this.sortColumn,
        this.sortDirection
      )
      .subscribe((response) => {
        if (response.data?.length && response.totalItems) {
          this.dataSource.data = response.data;
          this.totalItems = response.totalItems;
        } else {
          console.log(response.errors);
        }
        this.dataLoaded = true;
      });
  }

  deleteSubscriber(id: string) {
    this.subscriberApiService.deleteSubscriber(id)
      .subscribe(() => {
        this.dataSource.data = this.dataSource.data.filter((data) => data.id !== id);
        this.totalItems--;
      });
  }

  onPaginateChange(event: PageEvent) {
    this.pageIndex = event.pageIndex;
    this.pageSize = event.pageSize;
    this.loadSubscribers();
  }

  onSortChange() {
    this.sortColumn = this.sort.active;
    this.sortDirection = this.sort.direction || 'asc';
    this.loadSubscribers();
  }

  handleFilesSelected(formData: FormData) {
    this.subscriberApiService
      .importSubsribers(formData)
      .subscribe((response) => {
        if (response.data) {
          this.resetPagingAndSorting();
          this.loadSubscribers();
        } else {
          this.importErrors = response.errors!;
        }
      });
  }

  removeErrors() {
    this.importErrors = [];
  }

  private resetPagingAndSorting(): void {
    this.totalItems = 0;
    this.pageSize = 5;
    this.pageIndex = 0;
    this.sortColumn = 'id';
    this.sortDirection = 'asc';
  }
}
