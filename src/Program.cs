using Shakespeare;
using System.Diagnostics;
using System.Threading.Tasks;

Stopwatch stopWatch = new Stopwatch();
System.Timers.Timer timer = new System.Timers.Timer(10000); //10 seconds
List<Task> tasks = new List<Task>();

timer.Elapsed += (sender, e) => {
    System.Console.WriteLine("The monkeys are on guess " + Monkey.GuessCount.ToString("N0") + ".");
};

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
else {
    answer = answer.ToLower();
}

int phraseLength = answer.Length;

int seed = Environment.TickCount;
int coreCount = 0;
if (args.Length > 0) {
    try {
        //convert args[0] to an int
        coreCount = Convert.ToInt32(args[0]);
    } catch {
        System.Console.WriteLine("Invalid core argument. Using default core count.");
        coreCount = Environment.ProcessorCount;
    }
} else {
    coreCount = Environment.ProcessorCount;
}
System.Console.WriteLine("Using " + coreCount + " monkeys to guess the phrase: " + answer + ".");

/*the stopwatch keeps track of time, while the timer executes code every 10 seconds to give live updates*/
stopWatch.Start();
timer.Start();

for (int i = 0; i < coreCount; i++) {
    Monkey monkey = new Monkey(phraseLength, seed+i);
    tasks.Add(Task.Run(() => monkey.GetToWork(answer)));
    /*Alt approach that worked:
      Having GetToWork return a running Task and adding it to the list of tasks
    */
}
Task.WaitAny(tasks.ToArray()); /*this checks for any one of the monkeys to guess the phrase*/
Monkey.Stop = true; /*this signals the other monkeys to end their tasks*/
Task.WaitAll(tasks.ToArray()); /*wait for the other monkeys to end their tasks*/

timer.Stop();
stopWatch.Stop();

System.Console.WriteLine("It took " + Monkey.GuessCount.ToString("N0") + " tries to guess your phrase.");
System.Console.WriteLine("It took " + stopWatch.Elapsed.TotalSeconds + " seconds to guess your phrase.");