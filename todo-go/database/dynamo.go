package database

import (
	"context"

	"todo-go/types"

	"github.com/aws/aws-sdk-go-v2/feature/dynamodb/attributevalue"
	"github.com/aws/aws-sdk-go-v2/service/dynamodb"
)

var (
	DynamoClient *dynamodb.Client
	TodoTable    = "todos"
)

func AddTodoToDB(todo types.Todo) error {
	// Convert todo to a DynamoDB item
	av, err := attributevalue.MarshalMap(todo)
	if err != nil {
		return err
	}

	// Save to DynamoDB
	_, err = DynamoClient.PutItem(context.TODO(), &dynamodb.PutItemInput{
		TableName: &TodoTable,
		Item:      av,
	})
	return err
}

func GetTodosFromDB() ([]types.Todo, error) {
	out, err := DynamoClient.Scan(context.TODO(), &dynamodb.ScanInput{
		TableName: &TodoTable,
	})
	if err != nil {
		return nil, err
	}

	var todos []types.Todo
	err = attributevalue.UnmarshalListOfMaps(out.Items, &todos)
	if err != nil {
		return nil, err
	}

	return todos, nil
}
