package lib.Box

import lib.Pos.Pos

case class Box(min: Pos, max: Pos) extends BoxOps[Pos, Box] {
  override def factory: BoxFactory[Pos, Box] = Box

  override def iterator: Iterator[Pos] = {
    for {
      x <- (min.x to max.x).iterator
      y <- (min.y to max.y).iterator
    } yield Pos(x, y)
  }
}

object Box extends BoxFactory[Pos, Box] {

}