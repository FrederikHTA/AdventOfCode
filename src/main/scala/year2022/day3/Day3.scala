package year2022.day3

import year2021.day15.Day15.getClass

import scala.io.Source

object Day3 {
  type Compartment = Seq[String]

  private val range = Seq('-') ++ ('a' to 'z') ++ ('A' to 'Z')

  def part1(compartments: Seq[Compartment]): Int =
    compartments
      .map(findCommonChar)
      .map(_.head)
      .map(range.indexOf)
      .sum

  def part2(input: Seq[Seq[String]]): Int =
    input.map(findCommonChar).map(_.head).map(range.indexOf).sum

  def findCommonChar(input: Seq[String]) =
    input.map(_.toSet).reduce((a, b) => a.intersect(b))

  def parsePart1Input(input: Seq[String]): Seq[Compartment] =
    input.map(x => x.splitAt(x.length / 2)).map((x => Seq(x._1, x._2)))

  def parsePart2Input(input: Seq[String]): Seq[Seq[String]] =
    input.sliding(3, 3).map(x => x).toSeq

  def main(args: Array[String]): Unit = {
    val input = Source
      .fromInputStream(getClass.getResourceAsStream("data.txt"))
      .getLines()
      .toSeq

    val part1Res = part1(parsePart1Input(input))
    println(part1Res)
    assert(part1Res == 7848)

    val part2Res = part2(parsePart2Input(input))
    println(part2Res)
    assert(part2Res == 2616)
  }
}
