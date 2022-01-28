# wordle-string-helper
I would suggest using the Visual Studio Community 2019 IDE to open and run the project using the .sln file or the .csproj file.

For each iteration, the program will prompt the user to enter:
1) Any correct letters with their correct locations
2) Any correct letters with their incorrect locations
3) Any incorrect letters

The correct letters are inputted one at a time with a space between the letter & its location. For example, "a 1".
The incorrect letters are inputted all at once separated by spaces. For example, "a b c".

The program uses a recursive function GetInput() which runs for all 3 types of input and will call itself if any piece of input isn't valid. GetInput() will also call functions CorrectLetterCorrectPlace(), CorrectLetterWrongPlace(), and WrongLetter() which all eliminate words from the list based on the inputs. After any elimination, a message is printed showing the new number of possible words.

After each iteration of these functions, the new list will be printed out to the user to choose a word from and then enter the inputs corresponding to which letters are right and wrong. Once there is either one word or no words left in the list, a message is printed to the user, and the user is asked if they want to run the program again.
