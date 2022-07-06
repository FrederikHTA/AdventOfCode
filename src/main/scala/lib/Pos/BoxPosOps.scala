package lib.Pos

trait BoxPosOps[A <: BoxPosOps[A]] extends PosOps[A] {
  def <=(that: A): Boolean
  def min(that: A): A
  def max(that: A): A
}
