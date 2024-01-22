import React from "react";
import "./Task.css";

const Task = ({ id, title }) => <div className="task">{title}</div>;

const meta = {
	title: "My Stories/Task",
	component: Task,
	parameters: {
		layout: "centered",
		backgrounds: {
			values: [
				{ name: "red", value: "#f00" },
				{ name: "green", value: "#0f0" },
				{ name: "blue", value: "#00f" },
			],
		},
	},
	decorators: [
		(Story) => (
			<div
				style={{
					margin: "3em",
					backgroundColor: "red",
					padding: "4em",
					display: "inline-block",
				}}
			>
				<Story />
			</div>
		),
	],
};

export default meta;

export const BuyEgg = {
	args: { id: 1, title: "Buy egg" },
};
