package day2

import scala.io.Source

object Day2 extends App {
  val realData = Source.fromResource("day2/data.txt").getLines.toList
  val testData = Source.fromResource("day2/testdata.txt").getLines.toList

  part1(transformInput(testData))
  part2(transformInput(testData))

  def part1(input: List[(String, Int)]): Unit = println {
    val (horizontal, depth) = input.foldLeft((0, 0)) {
      case ((horizontal, depth), data) =>
        data match {
          case ("forward", x) => (horizontal + x, depth)
          case ("up", x) => (horizontal, depth - x)
          case ("down", x) => (horizontal, depth + x)
        }
    }

    horizontal * depth
  }

  def part2(input: List[(String, Int)]): Unit = println {
    val (horizontal, depth, _) = input.foldLeft((0, 0, 0)) {
      case ((horizontal, depth, aim), data) =>
        data match {
          case ("forward", x) => (horizontal + x, depth + (aim * x), aim)
          case ("up", x) => (horizontal, depth, aim - x)
          case ("down", x) => (horizontal, depth, aim + x)
        }
    }

    horizontal * depth
  }

  def transformInput(input: List[String]): List[(String, Int)] = {
    input
      .map(x => x.split(" "))
      .map { case Array(direction, integer) => (direction, integer.toInt) }
  }
}
