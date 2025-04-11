# Start with the official Go image
FROM golang:1.24.1

# Set the working directory inside the container
WORKDIR /app

# Copy go mod and sum files first
COPY go.mod go.sum ./

# Download dependencies
RUN go mod download

# Copy the rest of the source code
COPY . .

# Build the Go app
RUN go build -o todo-app

# Command to run the app
CMD ["./todo-app"]
