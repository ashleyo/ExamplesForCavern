using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Command = System.Func<string, string>;
using static Direction;

//Responsible for actioning commands

namespace Cavern {

    internal class CommandDespatcher {
        private static Dictionary<Parser.CanonicalCommand,Command> CommandTable = new();

        static internal void AddCommand(Parser.CanonicalCommand canonicalName, Command action) {
            CommandTable[canonicalName] = action;
        }        
        
        internal CommandDespatcher(Player P) {
            Commands commands = new Commands(P);
            CommandTable[Parser.CanonicalCommand.Unknown] = (_) => "I don't know how to do that!";
            AddCommand(Parser.CanonicalCommand.Move, commands.Move);
            AddCommand(Parser.CanonicalCommand.Look, commands.Look);
            AddCommand(Parser.CanonicalCommand.Help, commands.Help);
        }

        internal string  ActionCommand(Player P, Parser.CanonicalCommand C, string input) {
            return CommandTable[C](input);
        }

        internal class Commands {        
            private Player P;
            internal Commands(Player p) {
                P = p;
            }

            internal string Move(string input) {
                Direction D = input switch
                {
                    "n" => North,
                    "e" => East,
                    "s" => South,
                    "w" => West,
                    _   => throw new NotImplementedException()
                };
                if (P.CurrentLocation[D] != null) {
                    P.CurrentLocation = P.CurrentLocation[D];
                    if (P.CurrentLocation.ID == 0) {
                        Console.WriteLine("You Win! You found the Exit!");
                        Environment.Exit(0);
                    }
                    return $"You move {input}. You are in {P.CurrentLocation.Description}";
                }
                else {
                    return "There is no exit in that direction.";
                }
            }
            internal string Look(string _) => $"You look around. You are in {P.CurrentLocation.Description}\nThere are exits {P.CurrentLocation.ListExits()}";
            internal string Help(string _) => "Try looking around";
            internal string Quit(string _) => "The roof collapses. You die. The end";
        }
    }
}


