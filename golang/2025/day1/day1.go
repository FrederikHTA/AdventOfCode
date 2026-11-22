package main

import (
	_ "embed"
	"fmt"
	"strconv"
	"strings"
)

type Direction byte

const (
	Left  Direction = 'L'
	Right Direction = 'R'
)

type Move struct {
	Direction Direction
	Steps     int
}

//go:embed data.txt
var input string

//go:embed testdata.txt
var testInput string

func main() {
	part1()
}

func part1() {
	position := 50
	zeroCount := 0
	moves, err := parseInput(input)
	if err != nil {
		panic(err)
	}

	for _, move := range moves {
		offset := move.Steps
		if move.Direction == Left {
			offset = -move.Steps
		}

		position = ((position-offset)%100 + 100) % 100
		if position == 0 {
			zeroCount++
		}
	}

	fmt.Printf("Result of part 1: %d", zeroCount)
}

func parseInput(input string) ([]Move, error) {
	var moves []Move
	for line := range strings.Lines(strings.TrimSpace(input)) {
		line = strings.TrimSpace(line)
		if len(line) < 2 {
			continue
		}

		steps, err := strconv.Atoi(line[1:])
		if err != nil {
			return nil, fmt.Errorf("parse %q: %w", line, err)
		}

		direction := Direction(line[0])
		switch direction {
		case Left, Right:
		default:
			return nil, fmt.Errorf("invalid direction %q", direction)
		}

		moves = append(moves, Move{
			Direction: direction,
			Steps:     steps,
		})
	}

	return moves, nil
}

func part2() {

}
