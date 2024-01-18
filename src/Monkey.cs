namespace Shakespeare {
    public class Monkey {
        private string alphabet = "abcdefghijklmnopqrstuvwxyz ";
        private int guessLength = 0;
        private Random monkeyBrain;
        private static long guessCount = 0;
        public static long GuessCount {
            get {return Monkey.guessCount;}
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
            string word = "";
            for (int i = 0; i < this.guessLength; i++) {
                word += alphabet[monkeyBrain.Next(0, alphabet.Length)];
            }
            Monkey.guessCount++;
            return word;
        }

        public void GetToWork(string answer) {
            string guess;
            do {
                guess = GuessWord();
            } while (guess != answer);
        }
    }
}