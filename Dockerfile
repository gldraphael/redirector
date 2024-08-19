# syntax=docker/dockerfile:1

# Build the application from source
FROM golang:1.23 AS build-stage

WORKDIR /app

COPY go.mod go.sum ./
RUN go mod download

COPY *.go ./

RUN CGO_ENABLED=0 GOOS=linux go build -o /redirector

# Run the tests in the container
FROM build-stage AS run-test-stage
RUN go test -v ./...

# Deploy the application binary into a lean image
FROM gcr.io/distroless/base-debian11 AS build-release-stage

EXPOSE 8080

ENV REDIRECTOR_RULES=./rules.yaml
ENV REDIRECTOR_SERVER_ADDRESS=:8080

WORKDIR /app

COPY --from=build-stage /redirector /app/redirector
COPY rules.yaml /app/rules.yaml 



USER nonroot:nonroot

ENTRYPOINT ["/app/redirector"]
