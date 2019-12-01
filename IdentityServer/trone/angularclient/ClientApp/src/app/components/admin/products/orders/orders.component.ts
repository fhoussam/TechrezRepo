import { Component, OnInit } from '@angular/core';
import { propertyToUrl, urlToProperty, urlToList } from "query-string-params";
import { Location } from '@angular/common';
import { Router } from '@angular/router';

@Component({
  selector: 'app-orders',
  templateUrl: './orders.component.html',
  styleUrls: ['./orders.component.css']
})
export class OrdersComponent implements OnInit {

    constructor(
        private router: Router,
        private _location: Location,
    ) {
        //to refactor
        let searchParams: any = urlToProperty(location.search);
        searchParams.st = "orders";
        let queryString: string = propertyToUrl(searchParams);
        this.router.navigate(["/admin/products/" + searchParams.st], { queryParams: searchParams })
            .then(() => {
                let url: string = "/admin/products?" + queryString;
                this._location.replaceState(url);
            }).catch(navResult => {
                console.log("fail : " + navResult);
            });
    }

  ngOnInit() {
  }

}
