using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IenTD
{
    public static class Utils
    {
        public class SavedColors : IDisposable
        {
            private ConsoleColor _bg, _fg;

            public SavedColors(ConsoleColor bg, ConsoleColor fg)
            {
                _bg = bg;
                _fg = fg;
            }

            public void Dispose()
            {
                Console.BackgroundColor = _bg;
                Console.ForegroundColor = _fg;
            }
        }

        public class DrawMultipleEntry
        {
            public string Text { get; private set; }           

            public ConsoleColor BackgroundColor { get; set; }
            public ConsoleColor ForegroundColor { get; set; }

            public DrawMultipleEntry(string text)
            {
                Text = text;
                BackgroundColor = Console.BackgroundColor;
                ForegroundColor = Console.ForegroundColor;
            }

            public DrawMultipleEntry(string text, ConsoleColor backgroundColor, ConsoleColor foregroundColor)
            {
                Text = text;
                BackgroundColor = backgroundColor;
                ForegroundColor = foregroundColor;
            }
        }

        public static void SetSize(int w, int h)
        {
            Console.SetWindowSize(w, h);
            Console.SetBufferSize(w, h);
            Console.SetWindowSize(w, h);
        }

        public static void SetColors(ConsoleColor back, ConsoleColor fore)
        {
            Console.BackgroundColor = back;
            Console.ForegroundColor = fore;
        }

        public static SavedColors SetTemporaryColors(ConsoleColor back, ConsoleColor fore)
        {
            SavedColors result = new SavedColors(Console.BackgroundColor, Console.ForegroundColor);
            SetColors(back, fore);
            return result;
        }

        public static ValueTuple<ConsoleColor, ConsoleColor> SaveCurrentColors()
        {
            return (Console.BackgroundColor, Console.ForegroundColor);
        }

        public static void RestoreColors(ValueTuple<ConsoleColor, ConsoleColor> colors)
        {
            Console.BackgroundColor = colors.Item1;
            Console.ForegroundColor = colors.Item2;
        }

        public static void DrawCentered(string text, int vpos)
        {
            string[] lines = text.Split('\n');
            for (int i = 0; i < lines.Length; ++i)
            {
                string line = lines[i];
                int linePos = Math.Max((Console.BufferWidth / 2) - (line.Length / 2), 0);
                Console.SetCursorPosition(linePos, vpos + i);
                Console.Write(line[..Math.Min(Console.BufferWidth, line.Length)]);
            }
        }

        public static void DrawCenteredMultiple(IEnumerable<DrawMultipleEntry> entries, int vpos, int sepWidth = 1)
        {
            int maxLen = entries.Max(t => t.Text.Length);
            int totalWidth = ((entries.Count() - 1) * sepWidth) + (maxLen * entries.Count());
            int startPos = (Console.BufferWidth / 2) - (totalWidth / 2);
            Console.SetCursorPosition(startPos, vpos);
            foreach(var entry in entries)
            {
                using (var _ = Utils.SetTemporaryColors(entry.BackgroundColor, entry.ForegroundColor))
                {
                    Console.Write(entry.Text);
                }                
                Console.Write(new string(' ', sepWidth));
            }
        }

        public static void DrawBorders(char hchar, char vchar, char corner)
        {
            // Horizontal borders
            Console.SetCursorPosition(0, 0);
            string borderHorizontal = new string(hchar, Console.BufferWidth);
            Console.Write(borderHorizontal);
            Console.SetCursorPosition(0, Console.BufferHeight - 1);
            Console.Write(borderHorizontal);

            // Vertical borders
            for (int i = 1; i < Console.BufferHeight - 1; ++i)
            {
                Console.SetCursorPosition(0, i);
                Console.Write(vchar);
                Console.SetCursorPosition(Console.BufferWidth - 1, i);
                Console.Write(vchar);
            }

            // Corners
            Console.SetCursorPosition(0, 0);
            Console.Write(corner);
            Console.SetCursorPosition(Console.BufferWidth - 1, 0);
            Console.Write(corner);
            Console.SetCursorPosition(0, Console.BufferHeight - 1);
            Console.Write(corner);
            Console.SetCursorPosition(Console.BufferWidth - 1, Console.BufferHeight - 1);
            Console.Write(corner);
        }

        public static void ClearLine(int vpos, int padding)
        {
            Console.SetCursorPosition(padding, vpos);
            Console.Write(new string(' ', Console.BufferWidth - (padding * 2)));
        }
    }
}
