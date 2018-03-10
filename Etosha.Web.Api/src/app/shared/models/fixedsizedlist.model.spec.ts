import { FixedSizedList } from './fixedsizedlist.model';

describe('FixedSizedList Model', () => {
    it('should add three items', () => {
        const list = new FixedSizedList<number>(3);
        list.add(1);
        list.add(2);
        list.add(3);

        expect(list.values.length).toBe(3);
    });

    it('should remove the first item', () => {
        const list = new FixedSizedList<number>(3);
        list.add(1);
        list.add(2);
        list.add(3);
        list.add(4);

        expect(list.values).toEqual([2, 3, 4]);
    });
});
