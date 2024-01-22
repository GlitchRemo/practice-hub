class CounterButton extends React.Component {
	constructor() {
		super();
		this.state = { count: 0 };
	}

	render() {
		const button = React.createElement(
			"button",
			{
				onClick: () => this.setState((state) => ({ count: state.count + 1 })),
			},
			`You have clicked ${this.state.count} times`,
			React.createElement(
				"p",
				{ style: { border: "1px solid black" } },
				"Inner Element",
				React.createElement(
					"p",
					{ style: { border: "1px solid black" } },
					"Innermost Element"
				)
			)
		);

		return button;
	}
}

class Timer extends React.Component {
	constructor(props) {
		super(props);
		this.state = { secondsElapsed: 1 };
	}

	componentDidMount() {
		this.intervalId = setInterval(
			() =>
				this.setState(({ secondsElapsed }) => ({
					secondsElapsed: secondsElapsed + 1,
				})),
			1000
		);
	}

	componentWillUnmount() {
		clearInterval(this.intervalId);
	}

	render() {
		const timerElement = React.createElement(
			"p",
			null,
			`${this.state.secondsElapsed} seconds elapsed`
		);

		return timerElement;
	}
}

class HaltableTimer extends React.Component {
	constructor(props) {
		super(props);
		this.state = { isHalted: false };
	}

	render() {
		const timer = React.createElement(Timer);

		const stopButton = React.createElement(
			"button",
			{
				onClick: () => {
					this.setState(() => ({ isHalted: true }));
				},
			},
			"Stop"
		);

		return this.state.isHalted
			? React.createElement("div", null, `Finished`)
			: React.createElement("div", null, timer, stopButton);
	}
}

const Fruit = (props) =>
	React.createElement("li", { key: props.key }, props.name);

const fruitItems = ["mango", "apple"];
const fruits = fruitItems.map((name, index) =>
	React.createElement(Fruit, { name, key: index })
);

class Toggle extends React.Component {
	constructor(props) {
		super(props);
		this.state = { isToggled: false };
	}

	render() {
		const text = this.state.isToggled ? "On" : "Off";

		return React.createElement(
			"button",
			{
				onClick: () =>
					this.setState(({ isToggled }) => ({ isToggled: !isToggled })),
			},
			text
		);
	}
}

const mainContainer = document.getElementById("main-container");
// const counter = React.createElement(CounterButton);
// const timer = React.createElement(Timer);
// const haltableTimer = React.createElement(HaltableTimer);
// const fruitList = React.createElement("ul", null, fruits);
const toggle = React.createElement(Toggle);
ReactDOM.render(toggle, mainContainer);
