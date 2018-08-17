import { HttpHeaders } from '@angular/common/http';
import { BaseService } from './base.service';

class TestService extends BaseService {
  handleError(e: any) {
    return super.handleError(e);
  }
}

describe('BaseService', () => {
  let testObject: TestService;

  beforeEach(() => {
    testObject = new TestService();
  });

  it('sould return error from header', () => {
    const response = { headers: new Headers({'Application-Error': 'foo'}) };

    testObject.handleError(response).subscribe(
      () => { }, error => {
        expect(error).toBe('foo');
      });
  });

  it('sould return model error', () => {
    const response = { headers: new Headers({foo: 'bar'}), error: { email: 'Email is invalid.' } };

    testObject.handleError(response).subscribe(
      () => { }, error => {
        expect(error).toBe('Email is invalid.\n');
      });
  });
});
