"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.ADD_FEED = 'ADD_FEED';
exports.ADD_OLD_FEEDS = 'ADD_OLD_FEEDS';
exports.ADD_NEW_FEEDS = 'ADD_NEW_FEEDS';
var AddFeed = /** @class */ (function () {
    function AddFeed(payload) {
        this.payload = payload;
        this.type = exports.ADD_FEED;
    }
    return AddFeed;
}());
exports.AddFeed = AddFeed;
var AddOldFeeds = /** @class */ (function () {
    function AddOldFeeds(payload) {
        this.payload = payload;
        this.type = exports.ADD_OLD_FEEDS;
    }
    return AddOldFeeds;
}());
exports.AddOldFeeds = AddOldFeeds;
var AddNewFeeds = /** @class */ (function () {
    function AddNewFeeds(payload) {
        this.payload = payload;
        this.type = exports.ADD_NEW_FEEDS;
    }
    return AddNewFeeds;
}());
exports.AddNewFeeds = AddNewFeeds;
//# sourceMappingURL=feeds.actions.js.map