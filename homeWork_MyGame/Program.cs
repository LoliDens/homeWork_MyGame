using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;

namespace homeWork_MyGame
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const ConsoleKey CommmandUp = ConsoleKey.W;
            const ConsoleKey CommmandDown = ConsoleKey.S;
            const ConsoleKey CommmandLeft = ConsoleKey.A;
            const ConsoleKey CommmandRight = ConsoleKey.D;

            const char SkinPlayerLookUp = '▲';
            const char SkinPlayerLookDown = '▼';
            const char SkinPlayerLookLeft = '◄';
            const char SkinPlayerLookRight = '►';

            int playerPositionX = 0;
            int playerPositionY = 0;
            int derectionTravelPlayer = 0;
            int derectionTravePlayer = 0;
            int screenCenter = 25;
            char[] barrier = new char[2] { '+', '#' };
            bool isGameOver = false;
            char[,] map;
            char skinPlayer = SkinPlayerLookUp;
            ConsoleKeyInfo key;
            Console.CursorVisible = false;

            map = SelectionLevel(ref playerPositionX, ref playerPositionY);
            

            while (isGameOver == false) 
            {                               
                if (Console.KeyAvailable) 
                {
                    key = Console.ReadKey(true);

                    switch (key.Key) 
                    {
                        case CommmandUp:
                            derectionTravelPlayer = -1; 
                            derectionTravePlayer = 0;
                            skinPlayer = SkinPlayerLookUp;
                            break;

                        case CommmandDown:
                            skinPlayer = SkinPlayerLookDown;
                            derectionTravelPlayer = 1;
                            derectionTravePlayer = 0;
                            break;

                        case CommmandRight:
                            skinPlayer = SkinPlayerLookRight;
                            derectionTravelPlayer = 0;
                            derectionTravePlayer = 1;
                            break;

                        case CommmandLeft:
                            skinPlayer = SkinPlayerLookLeft;
                            derectionTravelPlayer = 0;
                            derectionTravePlayer = -1;
                            break;                                 
                    }

                    if (barrier.Contains(map[playerPositionX + derectionTravelPlayer, playerPositionY + derectionTravePlayer]) == false) 
                    {
                        Console.SetCursorPosition(playerPositionY, playerPositionX);
                        Console.Write(' ');

                        playerPositionX += derectionTravelPlayer;
                        playerPositionY += derectionTravePlayer;                        

                        Console.SetCursorPosition(playerPositionY, playerPositionX);
                        Console.Write(skinPlayer);
                        derectionTravelPlayer = 0;
                        derectionTravePlayer = 0;
                    }
                }

                if (map[playerPositionX, playerPositionY] == '$')
                {
                    Console.SetCursorPosition(screenCenter, 0);
                    Console.WriteLine("|  Game Over  |");
                    isGameOver = true;
                    Console.ReadKey();
                }

            }
        }
       
        static char[,] SelectionLevel(ref int playerPositionX,ref int playerPositionY)
        {
            const string CommandTutorialLevel = "1";
            const string CommandLevel_1 = "2";

            string selectedLevel;
            char[,] map = null;

            Console.WriteLine("Выберите уровень:");
            Console.WriteLine($"{CommandTutorialLevel} - для того чтобы пройти туториал");
            Console.WriteLine($"{CommandLevel_1} - для того чтобы запустить первый уровень");
            selectedLevel = Console.ReadLine();
            Console.Clear();

            switch (selectedLevel)
            {
                case CommandTutorialLevel:
                    map = ReadMap("TutoralLevel", ref playerPositionX, ref playerPositionY);
                    DrowMap(map);
                    break;

               case CommandLevel_1:
                    map = ReadMap("Level_1",ref playerPositionX,ref playerPositionY);
                    DrowMap(map);
                    break;

                default:
                    map = ReadMap("TestRoom", ref playerPositionX, ref playerPositionY);
                    DrowMap(map);
                    break;
            }

            return map;
        }
             
        static char[,] ReadMap(string mapName,ref int playerPositionX, ref int playerPositionY ) 
        {
            string[] fileMap = File.ReadAllLines($"Levels/{mapName}.txt");
            char[,] map = new char[fileMap.Length, fileMap[0].Length];

            for (int i = 0; i < map.GetLength(0);i++) 
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    map[i, j] = fileMap[i][j];

                    if (map[i,j] == '◄') 
                    {
                        playerPositionX = i;
                        playerPositionY = j;
                    }
                }
            }

            return map;
        }

        static void DrowMap(char[,] map) 
        {
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    Console.Write(map[i,j]);
                }

                Console.WriteLine();
            }
        }    
    }
}
