package lib

case class Pos(x: Int, y: Int) {
  def +(that: Pos): Pos = Pos(x + that.x, y + that.y)

  def *:(k: Int): Pos = Pos(k * x, k * y)

  def manhattanDistance(that: Pos): Int = (x - that.x).abs + (y - that.y).abs

  def <=(that: Pos): Boolean = x <= that.x && y <= that.y

  def min(that: Pos): Pos = Pos(x min that.x, y min that.y)

  def max(that: Pos): Pos = Pos(x max that.x, y max that.y)

  def getAdjacent: Seq[Pos] = List(Pos(x + 1, y), Pos(x - 1, y), Pos(x, y + 1), Pos(x, y - 1))
}

object Pos {
  val zero: Pos = Pos(0, 0)

  val axisOffsets: Seq[Pos] = Seq(Pos(0, 1), Pos(-1, 0), Pos(1, 0), Pos(0, -1))
  val diagonalOffsets: Seq[Pos] = Seq(Pos(-1, 1), Pos(1, 1), Pos(-1, -1), Pos(1, -1))
  val allOffsets: Seq[Pos] = axisOffsets ++ diagonalOffsets
}
