package main

import (
    "fmt"
    "net/http"
)

func main() {

	var rules = GetRules("./rules.yaml")

	for _, rule := range rules {
		http.HandleFunc(fmt.Sprintf("/%s", rule.Slug), func(w http.ResponseWriter, r *http.Request) {
			http.Redirect(w, r, rule.Target, http.StatusTemporaryRedirect)
		})	
	}

    http.ListenAndServe("localhost:8000", nil)
}
