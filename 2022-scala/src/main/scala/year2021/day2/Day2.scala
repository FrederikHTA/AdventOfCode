package year2021.day2

import scala.io.Source

sealed trait Command

case class Forward(amount: Int) extends Command

case class Down(amount: Int) extends Command

case class Up(amount: Int) extends Command

object Day2 {
  def part1(input: List[Command]): Int = {
    val (horizontal, depth) = input.foldLeft(0, 0) {
      case ((horizontal, depth), data) =>
        data match {
          case Forward(amount) => (horizontal + amount, depth)
          case Down(amount) => (horizontal, depth + amount)
          case Up(amount) => (horizontal, depth - amount)
        }
    }

    horizontal * depth
  }

  def part2(input: List[Command]): Int = {
    val (horizontal, depth, _) = input.foldLeft(0, 0, 0) {
      case ((horizontal, depth, aim), data) =>
        data match {
          case Forward(amount) => (horizontal + amount, depth + (aim * amount), aim)
          case Down(amount) => (horizontal, depth, aim + amount)
          case Up(amount) => (horizontal, depth, aim - amount)
        }
    }

    horizontal * depth
  }

  def transformInput(input: List[String]): List[Command] = {
    input
      .map(x => x.split(" "))
      .map { case Array(direction, integer) =>
        direction match {
          case "forward" => Forward(integer.toInt)
          case "up" => Up(integer.toInt)
          case "down" => Down(integer.toInt)
        }
      }
  }

  def main(args: Array[String]): Unit = {
    val realData = Source.fromInputStream(getClass.getResourceAsStream("data.txt")).getLines.toList
    val testData = Source.fromInputStream(getClass.getResourceAsStream("testdata.txt")).getLines.toList

    val part1Result = part1(transformInput(realData))
    println(part1Result)
    assert(part1Result == 1250395)

    val part2Result = part2(transformInput(realData))
    println(part2Result)
    assert(part2Result == 1451210346)
  }
}
