import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ProductcreateComponent } from './productcreate.component';

describe('ProductcreateComponent', () => {
  let component: ProductcreateComponent;
  let fixture: ComponentFixture<ProductcreateComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ProductcreateComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ProductcreateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
