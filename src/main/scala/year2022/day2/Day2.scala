package year2022.day2

import year2021.day15.Day15.getClass
import year2022.day2

import scala.collection.immutable.{AbstractSeq, LinearSeq}
import scala.io.Source
import RockPaperScissor._

final case class InputTuples(elfChoice: ElfChoice, myChoice: MyChoice)

enum RoundResult(value: Int):
  def getValue() = value
  case Loss extends RoundResult(0)
  case Draw extends RoundResult(3)
  case Win extends RoundResult(6)

enum RockPaperScissor:
  case Rock, Paper, Scissor

enum ElfChoice(choice: RockPaperScissor):
  def getChoice() = choice

  case A extends ElfChoice(Rock)
  case B extends ElfChoice(Paper)
  case C extends ElfChoice(Scissor)

enum MyChoice(choice: RockPaperScissor, choiceValue: Int):
  def getChoice() = choice
  def getValue() = choiceValue

  case X extends MyChoice(Rock, 1)
  case Y extends MyChoice(Paper, 2)
  case Z extends MyChoice(Scissor, 3)

object Day2 {
  def part1(input: Seq[InputTuples]): Int = {
    val totalScore = input.foldLeft(0)(calculateWinner)
    totalScore
  }

  def calculateWinner(score: Int, input: InputTuples): Int = {
    val myChoice = input.myChoice.getChoice()
    val elfChoice = input.elfChoice.getChoice()
    score + getWinner(elfChoice, myChoice).getValue() + input.myChoice.getValue()
  }

  def getWinner(elfChoice: RockPaperScissor, myChoice: RockPaperScissor): RoundResult = {
    if (myChoice == Rock && elfChoice == Paper)
      return RoundResult.Loss
    if (myChoice == Rock && elfChoice == Scissor)
      return RoundResult.Win
    if (myChoice == Paper && elfChoice == Rock)
      return RoundResult.Win
    if (myChoice == Paper && elfChoice == Scissor)
      return RoundResult.Loss
    if (myChoice == Scissor && elfChoice == Rock)
      return RoundResult.Loss
    if (myChoice == Scissor && elfChoice == Paper)
      return RoundResult.Win

    return  RoundResult.Draw
  }

  def parseInput(input: List[String]): Seq[InputTuples] = {
    val tuples = input.map(x => x.split(" "))
    tuples.map(x => InputTuples(ElfChoice.valueOf(x.head), MyChoice.valueOf(x.last)))
  }

  def main(args: Array[String]): Unit = {
    val input = Source
      .fromInputStream(getClass.getResourceAsStream("data.txt"))
      .getLines()
      .toList

    val part1Res = part1(parseInput(input))
    println(part1Res)
    assert(part1Res == 10310)
  }
}
