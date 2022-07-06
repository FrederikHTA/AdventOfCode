package day10


import scala.annotation.tailrec
import scala.io.Source

// this is garbage
object Day10 {
  def part1(input: Vector[List[String]]): Int = {
    val result = input.map(line => {
      (findLastCorrupt(line, "", ""), line)
    })

    result.map(x => x._1).filter(_.nonEmpty).map {
      case ")" => 3
      case "]" => 57
      case "}" => 1197
      case ">" => 25137
    }.sum
  }

  def part2(input: Vector[List[String]]): BigInt = {
    val corruptedLines = input.map(line => {
      (findLastCorrupt(line, "", ""), line)
    }).filter(x => x._1.nonEmpty).map(x => x._2)

    val nonCorruptedLines = input.filterNot(x => corruptedLines.contains(x))

    val result = nonCorruptedLines.map(line => {
      findMissingEndings(line, "")
    }).map(x => {
      x.split("").map {
        case "(" => ")"
        case "[" => "]"
        case "{" => "}"
        case "<" => ">"
      }.mkString("").reverse
    }).map(x => {
      x.split("").foldLeft(0: BigInt) {
        case (acc, ")") => (5 * acc) + 1
        case (acc, "]") => (5 * acc) + 2
        case (acc, "}") => (5 * acc) + 3
        case (acc, ">") => (5 * acc) + 4
        case _ => 0
      }
    })

    result.sorted.apply(result.length / 2)
  }

  @tailrec
  def findMissingEndings(input: List[String], state: String): String = {
    input match {
      case head :: tail => head match {
        case "{" => findMissingEndings(tail, state + "{")
        case "(" => findMissingEndings(tail, state + "(")
        case "[" => findMissingEndings(tail, state + "[")
        case "<" => findMissingEndings(tail, state + "<")
        case "}" => if (state.last == '{')
          findMissingEndings(tail, state.take(state.length - 1)) else findMissingEndings(List(), state)
        case ")" => if (state.last == '(')
          findMissingEndings(tail, state.take(state.length - 1)) else findMissingEndings(List(), "")
        case "]" => if (state.last == '[')
          findMissingEndings(tail, state.take(state.length - 1)) else findMissingEndings(List(), "")
        case ">" => if (state.last == '<')
          findMissingEndings(tail, state.take(state.length - 1)) else findMissingEndings(List(), "")
        case _ => state
      }
      case _ => state
    }
  }

  @tailrec
  def findLastCorrupt(input: List[String], state: String, corrupt: String): String = {
    if (corrupt.nonEmpty) corrupt
    else {
      input match {
        case head :: tail => head match {
          case "{" => findLastCorrupt(tail, state + "{", corrupt)
          case "(" => findLastCorrupt(tail, state + "(", corrupt)
          case "[" => findLastCorrupt(tail, state + "[", corrupt)
          case "<" => findLastCorrupt(tail, state + "<", corrupt)
          case "}" => if (state.last == '{') findLastCorrupt(tail, state.take(state.length - 1), corrupt) else findLastCorrupt(List(), "", head)
          case ")" => if (state.last == '(') findLastCorrupt(tail, state.take(state.length - 1), corrupt) else findLastCorrupt(List(), "", head)
          case "]" => if (state.last == '[') findLastCorrupt(tail, state.take(state.length - 1), corrupt) else findLastCorrupt(List(), "", head)
          case ">" => if (state.last == '<') findLastCorrupt(tail, state.take(state.length - 1), corrupt) else findLastCorrupt(List(), "", head)
        }
        case _ => corrupt
      }
    }
  }

  def parseInput(str: String): Vector[List[String]] = {
    str.linesIterator.map(res => res.split("").toList).toVector
  }

  def main(args: Array[String]): Unit = {
    val input = Source
      .fromInputStream(getClass.getResourceAsStream("data.txt"))
      .mkString
      .trim

    val part1Result = part1(parseInput(input))
    println(part1Result)
    assert(part1Result == 323613)

    val part2Result = part2(parseInput(input))
    println(part2Result)
    assert(part2Result == 3103006161L)
  }
}
