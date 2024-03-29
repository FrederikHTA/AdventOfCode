package scalalib.Graph

trait GraphTraversal[A] {
  val startNode: A
  def neighbors(node: A): IterableOnce[(A, Int)]
}

trait UnitNeighbors[A] { this: GraphTraversal[A] =>
  def unitNeighbors(node: A): IterableOnce[A]

  override final def neighbors(node: A): IterableOnce[(A, Int)] = unitNeighbors(node).iterator.map(_ -> 1)
}

trait Distances[A] {
  def distances: collection.Map[A, Int]

  def nodes: collection.Set[A] = distances.keySet
}
