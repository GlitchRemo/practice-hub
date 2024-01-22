import React from "react";

const PokemonDetails = ({ name, imageUrl }) =>
	React.createElement(
		"div",
		{ style: { border: "1px solid black" } },
		React.createElement("p", null, name),
		React.createElement("img", {
			src: imageUrl,
		})
	);

export default PokemonDetails;
