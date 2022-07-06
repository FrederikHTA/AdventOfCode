package lib.Pos

trait PosOps[A <: PosOps[A]] {
  def +(that: A): A
  def *:(k: Int): A
  def manhattanDistance(that: A): Int
}
