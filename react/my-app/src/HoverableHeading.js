import React, { useEffect, useRef, useState } from "react";

const useHover = () => {
	const [hovering, setHovering] = useState(false);
	const customRef = useRef(null);

	const handleMouseOver = () => setHovering(true);
	const handleMouseOut = () => setHovering(false);

	useEffect(() => {
		const node = customRef.current;
		if (node) {
			node.addEventListener("mouseover", handleMouseOver);
			node.addEventListener("mouseout", handleMouseOut);

			return () => {
				node.removeEventListener("mouseover", handleMouseOver);
				node.removeEventListener("mouseout", handleMouseOut);
			};
		}
	}, [customRef.current]);

	return [customRef, hovering];
};

const HoverableHeading = () => {
	const [hoverRef, hovering] = useHover();

	return (
		<h1 style={{ color: hovering ? "red" : "black" }} ref={hoverRef}>
			Hello
		</h1>
	);
};

export default HoverableHeading;
