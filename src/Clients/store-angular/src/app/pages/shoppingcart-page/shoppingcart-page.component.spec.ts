import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ShoppingCartPageComponent } from './shoppingcart-page.component';

describe('ShoppingcartPageComponent', () => {
  let component: ShoppingCartPageComponent;
  let fixture: ComponentFixture<ShoppingCartPageComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ShoppingCartPageComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ShoppingCartPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
