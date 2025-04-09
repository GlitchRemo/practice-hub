package main

import (
	"encoding/json"
	"fmt"

	"github.com/google/uuid"
)

type Status string

// Status constants
const (
	StatusDone   Status = "done"
	StatusUndone Status = "undone"
)

type Todo struct {
	ID     uuid.UUID `json:"id"`
	Title  string    `json:"title"`
	Status Status    `json:"status"`
}

type TodoList struct {
	Todos []Todo
}

// Add a new todo to the list
func (tl *TodoList) Add(todo Todo) {
	tl.Todos = append(tl.Todos, todo)
}

// Print all todos in the list
func (tl *TodoList) PrintTodos() {
	for i, todo := range tl.Todos {
		fmt.Printf("Todo %d\n", i+1)
		fmt.Printf("  ID: %s\n", todo.ID.String())
		fmt.Printf("  Title: %s\n", todo.Title)
		fmt.Printf("  Status: %s\n\n", todo.Status)
	}
}

// Marshal todos to JSON and print
func (tl *TodoList) PrintTodosAsJSON() {
	data, err := json.MarshalIndent(tl.Todos, "", "  ")
	if err != nil {
		fmt.Println("Error marshalling todos to JSON:", err)
		return
	}
	fmt.Println("Todos in JSON format:")
	fmt.Println(string(data))
}

func main() {
	todoList := TodoList{}

	todo1 := Todo{
		ID:     uuid.New(),
		Title:  "Learn Go",
		Status: StatusUndone,
	}

	todo2 := Todo{
		ID:     uuid.New(),
		Title:  "Build a web app",
		Status: StatusUndone,
	}

	todoList.Add(todo1)
	todoList.Add(todo2)

	todoList.PrintTodos()
	todoList.PrintTodosAsJSON()
}
