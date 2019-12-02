"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var router_1 = require("@angular/router");
var CustomUrlSerializer = /** @class */ (function () {
    function CustomUrlSerializer() {
    }
    CustomUrlSerializer.prototype.parse = function (url) {
        var dus = new router_1.DefaultUrlSerializer();
        return dus.parse(url);
    };
    CustomUrlSerializer.prototype.serialize = function (tree) {
        var dus = new router_1.DefaultUrlSerializer(), path = dus.serialize(tree);
        //console.log(tree.root.children.primary.segments);
        path = this.skipNavigation(path);
        return path;
    };
    CustomUrlSerializer.prototype.skipNavigation = function (path) {
        var pathsToSkip = ["details", "orders"];
        for (var i = 0; i < pathsToSkip.length; i++) {
            var pathToSkip = pathsToSkip[i];
            if (path.indexOf("/" + pathToSkip) != -1) {
                path = path.replace("/" + pathToSkip, "");
                if (path.indexOf("&st=" + pathToSkip) == -1) {
                    //clean any st param from URL
                    for (var j = 0; j < pathsToSkip.length; j++) {
                        path = path.replace("&st=" + pathsToSkip[j], "");
                    }
                    path = path += "&st=" + pathToSkip;
                }
            }
        }
        return path;
    };
    return CustomUrlSerializer;
}());
exports.CustomUrlSerializer = CustomUrlSerializer;
//# sourceMappingURL=custom-url-serializer.js.map