package day9

import lib.{Grid, Pos}
import lib.GridExtensions._

import scala.io.Source

object Day9 {
  def part1(input: Grid[Int]): Int = {
    val lowPoints = for {
      (row, x) <- input.zipWithIndex
      (cell, y) <- row.zipWithIndex
      pos = Pos(x, y)
      adjacent = pos.getAdjacent.filter(input.containsPos)
      if adjacent.forall(pos => input(pos) > cell)
    } yield pos

    lowPoints.map(pos => input(pos) + 1).sum
  }

  def parseInput(input: String): Grid[Int] =
    input.linesIterator.map(_.split("").toVector.map(_.toInt)).toVector

  def main(args: Array[String]): Unit = {
    val input = Source
      .fromInputStream(getClass.getResourceAsStream("data.txt"))
      .mkString
      .trim

    println(part1(parseInput(input)))
  }
}
