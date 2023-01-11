using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Direction;

namespace Cavern {
    internal class Game {
        internal Player P { get; private set; }
        internal CommandDespatcher Despatcher { get; private set; }
        public Game() {

            List<Location> locations= new List<Location>() {

                new() { Description="the Entrance"},
                new() { Description="a small room"},
                new() { Description="an even smaller room"},
                new() { Description="a lobby"},
                new() { Description="a waiting room"},
                new() { Description="a nondescript room"},
                new() { Description="a much larger room, sunlight floods in from skylights far above"},
                new() { Description="a dark room"},
                new() { Description="a room that smells of something disgusting"},
                new() { Description="the Entrance"},

            };

            locations[0][South] = locations[1];
            locations[0][West] = locations[2];

            locations[1][East] = locations[2];

            locations[2][East] = locations[2];
            locations[2][South] = locations[1];
            locations[2][North] = locations[3];

            locations[3][East] = locations[3];
            locations[3][South] = locations[2];
            locations[3][North] = locations[4];

            locations[4][East] = locations[4];
            locations[4][South] = locations[3];
            locations[4][North] = locations[5];

            locations[5][East] = locations[5];
            locations[5][South] = locations[4];
            locations[5][North] = locations[6];

            locations[6][East] = locations[6];
            locations[6][South] = locations[5];
            locations[6][North] = locations[7];

            locations[7][East] = locations[7];
            locations[7][South] = locations[6];
            locations[7][North] = locations[8];

            locations[8][East] = locations[8];
            locations[8][South] = locations[7];
            locations[8][North] = locations[9];

            locations[9].MakeThisExit();

            P = new() { Name="Steve", CurrentLocation=locations[0]};
            Despatcher = new(this);
        }
    }
}
