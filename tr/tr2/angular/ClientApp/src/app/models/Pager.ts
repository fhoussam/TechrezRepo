export class Pager {
  sortFieldIndex: number;
  pageIndex: number;
  isDesc: boolean;

  constructor() {
    this.isDesc = false;
    this.pageIndex = 0;
    this.sortFieldIndex = 0;
  }

  isEmptyQuery() {
    const tmp = { ...this };
    delete tmp['isDesc'];
    delete tmp['pageIndex'];
    delete tmp['sortFieldIndex'];

    const emptyObject = JSON.parse(JSON.stringify(tmp, (key, value) => {
      if (value !== null) return value;
    }));

    return Object.keys(emptyObject).length === 0
  }
}
