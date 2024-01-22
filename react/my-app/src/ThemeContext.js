import React, { useContext } from "react";

const ThemeContext = React.createContext(null);

const Heading = ({ children }) => {
	const { background, foreground } = useContext(ThemeContext);

	return (
		<h1 style={{ background: background, color: foreground }}>{children}</h1>
	);
};

const Theme = () => {
	const themes = {
		light: { background: "white", foreground: "black" },
		dark: { foreground: "white", background: "black" },
	};

	return (
		<ThemeContext.Provider value={themes["dark"]}>
			<Heading>Hello</Heading>
		</ThemeContext.Provider>
	);
};

export default Theme;
