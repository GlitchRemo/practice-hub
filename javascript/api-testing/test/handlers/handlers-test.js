const assert = require("assert");
const { describe, it } = require("node:test");
const { serveHome } = require("../../src/handlers/handlers");

describe("handlers", () => {
	describe("serveHome", () => {
		it("should send home page html", (context) => {
			const res = {
				send: context.mock.fn(),
			};

			serveHome({}, res);

			assert.deepStrictEqual(
				res.send.mock.calls[0].arguments[0],
				"<h2>Hello world</h2>"
			);
		});
	});
});
