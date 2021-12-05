package day5

import lib.IntegerExtensions.RangeOperations

import scala.io.Source

final case class Pos(x: Int, y: Int)

object Day5 {
  def part1(input: String, findDiagonal: Boolean = false): Int = {
    val parsedData = parseData(input)

    val resultNumbers = parsedData.map(pair => {
      val (Pos(x1, y1), Pos(x2, y2)) = pair

      if (x1 == x2) {
        (y1 createRangeWithDirection y2).map(Pos(x1, _))
      } else if (y1 == y2) {
        (x1 createRangeWithDirection x2).map(Pos(_, y1))
      } else if (findDiagonal) {
        val d1 = x1 createRangeWithDirection x2
        val d2 = y1 createRangeWithDirection y2
        d1.zip(d2).map(x => Pos(x._1, x._2))
      } else {
        List()
      }
    })

    resultNumbers
      .flatten
      .groupBy(identity)
      .mapValues(_.length)
      .toList
      .count(_._2 > 1)
  }

  def parseData(input: String): Array[(Pos, Pos)] = {
    // ew
    input
      .split("\n")
      .map(_.split("->"))
      .map(test => {
        val pos = test.map(x => {
          val res = x.split(",").map(_.trim.toInt)
          Pos(res.head, res.last)
        })
        (pos.head, pos.last)
      })
  }

  def main(args: Array[String]): Unit = {
    val testData = Source.fromResource("day5/data.txt")
      .getLines()
      .toList
      .mkString("\n")
      .trim

    println(s"Part 1: ${part1(testData)}")
    println(s"Part 2: ${part1(testData, findDiagonal = true)}")
  }
}
