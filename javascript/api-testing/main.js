const { createApp } = require("./src/app");

const main = () => {
	const PORT = 8000;
	const app = createApp();
	app.listen(PORT, () => console.log("Server listening to port", PORT));
};

main();
