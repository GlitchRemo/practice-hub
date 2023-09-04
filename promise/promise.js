const fs = require("fs");

const readFile = (filePath, encoding = "utf-8") => {
	return new Promise((resolve, reject) => {
		fs.readFile(filePath, encoding, (err, content) => {
			if (err) {
				reject(err);
				return;
			}

			resolve(content);
		});
	});
};

// readFile("index.html").then(console.log);
// readFile("bad.html").catch(console.log);

const wait = (delay, message) => {
	return new Promise((res, rej) => {
		setTimeout(res, delay, message);
	});
};

// wait(3000, "This message is shown up after 3 sec").then(console.log);

const a = (number) => new Promise((res) => res(number));

a(10)
	.then(console.log)
	.then(() => {
		return new Promise((res) => {
			setTimeout(() => {
				console.log(999);
				res();
			});
		});
	})
	.then(() => console.log(6));

const writeFile = () => {
	return new Promise((res, rej) => {
		fs.writeFile("./a.txt", "hello", () => {
			console.log("in write file");
			res();
		});
	});
};

writeFile().then(() => console.log("hello"));
