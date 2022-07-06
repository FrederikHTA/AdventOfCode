package lib.Box

import lib.Pos.Pos3

case class Box3(min: Pos3, max: Pos3) extends BoxOps[Pos3, Box3] {
  override def factory: BoxFactory[Pos3, Box3] = Box3

  override def iterator: Iterator[Pos3] = {
    for {
      x <- (min.x to max.x).iterator
      y <- (min.y to max.y).iterator
      z <- (min.z to max.z).iterator
    } yield Pos3(x, y, z)
  }
}

object Box3 extends BoxFactory[Pos3, Box3] {

}