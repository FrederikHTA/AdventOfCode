package lib.Graph

import lib.Grid
import lib.GridImplicits._
import lib.Pos.Pos

import scala.collection.mutable

object Dijkstra {
  def posNeighbors(pos: Pos, grid: Grid[Int]): IterableOnce[(Pos, Int)] = {
    for {
      neighbour <- pos.getAxisOffsets
      if grid.containsPos(neighbour)
    } yield neighbour -> grid(neighbour)
  }

  // TODO: reduce duplication without impacting performance
  def traverse[A](graphTraversal: GraphTraversal[A]): Distances[A] = {
    val visitedDistance: mutable.Map[A, Int] = mutable.Map.empty
    val toVisit: mutable.PriorityQueue[(Int, A)] = mutable.PriorityQueue.empty(Ordering.by(-_._1))

    def enqueue(node: A, dist: Int): Unit = {
      toVisit.enqueue((dist, node))
    }

    enqueue(graphTraversal.startNode, 0)

    while (toVisit.nonEmpty) {
      val (dist, node) = toVisit.dequeue()
      if (!visitedDistance.contains(node)) {
        visitedDistance(node) = dist

        def goNeighbor(newNode: A, distDelta: Int): Unit = {
          if (!visitedDistance.contains(newNode)) { // avoids some unnecessary queue duplication but not all
            val newDist = dist + distDelta
            enqueue(newNode, newDist)
          }
        }

        graphTraversal.neighbors(node).iterator.foreach((goNeighbor _).tupled) // eta expansion postfix operator _
      }
    }

    new Distances[A] {
      override def distances: collection.Map[A, Int] = visitedDistance
    }
  }

  def search[A](graphSearch: GraphSearch[A]): Distances[A] with Target[A] = {
    val visitedDistance: mutable.Map[A, Int] = mutable.Map.empty
    val toVisit: mutable.PriorityQueue[(Int, A)] = mutable.PriorityQueue.empty(Ordering.by(-_._1))

    def enqueue(node: A, dist: Int): Unit = {
      toVisit.enqueue((dist, node))
    }

    enqueue(graphSearch.startNode, 0)

    while (toVisit.nonEmpty) {
      val (dist, node) = toVisit.dequeue()
      if (!visitedDistance.contains(node)) {
        visitedDistance(node) = dist

        if (graphSearch.isTargetNode(node, dist)) {
          return new Distances[A] with Target[A] {
            override def distances: collection.Map[A, Int] = visitedDistance

            override def target: Option[(A, Int)] = Some(node -> dist)
          }
        }


        def goNeighbor(newNode: A, distDelta: Int): Unit = {
          if (!visitedDistance.contains(newNode)) { // avoids some unnecessary queue duplication but not all
            val newDist = dist + distDelta
            enqueue(newNode, newDist)
          }
        }

        graphSearch.neighbors(node).iterator.foreach((goNeighbor _).tupled) // eta expansion postfix operator _
      }
    }

    new Distances[A] with Target[A] {
      override def distances: collection.Map[A, Int] = visitedDistance

      override def target: Option[(A, Int)] = None
    }
  }
}