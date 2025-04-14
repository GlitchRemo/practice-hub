package database

import (
	"context"

	"todo-go/types"

	"github.com/aws/aws-sdk-go-v2/feature/dynamodb/attributevalue"
	"github.com/aws/aws-sdk-go-v2/service/dynamodb"
)

type DatabaseClient interface {
	AddTodoToDB(todo types.Todo) error
	GetTodosFromDB() ([]types.Todo, error)
}

type DynamoDBClient struct {
	Client    *dynamodb.Client
	TableName string
}

func (d *DynamoDBClient) AddTodoToDB(todo types.Todo) error {
	av, err := attributevalue.MarshalMap(todo)
	if err != nil {
		return err
	}

	_, err = d.Client.PutItem(context.TODO(), &dynamodb.PutItemInput{
		TableName: &d.TableName,
		Item:      av,
	})
	return err
}

func (d *DynamoDBClient) GetTodosFromDB() ([]types.Todo, error) {
	out, err := d.Client.Scan(context.TODO(), &dynamodb.ScanInput{
		TableName: &d.TableName,
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
