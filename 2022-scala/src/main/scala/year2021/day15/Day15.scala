package year2021.day15

import scalalib.Graph.{Dijkstra, GraphSearch, TargetNode}
import scalalib.Grid
import scalalib.GridImplicits._
import scalalib.Pos.Pos
import scalalib.StringImplicits._

import scala.io.Source

object Day15 {
  def findLowestTotalRisk(grid: Grid[Int]): Int = {
    val graph = new GraphSearch[Pos] with TargetNode[Pos] {
      override val targetNode: Pos = Pos(grid.width(), grid.height())
      override val startNode: Pos = Pos.zero

      override def neighbors(node: Pos): IterableOnce[(Pos, Int)] = Dijkstra.posNeighbors(node, grid)
    }

    Dijkstra.search(graph)
      .target
      .getOrElse(throw new IllegalStateException("No path found"))._2
  }

  def part1(grid: Grid[Int]): Int = {
    findLowestTotalRisk(grid)
  }

  def part2(grid: Grid[Int]): Int = {
    // lol
    val gridMap = Vector.tabulate(5,5)(_ + _)

    val newGrid = gridMap
      .map(_.map(y => grid.map(_.map(w => w + y))))

    val resultGrid = newGrid
      .flatMap(_.transpose.map(_.flatten))
      .map(_.map(y => if(y > 9) y - 9 else y))

    findLowestTotalRisk(resultGrid)
  }

  def parseInput(input: String): Grid[Int] = {
    input.toGrid
  }

  def main(args: Array[String]): Unit = {
    val input = Source
      .fromInputStream(getClass.getResourceAsStream("data.txt"))
      .mkString
      .trim

    val part1Result = part1(parseInput(input))
    println(part1Result)
    assert(part1Result == 702)

    val part2Result = part2(parseInput(input))
    println(part2Result)
    assert(part2Result == 2955)
  }
}
