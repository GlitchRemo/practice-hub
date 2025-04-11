package types

type Status string

const (
	StatusDone   = "done"
	StatusUndone = "undone"
)

type Todo struct {
	ID     string `dynamodbav:"id"`
	Title  string `dynamodbav:"title"`
	Status Status `dynamodbav:"status"`
}
