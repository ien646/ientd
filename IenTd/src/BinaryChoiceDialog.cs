using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IenTD
{
    public class BinaryChoiceDialog
    {
        public string Message { get; private set; }
        public string Text0 { get; private set; }
        public string Text1 { get; private set; }

        public BinaryChoiceDialog(string text0, string text1, string message)
        {
            Text0 = text0;
            Text1 = text1;
            Message = message;
        }

        public int Show()
        {
            Console.Clear();

            Utils.DrawBorders(
                Context.Instance.BorderCharacterHorizontal,
                Context.Instance.BorderCharacterVertical,
                Context.Instance.BorderCharacterCorner
            );

            int vpos = (Console.BufferHeight / 2) - 1;
            foreach (string line in Message.Split('\n'))
            {
                Utils.DrawCentered(line, vpos++);
            }

            Action drawButtonsNormal = () =>
            {
                Utils.DrawMultipleEntry entry0 = new Utils.DrawMultipleEntry(Text0);
                Utils.DrawMultipleEntry entry1 = new Utils.DrawMultipleEntry(Text1);
                Utils.DrawCenteredMultiple(new Utils.DrawMultipleEntry[] { entry0, entry1 }, Console.BufferHeight - 3);
            };

            Action<int> drawButtonsBlink = (int selected) =>
            {
                Utils.DrawMultipleEntry entry0 = new Utils.DrawMultipleEntry(Text0);
                Utils.DrawMultipleEntry entry1 = new Utils.DrawMultipleEntry(Text1);
                if (selected == 0)
                {
                    entry0.BackgroundColor = Context.Instance.SelectionBgColor;
                    entry0.ForegroundColor = Context.Instance.SelectionFgColor;
                }
                else
                {
                    entry1.BackgroundColor = Context.Instance.SelectionBgColor;
                    entry1.ForegroundColor = Context.Instance.SelectionFgColor;
                }
                Utils.DrawCenteredMultiple(new Utils.DrawMultipleEntry[] { entry0, entry1 }, Console.BufferHeight - 3);
            };

            int selected = 0;
            bool selectionFinished = false;

            int loopIndex = 0;

            while (!selectionFinished)
            {
                while(!Console.KeyAvailable)
                {
                    if (loopIndex++ % 2 == 0)
                    {
                        drawButtonsNormal();
                    }
                    else
                    {
                        drawButtonsBlink(selected);
                    }
                    Thread.Sleep(Context.Instance.BlinkSpeedMs);
                }
                switch(Console.ReadKey(true).Key)
                {
                    case ConsoleKey.LeftArrow:
                    case ConsoleKey.RightArrow:
                        selected = Math.Abs((selected + 1) % 2);
                        break;
                    case ConsoleKey.Enter:
                        selectionFinished = true;
                        break;
                }
            }

            return selected;
        }
    }
}
