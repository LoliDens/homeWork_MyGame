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
            int playerX = 0;
            int playerY = 0;
            int playerDX = 0;
            int playerDY = 0;
            int screenCenter = 25;
            char[] barrier = new char[2] { '+', '#' };
            bool isGameOver = false;
            char[,] map;
            char skinPlayer = '◄';
            ConsoleKeyInfo key;
            Console.CursorVisible = false;

            map = SelectionLevel(ref playerX, ref playerY);
            

            while (isGameOver == false) 
            {                               
                if (Console.KeyAvailable) 
                {
                    key = Console.ReadKey(true);

                    switch (key.Key) 
                    {
                        case ConsoleKey.W:
                            playerDX = -1; 
                            playerDY = 0;
                            skinPlayer = '▲';
                            break;

                        case ConsoleKey.S:
                            skinPlayer = '▼';
                            playerDX = 1;
                            playerDY = 0;
                            break;

                        case ConsoleKey.D:
                            skinPlayer = '►';
                            playerDX = 0;
                            playerDY = 1;
                            break;

                        case ConsoleKey.A:
                            skinPlayer = '◄';
                            playerDX = 0;
                            playerDY = -1;
                            break;                                 
                    }

                    if (barrier.Contains(map[playerX + playerDX, playerY + playerDY]) == false) 
                    {
                        Console.SetCursorPosition(playerY, playerX);
                        Console.Write(' ');

                        playerX += playerDX;
                        playerY += playerDY;

                        if (map[playerX, playerY] == '$') 
                        {
                            Console.SetCursorPosition(screenCenter, 0) ;
                            Console.WriteLine("|  Game Over  |");
                            isGameOver = true;
                            Console.ReadKey();
                        }

                        Console.SetCursorPosition(playerY, playerX);
                        Console.Write(skinPlayer);
                        playerDX = 0;
                        playerDY = 0;
                    }
                }

            }
        }

       
        static char[,] SelectionLevel(ref int playerX,ref int playerY)
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
                    map = ReadMap("TutoralLevel", ref playerX, ref playerY);
                    DrowMap(map);
                    break;

               case CommandLevel_1:
                    map = ReadMap("Level_1",ref playerX,ref playerY);
                    DrowMap(map);
                    break;

                default:
                    map = ReadMap("TestRoom", ref playerX, ref playerY);
                    DrowMap(map);
                    break;
            }

            return map;
        }

     

        static char[,] ReadMap(string mapName,ref int playerX, ref int playerY ) 
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
                        playerX = i;
                        playerY = j;
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
