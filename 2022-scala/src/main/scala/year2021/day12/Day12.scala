package year2021.day12


import scala.io.Source

object Day12 {
  def part1(input: String): Unit = {

  }

  def main(args: Array[String]): Unit = {
    val input = Source
      .fromInputStream(getClass.getResourceAsStream("data.txt"))
      .mkString
      .trim

    println(part1(input))
  }
}
