package year2022.day8

import lib.Grid
import lib.Pos.Pos
import year2021.day15.Day15.getClass
import year2022.day2.Day2.{parseInput, part1}

import scala.io.Source

final case class Tree(isVisible: Boolean, height: Int, pos: Pos)

object Day8 {
  def part1(grid: Grid[Int]): Int = {
    val gridTranspose = grid.transpose

    val left   = grid.map(_.scanLeft(-1)(_ max _))
    val right  = grid.map(_.scanRight(-1)(_ max _))
    val top    = gridTranspose.map(_.scanLeft(-1)(_ max _))
    val bottom = gridTranspose.map(_.scanRight(-1)(_ max _))

    val res = for {
      (row, y)        <- grid.zipWithIndex
      (treeHeight, x) <- row.zipWithIndex
      if treeHeight > left(y)(x)
        || treeHeight > right(y)(x + 1)
        || treeHeight > top(x)(y)
        || treeHeight > bottom(x)(y + 1)
    } yield ()

    res.length
  }

  def part2(grid: Grid[Int]) = {
    val res = for {
      (row, y)  <- grid.zipWithIndex
      (tree, x) <- row.zipWithIndex
      pos = Pos(x, y)
    } yield calculateScenicScore(pos, grid)

    res.max
  }

  extension (i: Int) {
    def orDefault(default: Int) = if (i < 0) default else i
  }

  def calculateScenicScore(pos: Pos, grid: Grid[Int]): Int = {
    val transposed = grid.transpose

    val row  = grid(pos.y)
    val col  = transposed(pos.x)
    val cell = row(pos.x)

    val lookLeft  = pos.x - row.lastIndexWhere(_ >= cell, pos.x - 1).orDefault(0)
    val lookRight = row.indexWhere(_ >= cell, pos.x + 1).orDefault(row.size - 1) - pos.x
    val lookUp    = pos.y - col.lastIndexWhere(_ >= cell, pos.y - 1).orDefault(0)
    val lookDown  = col.indexWhere(_ >= cell, pos.y + 1).orDefault(row.size - 1) - pos.y

    lookUp * lookDown * lookLeft * lookRight
  }

  def parseInput(input: String): Grid[Int] = {
    input.linesIterator
      .map(
        _.split("").toVector
          .map(x => x.toInt)
      )
      .toVector
  }

  def main(args: Array[String]): Unit = {
    val input = Source
      .fromInputStream(getClass.getResourceAsStream("data.txt"))
      .mkString
      .trim

    val parsedInput = parseInput(input)

    val part1Res = part1(parsedInput)
    println(part1Res)
    assert(part1Res == 1851)

    val part2Res = part2(parsedInput)
    println(part2Res)
    assert(part2Res == 574080)
  }
}
