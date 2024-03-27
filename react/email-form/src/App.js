import { useState } from "react";
import { useForm } from "react-hook-form";
import "./App.css";

function App() {
  const { register, handleSubmit } = useForm();
  const [value, setValue] = useState("");

  const onSubmit = (data) => console.log(data);

  return (
    <div className="App">
      <form onSubmit={handleSubmit(onSubmit)}>
        <input
          {...register("Email", { setValueAs: (value) => `${value}hello` })}
          placeholder="Enter email"
          value={value}
          onChange={(e) => {
            console.log("hello");
            setValue(e.target.value + "hii");
          }}
        ></input>
        <button>Submit</button>
      </form>
    </div>
  );
}

export default App;
