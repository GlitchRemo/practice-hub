const express = require("express");
const { buildSchema } = require("graphql");
const { graphqlHTTP } = require("express-graphql");
const cors = require("cors");
const { ComicDataHandler } = require("./comic-data-handler");

const app = express();

const comicDataHandler = new ComicDataHandler();

const schema = buildSchema(`
  type Comic {
    num: Int
    title: String
    img: String 
    alt: String
  }

  type Query {
    getComic(num: Int): Comic
    getRandomComic: Comic
  }
`);

const root = {
	getComic: ({ num}) => comicDataHandler.getComic(num),
	getRandomComic: () => comicDataHandler.getRandomComic(),
};

app.use((req, res, next) => {
	console.log(req.url, req.method);
	next();
});

app.use(cors());

app.use(
		"/graphql",
	graphqlHTTP({ schema: schema, rootValue: root, graphiql: true })
);

app.get("/api/fetchPost", (req, res) => {
	res.setHeader("Access-Control-Allow-Origin", "*");
	res.json({
		title: "Cool Post",
		text: "This is a cool post",
		likes: 5,
	});
});

app.listen(8002, () => console.log("App listening on 8002"));
