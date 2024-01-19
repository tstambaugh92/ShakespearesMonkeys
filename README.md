This is a small project I'm using to learn basic multithreading in C#.

The program simulates the "infinite monkey theorm", minus the infinite part. Each "monkey" mashes a guess until it matches the answer phrase given at the start of the program.

The number of monkeys generated is based on CPU cores. You can pass a fixed number via command line argument.

Built with .NET 8 sdk. Maybe lower versions too, I don't know if I used anything from 8 or up. The binary in the release will require the 8 runtime.