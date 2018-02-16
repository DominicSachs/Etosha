import { BaseService } from './base.service';

class TestService extends BaseService { }

describe('BaseService', () => {
  let testObject: TestService;
  
  beforeEach(() => {
    testObject = new TestService();
  });
});