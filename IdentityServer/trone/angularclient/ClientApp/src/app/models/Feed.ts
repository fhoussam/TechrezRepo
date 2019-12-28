export class Feed {
    userName: string;
    dateTimeStamp: Date;
    operationType: string;

    //constructor() {
    //    this.userName = '';
    //    this.dateTimeStamp = null;
    //    this.operationType = null;
    //}

    constructor(userName: string, dateTimeStamp: Date, operationType:string) {
        this.userName = userName;
        this.dateTimeStamp = dateTimeStamp;
        this.operationType = operationType;
    }
}
