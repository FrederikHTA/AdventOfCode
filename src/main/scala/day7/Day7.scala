package day7

import scala.io.Source

object Day7 {
  def part1(input: Seq[Int]): Int = {
    // get most frequent element
    val i = input.sorted.apply(input.size / 2)
    calculateDistance(input, i).sum
  }

  def part2(input: Seq[Int]): Int = {
    (input.min to input.max).map(i => calculateDistance(input, i).map(calculateStepIncreases).sum).min
  }

  def calculateDistance(input: Seq[Int], n: Int): Seq[Int] = {
    input.map(x => (x - n).abs)
  }

  // stolen from reddit meme
  def calculateStepIncreases(x: Int): Int = {
    (x * (x + 1)) / 2
  }

  def main(args: Array[String]): Unit = {
    lazy val input = Source.fromInputStream(getClass.getResourceAsStream("testdata.txt"))
      .mkString
      .trim
      .split(",")
      .toSeq
      .map(_.toInt)

    println("Part1 result: " + part1(input))
    println("Part2 result: " + part2(input))
  }
}
