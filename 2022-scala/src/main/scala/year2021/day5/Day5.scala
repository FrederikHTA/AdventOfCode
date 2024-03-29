package year2021.day5

import scalalib.IntegerImplicits._
import scalalib.Pos.Pos

import scala.io.Source

object Day5 {
  def part1(input: String, findDiagonal: Boolean = false): Int = {
    val parsedData = parseData(input)

    val resultNumbers = parsedData.map(pair => {
      val (Pos(x1, y1), Pos(x2, y2)) = pair

      if (x1 == x2) {
        (y1 rangeWithDirection y2).map(Pos(x1, _))
      } else if (y1 == y2) {
        (x1 rangeWithDirection x2).map(Pos(_, y1))
      } else if (findDiagonal) {
        val d1 = x1 rangeWithDirection x2
        val d2 = y1 rangeWithDirection y2
        d1.zip(d2).map(x => Pos(x._1, x._2))
      } else {
        List()
      }
    })

    resultNumbers
      .flatten
      .groupBy(identity)
      .view
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
    val testData = Source.fromInputStream(getClass.getResourceAsStream("data.txt"))
      .getLines()
      .toList
      .mkString("\n")
      .trim

    val part1Result = part1(testData)
    println(part1Result)
    assert(part1Result == 5576)

    val part2Result = part1(testData, findDiagonal = true)
    println(part2Result)
    assert(part2Result == 18144)
  }
}
