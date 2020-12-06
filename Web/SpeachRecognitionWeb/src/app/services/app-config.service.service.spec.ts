import { TestBed } from '@angular/core/testing';

import { AppConfig.ServiceService } from './app-config.service.service';

describe('AppConfig.ServiceService', () => {
  let service: AppConfig.ServiceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AppConfig.ServiceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
