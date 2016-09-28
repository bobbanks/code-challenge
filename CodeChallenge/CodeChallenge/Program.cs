using System;

namespace CodeChallenge {
    internal class Program {
        private static void Main(string[] args) {

            ProcessViaCharacterEnumeration("(id,created,employee(id,firstname,employeeType(id), lastname),location)");
            Console.ReadKey();
        }

        private static void ProcessViaCharacterEnumeration(string data) {
            var dataArray = data.ToCharArray();
            var indentLevel = 0;

            foreach (var character in dataArray) {
                switch (character) {
                    case '(':
                        indentLevel++;
                        if (indentLevel > 1) {
                            Console.WriteLine();
                        }
                        Console.Write(new string('-',indentLevel - 1));
                        if (indentLevel > 1) {
                            Console.Write(' ');
                        }
                        break;
                    case ')':
                        indentLevel--;
                        break;
                    case ',':
                        Console.WriteLine();
                        Console.Write(new string('-',indentLevel - 1));
                        if (indentLevel > 1) {
                            Console.Write(' ');
                        }
                        break;
                    case ' ':
                        break;
                    default:
                        Console.Write(character);
                        break;
                }
            }
            Console.WriteLine();
        }
    }
}