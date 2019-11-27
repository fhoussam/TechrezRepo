import { Component, OnInit } from '@angular/core';
import { select } from '@angular-redux/store';

@Component({
    selector: 'app-feed',
    templateUrl: './feed.component.html',
    styleUrls: ['./feed.component.css']
})
export class FeedComponent implements OnInit {

    @select() lastOperationTimeStamp;
    constructor() { }

    ngOnInit() {
    }

}
