package day7

import scala.io.Source

object Day7 {
  def part1(implicit input: Seq[Int]): Int = {
    val i = input.sorted.apply(input.size / 2)
    calculateDistance(i).sum
  }

  def part2(implicit  input: Seq[Int]): Int = {
    (input.min to input.max).map(i => calculateDistance(i).map(calculateStepIncreases).sum).min
  }

  def calculateDistance(n: Int)(implicit input: Seq[Int]): Seq[Int] = {
    input.map(x => (x - n).abs)
  }

  // stolen from reddit meme
  def calculateStepIncreases(x: Int): Int = {
    (x * (x + 1)) / 2
  }

  def main(args: Array[String]): Unit = {
    lazy val input = Source.fromInputStream(getClass.getResourceAsStream("data.txt"))
      .mkString
      .trim
      .split(",")
      .toSeq
      .map(_.toInt)

    val part1Result = part1(input)
    println(part1Result)
    assert(part1Result == 355521)

    val part2Result = part2(input)
    println(part2Result)
    assert(part2Result == 100148777)
  }
}
