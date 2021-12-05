package day1

import scala.io.Source

object Day1 extends App {
  val realData = Source.fromResource("day1/data.txt").getLines.toList.map(_.toInt)
  val testData = Source.fromResource("day1/testdata.txt").getLines.toList.map(_.toInt)

  println(
    part1(realData),
    part2(realData)
  )

  def part1(input: List[Int]): Int = {
    input.zip(input.tail).count { case (x, y) => y > x }
  }

  def part2(input: List[Int]): Int = {
    val slider = input.sliding(3).map(_.sum).toList
    slider.foreach(x => println(x))
    slider.zip(slider.tail).count { case (x, y) => y > x }
  }
}
