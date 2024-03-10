package year2021.day9

import scalalib.Grid
import scalalib.GridImplicits._
import scalalib.Pos.Pos
import org.scalatest.Assertions.assertResult

import scala.io.Source

object Day9 {
  def part1(input: Grid[Int]): Int = {
    val lowPoints = findLowPoints(input)

    lowPoints.map(pos => input(pos) + 1).sum
  }

  def part2(input: Grid[Int]): Int = {
    val lowPoints = findLowPoints(input)

    val res = lowPoints.map(pos => {
      findAllAdjacent(pos, input, Seq(pos))
    })

    res.sortBy(_.size).reverse.take(3).map(_.size).product
  }

  private def findAllAdjacent(pos: Pos, input: Grid[Int], basin: Seq[Pos]): Seq[Pos] = {
    val newPos = pos
      .getAxisOffsets
      .filter(input.containsPos)
      .filter(!basin.contains(_))
      .filter(x => input(x) < 9)

    val newBasin = newPos ++ basin

    if(newPos.nonEmpty) {
      newPos.foldLeft(newBasin)((newBasin, pos) => findAllAdjacent(pos, input, newBasin))
    } else newBasin
  }

  private def findLowPoints(input: Grid[Int]): Vector[Pos] = {
    for {
      (row, y) <- input.zipWithIndex
      (cell, x) <- row.zipWithIndex
      pos = Pos(x, y)
      adjacent = pos.getAxisOffsets.filter(input.containsPos)
      if adjacent.forall(pos => input(pos) > cell)
    } yield pos
  }

  private def parseInput(input: String): Grid[Int] =
    input.linesIterator.map(_.split("").toVector.map(_.toInt)).toVector

  def main(args: Array[String]): Unit = {
    val input = Source
      .fromInputStream(getClass.getResourceAsStream("data.txt"))
      .mkString
      .trim

    val part1Result = part1(parseInput(input))
    println(part1Result)
    assertResult(498)(part1Result)

    val part2Result = part2(parseInput(input))
    println(part2Result)
    assertResult(1071000)(part2Result)
  }
}
