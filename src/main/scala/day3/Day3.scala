package day3

import day2.Day2.{part1, testData, transformInput}

import scala.io.Source

object Day3 extends App {
  val testData = Source.fromResource("day3/testdata.txt").getLines.toList
  val data = Source.fromResource("day3/data.txt").getLines.toList

  part1(data)

  def part1(input: List[String]): Unit = println {
    val binary = input
      .map(_.split("").map(_.toInt).toList)
      .transpose
      .map(x => x.groupBy(identity).mapValues(_.size).maxBy(_._2)._1)

    val res = binary.mkString("")

    val res2 = binary.map {
      case 1 => 0
      case 0 => 1
    }.mkString("")

    val test = Integer.parseInt(res, 2)
    val test2 = Integer.parseInt(res2, 2)

    (s"res1 ${test * test2}")
  }
}
