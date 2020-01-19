import { Injectable } from '@angular/core';
import * as signalR from "@aspnet/signalr";
import { Feed } from '../models/Feed';
import { Store } from '@ngrx/store';
import { AddNewFeeds } from '../Redux/Feed/feeds.actions';
import { BehaviorSubject } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class SignalRService {
    //public data: Feed[];
    //public bradcastedData: Feed[];

    constructor(
        private feedStore: Store<{ feeds: { feeds: Feed[] } }>,
    ) { }

    private hubConnection: signalR.HubConnection;
    public onDataPush = new BehaviorSubject<Feed>(null);

    public startConnection() {
        this.hubConnection = new signalR.HubConnectionBuilder()
            .withUrl('https://localhost:44301/feedhub')
            .build();

        this.hubConnection
            .start()
            .then(() => console.log('Connection started'))
            .catch(err => console.log('Error while starting connection: ' + err))
    }

    public addTransferFeedDataListener = () => {
        this.hubConnection.on('transferfeeddata', (data: Feed[]) => {
            this.feedStore.dispatch(new AddNewFeeds(data));
            this.onDataPush.next(data[0]);
            //console.log(data);
        });
    }

    public stopConnection() {
        this.hubConnection.stop();
    }

    //public broadcastFeedData = () => {
    //    this.hubConnection.invoke('broadcastfeeddata', this.data)
    //        .catch(err => console.error(err));
    //}

    //public addBroadcastFeedDataListener = () => {
    //    this.hubConnection.on('broadcastfeeddata', (data) => {
    //        this.feedStore.dispatch(new AddFeeds(data))
    //        this.bradcastedData = data;
    //    })
    //}
}
