package lib

object Utils {
  def parseStringToGrid(input: String): Grid[Int] = {
    input.linesIterator.map(_.split("").toVector.map(x => x.toInt)).toVector
  }
}
