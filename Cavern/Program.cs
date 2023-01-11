using Cavern;
using System.Text;
using System.Web;
using static Direction;

Console.WriteLine("Welcome to Tiny Cavern, a pretty crap game.");

//real code
/*
Location L1 = new() { Description="the Entrance"};
Location L2 = new() { Description="a small room"};
Location L3 = new() { Description="the Exit"};

L1[South] = L2;
L1[West] = L3;
L2[East] = L3;
L3[North] = L1;
L3.MakeThisExit();

Player PlayerOne = new() { Name="Steve", CurrentLocation=L1};
Cavern.CommandDespatcher CD = new(PlayerOne);
*/

Game G = new Game();

while (true) {

    string? userInput = null;

    while (userInput == null) {
        Console.Write("> ");
        userInput = Console.ReadLine();
    }

    userInput = userInput.ToLower();
    Parser.CanonicalCommand command = Parser.Parse(userInput);
    string next = G.Despatcher.ActionCommand(command, userInput);    
    Console.WriteLine(next);
}

public enum Direction {
    North, East, South, West
}

public static class Extension {
    public static Direction ToDirection(this string d) {
        return d.Substring(0,1) switch
        {
            "n" => North,
            "e" => East,
            "s" => South,
            "w" => West,
            _   => throw new NotImplementedException()
        };
    }
}

class Player {
    public string Name { get; set; } = String.Empty;

    public Location CurrentLocation { get; set; }

    public override string ToString() => Name;

}

class Location {

    private static int last_id_used = 0;

    public int ID { get; private set; }
    public string? Description { get; set; }

    public Dictionary<Direction, Location> Exits { get; private set; } = new();

    public Location this[Direction D] {
        get { return Exits.ContainsKey(D) ? Exits[D] : null; }
        set { Exits[D] = value; }
    }

    public Location() {
        Location.last_id_used++;
        ID = last_id_used;
    }

    public void MakeThisExit() {
        ID = 0;
    }

    public string ListExits() {
        StringBuilder sb = new();
        foreach (Direction d in Exits.Keys)
            sb.Append($"{d} ");
        return sb.ToString();
    }

    public override string ToString() {
        StringBuilder s = new(Description);
        foreach (KeyValuePair<Direction,Location> KV in Exits)
                s.Append($"\nThere is an exit to the {KV.Key}");
        return s.ToString();
    }
}
