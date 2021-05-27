import { WindowRef } from './window.ref';

describe('Window reference', () => {
  let reference: WindowRef;

  beforeEach(() => {
    reference = new WindowRef();
  });

  it('provides native window instance', () => {
    expect(reference.nativeWindow).toBeDefined();
  });
});
