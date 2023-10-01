const express = require("express");
const {
	serveHome,
	greetIntern,
	serveLogin,
	loginUser,
} = require("./handlers/handlers");
const { logger } = require("./middlewares/logger");

const createApp = () => {
	const app = express();

	app.use(express.json());
	app.use(logger);

	app.get("/", serveHome);
	app.post("/intern", greetIntern);
	app.get("/login", serveLogin);
	app.post("/login", loginUser);

	return app;
};

module.exports = { createApp };
