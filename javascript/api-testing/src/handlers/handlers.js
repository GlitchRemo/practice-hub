const serveHome = (req, res) => {
	res.send("<h2>Hello world</h2>");
};

const greetIntern = (req, res) => {
	res.json({ naam: req.body.name });
};

const serveLogin = (req, res) => {
	res.send("<h1>Login Page</h1>");
};

const loginUser = (req, res) => {
	const { name, password } = req.body;

	res.cookie("name", name);
	res.cookie("password", password);

	res.end();
};

module.exports = { serveHome, greetIntern, serveLogin, loginUser };
