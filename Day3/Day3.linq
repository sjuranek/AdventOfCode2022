<Query Kind="Program">
  <IncludeUncapsulator>false</IncludeUncapsulator>
</Query>

void Main()
{
	var inputFile = Path.Combine(Path.GetDirectoryName(Util.CurrentQueryPath), "input.txt");
	var lines = File.ReadAllLines(inputFile);
	int priorityTotal = 0;

	foreach(var line in lines){
		var compartments = GetCompartments(line);
		
		var dupes = compartments.compartment1.Where(c => compartments.compartment2.Contains(c)).Distinct();
		foreach(var c in dupes){
			priorityTotal += GetPriorityForComponent(c);			
		}
	}
	
	Console.WriteLine(priorityTotal);
	
	priorityTotal = 0;
	for(int i = 0; i < lines.Length; i+=3){
		var elf1 = lines[i];
		var elf2 = lines[i+1];
		var elf3 = lines[i+2];
		
		var badge = elf1.Where(c => elf2.Contains(c) && elf3.Contains(c) ).Distinct().FirstOrDefault();
		priorityTotal += GetPriorityForComponent(badge);
	}
	
	Console.WriteLine(priorityTotal);
}

public (string compartment1, string compartment2) GetCompartments(string input){
	input = input.Trim();
	var total = input.Length;
	return (input.Substring(0,(total / 2) ), input.Substring((total / 2)) );
}

public int GetPriorityForComponent(char c){
	 return (Char.IsLower(c)) ? c - 96 : c - 64 + 26;
}