package main

import (
	"context"
	"fmt"
	"net/http"
	"os"

	"todo-go/database"
	"todo-go/handlers"

	"github.com/aws/aws-sdk-go-v2/config"
	"github.com/aws/aws-sdk-go-v2/service/dynamodb"
)

func main() {
	http.HandleFunc("/todos", func(w http.ResponseWriter, r *http.Request) {
		switch r.Method {
		case http.MethodGet:
			handlers.HandleGetTodos(w, r)
		case http.MethodPost:
			handlers.HandleAddTodo(w, r)
		default:
			http.Error(w, "Method not allowed", http.StatusMethodNotAllowed)
		}
	})

	// Load the AWS configuration
	// This will load the default config from ~/.aws/config
	cfg, err := config.LoadDefaultConfig(context.TODO())
	if err != nil {
		fmt.Println("Unable to load AWS config:", err)
		os.Exit(1)
	}

	database.DynamoClient = dynamodb.NewFromConfig(cfg)

	fmt.Println("Starting server on :8080...")
	http.ListenAndServe(":8080", nil)
}
