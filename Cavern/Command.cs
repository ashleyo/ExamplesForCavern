using Command = System.Func<Player, string, string>;
using static Direction;
using static Cavern.Parser.CanonicalCommand;
using CanonicalCommand = Cavern.Parser.CanonicalCommand;

//Responsible for actioning commands

namespace Cavern {

    internal class CommandDespatcher {
        private static Dictionary<CanonicalCommand,Command> CommandTable = new();

        static internal void AddCommand(CanonicalCommand canonicalName, Command action) {
            CommandTable[canonicalName] = action;
        }        
        
        internal CommandDespatcher(Player P) {
            CommandTable[Unknown] = (__,_) => "I don't know how to do that!";
            AddCommand(Move, Commands.Move);
            AddCommand(Look, Commands.Look);
            AddCommand(Help, Commands.Help);
        }

        internal string  ActionCommand(Player P, CanonicalCommand C, string input) {
            return CommandTable[C](P, input);
        }

        internal static class Commands {        

            internal static string Move(Player P, string input) {
                Direction D = input.ToDirection();             
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
            internal static string Look(Player P, string _) => $"You look around. You are in {P.CurrentLocation.Description}\nThere are exits {P.CurrentLocation.ListExits()}";
            internal static string Help(Player _, string __) => "Try looking around";
            internal static string Quit(Player _, string __) => "The roof collapses. You die. The end";
        }
    }
}


