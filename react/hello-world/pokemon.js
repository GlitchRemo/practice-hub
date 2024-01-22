const PokemonDetails = ({ name, imageUrl }) =>
	React.createElement(
		"div",
		null,
		React.createElement("p", null, name),
		React.createElement("img", {
			src: imageUrl,
		})
	);

class Pokemon extends React.Component {
	constructor(props) {
		super(props);
		this.state = { loading: true, pokemonDetails: null };
	}

	componentDidMount() {
		fetch(`https://pokeapi.co/api/v2/pokemon/${this.props.pokemonId}`)
			.then((res) => res.json())
			.then(({ sprites, name }) => {
				this.setState(() => ({
					loading: false,
					pokemonDetails: { imageUrl: sprites.front_default, name },
				}));
			});
	}

	render() {
		return this.state.loading
			? React.createElement("p", null, "Loading...")
			: React.createElement(PokemonDetails, this.state.pokemonDetails);
	}
}

class Pokemons extends React.Component {
	constructor(props) {
		super(props);
		this.state = { numberOfPokemons: 10 };
		this.loadMore = this.loadMore.bind(this);
	}

	loadMore() {
		this.setState((state) => ({
			numberOfPokemons: state.numberOfPokemons + 10,
		}));
	}

	render() {
		const pokemons = new Array(this.state.numberOfPokemons)
			.fill(1)
			.map((x, index) =>
				React.createElement(Pokemon, { pokemonId: index + 1, key: index + 1 })
			);

		const gallery = React.createElement("div", { class: "pokemons" }, pokemons);

		const loadMore = React.createElement(
			"button",
			{
				onClick: this.loadMore,
			},
			"Load More"
		);

		const reset = React.createElement(
			"button",
			{
				onClick: () => this.setState(() => (this.state.numberOfPokemons = 10)),
			},
			"Reset"
		);

		return React.createElement("div", null, gallery, loadMore, reset);
	}
}

ReactDOM.render(React.createElement(Pokemons), main_container);
