package main

import (
	"fmt"
	"hello-world/util"
)

func main() {
	fmt.Println("Hello world")

	limit := 10
	util.CountWithDefer(limit)
}
