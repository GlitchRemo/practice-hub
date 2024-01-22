let img;
const width = 1000;
const height = 1000;

function setup() {
	createCanvas(width, height);
}

function preload() {
	img = loadImage("assets/M.jpg");
}

function draw() {
	background(220);
	image(
		img,
		50,
		50,
		width - 100,
		height - 100,
		0,
		0,
		img.width,
		img.height,
		COVER
	);

	loadPixels();

	// for (let y = 50; y < img.height; y++) {
	// 	for (let x = 50; x < img.width; x++) {
	// 		const index = (x + y * width) * 4;

	// 		pixels[index] = 255;
	// 		pixels[index + 1] = 255;
	// 		pixels[index + 2] = 255;
	// 		pixels[index + 3] = 255;
	// 	}
	// }

	// // const p = get(100, 100);
	// // console.log(p);

	updatePixels();
}
