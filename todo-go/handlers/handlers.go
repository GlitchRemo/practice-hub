package handlers

import (
	"encoding/json"
	"net/http"

	database "todo-go/database"
	"todo-go/types"

	"github.com/google/uuid"
)

func HandleAddTodo(w http.ResponseWriter, r *http.Request) {
	var todo types.Todo
	guid := uuid.New()
	err := json.NewDecoder(r.Body).Decode(&todo)
	if err != nil {
		http.Error(w, "Invalid Json", http.StatusBadRequest)
		return
	}

	todo.ID = guid.String()
	if todo.Status == "" {
		todo.Status = types.StatusUndone
	}

	err = database.AddTodoToDB(todo)
	if err != nil {
		http.Error(w, "Could not write to DynamoDB", http.StatusInternalServerError)
		return
	}

	w.Header().Set("Content-Type", "application/json")
	w.WriteHeader(http.StatusCreated)
	response := map[string]any{
		"status": "201 Created",
		"todo":   todo,
	}
	json.NewEncoder(w).Encode(response)
}

func HandleGetTodos(w http.ResponseWriter, _ *http.Request) {
	todos, err := database.GetTodosFromDB()
	if err != nil {
		http.Error(w, "Error reading from DynamoDB", http.StatusInternalServerError)
		return
	}

	w.Header().Set("Content-Type", "application/json")
	json.NewEncoder(w).Encode(todos)
}
