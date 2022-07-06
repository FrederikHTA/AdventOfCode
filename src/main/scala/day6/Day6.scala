package day6

import scala.io.Source

object Day6 {
  // Inspired heavily by sim642 on reddit.

  def part1(input: Vector[Long], days: Int): Long = {

    // iterate number of days
    val state = Vector.tabulate(9)(i => input.count(_ == i).toLong)

    val result = Range.inclusive(1, days).foldLeft(state) {
      case (state, _) => newState(state)
    }

    result.sum
  }

  def newState(state: Vector[Long]): Vector[Long] = {
    val head +: tail = state
    tail.updated(6, head + tail(6)) :+ head
  }

  def main(args: Array[String]): Unit = {
    lazy val input = Source.fromInputStream(getClass.getResourceAsStream("data.txt"))
      .mkString
      .trim
      .split(",")
      .toVector
      .map(_.toLong)

    val part1Result = part1(input, 80)
    println(part1Result)
    assert(part1Result == 373378)

    val part2Result = part1(input, 256)
    println(part2Result)
    assert(part2Result == 1682576647495L)
  }
}
