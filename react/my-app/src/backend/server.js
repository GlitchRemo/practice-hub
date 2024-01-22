const express = require("express");
const app = express();

app.use((req, res, next) => {
	console.log(req.url, req.method);
	next();
});

app.get("/api/fetchPost", (req, res) => {
	res.setHeader("Access-Control-Allow-Origin", "*");
	res.json({
		title: "Cool Post",
		text: "This is a cool post",
		likes: 5,
	});
});

app.listen(8002, () => console.log("App listening on 8002"));
	