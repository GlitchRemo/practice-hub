function setup() {
	createCanvas(windowWidth, windowHeight);
}

function windowResized() {
	resizeCanvas(windowWidth, windowHeight);
}

function draw() {
	background(0);
	translate(width / 2, height - 100);
	branch(150, 10);
}

function drawLine(length) {
	stroke(255);
	line(0, 0, 0, -length);
	translate(0, -length);
}

function branch(length, depth) {
	if (depth <= 1) return;

	drawLine(length);

	push();
	rotate(PI / 4);
	branch(length * 0.75, depth - 1);
	pop();

	push();
	rotate(-PI / 8);
	branch(length * 0.75, depth - 1);
	pop();
}
