package year2021.day22

import lib.Box.Box3
import lib.Pos.Pos3

import scala.io.Source
import scala.util.matching.Regex

object Day22 {
  final case class RebootStep(isTurnedOn: Boolean, box: Box3)

  final case class Positions(minX: Int, maxX: Int, minY: Int, maxY: Int, minZ: Int, maxZ: Int)

  def runStep(pos: Set[Pos3], step: RebootStep): Set[Pos3] = {
    if (step.isTurnedOn) {
      pos ++ step.box.iterator
    } else {
      pos.filterNot(step.box.contains)
    }
  }

  def part1(steps: Seq[RebootStep]): Int = {
    val boxRegion = Box3(Pos3(-50, -50, -50), Pos3(50, 50, 50))

    val filteredSteps = steps.filter(boxRegion contains _.box)

    filteredSteps.foldLeft(Set.empty)(runStep).size
  }

  def findMinMaxPos(steps: Seq[Day22.RebootStep]): Positions = {
    val minBox = steps.map(_.box.min)
    val maxBox = steps.map(_.box.max)

    val (minX, maxX) = (minBox.map(_.x).min, maxBox.map(_.x).max)
    val (minY, maxY) = (minBox.map(_.y).min, maxBox.map(_.y).max)
    val (minZ, maxZ) = (minBox.map(_.z).min, maxBox.map(_.z).max)
    Positions(minX, maxX, minY, maxY, minZ, maxZ)
  }

  def part2(steps: Seq[RebootStep]): Int = {
    val positions = findMinMaxPos(steps)

    val boxRegion = Box3(Pos3(-50, -50, -50), Pos3(50, 50, 50))

    val boxes = for {
      x <- Range.inclusive(positions.minX, positions.maxX, 6)
      y <- Range.inclusive(positions.minY, positions.maxY, 6)
      z <- Range.inclusive(positions.minZ, positions.maxZ, 6)
    } yield (x, y, z)

    println(boxes)
    val slider = boxes.sliding(2,1).toSeq

    //    val filteredSteps = steps.filter(boxRegion contains _.box)
    //    filteredSteps.foldLeft(Set.empty)(runStep).size
    1
  }

  // TODO: Find min and max for each axis???
  // TODO: Split into X sub boxes
  // TODO: Run all steps for each sub box, returning number of turned on positions
  // TODO: sum sub box results??

  val regexPattern: Regex = """(on|off) x=(-?\d+)\.\.(-?\d+),y=(-?\d+)\.\.(-?\d+),z=(-?\d+)\.\.(-?\d+)""".r

  def parseInput(input: String): Seq[RebootStep] =
    input.linesIterator.map(parseLine).toSeq

  def parseLine(input: String): RebootStep = input match {
    case regexPattern(action, xMin, xMax, yMin, yMax, zMin, zMax) =>
      RebootStep(action == "on",
        Box3(
          Pos3(xMin.toInt, yMin.toInt, zMin.toInt),
          Pos3(xMax.toInt, yMax.toInt, zMax.toInt)
        )
      )
  }

  def main(args: Array[String]): Unit = {
    val input = Source
      .fromInputStream(getClass.getResourceAsStream("testdata4.txt"))
      .mkString
      .trim

    val data = Source
      .fromInputStream(getClass.getResourceAsStream("data.txt"))
      .mkString
      .trim

    //    assert(part1(parseInput(data)) == 601104)
    println(part2(parseInput(data)))
  }
}
