package year2022.day6

import scala.io.Source

object Day6 {
  def part1(input: String) = {
    val res = input
      .sliding(4)
      .filter(_.distinct.length == 4)
      .toSeq
      .head

    input.indexOfSlice(res) + 4
  }

  def part2(input: String) = {
    val res = input
      .sliding(14)
      .filter(_.distinct.length == 14)
      .toSeq
      .head

    input.indexOfSlice(res) + 14
  }

  def main(args: Array[String]): Unit = {
    val input = Source
      .fromInputStream(getClass.getResourceAsStream("data.txt"))
      .mkString
      .trim

    val part1Res = part1(input)
    println(part1Res)
    assert(part1Res == 1833)

    val part2Res = part2(input)
    println(part2Res)
    assert(part2Res == 3425)
  }
}
