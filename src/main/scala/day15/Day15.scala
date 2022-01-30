package day15

import lib.Graph.{Dijkstra, GraphSearch, TargetNode}
import lib.{Grid, Utils}
import lib.GridImplicits._
import lib.pos.Pos

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
    Utils.parseStringToGrid(input)
  }

  def main(args: Array[String]): Unit = {
    val input = Source
      .fromInputStream(getClass.getResourceAsStream("data.txt"))
      .mkString
      .trim

    println(part1(parseInput(input)))
    println(part2(parseInput(input)))
  }
}
