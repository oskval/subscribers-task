import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ApiResponse, Subscriber } from '../../../models';

@Injectable({
  providedIn: 'root'
})
export class SubscriberApiService {
  private apiUrl = 'http://localhost:5219/api/Subscribers';

  constructor(private http: HttpClient) { }

  getSubscribers(page: number, pageSize: number, sortColumn: string, sortDirection: string): Observable<ApiResponse<Subscriber[]>> {
    const params = new HttpParams()
      .set('page', page.toString())
      .set('pageSize', pageSize.toString())
      .set('sortColumn', sortColumn)
      .set('sortDirection', sortDirection);

    return this.http.get<ApiResponse<Subscriber[]>>(`${this.apiUrl}`, { params });
  }

  importSubsribers(files: FormData): Observable<ApiResponse<boolean>> {
    return this.http.post<ApiResponse<boolean>>(`${this.apiUrl}/import`, files);
  }

  deleteSubscriber(id: string): Observable<ApiResponse<boolean>> {
    return this.http.delete<ApiResponse<boolean>>(`${this.apiUrl}/${id}`);
  }
}
