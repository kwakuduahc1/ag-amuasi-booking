import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ServicesCostsComponent } from './services-costs-component';

describe('ServicesCostsComponent', () => {
  let component: ServicesCostsComponent;
  let fixture: ComponentFixture<ServicesCostsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ServicesCostsComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ServicesCostsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
