export class GridField {
  fieldName: string;
  fieldDescription: string;
  hidden: boolean;

  constructor(fieldName, fieldDescription, hidden) {
    this.fieldName = fieldName;
    this.fieldDescription = fieldDescription;
    this.hidden = hidden;
  }
}
