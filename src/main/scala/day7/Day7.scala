package day7

import scala.io.Source

object Day7 {
  def part1(input: Seq[Int]): Int = {
    // get most frequent element
    val alignPosition = input((input.length / 2).floor.toInt)

    input.map(x => (x - alignPosition).abs).sum
  }

  def part2(input: Seq[Int]): Unit = {
    // TODO: implement
  }

  def main(args: Array[String]): Unit = {
    lazy val input = Source.fromInputStream(getClass.getResourceAsStream("testdata.txt"))
      .mkString
      .trim
      .split(",")
      .toSeq
      .map(_.toInt)
      .sorted

    println(input.mkString("Array(", ", ", ")"))
    println("Part1 result: " + part1(input))
    println("Part2 result: " + part2(input))
  }
}
