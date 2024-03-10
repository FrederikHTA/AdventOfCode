package scalalib.Pos

trait PosFactory[A <: PosOps[A]] {
  val zero: A
}