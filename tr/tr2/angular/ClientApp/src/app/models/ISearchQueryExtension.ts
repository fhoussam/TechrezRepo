export class SearchQueryExtension {
  sortField: string;
  pageIndex: number;
  isDesc: boolean;

  constructor(defaultSortField: string) {
    this.isDesc = false;
    this.pageIndex = 0;
    this.sortField = defaultSortField;
  }

  isEmptyQuery() {
    let tmp = { ...this };
    delete tmp['isDesc'];
    delete tmp['pageIndex'];
    delete tmp['sortField'];

    var emptyObject = JSON.parse(JSON.stringify(tmp, (key, value) => {
      if (value !== null) return value;
    }));

    return Object.keys(emptyObject).length === 0
  }
}
