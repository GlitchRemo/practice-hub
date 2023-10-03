range(5);
range(1, 5);
range(1, 5, 2);
range(5, 1, -1, (x, y) => x > y);

const range = function* (from, to, step = 1) {
	let stepper = step;
	if (Number.isFinite(step) || typeof step === "number") {
		stepper = (x) => x + step;
	}

	while (from <= to) {
		yield from;
		from = stepper(from);
	}
};
