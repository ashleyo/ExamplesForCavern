using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static Cavern.Parser.CanonicalCommand;

[assembly: InternalsVisibleTo("TestProject1")]

namespace Cavern {
    static internal class Parser {

        /* follow more-or-less the scheme used previously
         * a directory (could be a bunch of switch expressions?) to map 
         * user input onto 'canonical' command names
         * and another directory to map canonical command names onto functions to execute
         */

        /* Parsers job is _only_ to accept input and decide which CanonicalCommand should be actioned */

        internal enum CanonicalCommand { Unknown, Move, Look, Quit, Help }
        
        private static Dictionary<string,CanonicalCommand> CanonicalCommands = new();

        static Parser() {
            string[] dirs = Enum.GetNames(typeof(Direction));
            foreach (string dir in dirs) {
                string ldir = dir.ToLower();
                AddParsing(new string[] { ldir, ldir.Substring(0, 1) }, Move);
            AddParsing(new string[] { "quit", "q" }, Quit);
            AddParsing("look", Look);
            AddParsing("unknown", Unknown);
          }
        }

        static internal void AddParsing(string input, CanonicalCommand canonicalName) {
            CanonicalCommands[input] = canonicalName;
        }

        static internal void AddParsing(string[] inputs, CanonicalCommand canonicalName) {
            foreach (string input in inputs) AddParsing(input, canonicalName);
        }

        static internal CanonicalCommand Parse(string input) {
            string key = input.ToLower().Split(' ')[0];
            if (CanonicalCommands.ContainsKey(key))
                return CanonicalCommands[key];
            else
                return Unknown;
        }
    }
}

