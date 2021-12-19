package lib

object GridImplicits {

  implicit class PosGridOps[A](grid: Grid[A]) {
    def apply(pos: Pos): A = grid(pos.x)(pos.y)

    def containsPos(pos: Pos): Boolean = {
      val exists = 0 <= pos.x && 0 <= pos.y && pos.x < grid.size && pos.y < grid(pos.x).size
      exists
    }

    def updateGrid(pos: Pos, value: A): Grid[A] = {
      grid.updated(pos.x, grid(pos.x).updated(pos.y, value))
    }

    def width(): Int = {
      grid(0).size - 1
    }

    def height(): Int = {
      grid.size - 1
    }
  }
}
