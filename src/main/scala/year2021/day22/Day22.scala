package year2021.day22

import lib.Box.Box3
import lib.Pos.Pos3

import scala.io.Source
import scala.util.matching.Regex

object Day22 {
  final case class RebootStep(onOff: Boolean, box: Box3)

  def runStep(pos: Set[Pos3], step: RebootStep): Set[Pos3] = {
    if (step.onOff) {
      pos ++ step.box.iterator
    } else {
      pos.filterNot(step.box.contains)
    }
  }

  def part1(steps: Seq[RebootStep]): Int = {
    val boxRegion = Box3(Pos3(-50, -50, -50), Pos3(50, 50, 50))

    val filteredSteps = steps.filter(boxRegion contains _.box)

    filteredSteps.foldLeft(Set.empty[Pos3])(runStep).size
  }

  def part2(steps: Seq[RebootStep]): Int = {
    // TODO
    1
  }

  // TODO: Find min and max for each axis???
  // TODO: Split into X sub boxes
  // TODO: Run all steps for each sub box, returning number of turned on positions
  // TODO: sum sub box results??

  val regexPattern: Regex = """(on|off) x=(-?\d+)\.\.(-?\d+),y=(-?\d+)\.\.(-?\d+),z=(-?\d+)\.\.(-?\d+)""".r

  def parseInput(input: String): Seq[RebootStep] = input.linesIterator.map(parseLine).toSeq

  def parseLine(input: String): RebootStep = input match {
    case regexPattern(action, xMin, xMax, yMin, yMax, zMin, zMax) =>
      RebootStep(action == "on", Box3(Pos3(xMin.toInt, yMin.toInt, zMin.toInt), Pos3(xMax.toInt, yMax.toInt, zMax.toInt)))
  }

  def main(args: Array[String]): Unit = {
    val input = Source
      .fromInputStream(getClass.getResourceAsStream("testdata.txt"))
      .mkString
      .trim

    val data = Source
      .fromInputStream(getClass.getResourceAsStream("data.txt"))
      .mkString
      .trim

    assert(part1(parseInput(data)) == 601104)

    val part1Result = part1(parseInput(data))
    println(part1Result)
    assert(part1Result == 601104)
  }
}
