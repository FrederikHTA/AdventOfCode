package day6

object Day6 {
  def part1(input: Vector[Int]): Unit = {
  }

  def main(args: Array[String]): Unit = {
    lazy val input = io.Source.fromInputStream(getClass.getResourceAsStream("testdata.txt"))
      .mkString
      .trim
      .split(",")
      .toVector
      .map(_.toInt)

    println(part1(input))
  }
}
