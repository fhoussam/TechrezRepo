import { Component, OnInit, Input, NgZone } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { adminProductListItem } from '../../../../models/adminProductListItem';

@Component({
    selector: 'explore',
    templateUrl: './explore.component.html',
    styleUrls: ['./explore.component.css']
})
export class ExploreComponent implements OnInit {

    @Input() selectedItem: adminProductListItem;
    constructor(private route: ActivatedRoute, private router: Router, private zone: NgZone) { console.log(this.selectedItem); }

    details() {
        let r: string = 'explore/details/' + this.selectedItem.code;
        //this.router.navigate(['explore/details'], { relativeTo: this.route });
        this.zone.run(() => { this.router.navigate(['explore/details']); });
    }

    orders() {
        //this.router.navigate(['explore/order'], { relativeTo: this.route });
        this.zone.run(() => { this.router.navigate(['explore/orders']); });
    }

    ngOnInit() {

    }
}
