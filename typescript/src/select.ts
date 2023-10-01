enum Gender {
	MALE,
	FEMALE,
}

type Person = {
	name: string;
	age: number;
	gender?: Gender;
};

const eligibleVoters = (
	people: Person[],
	selector: (person: Person) => boolean
) => people.filter(selector);

const bittu = {
	name: "Bittu",
	age: 17,
};

const sauma = {
	name: "Sauma",
	age: 23,
};

const milan = {
	name: "Milan",
	age: 20,
	gender: Gender.MALE,
};

const selector = (p: Person) => p.age >= 18;

console.log(eligibleVoters([milan, bittu, sauma], selector));
console.log(eligibleVoters([], selector));
