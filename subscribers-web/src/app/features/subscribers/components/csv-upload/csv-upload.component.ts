import { Component, EventEmitter, Output } from '@angular/core';

@Component({
  selector: 'csv-upload',
  templateUrl: './csv-upload.component.html',
  styleUrls: ['./csv-upload.component.css']
})
export class CsvUploadComponent {
  @Output() filesSelected = new EventEmitter<FormData>(); 
  @Output() removeErrors = new EventEmitter<void>(); 

  selectedFiles: File[] = [];
  fileNames: string[] = [];

  constructor() {}

  onFileChange(event: any) {
    const files = event.target.files;
    if (files) {
      this.selectedFiles = Array.from(files);
      this.fileNames = this.selectedFiles.map(file => file.name);
    }
    this.removeErrors.emit();
  }

  emitFiles() {
    const formData = new FormData();
    this.selectedFiles.forEach(file => {
      formData.append('files', file, file.name);
    });
    this.filesSelected.emit(formData);
    this.selectedFiles = [];
    this.fileNames = [];
  }

  removeFile(fileName: string) {
    this.selectedFiles = this.selectedFiles.filter(file => file.name !== fileName);
    this.fileNames = this.fileNames.filter(file => file !== fileName);
  }
}
