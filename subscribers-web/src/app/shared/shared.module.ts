import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTableModule } from '@angular/material/table';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatSortModule } from '@angular/material/sort';
import { MatPaginatorModule } from '@angular/material/paginator';

@NgModule({
  imports: [
    CommonModule,
    MatTableModule,
    BrowserModule,
    BrowserAnimationsModule,
    MatSortModule,
    MatPaginatorModule,
  ],
  exports: [
    CommonModule,
    MatTableModule,
    BrowserModule,
    BrowserAnimationsModule,
    MatSortModule,
    MatPaginatorModule,
  ],
})
export class SharedModule {}
