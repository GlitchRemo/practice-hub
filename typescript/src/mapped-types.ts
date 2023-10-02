type move = "0" | "2";
type Moves = { [m in move]: string };
const o: Moves = { "0": "a", "2": "b" };
console.log(o);
