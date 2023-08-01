package scala-lib

import scala-lib.Pos.Pos

object GridImplicits {
  extension[A] (grid: Grid[A]) {
    def apply(pos: Pos): A = grid(pos.y)(pos.x)

    def containsPos(pos: Pos): Boolean =
      0 <= pos.x && 0 <= pos.y && pos.y < grid.size && pos.x < grid(pos.y).size

    def updateGrid(pos: Pos, value: A): Grid[A] =
      grid.updated(pos.y, grid(pos.y).updated(pos.x, value))

    def width(): Int = grid(0).size - 1

    def height(): Int = grid.size - 1
  }
}