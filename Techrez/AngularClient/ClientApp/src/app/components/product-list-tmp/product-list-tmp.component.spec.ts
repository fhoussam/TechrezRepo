import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ProductListTmpComponent } from './product-list-tmp.component';

describe('ProductListTmpComponent', () => {
  let component: ProductListTmpComponent;
  let fixture: ComponentFixture<ProductListTmpComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ProductListTmpComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ProductListTmpComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
