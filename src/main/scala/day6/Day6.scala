package day6

object Day6 {
  // Inspired heavily by sim642 on reddit.
  def part1(input: Seq[Int], days: Int): Long = {

    // iterate number of days
    val state = Vector.tabulate(9)(i => input.count(_ == i).toLong)

    val result = Range.inclusive(1, days).foldLeft(state) {
      case (state, _) => newState(state)
    }

    result.sum
  }

  def newState(state: Vector[Long]): Vector[Long] = {
    val head +: tail = state
    tail.updated(6, head + tail(6)) :+ head
  }

  def main(args: Array[String]): Unit = {
    lazy val input = io.Source.fromInputStream(getClass.getResourceAsStream("data.txt"))
      .mkString
      .trim
      .split(",")
      .toSeq
      .map(_.toInt)

    println(part1(input, 80))
    println(part1(input, 256))
  }
}
