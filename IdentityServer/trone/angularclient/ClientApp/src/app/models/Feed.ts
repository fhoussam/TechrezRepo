export class Feed {
    code: string;
    userName: string;
    dateTimeStamp: Date;
    operationType: string;

    constructor(userName: string, dateTimeStamp: Date, operationType:string) {
        this.userName = userName;
        this.dateTimeStamp = dateTimeStamp;
        this.operationType = operationType;
    }
}
