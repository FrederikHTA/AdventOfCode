package day16

import scala.io.Source
import lib.StringImplicits._

final case class PacketResult(packetVersion: Int, packetTypeId: Int, packetValue: Int)

object Day16 {

  def part1(input: String): Unit = {
    val binaryInputString = input.split("").map(x => hexadecimalToBinary(x)).mkString

    val packetVersion = getPacketVersion(binaryInputString)
    val packetTypeId = getPacketTypeId(binaryInputString)
    val packetLengthTypeId = getPacketLengthTypeId(binaryInputString)
    val subPacketData = getSubPacketData(binaryInputString)

    if(packetLengthTypeId == "0") {
      val totalLengthOfSubPackets = subPacketData.substring(0, 15).binaryToInteger()
      val subPackets = subPacketData.substring(15, 15 + totalLengthOfSubPackets).grouped(11).toList

      val firstGroups = subPackets.take(subPackets.size - 2)
      val lastGroups = subPackets.takeRight(2).mkString("")

      val groups = firstGroups :+ lastGroups

      val resultGroups = groups.map(parsePacket)

      val versions = resultGroups.map(x => x.packetVersion)

      println(versions.sum)
    } else {
      // TODO
      /***
       * As another example, here is an operator packet (hexadecimal string EE00D40C823060) with length type ID 1 that contains three sub-packets:
       */
    }
  }

  def parsePacket(binaryInputString: String): PacketResult = {
    val packetVersion = getPacketVersion(binaryInputString)
    val packetTypeId = getPacketTypeId(binaryInputString)
    val packetData = getPacketData(binaryInputString).grouped(5)

    val packetValue = packetData.map(_.substring(1)).mkString("").binaryToInteger()

    PacketResult(packetVersion, packetTypeId, packetValue)
  }

  def getSubPacketData(binaryString: String): String = {
    binaryString.substring(7)
  }

  def getPacketData(binaryString: String): String = {
    binaryString.substring(6)
  }

  def getPacketLengthTypeId(binaryString: String): String = {
    binaryString(6).toString
  }

  def getPacketVersion(binaryString: String): Int = {
    binaryString.substring(0,3).binaryToInteger()
  }

  def getPacketTypeId(binaryString: String): Int = {
    binaryString.substring(3,6).binaryToInteger()
  }

  def hexadecimalToBinary: Map[String, String] = Map[String, String](
    "0" -> "0000",
    "1" -> "0001",
    "2" -> "0010",
    "3" -> "0011",
    "4" -> "0100",
    "5" -> "0101",
    "6" -> "0110",
    "7" -> "0111",
    "8" -> "1000",
    "9" -> "1001",
    "A" -> "1010",
    "B" -> "1011",
    "C" -> "1100",
    "D" -> "1101",
    "E" -> "1110",
    "F" -> "1111",
  )

  def main(args: Array[String]): Unit = {
    val input = Source
      .fromInputStream(getClass.getResourceAsStream("testdata.txt"))
      .mkString
      .trim

    println(part1(input))
  }
}
