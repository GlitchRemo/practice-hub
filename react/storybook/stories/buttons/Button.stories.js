import { Button } from "./Button";

export default {
	component: Button,
	title: "Example/Button",
	parameters: {
		layout: "centered",
	},
};

// More on writing stories with args: https://storybook.js.org/docs/react/writing-stories/args
export const Primary = {
	args: {
		primary: true,
		label: "Button",
	},
};

export const Secondary = {
	args: {
		label: "Button",
	},
};

export const Large = {
	args: {
		size: "large",
		label: "Button",
	},
};

export const Small = {
	args: {
		size: "small",
		label: "Button",
	},
};
