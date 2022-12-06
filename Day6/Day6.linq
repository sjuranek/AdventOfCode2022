<Query Kind="Program">
  <IncludeUncapsulator>false</IncludeUncapsulator>
</Query>

void Main()
{
	var inputFile = Path.Combine(Path.GetDirectoryName(Util.CurrentQueryPath), "input.txt");

	var stream = System.IO.File.ReadAllText(inputFile).AsSpan();
	
	Console.WriteLine(GetEndOfPacket(stream, 4));
	Console.WriteLine(GetEndOfPacket(stream, 14));
}

int GetEndOfPacket(ReadOnlySpan<char> stream, int packetSize)
{
	int endOfPacketIndex = 0;
	for (int i = 0; i < stream.Length; i++)
	{
		var msg = stream.Slice(i, packetSize);
		if (msg.ToArray().Distinct().Count() == packetSize)
		{
			endOfPacketIndex = i + packetSize;
			break;
		}
	}
	return endOfPacketIndex;
}