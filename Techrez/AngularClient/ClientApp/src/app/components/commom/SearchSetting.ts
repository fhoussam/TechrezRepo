export class SearchSetting 
{
    pageSize:number;
    pageIndex:number;
    orderColumn:string;
    isDesc:boolean;
    constructor() {
        this.pageIndex = 0;
        this.pageSize = 5;
        this.isDesc = false;
    }
}