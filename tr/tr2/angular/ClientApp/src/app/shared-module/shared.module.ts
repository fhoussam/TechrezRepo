import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AlertMessageComponent } from './alert-message/alert-message.component';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';

@NgModule({
  declarations: [
    AlertMessageComponent,
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    FormsModule
  ],
  exports: [
    AlertMessageComponent,
    ReactiveFormsModule,
    FormsModule
  ]
})
export class SharedModule { }
