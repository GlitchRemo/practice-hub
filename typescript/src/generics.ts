const identity = <T>(element: T): T => element;

const reverse = <T>(elements: Array<T>): Array<T> => [...elements].reverse();

const oneToThree = [1, 2, 3];
const threeToOne = reverse(oneToThree);

type Vehicle = {
	model: string;
	price: number;
};

type Car = Vehicle & {
	fourWheeler: boolean;
};

type Bike = Vehicle & {
	twoWheeler: boolean;
};

const sortVehicle = <T extends Vehicle>(vehicles: Array<T>) =>
	vehicles.sort((v1, v2) => v1.price - v2.price);

const bike: Bike = { model: "R15", price: 123456, twoWheeler: true };
const car: Car = { model: "Revoult", price: 1234, fourWheeler: true };
const vehicle: Vehicle = { model: "Ferrari", price: 12345 };

const foo = <K>(): K | undefined => {
	return;
};

class Stack<T> {
	#elements: Array<T>;

	constructor() {
		this.#elements = new Array();
	}

	push(element: T): void {
		this.#elements.push(element);
	}

	pop(): T {
		const topElement = this.#elements.pop();
		if (topElement === undefined) throw new Error("Stack Underflow");

		return topElement;
	}

	values(): T[] {
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
