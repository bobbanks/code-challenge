using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;

namespace CodeChallenge {
    internal class Program {

        private const string TermBoundary = "(),";
        private const string TestData = "(id,created,employee(id,firstname,employeeType(id), lastname),location)";

        private static void Main(string[] args) {
            
            //ProcessViaCharacterEnumeration(testData);
            var rootTerm = new Term { Text = "root"};
            ProcessViaRecursion(TestData, 0, rootTerm);

            Console.WriteLine("Order as specified:");
            OutputTerms(rootTerm.Children);
            Console.WriteLine("");

            Console.WriteLine("Order ascending:");
            OutputOrderedTerms(rootTerm.Children);
            Console.ReadKey();
        }

        private class Term {
            public string Text { get; set; }
            public List<Term> Children { get; private set; }

            public Term() {
                Children = new List<Term>();
            }
        }

        private static int ProcessViaRecursion(string data, int position, Term term) {
            StringBuilder sb = new StringBuilder();
            Term currentTerm = term;
            
            while (position <= data.Length - 1) {
                var termChar = data.Substring(position, 1);

                if (TermBoundary.Contains(termChar)) { // if character is a term boundary
                    
                    if (sb.Length > 0) { // If we have a word in the buffer, add it as a term
                        currentTerm = new Term {Text = sb.ToString().Trim()};
                        term.Children.Add(currentTerm);
                        sb.Clear();
                    }
                    
                    switch (termChar) {
                        case "(": // process nested terms
                            position = ProcessViaRecursion(data, position + 1, currentTerm);
                            break;
                        case ")": // end recursion
                            return position;
                    }
                } else {
                   // Add letter to word buffer
                   sb.Append(termChar); 
                }
                position++;
            }
            return position;
        }

        private static void OutputTerms(List<Term> terms, int level = 0) {
            if (terms == null || !terms.Any()) {
                return;
            }

            foreach (var term in terms) {
                Console.WriteLine($"{new string('-',level)}{(level > 0 ? " " : "")}{term.Text}");
                OutputTerms(term.Children, level + 1);
            }
        }

        private static void OutputOrderedTerms(List<Term> terms, int level = 0) {
            if (terms == null || !terms.Any()) {
                return;
            }

            foreach (var term in terms.OrderBy(x => x.Text)) {
                Console.WriteLine($"{new string('-',level)}{(level > 0 ? " " : "")}{term.Text}");
                OutputOrderedTerms(term.Children, level + 1);
            }
        }
    }
}