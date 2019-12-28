"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.ADD_FEED = 'ADD_FEED';
exports.LOAD_FEEDS = 'LOAD_FEEDS';
var AddFeed = /** @class */ (function () {
    function AddFeed(payload) {
        this.payload = payload;
        this.type = exports.ADD_FEED;
    }
    return AddFeed;
}());
exports.AddFeed = AddFeed;
var LoadFeeds = /** @class */ (function () {
    function LoadFeeds(payload) {
        this.payload = payload;
        this.type = exports.LOAD_FEEDS;
    }
    return LoadFeeds;
}());
exports.LoadFeeds = LoadFeeds;
//# sourceMappingURL=feeds.actions.js.map