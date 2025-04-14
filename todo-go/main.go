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

func setupRoutes(dbClient database.DatabaseClient) {
	http.HandleFunc("/todos", func(w http.ResponseWriter, r *http.Request) {
		switch r.Method {
		case http.MethodGet:
			handlers.HandleGetTodos(w, r, dbClient)
		case http.MethodPost:
			handlers.HandleAddTodo(w, r, dbClient)
		default:
			http.Error(w, "Method not allowed", http.StatusMethodNotAllowed)
		}
	})
}

func initDynamoClient() *dynamodb.Client {
	cfg, err := config.LoadDefaultConfig(context.TODO(), config.WithRegion(os.Getenv("AWS_REGION")))
	if err != nil {
		fmt.Println("Error loading AWS config:", err)
		os.Exit(1)
	}
	return dynamodb.NewFromConfig(cfg)
}

func main() {
	dynamoClient := initDynamoClient()
	dbClient := &database.DynamoDBClient{
		Client:    dynamoClient,
		TableName: "todos",
	}

	setupRoutes(dbClient)

	port := os.Getenv("PORT")
	if port == "" {
		port = "8080"
	}

	fmt.Println("Server is running on port", port)
	http.ListenAndServe(":"+port, nil)
}
