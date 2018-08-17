export class FixedSizedList<T> {
    items: T[];

    constructor(private length: number) {
        this.items = [];
    }

    get values(): ReadonlyArray<T> {
        const readonlyArray: ReadonlyArray<T> = this.items;
        return readonlyArray;
    }

    add(item: T) {
        this.items.push(item);

        while (this.items.length > this.length) {
            this.items.shift();
        }
    }
}
