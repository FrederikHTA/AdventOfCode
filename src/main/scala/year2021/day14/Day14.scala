package year2021.day14

import scala.io.Source


object Day14 {
  final case class Polymer(elements: Map[Char, Long], pairs: Map[Pair, Long])

  final case class Input(polymer: Polymer, rules: Map[Pair, Char])

  final case class PairCount(pair: Pair, count: Long)

  type Pair = (Char, Char)
  type Rules = Map[Pair, Char]

  def findNewPairs(polymer: Polymer, rules: Map[Pair, Char]): Polymer = {
    val newPairs = polymer.pairs.iterator.flatMap {
      case (pair, count) =>
        val insertChar = rules(pair)
        val pair1 = PairCount((pair._1, insertChar), count)
        val pair2 = PairCount((insertChar, pair._2), count)
        Iterator(pair1, pair2)
    }.toSeq
      .groupBy(_.pair)
      .view
      .mapValues(x => x.map(_.count).sum).toMap

    val newElements = polymer.pairs.foldLeft(polymer.elements) {
      case (elements, (pair, count)) =>
        val insertChar = rules(pair)
        elements + (insertChar -> (elements.getOrElse(insertChar, 0L) + count))
    }

    Polymer(newElements, newPairs)
  }

  def part1(input: Input, steps: Int = 10): Long = {
    val res = (1 to steps).foldLeft(input.polymer)((polymer, _) => findNewPairs(polymer, input.rules))
    val elementValues = res.elements.values
    elementValues.max - elementValues.min
  }

  def parseRules(rule: String): ((Char, Char), Char) = {
    val Seq(left, right) = rule.split(" -> ").toSeq
    (left.head, left.last) -> right.head
  }

  def parsePolymer(polymer: String): Polymer = {
    val elements = polymer.groupBy(identity).view.mapValues(_.length.toLong).toMap
    val pairs = polymer.zip(polymer.tail).groupBy(identity).view.mapValues(_.length.toLong).toMap
    Polymer(elements, pairs)
  }

  def parseInput(input: String): Input = {
    val Seq(p, r) = input.split("\n\n", 2).toSeq

    val polymer = parsePolymer(p)
    val rules = r.linesIterator.map(parseRules).toMap

    Input(polymer, rules)
  }

  def main(args: Array[String]): Unit = {
    val input = Source
      .fromInputStream(getClass.getResourceAsStream("data.txt"))
      .mkString
      .trim

    val part1Result = part1(parseInput(input))
    println(part1Result)
    assert(part1Result == 2988)

    val part2Result = part1(parseInput(input), 40)
    println(part2Result)
    assert(part2Result == 3572761917024L)
  }
}
