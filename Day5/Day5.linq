<Query Kind="Program">
  <IncludeUncapsulator>false</IncludeUncapsulator>
</Query>

void Main()
{
	var inputFile = Path.Combine(Path.GetDirectoryName(Util.CurrentQueryPath), "input.txt");
	var lines = File.ReadAllLines(inputFile);
	
	Predicate<string> matchEmptyLIne = line => string.IsNullOrEmpty(line);
	
	var emptyLineNumber = Array.FindIndex(lines, matchEmptyLIne);

	var stacks = GetInitialStackConfiguration(lines,emptyLineNumber - 1);

	for(int i = emptyLineNumber + 1; i < lines.Length; i++){
		var move = ParseMove(lines[i]);
		PerformMoveWith9001(stacks, move);
	}

	var answer = new String( stacks.Select(s => s.Peek()).ToArray());
	answer.Dump();
		

}

private Stack<char>[] GetInitialStackConfiguration(string[] lines, int lastLineIndex){

	var stackConfig = lines[lastLineIndex].Select((x, i) => new { stack = x, charPosition = i }).Where(c => char.IsDigit(c.stack)).ToArray();

	var stacks = new Stack<char>[stackConfig.Length];
	for (int i = 0; i < stacks.Length; i++)
	{
		stacks[i] = new Stack<char>();
	}

	for(int i = lastLineIndex - 1; i >= 0; i--){
		foreach(var config in stackConfig){
			var stackIndex = (int)(char.GetNumericValue(config.stack)) - 1;

			var crate = lines[i][config.charPosition];
			if (char.IsAsciiLetter(crate)){
				stacks[stackIndex].Push(crate);
			}
		}
	}
	
	return stacks;
}

private Move ParseMove(string line){
	
	var numbers = Regex.Matches(line, @"\d+");
	
	return new Move(){
		NumberOfCrates = int.Parse(numbers[0].Value),
		SourceStack = int.Parse(numbers[1].Value),
		DestinationStack = int.Parse(numbers[2].Value)
	};
}

private void PerformMove(Stack<char>[] stacks, Move move){
	var sourceStack = stacks[move.SourceStack - 1];
	var destStack = stacks[move.DestinationStack - 1];
	
	for(int i = 0; i < move.NumberOfCrates; i++){
		destStack.Push(sourceStack.Pop());
	}	
}

private void PerformMoveWith9001(Stack<char>[] stacks, Move move)
{
	var sourceStack = stacks[move.SourceStack - 1];
	var destStack = stacks[move.DestinationStack - 1];

	var tempStack = new Stack<char>();
	for (int i = 0; i < move.NumberOfCrates; i++)
	{
		tempStack.Push(sourceStack.Pop());
	}
	for (int i = 0; i < move.NumberOfCrates; i++)
	{
		destStack.Push(tempStack.Pop());
	}
}

private class Move {
	public int NumberOfCrates {get;set;}
	public int SourceStack { get; set;}
	public int DestinationStack { get; set;}
}