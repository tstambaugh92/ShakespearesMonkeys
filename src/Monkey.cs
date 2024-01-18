namespace Shakespeare {
    public class Monkey {
        private string alphabet = "abcdefghijklmnopqrstuvwxyz ";
        private int guessLength = 0;
        private Random monkeyBrain;
        private static long guessCount = 0;
        public static long GuessCount {
            get {return Monkey.guessCount;}
        }
        private static bool stop = false;
        public static bool Stop {
            set {Monkey.stop = value;}
        }

        /*This is a test function to see if the monkey is guessing randomly*/
        public void SayOok() {
            for(int i = 0; i < this.guessLength; i++) {
                System.Console.Write(GuessWord() + "\n");
            }
        }

        public Monkey(int guessLength, int seed) {
            this.guessLength = guessLength;
            this.monkeyBrain = new Random(seed);
        }

        public string GuessWord() {
            char[] word = new char[this.guessLength];
            for (int i = 0; i < this.guessLength; i++) {
                word[i] = alphabet[monkeyBrain.Next(0, alphabet.Length)];
            }
            Interlocked.Increment(ref Monkey.guessCount);
            /*Note: Increment is a slight performance boost over guessCount++
              It is an atomic operation, whatever that means lol 
            */
            return new string(word);
        }

        public void GetToWork(string answer) {
                string guess;
                do {
                    guess = GuessWord();
                } while (guess != answer && Monkey.stop == false);
                return;
        }
    }
}