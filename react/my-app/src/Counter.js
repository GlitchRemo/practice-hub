import React, { useEffect, useState } from "react";

const Counter = (props) => {
	const [count, setState] = useState(0);
	return (
		<button onClick={() => setState(count + 1)}>Clicked {count} times</button>
	);
};

const Timer = (props) => {
	const [secondsElapsed, setSecondsElapsed] = useState(1);
	const [interval, setValueOfInterval] = useState(1000);

	useEffect(() => {
		const intervalId = setInterval(
			() => setSecondsElapsed((s) => s + 1),
			interval
		);

		return () => {
			console.log("Timer is unmounting");
			clearInterval(intervalId);
		};
	}, [interval]);

	return (
		<div>
			<p>{secondsElapsed} seconds elapsed</p>
			<button
				onClick={() => {
					setValueOfInterval(interval + 500);
				}}
			>
				Increase
			</button>
		</div>
	);
};

export default Timer;
