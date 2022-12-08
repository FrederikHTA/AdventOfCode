package year2022.day8

import lib.Grid
import lib.Pos.Pos
import year2021.day15.Day15.getClass
import year2022.day2.Day2.{parseInput, part1}

import scala.io.Source

final case class Tree(isVisible: Boolean, height: Int)

object Day8 {
  def part1(grid: Grid[Int]) = {
    val trees = buildTrees(grid)
    val pos = buildPos(grid)

    val sorted = pos.sortBy(x => x.x)
    val updatedTrees = pos.foldLeft(trees)(findVisible)

    val visibleTrees = updatedTrees.flatten.map(_.isVisible).count(x => x)

    val len = trees.length * 2
    val hei = grid.length * 2
    val res = visibleTrees - (len + hei)
    res
  }

  def findVisible(trees: Grid[Tree], pos: Pos): Grid[Tree] = {
    import lib.GridImplicits._
    val offsets = pos.getAxisOffsets.map(offsetPos => trees.apply(offsetPos))
    val currentTree = trees.apply(pos)

    val isVisible = offsets.exists(tree => tree.height <= currentTree.height && tree.isVisible)

    trees.updateGrid(pos, Tree(isVisible, currentTree.height))
  }

  def buildPos(grid: Grid[Int]): Vector[Pos] = {
    for {
      (row, rowIndex) <- grid.zipWithIndex
      (_, cellIndex) <- row.zipWithIndex
      pos = Pos(cellIndex + 1, rowIndex + 1)
    } yield pos
  }

  def buildTrees(grid: Grid[Int]): Grid[Tree] = {
    val edgeTrees = (0 to grid.length + 1).map(_ => Tree(true, 0)).toVector

    val trees = for {
      (row, rowIndex) <- grid.zipWithIndex
      trees = row.map(height => Tree(false, height))
    } yield Tree(true, 0) +: trees :+ Tree(true, 0)

    val res = edgeTrees +: trees :+ edgeTrees
    res
  }

  def parseInput(input: String): Grid[Int] = {
    input
      .linesIterator
      .map(_.split("")
        .toVector
        .map(x => x.toInt))
      .toVector
  }

  def main(args: Array[String]): Unit = {
    val input = Source
      .fromInputStream(getClass.getResourceAsStream("testdata.txt"))
      .mkString
      .trim

    val parsedInput = parseInput(input)
    val part1Res = part1(parsedInput)
    println(part1Res)
    //    assert(part1Res == )
  }
}
