package print_utils

import (
	"encoding/json"
	"fmt"
	"todo-go/types"
)

type TodoList struct {
	Todos []types.Todo
}

// Add a new todo to the list
func (tl *TodoList) Add(todo types.Todo) {
	tl.Todos = append(tl.Todos, todo)
}

// Print all todos in the list
func (tl *TodoList) PrintTodos() {
	for i, todo := range tl.Todos {
		fmt.Printf("Todo %d\n", i+1)
		fmt.Printf("  ID: %s\n", todo.ID)
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
