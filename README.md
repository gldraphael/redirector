# redirector

`redirector` is a config-only bare bones URL shortener. 

It supports the following features:

* Use configuration to set URL slugs to redirect to a target URL.
* 30MB uncompressed docker image (13MB compressed).
* No database, no analytics, no external dependencies.

Redirection rules can be configured using a simple `rules.yaml` file:

```yaml
rules:
  - slug:   author
    target: https://github.com/gldraphael
  - slug:   repo
    target: https://github.com/gldraphael/redirector
```

## Quickstart with docker

```sh
docker run --rm -p 8080:8080 ghcr.io/gldraphael/redirector:main
# localhost:8080/repo will now redirect to this repo
```

## Quickstart with go

```sh
go run .
```

## Configuration

Environment variable        | Default value    | Description
----------------------------|------------------|----------------------
`REDIRECTOR_RULES`          | `./rules.yaml`   | The file to read the redirection rules from.
`REDIRECTOR_SERVER_ADDRESS` | `localhost:8080` | The server address.
