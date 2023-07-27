package year2022.day5

import year2021.day15.Day15.getClass
import year2022.day2.Day2.{parseInput, part1}

import scala.io.Source

object Day5 {
  type Stack = Seq[Char]

  final case class Move(count: Int, from: Int, to: Int)

  final case class Input(moves: Seq[Move], crates: Seq[Stack])

  def part1(input: Input): String =
    input
      .moves
      .foldLeft(input.crates)(doMovePart1)
      .map(_.head)
      .mkString

  def part2(input: Input): String =
    input
      .moves
      .foldLeft(input.crates)(doMovePart2)
      .map(_.head)
      .mkString

  def doMovePart2(crates: Seq[Stack], move: Move): Seq[Stack] = {
    val Move(count, from, to) = move

    val (toAdd, newFrom) = crates(from).splitAt(count)

    crates
      .updated(from, newFrom)
      .updated(to, toAdd ++ crates(to))
  }

  def doMovePart1(crates: Seq[Stack], move: Move): Seq[Stack] = {
    val Move(count, from, to) = move

    val (toAdd, newFrom) = crates(from).splitAt(count)

    crates
      .updated(from, newFrom)
      .updated(to, toAdd.reverse ++ crates(to))
  }

  def parseCrates(crates: String): Seq[Stack] = {
    val lines = crates.linesIterator.toSeq
    val maxLineLength = lines.map(_.length).max
    val paddedLines = lines.map(_.padTo(maxLineLength, ' '))
    val transposed = paddedLines.reverse.transpose
    (1 until transposed.size by 4).map(index => transposed(index).tail.filter(_ != ' ').reverse)
  }

  def parseMoves(movesString: String): Seq[Move] =
    movesString.linesIterator.map(parseMove).toSeq

  def parseMove(move: String): Move = {
    val moveRegex = """move (\d+) from (\d+) to (\d+)""".r
    move match {
      case moveRegex(count, from, to) => Move(count.toInt, from.toInt - 1, to.toInt - 1)
    }
  }

  def parseInput(input: String): Input = {
    val Array(cratesString, movesString) = input.split("\r\n\r\n")
    val crates = parseCrates(cratesString)
    val moves = parseMoves(movesString)

    Input(moves, crates)
  }

  def main(args: Array[String]): Unit = {
    val input = Source
      .fromInputStream(getClass.getResourceAsStream("data.txt"))
      .mkString

    val parsedInput = parseInput(input)

    val part1Res = part1(parsedInput)
    println(part1Res)
    assert(part1Res == "LJSVLTWQM")

    val part2Res = part2(parsedInput)
    println(part2Res)
    assert(part2Res == "BRQWDBBJM")
  }
}
