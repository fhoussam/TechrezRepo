"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.ADD_FEED = 'ADD_FEED';
exports.ADD_FEEDS = 'ADD_FEEDS';
var AddFeed = /** @class */ (function () {
    function AddFeed(payload) {
        this.payload = payload;
        this.type = exports.ADD_FEED;
    }
    return AddFeed;
}());
exports.AddFeed = AddFeed;
var AddFeeds = /** @class */ (function () {
    function AddFeeds(payload) {
        this.payload = payload;
        this.type = exports.ADD_FEEDS;
    }
    return AddFeeds;
}());
exports.AddFeeds = AddFeeds;
//# sourceMappingURL=feeds.actions.js.map