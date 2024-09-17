import { NgModule } from '@angular/core';
import { SubscriberApiService } from './services/subscriberApiService';
import { SubscriberListComponent } from './components/subscriber-list/subscriber-list.component';
import { SharedModule } from '../../shared/shared.module';
import { CsvUploadComponent } from './components/csv-upload/csv-upload.component';

@NgModule({
  declarations: [SubscriberListComponent, CsvUploadComponent],
  providers: [SubscriberApiService],
  imports: [SharedModule],
  exports: [SubscriberListComponent],
})
export class SubscriberModule {}
