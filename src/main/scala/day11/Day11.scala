package day11

import lib._
import lib.GridImplicits._
import lib.Pos._

import scala.annotation.tailrec
import scala.io.Source

object Day11 {
  def part1(grid: Grid[Int], n: Int): Int = {
    val (_, flashCount) = (1 to n).foldLeft((grid, 0)) {
      case ((grid, prevFlashCount), _) =>
        val allIncreasedBy1 = increaseAllBy1(grid)
        val allRecursivelyIncreased = increaseAllAbove9recursively(allIncreasedBy1)
        val flashesThisIteration = getAmountOfFlashes(allRecursivelyIncreased)
        val updatedGrid = resetFlashedOctopuses(allRecursivelyIncreased)

        (updatedGrid, prevFlashCount + flashesThisIteration)
    }

    flashCount
  }

  // TODO: Fix
  def part2(grid: Grid[Int]): Int = {
    // idk how to solve this properly, so I'm just going to brute force it
    val (_, flashCount) = (1 to 400).foldLeft((grid, 0)) {
      case ((grid, allFlashIteration), iteration) =>
        val allIncreasedBy1 = increaseAllBy1(grid)
        val allRecursivelyIncreased = increaseAllAbove9recursively(allIncreasedBy1)
        val flashesThisIteration = getAmountOfFlashes(allRecursivelyIncreased)
        val updatedGrid = resetFlashedOctopuses(allRecursivelyIncreased)

        val hasAllFlashed = flashesThisIteration == grid.size * grid(0).size
        (updatedGrid, if (hasAllFlashed && allFlashIteration == 0) iteration else allFlashIteration)
    }

    flashCount
  }

  def getAmountOfFlashes(input: Grid[Int]): Int = {
    input.map(_.count(_ < 0)).sum
  }

  @tailrec
  def increaseAllAbove9recursively(grid: Grid[Int]): Grid[Int] = {
    val positions = for {
      (row, x) <- grid.zipWithIndex
      (cell, y) <- row.zipWithIndex
      pos = Pos(x, y)
      if cell > 9 // greater than 9 and has not flashed before
    } yield pos

    val result = positions.foldLeft(grid) { (grid, pos) =>
      val allOffsets = pos.getAllOffsets.filter(grid.containsPos)

      val newGrid = allOffsets.foldLeft(grid)((grid, pos) => grid.updateGrid(pos, grid(pos) + 1))

      newGrid.updateGrid(pos, -10000)
    }

    val isAnyAbove9 = result.exists(_.exists(_ > 9))
    if (isAnyAbove9) increaseAllAbove9recursively(result) else result
  }

  private def increaseAllBy1(input: Grid[Int]): Grid[Int] = {
    input.map(_.map(x => x + 1))
  }

  private def resetFlashedOctopuses(input: Grid[Int]): Grid[Int] = {
    input.map(_.map(x => if (x < 0) 0 else x))
  }

  private def parseInput(input: String): Grid[Int] =
    input.linesIterator.map(_.split("").toVector.map(x => x.toInt)).toVector

  def main(args: Array[String]): Unit = {
    val input = Source
      .fromInputStream(getClass.getResourceAsStream("data.txt"))
      .mkString
      .trim

    println(part1(parseInput(input), 100))
    println(part2(parseInput(input)))
  }
}
