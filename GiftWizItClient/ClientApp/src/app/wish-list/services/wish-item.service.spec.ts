import { TestBed } from '@angular/core/testing';

import { WishItemService } from './wish-item.service';

describe('WishItemService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: WishItemService = TestBed.get(WishItemService);
    expect(service).toBeTruthy();
  });
});
