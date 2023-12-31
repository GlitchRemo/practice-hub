import { gql, useQuery } from "@apollo/client";
import "./App.css";

const GET_RANDOM_COMIC = gql`
	query {
		getRandomComic {
			num
			title
			img
			alt
		}
	}
`;

const App = () => {
	const { data, error, loading } = useQuery(GET_RANDOM_COMIC);

	if (loading) return <p>Loading...</p>;
	if (error) return <p>Error...</p>;

	const { getRandomComic } = data;

	return (
		<div className="App">
			<header className="App-header">
				<p>{getRandomComic.title}</p>
				<img src={getRandomComic.img} alt={getRandomComic.alt}></img>
			</header>
		</div>
	);
};

export default App;
