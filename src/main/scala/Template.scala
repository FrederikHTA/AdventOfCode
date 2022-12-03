import year2021.day15.Day15.getClass
import year2022.day2.Day2.{parseInput, part1}

import scala.io.Source

object Template {
  def part1(input: String): Unit = {

  }

  def parseInput(input: String) = {
    ""
  }

  def main(args: Array[String]): Unit = {
    val input = Source
      .fromInputStream(getClass.getResourceAsStream("data.txt"))
      .mkString
      .trim

    val part1Res = part1(parseInput(input))
    println(part1Res)
//    assert(part1Res == )
  }
}
