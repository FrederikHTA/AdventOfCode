package day14

import scala.io.Source

final case class Input(polymer: Polymer, rules: Map[(Char, Char), Char])

final case class Polymer(elements: Map[Char, Long], pairs: Map[(Char, Char), Long])

object Day14 {
  def findNewPairs(polymer: Polymer, rules: Map[(Char, Char), Char]): Polymer = {
    val newPairs = polymer.pairs.iterator.flatMap {
      case (pair, count) =>
        val insertChar = rules(pair)
        val pair1 = (pair._1, insertChar) -> count
        val pair2 = (insertChar, pair._2) -> count
        Iterator(pair1, pair2)
    }.toSeq.groupBy(_._1).mapValues(_.map(_._2).sum)

    val newElements = polymer.pairs.foldLeft(polymer.elements) {
      case (elements, (pair, count)) =>
        val insertChar = rules(pair)
        elements + (insertChar -> (elements.getOrElse(insertChar, 0L) + count))
    }

    Polymer(newElements, newPairs)
  }

  def part1(input: Input, steps: Int = 10): Long = {
    val res = (1 to steps).foldLeft(input.polymer) { (polymer, _) =>
      findNewPairs(polymer, input.rules)
    }
    val elementValues = res.elements.values
    elementValues.max - elementValues.min
  }

  def parseRules(rule: String): ((Char, Char), Char) = {
    val Seq(left, right) = rule.split(" -> ").toSeq
    (left.head, left.last) -> right.head
  }

  def parsePolymer(polymer: String): Polymer = {
    val elements = polymer.groupBy(identity).mapValues(_.length.toLong)

    val pairs = polymer.zip(polymer.tail).groupBy(identity).mapValues(_.length.toLong)
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

    println(part1(parseInput(input)))
    println(part1(parseInput(input), 40))
  }
}
