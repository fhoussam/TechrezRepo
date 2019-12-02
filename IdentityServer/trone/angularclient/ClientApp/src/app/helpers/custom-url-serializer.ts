import { UrlSerializer, UrlTree, DefaultUrlSerializer } from '@angular/router';
export class CustomUrlSerializer implements UrlSerializer {
    parse(url: any): UrlTree {
        let dus = new DefaultUrlSerializer();
        return dus.parse(url);
    }
    serialize(tree: UrlTree): any {
        let dus = new DefaultUrlSerializer(),
            path = dus.serialize(tree);

        //console.log(tree.root.children.primary.segments);
        path = this.skipNavigation(path);
        return path;
    }

    skipNavigation(path: string) {

        let pathsToSkip: string[] = [ "details", "orders" ];
        for (var i = 0; i < pathsToSkip.length; i++) {
            let pathToSkip: string = pathsToSkip[i];
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
    }
}
