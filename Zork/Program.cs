using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zork
{
    enum Commands
    {
        QUIT,
        LOOK,
        NORTH,
        SOUTH,
        EAST,
        WEST,
        UNKNOWN
    }

    internal class Program
    {
        private static readonly Room[,] Rooms =
        {
            {new Room("Rocky Trail"), new Room("South of House"), new Room("Canyon View")},
            {new Room("Forest"), new Room("West of House"), new Room("Behind House")},
            {new Room("Dense Woods"), new Room("North of House"), new Room("Clearing")}

        };
        static Dictionary<string, Room> RoomMap;


        public static Room CurrentRoom
        {
            get => Rooms[Location.Row, Location.Column];
        }
        private static (int Row, int Column) Location = (1, 1);
        private static void InitializeRoomDescriptions()
        {
            RoomMap = new Dictionary<string, Room>();
            foreach (Room room in Rooms)
            {
                RoomMap.Add(room.Name, room);
            }

            Rooms[0, 0].Description = "You are on a rock-strewn trail.";
            Rooms[0, 1].Description = "You are facing the south side of a white house. There is no door here, and all the windows are barred."; 
            Rooms[0, 2].Description = "You are at the top of the Great Canyon on its south wall.";
            Rooms[1, 0].Description = "This is a forest, with trees in all directions around you.";
            Rooms[1, 1].Description = "This is an open field west of a white house, with a boarded front door."; 
            Rooms[1, 2].Description = "You are behind the white house. In one corner of the house there is a small window which is slightly ajar."; 
            Rooms[2, 0].Description = "This is a dimly lit forest, with large trees all around. To the east, there appears to be sunlight."; 
            Rooms[2, 1].Description = "You are facing the north side of a white house. There is no door here, and all the windows are barred."; 
            Rooms[2, 2].Description = "You are in a clearing, with a forest surrounding you on the west and south.";
        }
        static void Main(string[] args)
        {
            InitializeRoomDescriptions();

            Console.WriteLine("Welcome to Zork");


            Room previousRoom = null;
            Commands command = Commands.UNKNOWN;
            while (command != Commands.QUIT)
            {
                Console.Write("> ");
                command = ToCommand(Console.ReadLine().Trim());

                string outputString = "";
                bool moved = false;
                switch (command)
                {
                    case Commands.LOOK:
                        outputString = CurrentRoom.Description;
                        break;
                    case Commands.NORTH:
                    case Commands.SOUTH:
                    case Commands.EAST:
                    case Commands.WEST:
                        moved = Move(command);
                        outputString = moved ? CurrentRoom.ToString() : "You cannot move in that direction.";
                        break;
                    case Commands.QUIT:
                        outputString = "Thank you for playing!";
                        break;
                    default:
                        outputString = "Unknown command.";
                        break;
                }

                Console.WriteLine(outputString);

                if(moved && previousRoom != CurrentRoom)
                {
                    previousRoom = CurrentRoom;
                    Console.WriteLine(CurrentRoom.Description);
                }
            }
        }

        public static bool Move(Commands direction)
        {
            Assert.IsTrue(IsDirection(direction), "Invalid direction command");
            bool isValidMove = true;
            switch (direction)
            {
                case Commands.NORTH when Location.Row < Rooms.GetLength(1) - 1:
                    Location.Row++;
                    break;
                case Commands.SOUTH when Location.Row > 0:
                    Location.Row--;
                    break;
                case Commands.EAST when Location.Column < Rooms.GetLength(0) - 1:
                    Location.Column++;
                    break;
                case Commands.WEST when Location.Column > 0:
                    Location.Column--;
                    break;
                default:
                    isValidMove = false;
                    break;
            }
            return isValidMove;
        }

        private static List<Commands> Directions = new List<Commands> { Commands.NORTH, Commands.SOUTH, Commands.EAST, Commands.WEST };
        private static bool IsDirection(Commands command)
        {
            return Directions.Contains(command);
        }
        public static Commands ToCommand(string commandString) => Enum.TryParse(commandString, true, out Commands command) ? command : Commands.UNKNOWN;
    }
}
