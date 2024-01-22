import React from "react";

class MirroredText extends React.Component {
	constructor(props) {
		super(props);
		this.state = { inputText: "sample" };
		this.handleOnChange = this.handleOnChange.bind(this);
	}

	handleOnChange(event) {
		return this.setState(() => ({ inputText: event.target.value }));
	}

	mirror() {
		return this.state.inputText.split("").reverse().join("");
	}

	render() {
		return (
			<div>
				<input value={this.state.inputText} onChange={this.handleOnChange} />
				<p>{this.mirror()}</p>
			</div>
		);
	}
}

export default MirroredText;
