package main

import (
	"fmt"
	"os"
	"path/filepath"
	"strings"
)

func main() {
	readInput(1, 2025)
}

func readInput(day, year int) (data []byte) {
	f, _ := os.Getwd()
	dir := filepath.Dir(f)
	dayFormatted := fmt.Sprintf("day%d", day)
	filePath := strings.Join(
		[]string{
			dir,
			dayFormatted,
			"data.txt"},
		string(os.PathSeparator))

	fmt.Println(filePath)
	bytes, err := os.ReadFile(filePath)
	if err != nil {
		panic(err)
	}

	return bytes
}
