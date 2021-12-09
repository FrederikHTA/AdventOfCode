package day9


import scala.io.Source

object Day9 {
  def part1(input: Vector[Vector[Int]]): Int = {
    val points = for {
      (row, x) <- input.view.zipWithIndex
      (cell, y) <- row.view.zipWithIndex
    } yield (cell, x, y)

    val result = points.map(p => {
      val (cell, x, y) = p

      val adjacent = points.filter(p => {
        val (_, x2, y2) = p
        (x2 == x && y2 == y + 1) ||
        (x2 == x && y2 == y - 1) ||
        (x2 == x + 1 && y2 == y) ||
        (x2 == x - 1 && y2 == y)
      })
      val adjacentValues = adjacent.map(_._1)
      if (adjacentValues.forall(_ > cell)) cell + 1 else 0
    })

    result.sum
  }

  def parseInput(input: String): Vector[Vector[Int]] = {
    input.linesIterator.map(_.split("").toVector.map(_.toInt)).toVector
  }

  def main(args: Array[String]): Unit = {
    lazy val input = Source
      .fromInputStream(getClass.getResourceAsStream("data.txt"))
      .mkString
      .trim

    println(part1(parseInput(input)))
  }
}
