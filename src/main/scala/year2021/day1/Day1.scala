package year2021.day1

import scala.io.Source

object Day1 {
  def part1(input: List[Int]): Int = {
    input.zip(input.tail).count { case (x, y) => y > x }
  }

  def part2(input: List[Int]): Int = {
    val slider = input.sliding(3).map(_.sum).toList
    slider.zip(slider.tail).count { case (x, y) => y > x }
  }

  def main(args: Array[String]): Unit = {
    val realData = Source.fromResource("day1/data.txt").getLines.toList.map(_.toInt)
    val testData = Source.fromResource("day1/testdata.txt").getLines.toList.map(_.toInt)

    val part1Result = part1(realData)
    println(part1Result)
    assert(part1Result == 1266)

    val part2Result = part2(realData)
    println(part2Result)
    assert(part2Result == 1217)
  }
}
