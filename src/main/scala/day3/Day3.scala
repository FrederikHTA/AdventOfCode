package day3

import scala.io.Source

// TODO: Rewrite this trashcan to use booleans to represent binary number as they are easier to flip
object Day3 {
  def part1(input: List[List[Int]]): Int = {
    val binary = input
      .transpose
      .map(x => x.groupBy(identity).mapValues(_.size).maxBy(_._2)._1)

    val res1 = binary.mkString("")

    val res2 = binary.map {
      case 1 => 0
      case 0 => 1
    }.mkString("")

    val test = Integer.parseInt(res1, 2)
    val test2 = Integer.parseInt(res2, 2)

    test * test2
  }

  def part2(input: List[List[Int]]): Int = {
    val oxygenGeneratorRatingBinary = input.transpose.foldLeft((input, 0))({
      case ((input, iteration), _) =>
        if (input.length == 1) (input, 0) else {
          val transposedInput = input.transpose
          val entriesCount = transposedInput(iteration).groupBy(identity).mapValues(_.size)
          val isEqualEntries = entriesCount.values.forall(_ == entriesCount.head._2)
          val mostCommon = if (isEqualEntries) 1 else entriesCount.maxBy(_._2)._1
          val newList = input.filter(x => x(iteration) == mostCommon)
          (newList, iteration + 1)
        }
    })._1.map(x => x.mkString("")).head

    val co2ScrubberRatingBinary = input.transpose.foldLeft((input, 0))({
      case ((input, iteration), _) =>
        if (input.length == 1) (input, 0)
        else {
          val transposedInput = input.transpose
          val entriesCount = transposedInput(iteration).groupBy(identity).mapValues(_.size)
          val isEqualEntries = entriesCount.values.forall(_ == entriesCount.head._2)
          val leastCommon = if (isEqualEntries) 0 else entriesCount.minBy(_._2)._1
          val newList = input.filter(x => x(iteration) == leastCommon)
          (newList, iteration + 1)
        }
    })._1.map(x => x.mkString("")).head

    val oxygenGeneratorRating = Integer.parseInt(oxygenGeneratorRatingBinary, 2)
    val co2ScrubberRating = Integer.parseInt(co2ScrubberRatingBinary, 2)

    oxygenGeneratorRating * co2ScrubberRating
  }

  def main(args: Array[String]): Unit = {
    val testData = Source.fromResource("day3/testdata.txt").getLines.toList.map(_.split("").map(_.toInt).toList)
    val data = Source.fromResource("day3/data.txt").getLines.toList.map(_.split("").map(_.toInt).toList)

    println(part1(data))
    println(part2(data))

    val part1Result = part1(data)
    println(part1Result)
    assert(part1Result == 4139586)

    val part2Result = part2(data)
    println(part2Result)
    assert(part2Result == 1800151)
  }
}
