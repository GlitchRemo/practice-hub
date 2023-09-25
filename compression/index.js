const createApp = require("./src/app");

const main = () => {
	const app = createApp();
	app.listen(8999, () => console.log("listening on port 8999"));
};

main();
