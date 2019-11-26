import { Component, OnInit, Input } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { adminProductListItem } from '../../../../models/adminProductListItem';

@Component({
    selector: 'explore',
    templateUrl: './explore.component.html',
    styleUrls: ['./explore.component.css']
})
export class ExploreComponent implements OnInit {

    @Input() selectedItem: adminProductListItem;
    constructor(private route: ActivatedRoute, private router: Router) {
    }

    ngOnInit() {

    }
}
