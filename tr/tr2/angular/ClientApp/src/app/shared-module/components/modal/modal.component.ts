import { Component, OnInit, Input } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-modal',
  templateUrl: './modal.component.html',
  styleUrls: ['./modal.component.css']
})
export class ModalComponent implements OnInit {

  @Input() title: string;
  @Input() cancelUrl: string;

  constructor(
    private router: Router
  ) { }

  hideModalAfterCancel() {
    this.router.navigateByUrl(this.cancelUrl);
  }

  ngOnInit() {
  }
}
