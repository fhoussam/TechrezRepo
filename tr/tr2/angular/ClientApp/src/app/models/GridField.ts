export class GridField {
  fieldName: string;
  fieldDescription: string;
  hidden: boolean;
  sortfieldIndex: number;

  constructor(fieldName, fieldDescription, sortfieldIndex, hidden) {
    this.fieldName = fieldName;
    this.fieldDescription = fieldDescription;
    this.sortfieldIndex = sortfieldIndex;
    this.hidden = hidden;
  }
}
