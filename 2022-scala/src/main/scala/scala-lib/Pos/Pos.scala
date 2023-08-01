package scala-lib.Pos

final case class Pos(x: Int, y: Int) extends BoxPosOps[Pos] {
  def +(that: Pos): Pos =
    Pos(x + that.x, y + that.y)

  def *:(k: Int): Pos =
    Pos(k * x, k * y)

  def manhattanDistance(that: Pos): Int =
    (x - that.x).abs + (y - that.y).abs

  def <=(that: Pos): Boolean =
    x <= that.x && y <= that.y

  def min(that: Pos): Pos =
    Pos(x min that.x, y min that.y)

  def max(that: Pos): Pos =
    Pos(x max that.x, y max that.y)

  def getAxisOffsets: Seq[Pos] =
    Pos.axisOffsets.map(_ + Pos(x, y))

  def getDiagonalOffsets: Seq[Pos] =
    Pos.diagonalOffsets.map(_ + Pos(x, y))

  def getAllOffsets: Seq[Pos] =
    getAxisOffsets ++ getDiagonalOffsets

  def contains(that: Pos): Boolean =
    x <= that.x && that.y <= y

  def overlaps(that: Pos): Boolean =
    x <= that.y && that.x <= y
}

object Pos extends PosFactory[Pos] {
  val zero: Pos = Pos(0, 0)
  val axisOffsets: Seq[Pos] = Seq(Pos(0, 1), Pos(-1, 0), Pos(1, 0), Pos(0, -1))
  val diagonalOffsets: Seq[Pos] = Seq(Pos(-1, 1), Pos(1, 1), Pos(-1, -1), Pos(1, -1))
  val allOffsets: Seq[Pos] = axisOffsets ++ diagonalOffsets
}
