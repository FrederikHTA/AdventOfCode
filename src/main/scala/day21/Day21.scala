package day21

import scala.annotation.tailrec

final case class Player(playerName: String, position: Int, score: Int = 0)

final case class PlayerList(player1: Player, player2: Player) {
  def getPlayer(playerTurn: Int): Player = {
    if ((playerTurn % 2) == 0) player1 else player2
  }

  def getOtherPlayer(playerTurn: Int): Player = {
    if ((playerTurn % 2) == 0) player2 else player1
  }

  def updatePlayers(turn: Int, player: Player): PlayerList = {
    if (turn % 2 == 0) {
      copy(player1 = player)
    } else {
      copy(player2 = player)
    }
  }
}

final case class Die(rollCount: Int, value: Int) {
  def increaseRollCount(): Die = {
    copy(rollCount = rollCount + 3)
  }

  def setValue(newValue: Int): Die = {
    copy(value = newValue)
  }

  def rollDie(): Die = {
    val dieValue = (1 to 3).map(_ + rollCount).sum

    setValue(dieValue).increaseRollCount()
  }
}

object Day21 {
  def calculatePlayerPosition(position: Int, moves: Int): Int = {
    if ((position + moves) % 10 == 0) 10 else (position + moves) % 10
  }

  def calculatePlayerScore(moves: Int, player: Player): Player = {
    val playerMoves = moves % 10
    val playerPosition = calculatePlayerPosition(player.position, playerMoves)

    Player(player.playerName, playerPosition, player.score + playerPosition)
  }

  @tailrec
  def playGame(turn: Int, die: Die, playerList: PlayerList): (Player, Die) = {
    val playerInTurn = playerList.getPlayer(turn)

    val newDie = die.rollDie()
    val newPlayer = calculatePlayerScore(newDie.value, playerInTurn)

    if (newPlayer.score >= 1000) {
      (playerList.getOtherPlayer(turn), newDie)
    } else {
      playGame(turn + 1, newDie, playerList.updatePlayers(turn, newPlayer))
    }
  }


  def part1(playerList: PlayerList): Int = {
    val gameResult = playGame(0, Die(0, 1), playerList)
    gameResult._1.score * gameResult._2.rollCount
  }

  def main(args: Array[String]): Unit = {
    val example = PlayerList(Player("Player1", 4), Player("Player2", 8))
    val data = PlayerList(Player("Player1", 5), Player("Player2", 6))

    println(s"Example: ${part1(example)}")
    println(s"Part1: ${part1(data)}")

    assert(part1(example) == 739785)
    assert(part1(data) == 1002474)
  }
}
