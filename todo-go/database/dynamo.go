package database

import "github.com/aws/aws-sdk-go-v2/service/dynamodb"


 var (
	DynamoClient *dynamodb.Client
	TodoTable    = "todos"
)