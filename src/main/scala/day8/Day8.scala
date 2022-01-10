package day8

import scala.collection.immutable.BitSet
import scala.io.Source

object Day8 {
  def main(args: Array[String]): Unit = {
    val input = Source
      .fromInputStream(getClass.getResourceAsStream("testdata1.txt"))
      .mkString
      .trim

    val numbers = Map[Int, String](
      0 -> "abcefg",
      1 -> "cf",
      2 -> "acdeg",
      3 -> "acdfg",
      4 -> "bcdf",
      5 -> "abdfg",
      6 -> "abdefg",
      7 -> "acf",
      8 -> "acdefg",
      9 -> "abcdfg"
    )
  }
}
