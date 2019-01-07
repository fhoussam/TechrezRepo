import { Component, OnInit, Inject, Output, EventEmitter } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { DialogData, product } from 'src/app/models/product';

@Component({
  selector: 'app-productcreate',
  templateUrl: './productcreate.component.html',
  styleUrls: ['./productcreate.component.css']
})
export class ProductcreateComponent implements OnInit {
  constructor(
    public dialogRef: MatDialogRef<ProductcreateComponent>,
    @Inject(MAT_DIALOG_DATA) public productToAdd: product) {
    //this.productToAdd.categoryID = 1;
    //this.productToAdd.description = "Asus 1080 GTX Strix OC";
    //this.productToAdd.price = 100;
    //this.productToAdd.stock = 100;
  }

  closeModal(): void {
    this.dialogRef.close();
  }

  updateParent()
  {
    let elem = document.getElementById('fakeButton');
    elem.click();
    this.dialogRef.close();
  }

  ngOnInit() {
  }
}
