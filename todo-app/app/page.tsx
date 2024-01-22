"use client";
import { useState } from "react";
import styles from "./page.module.css";

const createTodo = () => ({ title: "Buy milk", isDone: false });

type TodoProps = { title: string };

const Todo = ({ title }: TodoProps) => (
	<div>
		<p>{title}</p>
	</div>
);

const AddTodo = ({ onSubmit }: { onSubmit: (todo: string) => void }) => {
	const [todo, setTodo] = useState("");

	return (
		<form className={styles.form} onSubmit={() => onSubmit(todo)}>
			<input
				type="text"
				className={styles.inputBox}
				onChange={(e) => setTodo(e.target.value)}
			></input>
			<button type="submit" className={styles.addButton}>
				+
			</button>
		</form>
	);
};

export default function Home() {
	const [todos, setTodos] = useState<string[]>([]);

	const handleSubmit = (todo: string) => {
		setTodos([...todos, todo]);
	};

	console.log(todos, new Date().toLocaleString());

	return (
		<main className={styles.main}>
			<h1>TODO APP</h1>
			<AddTodo onSubmit={handleSubmit} />
			{todos.map((todo) => (
				<Todo title={todo} key={Date.now()} />
			))}
		</main>
	);
}
