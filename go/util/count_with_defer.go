package util

import "fmt"

func CountWithDefer(limit int) {
	fmt.Println("Counting")

	for i := range limit {
		defer fmt.Println(i)
	}

	fmt.Println("Done")
}
