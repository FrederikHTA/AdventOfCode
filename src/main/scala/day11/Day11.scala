package day11

import lib._
import lib.GridExtensions._
import lib.Pos._

import scala.annotation.tailrec
import scala.io.Source

object Day11 {

  def getAmountOfFlashes(input: Grid[Int]): Int = {
    input.map(_.count(_ > 9)).sum
  }

  def part1(grid: Grid[Int], n: Int): Unit = {
    grid.foreach(row => println(row.mkString("")))

    val (resultGrid, flashCount) = (1 to n).foldLeft((grid, 0)) {
      case ((grid, prevFlashCount), iteration) =>
        val allIncreasedBy1 = increaseAllBy1(grid)
        // TODO: Something wrong with recursive step 2 thing here
        val allRecursivelyIncreased = increaseAllAbove9recursively(allIncreasedBy1)
        val flashesThisIteration = getAmountOfFlashes(allRecursivelyIncreased)
        val updatedGrid = resetFlashedOctopuses(allRecursivelyIncreased)

        println("\n -------------- \n")
        println(s"Iteration: $iteration - flashCount: ${prevFlashCount + flashesThisIteration}")
        updatedGrid.foreach(row => println(row.mkString("")))

        (updatedGrid, prevFlashCount + flashesThisIteration)
    }

    println("\n -------------- \n")
    println("flashCount: " + flashCount)
    resultGrid.foreach(row => println(row.mkString("")))
    flashCount
  }

  @tailrec
  def increaseAllAbove9recursively(grid: Grid[Int]): Grid[Int] = {
    val positions = for {
      (row, x) <- grid.zipWithIndex
      (cell, y) <- row.zipWithIndex
      pos = Pos(x, y)
      // some problems with this: <-----------------------------------------------------
      if cell > 9 && cell < 9999 // greater than 9 and has not flashed before
    } yield pos

    val res = positions.foldLeft(grid) { (g1, p1) =>
      val allOffsets = p1.getAllOffsets
      val filteredOffsets = allOffsets.filter(g1.containsPos)

      val newGrid = filteredOffsets.foldLeft(g1) { (g2, p2) =>
        g2.updateGrid(p2, g2(p2) + 1)
      }

      // in relation to this: <-----------------------------------------------------
      newGrid.updateGrid(p1, newGrid(p1) + 9999)
    }

    println("\n -------------- \n")
    grid.foreach(row => println(row.mkString(" ")))

    if(grid.count(x => x.contains(10)) > 0)
      increaseAllAbove9recursively(res)
    else
      res
  }

  private def increaseAllBy1(input: Grid[Int]): Grid[Int] = {
    input.map(_.map(x => x + 1))
  }

  private def resetFlashedOctopuses(input: Grid[Int]): Grid[Int] = {
    input.map(_.map(x => if (x > 9) 0 else x))
  }

  private def parseInput(input: String): Grid[Int] =
    input.linesIterator.map(_.split("").toVector.map(x => x.toInt)).toVector

  def main(args: Array[String]): Unit = {
    val input = Source
      .fromInputStream(getClass.getResourceAsStream("testdata.txt"))
      .mkString
      .trim

    println(part1(parseInput(input), 101))
  }
}
