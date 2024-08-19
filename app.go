package main

import (
	"fmt"
	"net/http"
	"os"
)

func main() {
	
	var rulesFile = getEnv("REDIRECTOR_RULES", "./rules.yaml");
	var serverAddr = getEnv("SERVER_ADDRESS", "localhost:8080");
	var rules = GetRules(rulesFile)

	for _, rule := range rules {
		http.HandleFunc(fmt.Sprintf("/%s", rule.Slug), func(w http.ResponseWriter, r *http.Request) {
			http.Redirect(w, r, rule.Target, http.StatusTemporaryRedirect)
		})
	}

    http.ListenAndServe(serverAddr, nil)
}

func getEnv(key, fallback string) string {
    value, exists := os.LookupEnv(key)
    if !exists {
        value = fallback
    }
    return value
}
