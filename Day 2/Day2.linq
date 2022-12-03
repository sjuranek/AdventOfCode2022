<Query Kind="Program">
  <IncludeUncapsulator>false</IncludeUncapsulator>
</Query>

void Main()
{
	var inputFile = Path.Combine(Path.GetDirectoryName(Util.CurrentQueryPath), "Day2.txt");
	var lines = File.ReadAllLines(inputFile);
	//var lines = new string[] {"A Y","B X","C Z"};

	int score = 0;
	foreach(var line in lines){
		var oppPlay = TranslateGesure(line[0]);
		var myPlay = TranslateGesure(line[2]);
		//Console.WriteLine(oppPlay + " " + myPlay);
		score += GetScoreForRound(oppPlay, myPlay);		
	}
	
	Console.WriteLine("Total Score " + score);

	// Part 2
	score = 0;
	foreach (var line in lines)
	{
		var oppPlay = TranslateGesure(line[0]);
		var desiredOutcome = TranslateOutcome(line[2]);
		var myPlay = GetMyPlay(oppPlay, desiredOutcome);

		score += GetScoreForRound(oppPlay, myPlay);
	}
	
	Console.WriteLine("Total Score " + score);
}

Gesture TranslateGesure(char input){
	switch(input){
		case 'A':
		case 'X':
			return Gesture.Rock;
		case 'B':
		case 'Y':
			return Gesture.Paper;
		default:
			return Gesture.Scissors;
	}
}

Outcome TranslateOutcome(char input)
{
	switch (input)
	{
		case 'X':
			return Outcome.Lose;
		case 'Y':
			return Outcome.Draw;
		default:
			return Outcome.Win;
	}
}

int GetScoreForRound(Gesture oppPlay, Gesture myPlay){
	var myScore = (int)myPlay;
	
	if (oppPlay == myPlay){
		myScore += 3;
	} else {
		if (oppPlay == Gesture.Rock){
			myScore += (myPlay == Gesture.Paper) ? 6 : 0;
		} else if (oppPlay == Gesture.Paper){
			myScore += (myPlay == Gesture.Scissors) ? 6 : 0;
		} else if (oppPlay == Gesture.Scissors){
			myScore += (myPlay == Gesture.Rock) ? 6 : 0;
		}
	}
	
	return myScore;
}

Gesture GetMyPlay(Gesture oppPlay, Outcome desiredOutcome){
	if (desiredOutcome == Outcome.Draw){
		return oppPlay;
	}
	
	if (oppPlay == Gesture.Rock){
		return (desiredOutcome == Outcome.Win) ? Gesture.Paper : Gesture.Scissors;
	} else if (oppPlay == Gesture.Paper){
		return (desiredOutcome == Outcome.Win) ? Gesture.Scissors : Gesture.Rock;
	} else {
		return (desiredOutcome == Outcome.Win) ? Gesture.Rock : Gesture.Paper;
	}

}

enum Gesture {
	Rock = 1,
	Paper = 2,
	Scissors = 3
}

enum Outcome {
	Win,
	Lose,
	Draw
}

// You can define other methods, fields, classes and namespaces here