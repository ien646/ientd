using System;

namespace IenTD
{
    public class Context
    {
        public static Context Instance { get; private set; } = new Context();

        private Context() { }

        public int DefaultWidth = 80;
        public int DefaultHeight = 24;

        public int BlinkSpeedMs = 150;

        public ConsoleColor DefaultFgColor = ConsoleColor.White;
        public ConsoleColor DefaultBgColor = ConsoleColor.DarkBlue;

        public ConsoleColor SelectionFgColor = ConsoleColor.Yellow;
        public ConsoleColor SelectionBgColor = ConsoleColor.Blue;

        public char BorderCharacterHorizontal = '-';
        public char BorderCharacterVertical = '|';
        public char BorderCharacterCorner = '+';

        public int DialogTitleVpos = 2;

        public ConsoleColor ProgressBarFgColor = ConsoleColor.Green;
        public ConsoleColor ProgressBarBgColor = ConsoleColor.DarkGray;

        public char ProgressBarFillCharacter = '█';
        public char ProgressBarUndefinedCharacter = '?';

        public void SetDefaults()
        {
            SetDefaultColors();
            SetDefaultSize();
            Console.CursorVisible = false;
        }

        public void SetDefaultColors()
        {
            Utils.SetColors(DefaultBgColor, DefaultFgColor);
        }

        public void SetDefaultSize()
        {
            Utils.SetSize(DefaultWidth, DefaultHeight);
        }
    }
}
