package handlers

import (
	"context"
	"encoding/json"
	"net/http"

	"todo-go/database"
	"todo-go/types"

	"github.com/aws/aws-sdk-go-v2/feature/dynamodb/attributevalue"
	"github.com/aws/aws-sdk-go-v2/service/dynamodb"
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

	// Convert todo to a DynamoDB item
	av, err := attributevalue.MarshalMap(todo)
	if err != nil {
		http.Error(w, "Could not marshal todo", http.StatusInternalServerError)
		return
	}

	// Save to DynamoDB
	_, err = database.DynamoClient.PutItem(context.TODO(), &dynamodb.PutItemInput{
		TableName: &database.TodoTable,
		Item:      av,
	})
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
	out, err := database.DynamoClient.Scan(context.TODO(), &dynamodb.ScanInput{
		TableName: &database.TodoTable,
	})
	if err != nil {
		http.Error(w, "Error reading from DynamoDB", http.StatusInternalServerError)
		return
	}

	var todos []types.Todo
	err = attributevalue.UnmarshalListOfMaps(out.Items, &todos)
	if err != nil {
		http.Error(w, "Error unmarshaling response", http.StatusInternalServerError)
		return
	}

	w.Header().Set("Content-Type", "application/json")
	json.NewEncoder(w).Encode(todos)
}
