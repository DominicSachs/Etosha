import { BaseService } from './base.service';
import { HttpHeaders } from '@angular/common/http';

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
      () => { },
      (error) => {
        expect(error).toBe('foo');
      });
  });

  it('sould return model error', () => {
    const response = { headers: new Headers({'foo': 'bar'}), error: { 'email': 'Email is invalid.' } };

    testObject.handleError(response).subscribe(
      () => { },
      (error) => {
        expect(error).toBe('Email is invalid.\n');
      });
  });

  it('sould return server error', () => {
    const response = { headers: new Headers({'foo': 'bar'}), error: { type: 'BadRequest' } };

    testObject.handleError(response).subscribe(
      () => { },
      (error) => {
        expect(error).toBe('Server error');
      });
  });
});
