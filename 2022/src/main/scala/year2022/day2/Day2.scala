package year2022.day2

import year2021.day15.Day15.getClass
import year2022.day2

import scala.collection.immutable.{AbstractSeq, LinearSeq}
import scala.io.Source
import RockPaperScissor._
import RoundResult._

final case class InputTuples(elfChoice: Choice, myChoice: Choice)

enum RoundResult(value: Int):
  def getValue() = value

  case Loss extends RoundResult(0)
  case Draw extends RoundResult(3)
  case Win extends RoundResult(6)

enum RockPaperScissor(value: Int):
  def getValue() = value

  case Rock extends RockPaperScissor(1)
  case Paper extends RockPaperScissor(2)
  case Scissor extends RockPaperScissor(3)

enum Choice(choice: RockPaperScissor, choiceValue: Int, desiredResult: RoundResult = Loss):
  def getChoice() = choice

  def getValue() = choiceValue

  def getDesiredResult() = desiredResult

  case X extends Choice(Rock, 1, Loss)
  case Y extends Choice(Paper, 2, Draw)
  case Z extends Choice(Scissor, 3, Win)

  case A extends Choice(Rock, -1)
  case B extends Choice(Paper, -1)
  case C extends Choice(Scissor, -1)

object Day2 {
  def part1(input: Seq[InputTuples]): Int = {
    val totalScore = input.foldLeft(0)(calculateWinner)
    totalScore
  }

  def part2(input: Seq[InputTuples]): Int = {
    val totalScore = input.foldLeft(0)(calculateDesiredOutcome)
    totalScore
  }

  def calculateDesiredOutcome(score: Int, input: InputTuples): Int = {
    val elfChoice = input.elfChoice.getChoice()
    val desiredResult = input.myChoice.getDesiredResult()
    val myChoice = getDesiredOutcome(elfChoice, desiredResult).getValue()
    score + desiredResult.getValue() + myChoice
  }

  def getDesiredOutcome(elfChoice: RockPaperScissor, myChoice: RoundResult): RockPaperScissor = {
    myChoice match {
      case Loss => elfChoice match {
        case Rock => Scissor
        case Paper => Rock
        case Scissor => Paper
      }
      case Win => elfChoice match {
        case Rock => Paper
        case Paper => Scissor
        case Scissor => Rock
      }
      case Draw => elfChoice
    }
  }

  def calculateWinner(score: Int, input: InputTuples): Int = {
    val elfChoice = input.elfChoice.getChoice()
    val myChoice = input.myChoice.getChoice()
    val roundResult = getWinner(elfChoice, myChoice).getValue()
    score + roundResult + input.myChoice.getValue()
  }

  def getWinner(elfChoice: RockPaperScissor, myChoice: RockPaperScissor): RoundResult = {
    if (elfChoice.getValue() == myChoice.getValue())
      return RoundResult.Draw

    myChoice match {
      case Rock => elfChoice match {
        case Paper => Loss
        case Scissor => Win
      }
      case Paper => elfChoice match {
        case Rock => Win
        case Scissor => Loss
      }
      case Scissor => elfChoice match {
        case Rock => Loss
        case Paper => Win
      }
    }
  }

  def parseInput(input: List[String]): Seq[InputTuples] = {
    val tuples = input.map(x => x.split(" "))
    tuples.map(x => InputTuples(Choice.valueOf(x.head), Choice.valueOf(x.last)))
  }

  def main(args: Array[String]): Unit = {
    val input = Source
      .fromInputStream(getClass.getResourceAsStream("data.txt"))
      .getLines()
      .toList

    val part1Res = part1(parseInput(input))
    println(part1Res)
    assert(part1Res == 10310)

    val part2Res = part2(parseInput(input))
    println(part2Res)
    assert(part2Res == 14859)
  }
}
