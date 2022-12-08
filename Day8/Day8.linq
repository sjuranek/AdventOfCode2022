<Query Kind="Program">
  <IncludeUncapsulator>false</IncludeUncapsulator>
</Query>

void Main()
{
	var inputFile = Path.Combine(Path.GetDirectoryName(Util.CurrentQueryPath), "input.txt");
	var lines = File.ReadAllLines(inputFile);
	
	var numCols = lines[0].Trim().Length;
	var numRows = lines.Length;
	
	int visible = 0;
	int treesOutsideGrid = (numCols * 2) + (numRows * 2) - 4;
	int maxSceneScore = 0;
	
	for(int row = 1; row < numRows - 1; row++){
		for(int col = 1; col < numCols - 1; col++){
		
			var treeSize = lines[row][col];
			var maxInColumn = GetColumnMax(col, row, lines);	
			var maxInRow = GetRowMax(col, row, lines);
			
			bool isVisible = treeSize > maxInColumn.up || treeSize > maxInColumn.down || treeSize > maxInRow.left || treeSize > maxInRow.right;
			visible += (isVisible ? 1 : 0);

			var colScore = GetTreeScoreForColumn(col, row, lines);
			var rowScore = GetTreeScoreForRow(col, row, lines);
			var sceneScore = colScore.scoreUp * colScore.scoreDown * rowScore.scoreLeft * rowScore.scoreRight;
			if (sceneScore > maxSceneScore){
				maxSceneScore = sceneScore;
			}
		}
	}

	Console.WriteLine($"There are {visible} trees visible inside the grid. {treesOutsideGrid + visible} total visible. The max scene score is {maxSceneScore}");
}


(char up, char down) GetColumnMax(int col, int row, string[] lines){
	char maxUp = '0';
	char maxDown = '0';
	for(int i = 0; i < lines.Length; i++){
		if(i < row && lines[i][col] > maxUp){
			maxUp = lines[i][col];
		}
		if (i > row && lines[i][col] > maxDown)
		{
			maxDown = lines[i][col];
		}
	}
	return (maxUp, maxDown);
}

(char left, char right) GetRowMax(int col, int row, string[] lines)
{
	char maxLeft = '0';
	char maxRight = '0';
	for (int i = 0; i < lines[row].Length; i++)
	{
		if (i < col && lines[row][i] > maxLeft)
		{
			maxLeft = lines[row][i];
		}
		if (i > col && lines[row][i] > maxRight)
		{
			maxRight = lines[row][i];
		}
	}
	return (maxLeft, maxRight);
}

(int scoreUp, int scoreDown) GetTreeScoreForColumn(int col, int row, string[] lines){

	int scoreUp = 0;
	int scoreDown = 0;
	var treeSize = lines[row][col];
	for (int i = row - 1; i >= 0; i--){
		scoreUp++;
		if (lines[i][col] >= treeSize){
			break;
		}
	}
	for (int i = row + 1; i < lines.Length; i++){
		scoreDown++;
		if (lines[i][col] >= treeSize)
		{
			break;
		}
	}
	return (scoreUp, scoreDown);
}

(int scoreLeft, int scoreRight) GetTreeScoreForRow(int col, int row, string[] lines){
	int scoreLeft = 0;
	int scoreRight = 0;
	var treeSize = lines[row][col];
	for (int i = col - 1; i >= 0; i--){
		scoreLeft++;
		if (lines[row][i] >= treeSize){
			break;
		}
	}
	for (int i = col + 1; i < lines[row].Length; i++){
		scoreRight++;
		if (lines[row][i] >= treeSize){
			break;
		}
	}
	return (scoreLeft, scoreRight);
}