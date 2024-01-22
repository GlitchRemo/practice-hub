import { useState } from "react";
import { FaStar } from "react-icons/fa";

const Star = ({ selected, onClick }) => (
	<FaStar
		style={{ color: selected ? "red" : "black" }}
		onClick={onClick}
	></FaStar>
);

const rating = ({ numberOfStars = 5 }) => {
	const [selected, setSelected] = useState(-1);

	return [...Array(numberOfStars)].map((x, i) => (
		<Star
			onClick={() => {
				setSelected(i + 1);
			}}
			selected={i < selected}
			key={i}
		/>
	));
};

export default rating;
