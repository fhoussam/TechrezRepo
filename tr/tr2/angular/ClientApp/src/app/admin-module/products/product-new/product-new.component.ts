import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-product-new',
  templateUrl: './product-new.component.html',
  styleUrls: ['./product-new.component.css']
})
export class ProductNewComponent implements OnInit {

  title = "Search product";
  exitUrl = '..';

  hideModalAfterCancel() {
    this.router.navigate([this.exitUrl], { relativeTo: this.activatedRoute });
  }

  hideModalAfterSuccess(event) {
    this.router.navigateByUrl(event);
  }

  constructor(private router: Router, private activatedRoute: ActivatedRoute) { }

  ngOnInit() {
  }

}
