<Query Kind="Program">
  <IncludeUncapsulator>false</IncludeUncapsulator>
</Query>

void Main()
{
	var inputFile = Path.Combine(Path.GetDirectoryName(Util.CurrentQueryPath), "input.txt");
	var lines = File.ReadAllLines(inputFile);
	int pairsContainedInEachOther = 0;
	int pairsThatOverlap = 0;

	foreach(var line in lines){
		var assignments = GetPairAssignments(line);
		if (assignments.first.Contains(assignments.second) || assignments.second.Contains(assignments.first)){
			pairsContainedInEachOther++;
		}
		if (assignments.first.Overlaps(assignments.second))
		{
			pairsThatOverlap++;
		}
	}

	Console.WriteLine(pairsContainedInEachOther);
	Console.WriteLine(pairsThatOverlap); 
}

struct WorkAssignment {

	public WorkAssignment(string rawAssignment)
	{
		var sections = rawAssignment.Split('-');
		StartAtSection = int.Parse(sections[0]);
		EndAtSection = int.Parse(sections[1]);
	}
	
	public int StartAtSection { get; set;}
	public int EndAtSection { get; set;}
	
	public bool Contains(WorkAssignment assignment){
		return (this.StartAtSection <= assignment.StartAtSection) && (this.EndAtSection >= assignment.EndAtSection);
	}
	
	public bool Overlaps(WorkAssignment assignment)
	{
		return (this.StartAtSection >= assignment.StartAtSection && this.StartAtSection <= assignment.EndAtSection) 
			|| (this.EndAtSection >= assignment.StartAtSection && this.StartAtSection <= assignment.EndAtSection);
	}
	
}

(WorkAssignment first, WorkAssignment second) GetPairAssignments(string line){
	string[] pairs = line.Split(',');
	return (new WorkAssignment(pairs[0]), new WorkAssignment(pairs[1]));
}