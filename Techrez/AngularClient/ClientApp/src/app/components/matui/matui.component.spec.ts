import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MatuiComponent } from './matui.component';

describe('MatuiComponent', () => {
  let component: MatuiComponent;
  let fixture: ComponentFixture<MatuiComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MatuiComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MatuiComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
