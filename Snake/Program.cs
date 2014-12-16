using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Snake
{
    class Program
    {
        public static coord viewport;

        static void Main(string[] args)
        {
            Console.Title = "Snake.NET";

            scaleToWindowSize();
            intro();

            //One does not simply leave the game loop
            while (true)
            {
                gameLoop();
            }
        }

        static void scaleToWindowSize()
        {
            //Set viewport to window width/height
            viewport.x = Console.WindowWidth;
            viewport.y = Console.WindowHeight;
        }

        static void intro()
        {
            drawBox();
            string text = "Snake.NET - benbristow.co.uk 2014";
            Console.SetCursorPosition((viewport.x / 2) - (text.Length / 2), viewport.y / 2);
            Console.Write(text);
            Console.ReadLine();
        }

        static void gameLoop()
        {
            //Reset title
            Console.Title = "Snake.NET | Score: 0";

            //Clear Display
            Console.Clear();
            scaleToWindowSize();

            //Draw main outer box
            drawBox();

            //Score
            int score = 0;

            //Initialize objects          
            Snake snk = new Snake(viewport);
            List<Food> fd = new List<Food>();
            fd.Add(new Food(viewport));

            //Main Loop
            while (snk.isAlive)
            {
                //Move snake and Delete left over pieces of snake
                deleteOrphanedPiecesOfSnake(snk.Move());

                //Draw snake
                drawSnake(snk);

                //Check if collides with food 
                if (snk.checkCollidesWithFood(fd))
                {
                    score++;
                    Console.Title = "Snake.NET | Score: " + score.ToString();
                }

                //Draw food     
                drawFood(fd);

                //Take it easy
                Thread.Sleep(60);

                //Take any user input if any
                Console.SetCursorPosition(0, 0);
                if (Console.KeyAvailable)
                {
                    var c = Console.ReadKey();

                    switch (c.Key)
                    {
                        case ConsoleKey.UpArrow:
                        case ConsoleKey.W:
                            if (snk.direction != 3)
                            {
                                snk.direction = 1;
                            }
                            break;
                        case ConsoleKey.DownArrow:
                        case ConsoleKey.S:
                            if (snk.direction != 1)
                            {
                                snk.direction = 3;
                            }
                            break;
                        case ConsoleKey.LeftArrow:
                        case ConsoleKey.A:
                            if (snk.direction != 2)
                            {
                                snk.direction = 4;
                            }
                            break;
                        case ConsoleKey.RightArrow:
                        case ConsoleKey.D:
                            if (snk.direction != 4)
                            {
                                snk.direction = 2;
                            }
                            break;
                        case ConsoleKey.Q:
                        case ConsoleKey.Escape:
                            Environment.Exit(0);
                            break;
                    }
                }
            }

            Console.Clear();
            showScore(score);
        }

        static void drawBox()
        {
            for (int y = 0; y <= viewport.y - 1; y++)
            {
                for (int x = 0; x <= viewport.x - 1; x++)
                {
                    //Set cursor to pointer.
                    Console.SetCursorPosition(x, y);

                    //Top and bottom, else left/right sides.
                    if (y == 0 || y == viewport.y - 1)
                    {
                        Console.Write("X");
                    }
                    else if (x == 0 || x == viewport.x - 1)
                    {
                        Console.Write("X");
                    }
                }

                //Reset X to 0
                Console.SetCursorPosition(0, y);
            }

            //Jump screen back to top
            Console.SetCursorPosition(0, 0);
        }


        static void drawSnake(Snake s)
        {
            foreach (coord c in s.the_queue)
            {
                Console.SetCursorPosition(c.x, c.y);
                Console.Write("o");

            }
        }

        static void deleteOrphanedPiecesOfSnake(Queue<coord> deleted)
        {
            foreach (coord c in deleted)
            {
                Console.SetCursorPosition(c.x, c.y);
                Console.Write(" ");
            }
        }

        static void drawFood(List<Food> fd)
        {
            foreach (Food f in fd)
            {
                Console.SetCursorPosition(f.position.x, f.position.y);
                Console.Write("F");
            }
        }

        static void showScore(int score)
        {
            Console.Clear();
            drawBox();
            string text = string.Format("Game over! You scored: {0}", score.ToString());
            Console.SetCursorPosition((viewport.x / 2) - (text.Length / 2), viewport.y / 2);
            Console.Write(text);
            Thread.Sleep(2000);
        }
    }
}

