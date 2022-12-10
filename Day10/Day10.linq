<Query Kind="Program">
  <IncludeUncapsulator>false</IncludeUncapsulator>
</Query>

void Main()
{
	var inputFile = Path.Combine(Path.GetDirectoryName(Util.CurrentQueryPath), "input.txt");
	var lines = File.ReadAllLines(inputFile);

	int registerX = 1;
	int cycle = 0;
	List<int> signalStrengths = new List<int>();
	var crt = new char[6,40]; 

	foreach (var cmd in lines)
	{
		if (cmd.Trim() == "noop"){
			IncrementAndDrawCRT();
		}
		else if (cmd.StartsWith("addx"))
		{
			// Lame calling this twice in a row good enough for this excersize
			IncrementAndDrawCRT();
			IncrementAndDrawCRT();
			registerX += int.Parse(cmd.Substring(5));
		}				
	}

	Console.WriteLine($"Sum of all special cycles: {signalStrengths.Sum(x => x)}");
	DisplayCRT();
	
	void IncrementAndDrawCRT()
	{
		cycle++;
		// Part 1 - Check if special cycle
		if (cycle == 20 || (cycle - 20) % 40 == 0)
		{
			signalStrengths.Add(cycle * registerX);
		}
		// Part 2 - Draw
		var line = (cycle -1 ) / 40;
		var col = (cycle - 1) % 40;
		crt[line,col] = (col >= registerX - 1 && col <= registerX + 1) ? '#' : '.';
	}
	
	void DisplayCRT(){
		
		for(int line = 0; line < 6; line++){
			StringBuilder currentLine = new StringBuilder();
			for(int col = 0; col < 40; col++){
				currentLine.Append(crt[line,col]);
			}
			Console.WriteLine(currentLine.ToString());;
		}

	}
}

// You can define other methods, fields, classes and namespaces here