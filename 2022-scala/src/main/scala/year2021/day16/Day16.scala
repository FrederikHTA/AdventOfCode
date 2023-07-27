package year2021.day16

import scala.io.Source
import lib.StringImplicits._

sealed trait Packet {
  def packetVersion: BigInt

  def packetTypeId: BigInt
}

case class OperatorPacket(packetVersion: BigInt, packetTypeId: BigInt, subPackets: List[Packet]) extends Packet

case class LiteralPacket(packetVersion: BigInt, packetTypeId: BigInt, packetValue: BigInt) extends Packet

object Day16 {

  def part1(input: String): BigInt = {
    val result = parsePacketString(input)
    val sum = sumVersions(result._1)

    println(s"Part 1 version sum result: $sum")

    sum
  }

  def part2(input: String): BigInt = {
    val result = parsePacketString(input)
    val packetValue = applyRules(result._1)

    println(s"Part 2 packet value: $packetValue")

    packetValue
  }

  def applyRules(packet: Packet): BigInt = packet match {
    case LiteralPacket(_, _, packetValue) => packetValue
    case OperatorPacket(_, packetTypeId, subPackets) =>
      packetTypeId.toInt match {
        case 0 => subPackets.map(applyRules).sum
        case 1 => subPackets.map(applyRules).product
        case 2 => subPackets.map(applyRules).min
        case 3 => subPackets.map(applyRules).max
        case 5 =>
          val head = getPacketValue(subPackets.head)
          val last = getPacketValue(subPackets.last)
          if (head > last) 1 else 0
        case 6 =>
          val head = getPacketValue(subPackets.head)
          val last = getPacketValue(subPackets.last)
          if (head < last) 1 else 0
        case 7 =>
          val head = getPacketValue(subPackets.head)
          val last = getPacketValue(subPackets.last)
          if (head == last) 1 else 0
      }
  }

  def getPacketValue(packet: Packet): BigInt = packet match {
    case OperatorPacket(_, _, subPackets) => subPackets.map(getPacketValue).sum
    case LiteralPacket(_, _, packetValue) => packetValue
  }

  def sumVersions(packet: Packet): BigInt = {
    packet match {
      case OperatorPacket(packetVersion, _, subPackets) => packetVersion + subPackets.map(sumVersions).sum
      case LiteralPacket(packetVersion, _, _) => packetVersion
    }
  }

  def parsePacketString(input: String): (Packet, String) = {
    val (packetVersion, tail) = input.splitAt(3)
    val (packetTypeId, tail2) = tail.splitAt(3)

    packetTypeId.binaryToBigInt().toInt match {
      case 4 =>
        val (literalValue, tail) = parseLiteralValue(tail2)
        (LiteralPacket(packetVersion.binaryToBigInt(), packetTypeId.binaryToBigInt(), literalValue.binaryToBigInt()), tail)
      case _ =>
        val (packetLengthTypeId, tail3) = tail2.splitAt(1)

        packetLengthTypeId match {
          case "0" =>
            val (bits, tail4) = tail3.splitAt(15)
            val length = bits.binaryToBigInt()
            val (subPacketBits, tail5) = tail4.splitAt(length.toInt)
            val subPackets = parseSubPacketsLength(subPacketBits)
            (OperatorPacket(packetVersion.binaryToBigInt(), packetTypeId.binaryToBigInt(), subPackets), tail5)
          case _ =>
            val (bits, tail4) = tail3.splitAt(11)
            val number = bits.binaryToBigInt()
            val (subPackets, tail5) = parseNSubPackets(tail4, number)
            (OperatorPacket(packetVersion.binaryToBigInt(), packetTypeId.binaryToBigInt(), subPackets), tail5)
        }
    }
  }

  def parseNSubPackets(binaryString: String, number: BigInt): (List[Packet], String) = {
    if (number == 0) {
      (Nil, binaryString)
    } else {
      val (packet, tail) = parsePacketString(binaryString)
      val (subPackets, tail2) = parseNSubPackets(tail, number - 1)
      (packet :: subPackets, tail2)
    }
  }

  def parseSubPacketsLength(binaryString: String): List[Packet] = {
    binaryString match {
      case "" => List()
      case _ =>
        val (packet, tail) = parsePacketString(binaryString)
        val subPackets = parseSubPacketsLength(tail)
        packet :: subPackets
    }
  }

  def parseLiteralValue(binaryString: String): (String, String) = {
    val (value, tail) = binaryString.splitAt(5)
    val (prefixBit, bitGroup) = value.splitAt(1)

    prefixBit match {
      case "0" => (bitGroup, tail)
      case _ =>
        val (value2, tail2) = parseLiteralValue(tail)
        (value.tail + value2, tail2)
    }
  }

  def hexToBinaryMap: Map[String, String] = Map[String, String](
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

  def parseHex(input: String): String = {
    input.split("").map(x => hexToBinaryMap(x)).mkString
  }

  def main(args: Array[String]): Unit = {
    val input = Source
      .fromInputStream(getClass.getResourceAsStream("data.txt"))
      .mkString
      .trim

    val part1Result = part1(parseHex(input))
    println(part1Result)
    assert(part1Result == 886)

    val part2Result = part2(parseHex(input))
    println(part2Result)
    assert(part2Result == 184487454837L)
  }
}
