const express = require("express");
const morgan = require("morgan");
const compression = require("compression");

const createApp = () => {
	const app = express();

	app.use(morgan(":method :url :response-time ms"));
	app.use(express.static("public"));
	app.use(compression());

	return app;
};

module.exports = createApp;
