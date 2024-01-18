using Shakespeare;
using System.Diagnostics;

Stopwatch timer = new Stopwatch();

/*prompt user for a string*/
System.Console.WriteLine("Enter a name or phrase to guess:");
string answer = System.Console.ReadLine() ?? string.Empty;

if (answer == null) {
    System.Console.WriteLine("You didn't enter anything!");
    return;
} else if (answer.Length == 0) {
    System.Console.WriteLine("You didn't enter anything!");
    return;
}

System.Console.WriteLine("You entered: " + answer);
int phraseLength = answer.Length;

int seed = Environment.TickCount;
int coreCount = Environment.ProcessorCount;
//System.Console.WriteLine("Using " + coreCount + " cores to guess your phrase.");

Monkey myMonkey = new Monkey(phraseLength, seed);
timer.Start();
myMonkey.GetToWork(answer);
timer.Stop();

System.Console.WriteLine("It took " + Monkey.GuessCount + " tries to guess your phrase.");
System.Console.WriteLine("It took " + timer.Elapsed.TotalSeconds + " seconds to guess your phrase.");