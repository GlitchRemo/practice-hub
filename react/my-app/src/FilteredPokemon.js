import React from "react";
import PokemonDetails from "./pokemon";

class FilteredPokemon extends React.Component {
	constructor(props) {
		super(props);
		this.state = { inputText: "" };
		this.handleOnChange = this.handleOnChange.bind(this);
	}

	handleOnChange(event) {
		this.setState(() => ({ inputText: event.target.value }));
	}

	filterPokemons() {
		return this.props.data
			.filter(({ name }) => name.includes(this.state.inputText))
			.map(({ name, imageUrl }) => (
				<PokemonDetails name={name} imageUrl={imageUrl} key={name} />
			));
	}

	render() {
		return (
			<div>
				<input value={this.state.inputText} onChange={this.handleOnChange} />
				<div
					style={{
						display: "flex",
						gap: "10px",
						flexWrap: "wrap",
					}}
				>
					{this.filterPokemons()}
				</div>
			</div>
		);
	}
}

export default FilteredPokemon;
