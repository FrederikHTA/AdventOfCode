package year2021.day13

import lib.Grid

import scala.io.Source
import scala.util.matching.Regex
import lib.GridImplicits._
import lib.Pos.Pos

sealed trait Fold {
  def apply(positions: Set[Pos]): Set[Pos]
}

case class FoldUp(y: Int) extends Fold {
  override def apply(positions: Set[Pos]): Set[Pos] = positions.map {
    case Pos(posX, posY) if posY > y => Pos(posX, y - (posY - y))
    case pos => pos
  }
}

case class FoldLeft(x: Int) extends Fold {
  override def apply(positions: Set[Pos]): Set[Pos] = positions.map {
    case Pos(posX, posY) if posX > x => Pos(x - (posX - x), posY)
    case pos => pos
  }
}

case class Input(positions: Set[Pos], folds: Seq[Fold])

object Day13 {
  def part1(input: Input): Int = {
    input.folds.head(input.positions).size
  }

  def part2(input: Input): String = {
    val foldedPosition = input.folds.foldLeft(input.positions)((positions, fold) => fold(positions))

    // TODO: could be better lmao
    val xLength = foldedPosition.map(_.x).max + 1
    val yLength = foldedPosition.map(_.y).max + 1
    val visualization: Grid[String] = Vector.fill(yLength)(Vector.fill(xLength)(" "))

    // good luck reading this shit
    foldedPosition
      .foldLeft(visualization)((grid, pos) => grid.updateGrid(pos, "X"))
      .map(x => x.mkString(""))
      .mkString("\n")
  }

  def parsePos(s: String): Pos = {
    val Seq(x, y) = s.split(",", 2).toSeq
    Pos(x.toInt, y.toInt)
  }

  def parseFolds(s: String): Fold = {
    // TODO: more testing with this
    val foldRegex: Regex = """fold along ([xy])=(\d+)""".r

    s match {
      case foldRegex("y", y) => FoldUp(y.toInt)
      case foldRegex("x", x) => FoldLeft(x.toInt)
    }
  }

  private def parseInput(input: String): Input = {
    val Seq(p, f) = input.split("\r\n\r\n").toSeq
    val positions = p.linesIterator.map(parsePos).toSet
    val folds = f.linesIterator.map(parseFolds).toSeq
    Input(positions, folds)
  }

  def main(args: Array[String]): Unit = {
    val input = Source
      .fromInputStream(getClass.getResourceAsStream("data.txt"))
      .mkString
      .trim

    val part1Result = part1(parseInput(input))
    println(part1Result)
    assert(part1Result == 716)

    val part2Result = part2(parseInput(input))
    println(part2Result)
  }
}
