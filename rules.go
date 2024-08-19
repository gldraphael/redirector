package main

import (
	"os"
	"log"
	"gopkg.in/yaml.v3"
)

type Rule struct {
	Slug   string
	Target string
}

type rulesWrapper struct {
	Rules []Rule
}

func GetRules(filePath string) []Rule {
	fileText, err := os.ReadFile(filePath)
	if err != nil {
		log.Fatal(err)
	}

	var rules rulesWrapper
	if err := yaml.Unmarshal([]byte(fileText), &rules); err != nil {
        log.Fatal(err)
    }

	return rules.Rules
}
