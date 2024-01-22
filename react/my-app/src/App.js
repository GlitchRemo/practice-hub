import React, { useEffect, useState } from "react";

const Post = ({ title, likes, children }) => (
	<div>
		<h3>{title}</h3>
		<p>{children}</p>
		<p>Likes: {likes}</p>
		<button onClick={() => console.log("clicked")}>Like</button>
	</div>
);

const PostApi = {};
PostApi.fetchPost = () => fetch("/api/fetchPost").then((res) => res.json());

const App = () => {
	const [post, setPost] = useState(null);

	useEffect(() => {
		PostApi.fetchPost().then(setPost);
	}, []);

	if (post === null) return <p>Loading...</p>;

	return (
		<Post title={post.title} likes={post.likes}>
			{post.text}
		</Post>
	);
};

export default App;
