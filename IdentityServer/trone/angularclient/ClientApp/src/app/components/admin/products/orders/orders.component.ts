import { Component, OnInit } from '@angular/core';

@Component({
    selector: 'app-orders',
    templateUrl: './orders.component.html',
    styleUrls: ['./orders.component.css']
})
export class OrdersComponent implements OnInit {

    constructor() {
        //aside from custom URL serilizer, here's another way to replace strings within the URL we actually want to GO to
        //this.router.navigate(["/admin/products/" + tab], { queryParams: searchParams })
        //    .then(navResult => {
        //        //console.log('success : ' + navResult);
        //        let url: string = "/admin/products?" + queryString;
        //        this.location.replaceState(url);
        //    }).catch(navResult => {
        //        console.log("fail : " + navResult);
        //    });
    }

    ngOnInit() {
    }

}
