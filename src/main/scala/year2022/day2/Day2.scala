package year2022.day2

import year2021.day15.Day15.getClass

import scala.collection.immutable.{AbstractSeq, LinearSeq}
import scala.io.Source

final case class Input(elfChoice: ElfChoice, myChoice: MyChoice) {

}

enum RockPaperScissor:
  case Rock, Paper, Scissor

enum ElfChoice:
  case A, B, C

enum MyChoice:
  case X, Y, Z

/**
 * Elf Choices
 * A = Rock,
 * B = Paper,
 * C = Scissors
 */

/**
 * My Choices
 * X = Rock,
 * Y = Paper,
 * Z = Scissors
 */

object Day2 {
  val loss = 0
  val draw = 3
  val win = 6

  val rock = 1
  val paper = 2
  val scissor = 3

  def part1(input: Seq[Input]): Int = {
    val result = input.foldLeft(0) {
      case (score, input) => {

        score
      }
    }

    result
  }

  def parseInput(input: List[String]): Seq[Input] = {
    val tuples = input.map(x => x.split(" "))
    tuples.map(x => Input(ElfChoice.valueOf(x.head), MyChoice.valueOf(x.last)))
  }

  def main(args: Array[String]): Unit = {
    val input = Source
      .fromInputStream(getClass.getResourceAsStream("data.txt"))
      .getLines()
      .toList

    val a = input.head
    val b = input.last

    val part1Res = part1(parseInput(input))
    println(part1Res)
  }
}
