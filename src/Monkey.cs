namespace Shakespeare {
    public class Monkey {
        private string alphabet = "abcdefghijklmnopqrstuvwxyz ";
        private int guessLength = 0;
        private Random monkeyBrain;
        private char[] guess;
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
            this.guess = new char[guessLength];
        }

        public string GuessWord() {
            /*63 random bits to be used in 5 bit chunks. The 64th non-random bit
              shouldnt matter since 4 bits get tossed to the bit bucket anyway.
            */
            ulong randomNumber = 0;
            int remainingBits = 0;

            for (int i = 0; i < this.guessLength; i++) {
                /* Use 5 bits of the random number to select a letter.
                   5 comes from 2^5 = 32, min number we need to represent 26 letters and a space.
                   There is a bias removal logic here by sliding the bits over if they're >27
                */
                if(remainingBits <= 5) {
                    randomNumber = (ulong)monkeyBrain.Next() << 31 | (uint)monkeyBrain.Next();
                    remainingBits = 63;
                }

                int index = (int)(randomNumber & 0x1F);
                randomNumber >>= 5;
                remainingBits -= 5;

                while(index >= 27) {
                    /*I slide one bit at a time instead of 5 to remove bias in attempt to make use of the full 63 bits*/
                    randomNumber >>= 1;
                    remainingBits--;
                    index = (int)(randomNumber & 0x1F);
                }

                this.guess[i] = alphabet[index % alphabet.Length];
            }

            Interlocked.Increment(ref Monkey.guessCount);
            return new string(this.guess);
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