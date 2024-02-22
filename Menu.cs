using System;
using System.Collections.Generic;

namespace GuitarRentalService
{
    public class Menu
    {
        public string Info { get; set; }
        public List<string> Options { get; set; }
        public int SelectedIndex { get; set; }
        public List<string> SelectionInfo { get; set; }

        public Menu(string info, List<string> options)
        {
            Info = info;
            Options = options;
            SelectedIndex = 0;
            SelectionInfo = new();
        }

        public Menu(string info, List<string> options, List<string> selectionInfo)
        {
            Info = info;
            Options = options;
            SelectedIndex = 0;
            SelectionInfo = selectionInfo;
        }

        public void DisplayOptions()
        {
            Methods.DrawLogo();
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(Info);
            Console.ResetColor();

            if (SelectionInfo.Count > 0)
            {
                foreach (string item in SelectionInfo)
                {
                    Console.WriteLine(item);
                }
                Console.WriteLine();
            }

            for (int i = 0; i < Options.Count; i++)
            {
                string currentOption = Options[i];
                string marker;
                if (i == SelectedIndex)
                {
                    marker = ">";
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.White;
                }
                else
                {
                    marker = " ";
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                }

                Console.WriteLine(marker + currentOption);
            }
            Console.ResetColor();
        }

        public int DrawMenu()
        {
            ConsoleKey keyPressed;
            do
            {
                DisplayOptions();

                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                keyPressed = keyInfo.Key;

                if (keyPressed == ConsoleKey.UpArrow)
                {
                    SelectedIndex--;
                    if (SelectedIndex == -1)
                    {
                        SelectedIndex = Options.Count - 1;
                    }
                }
                else if (keyPressed == ConsoleKey.DownArrow)
                {
                    SelectedIndex++;
                    if (SelectedIndex == Options.Count)
                    {
                        SelectedIndex = 0;
                    }
                }
            } while (keyPressed != ConsoleKey.Enter);

            return SelectedIndex;
        }
    }
}