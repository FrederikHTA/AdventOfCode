package day21

import scala.annotation.tailrec
import lib.LazyListImplicits._

import scala.collection.mutable

object Day21 {
  final case class Players(p1: Player, p2: Player)

  final case class DeterministicDie(rolls: Int = 0, values: LazyList[Int] = LazyList.from(1).take(100).cycle) {
    def roll: (DeterministicDie, Int) = {
      val (roll, newValueList) = values.splitAt(3)
      (DeterministicDie(rolls + 3, newValueList), roll.sum)
    }
  }

  case class Player(position: Int, score: Int = 0) {
    def move(amount: Int): Player = {
      val playerPosition = (position + amount - 1) % 10 + 1
      Player(playerPosition, score + playerPosition)
    }
  }

  @tailrec
  def play(p1: Player, p2: Player, die: DeterministicDie): (Player, DeterministicDie) = {
    val (newDie, roll) = die.roll
    val newP1 = p1.move(roll)

    if (newP1.score >= 1000) {
      (p2, newDie)
    } else {
      play(p2, newP1, newDie)
    }
  }

  def part1(players: Players): Int = {
    val (player, die) = play(players.p1, players.p2, DeterministicDie())
    player.score * die.rolls
  }

  private val diracRollCounts = {
    (for {
      r1 <- 1 to 3
      r2 <- 1 to 3
      r3 <- 1 to 3
    } yield r1 + r2 + r3).groupMapReduce(identity)(_ => 1)(_ + _)
  }

  // I did NOT solve this my self.... credit to /u/sim642
  def play2(p1: Player, p2: Player): (Long, Long) = {
    val memo = mutable.Map.empty[(Player, Player), (Long, Long)]

    def helper(p1: Player, p2: Player): (Long, Long) = {
      memo.getOrElseUpdate((p1, p2), {
        if (p1.score >= 21)
          (1L, 0L)
        else if (p2.score >= 21)
          (0L, 1L)
        else {
          val res = for {
            (rollValue, rollCount) <- diracRollCounts.iterator
            newP1 = p1.move(rollValue)
            (p2WinCount, p1WinCount) = helper(p2, newP1)
          } yield (p1WinCount * rollCount, p2WinCount * rollCount)
          res.reduce((a, b) => (a._1 + b._1, a._2 + b._2))
        }
      })
    }

    helper(p1, p2)
  }


  def part2(players: Players): (Long, Long) = {
    val (player1Wins, player2Wins) = play2(players.p1, players.p2)
    (player1Wins, player2Wins)
  }

  def main(args: Array[String]): Unit = {
    val example: Players = Players(Player(4), Player(8))
    val data: Players = Players(Player(5), Player(6))

//    assert(part1(example) == 739785)
//    assert(part1(data) == 1002474)
//
//    assert(part2(example) == (444356092776315L, 341960390180808L))
//    assert(part2(data) == (919758187195363L, 635572886949720L))

    val part1Result = part1(data)
    println(part1Result)
    assert(part1Result == 1002474)

    val part2Result = part2(data)
    println(part2Result)
    assert(part2Result == (919758187195363L, 635572886949720L))
  }
}
