enum Gender {
	MALE,
	FEMALE,
}

type Person = {
	name: string;
	age: number;
	gender?: Gender;
};

const milan = {
	name: "Milan",
	age: 20,
	gender: Gender.MALE,
};

const display = (person: Person): void => {
	console.log("Name:", person.name);
	console.log("Age:", person.age);
	console.log("Gender:", person.gender);
};

display(milan);

enum Level {
	ERROR,
	WARN = 5,
	INFO,
	DEBUG,
}

console.log("Level: ", Level);
