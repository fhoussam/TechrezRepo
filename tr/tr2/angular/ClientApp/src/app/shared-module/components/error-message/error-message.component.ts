import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-error-message',
  templateUrl: './error-message.component.html',
  styleUrls: ['./error-message.component.css']
})
export class ErrorMessageComponent implements OnInit {

  @Input() message: string;
  @Output() close = new EventEmitter();

  closeErrorMessage() {
    this.close.emit(null);
  }

  constructor() { }

  ngOnInit() {
  }
}
