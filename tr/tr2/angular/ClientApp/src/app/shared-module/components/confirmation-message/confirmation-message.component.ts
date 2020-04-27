import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-confirmation-message',
  templateUrl: './confirmation-message.component.html',
  styleUrls: ['./confirmation-message.component.css']
})
export class ConfirmationMessageComponent {

  @Input() message: string;
  @Output() close = new EventEmitter();

  onClose(decision) {
    this.close.emit(decision);
  }
}
