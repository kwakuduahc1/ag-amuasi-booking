import { TestBed } from '@angular/core/testing';

import { ServicesHttp } from './services-http';

describe('ServicesHttp', () => {
  let service: ServicesHttp;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ServicesHttp);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
