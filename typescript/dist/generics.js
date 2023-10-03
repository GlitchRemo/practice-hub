"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
const identity = (element) => element;
const reverse = (elements) => [...elements].reverse();
const oneToThree = [1, 2, 3];
const threeToOne = reverse(oneToThree);
const sortVehicle = (vehicles) => vehicles.sort((v1, v2) => v1.price - v2.price);
const bike = { model: "R15", price: 123456, twoWheeler: true };
const car = { model: "Revoult", price: 1234, fourWheeler: true };
const vehicle = { model: "Ferrari", price: 12345 };
const foo = () => {
    return;
};
class Stack {
    #elements;
    constructor() {
        this.#elements = new Array();
    }
    push(element) {
        this.#elements.push(element);
    }
    pop() {
        const topElement = this.#elements.pop();
        if (topElement === undefined)
            throw new Error("Stack Underflow");
        return topElement;
    }
    values() {
        return [...this.#elements];
    }
}
const main = () => {
    console.log(identity(2));
    console.log(identity("riya"));
    console.log(threeToOne);
    console.log(sortVehicle([car, vehicle, bike]));
    const stack = new Stack();
    stack.push("abc");
    stack.push(1);
    console.log(stack.values());
};
main();
