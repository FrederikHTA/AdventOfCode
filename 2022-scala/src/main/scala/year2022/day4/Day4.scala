package year2022.day4

import scalalib.Pos.Pos
import year2021.day15.Day15.getClass
import year2022.day2.Day2.{parseInput, part1}

import scala.io.Source

final case class ElfPair(elf1: Pos, elf2: Pos)

object Day4 {
  def part1(input: Seq[ElfPair]): Int =
    input.count(findContainedCount)

  def part2(input: Seq[ElfPair]): Int =
    input.count(findOverlapCount)

  def findOverlapCount(elfPairs: ElfPair): Boolean = {
    elfPairs.elf1.overlaps(elfPairs.elf2)
  }

  def findContainedCount(elfPairs: ElfPair): Boolean = {
    elfPairs.elf1.contains(elfPairs.elf2) || elfPairs.elf2.contains(elfPairs.elf1)
  }

  def parseInput(input: Seq[String]): Seq[ElfPair] = {
    val regex = """(\d+)-(\d+),(\d+)-(\d+)""".r

    input.map(line => line match {
      case regex(min1, max1, min2, max2) =>
        ElfPair(Pos(min1.toInt, max1.toInt), Pos(min2.toInt, max2.toInt))
    })
  }

  def main(args: Array[String]): Unit = {
    val input = Source
      .fromInputStream(getClass.getResourceAsStream("data.txt"))
      .getLines
      .toSeq

    val parsedInput = parseInput(input)

    val part1Res = part1(parsedInput)
    println(part1Res)
    assert(part1Res == 530)

    val part2Res = part2(parsedInput)
    println(part2Res)
    assert(part2Res == 903)
  }
}
