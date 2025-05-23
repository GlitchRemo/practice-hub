package main

import (
	"fmt"
	"net/http"
)

func main() {
	http.HandleFunc("/", func(w http.ResponseWriter, r *http.Request) {
		fmt.Fprintln(w, "Welcome to the Notes App Backend!")
	})

	fmt.Println("Server is running on port 8080")
	http.ListenAndServe(":8080", nil)
}
