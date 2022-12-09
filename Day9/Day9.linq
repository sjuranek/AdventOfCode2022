<Query Kind="Program">
  <IncludeUncapsulator>false</IncludeUncapsulator>
</Query>

void Main()
{
	var inputFile = Path.Combine(Path.GetDirectoryName(Util.CurrentQueryPath), "input.txt");
	var lines = File.ReadAllLines(inputFile);
	
	var numKnots = 10; // Change to 2 for Part 1
	Position[] knots = new Position[numKnots];
	
	for(int i = 0; i < numKnots; i++){
		knots[i] = new Position(1,1);
	}
	
	foreach(var cmd in lines){
		var parts = cmd.Trim().Split(' ');
		for(int j = 0; j < int.Parse(parts[1]); j++){
		
			knots[0].Move(parts[0][0]);
			// Now move each subsequent knots based
			for(int i = 1; i < numKnots; i++){
				knots[i].Move(knots[i - 1]);
			}
		}
	}

	Console.WriteLine($"The tail visited {knots[numKnots - 1].NumberOfPositions} positions");
}

public class Position {

	public Position(int x, int y)
	{
			X = x;
			Y = y;
			RecordHistory();
	}

	public int X {get; private set;}
	public int Y { get; private set;}
	
	public int NumberOfPositions => _history.Count();
	
	private Dictionary<(int,int), int> _history  = new Dictionary<(int,int), int>();
	
	// Move based on a command, which is always just for the head.
	public void Move(char direction){
		switch(direction){
			case 'R':
				X++;
				break;
			case 'L':
				X--;
				break;
			case 'U':
				Y++;
				break;
			case 'D':
				Y--;
				break;
		}
	}
	
	// Move based on the position of our upstream knot
	public void Move(Position head){
		int diff = 0;
		if (this.X == head.X)
		{
			diff = head.Y - this.Y;
			if (Math.Abs(diff) > 1)
			{
				diff = (diff < 0) ? diff + 1 : diff - 1;
				this.Y += (diff);
				RecordHistory();
			}
		}
		else if (this.Y == head.Y){
			diff = head.X - this.X;
			if (Math.Abs(diff) > 1)
			{
				diff = (diff < 0) ? diff + 1 : diff - 1;
				this.X += (diff);
				RecordHistory();
			}
		} else {
			// Must be diagonally touching or need to move that direction
			var diffX = head.X - this.X;
			var diffY = head.Y - this.Y;
			if (Math.Abs(diffX) > 1 || Math.Abs(diffY) > 1){
				// Need to move diagonally
				if (diffX > 1) diffX = 1;
				if (diffX < -1) diffX = -1;
				if (diffY > 1) diffY = 1;
				if (diffY < -1) diffY = -1;
				this.X += (diffX);
				this.Y += (diffY);
				RecordHistory();
			}
		}
	}
	
	private void RecordHistory(){
		var pos = (X,Y);
		if (_history.ContainsKey(pos)){
			_history[pos] = _history[pos] + 1;
		} else {
			_history.Add(pos,1);
		}
	}
}