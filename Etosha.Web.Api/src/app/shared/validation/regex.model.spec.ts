import { Regex } from './regex.model';

describe('Regex Model', () => {
    it('should match an email', () => {
        const result = new RegExp(Regex.EMAIL_REGEX).test('test@test.com');
        expect(result).toBeTruthy();
    });

    it('should not match an email', () => {
        const result = new RegExp(Regex.EMAIL_REGEX).test('test@test.a');
        expect(result).toBeFalsy();
    });
});
