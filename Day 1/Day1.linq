<Query Kind="Program">
  <IncludeUncapsulator>false</IncludeUncapsulator>
</Query>

void Main()
{
	var inputFile = Path.Combine(Path.GetDirectoryName(Util.CurrentQueryPath), "Day1.txt");
	var lines = File.ReadAllLines(inputFile);
	
	int[] calorieCounts = new int[1000]; // Assume there are < 1000 elves for now
	int elf = 0;
	
	foreach(var line in lines){
		if (string.IsNullOrEmpty(line.Trim())){
			elf++;
			continue;
		}
		calorieCounts[elf] += int.Parse(line);
	}

	int maxCalories = calorieCounts.Max(x  => x);
	Console.WriteLine($"Elf with most calories as {maxCalories}");
	
	var caloriesByTop3 = calorieCounts.OrderByDescending(c => c).Take(3).Sum();
	Console.WriteLine($"Total calories of top 3 elves: {caloriesByTop3}");
}

// You can define other methods, fields, classes and namespaces here