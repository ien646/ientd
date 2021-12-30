using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace IenTD
{
    public class SelectionDialog : Dialog
    {
        public IEnumerable<string> Options { get; private set; }

        const int LIST_START_VPOS = 6;

        public SelectionDialog(string title, IEnumerable<string> options)
            : base(title)
        {
            Options = options;
        }

        public override int ShowDialog()
        {
            base.ShowDialog();
            
            int maxDisplayable = Console.BufferHeight - (LIST_START_VPOS * 2);
            int maxOffset = Math.Max(0, Options.Count() - maxDisplayable);
            bool scrollable = maxDisplayable < Options.Count();            

            void drawNormal(int offset)
            {
                if (scrollable)
                {
                    Utils.ClearLine(LIST_START_VPOS - 1, 1);
                    Utils.ClearLine(LIST_START_VPOS + maxDisplayable + 1, 1);
                    if (offset != 0)
                    {                        
                        Utils.DrawCentered("↑ ↑ ↑", LIST_START_VPOS - 2);
                    }
                    if(offset != maxOffset)
                    {                        
                        Utils.DrawCentered("↓ ↓ ↓", LIST_START_VPOS + maxDisplayable + 1);
                    }
                }

                for (int i = 0; i < Math.Min(maxDisplayable, Options.Count()); ++i)
                {
                    Utils.ClearLine(LIST_START_VPOS + i, 1);
                    Utils.DrawCentered(Options.ElementAt(i + offset), LIST_START_VPOS + i);
                }
            }

            void drawAllBlink(int selected, int offset)
            {
                for (int i = 0; i < Math.Min(maxDisplayable, Options.Count()); ++i)
                {
                    if (i == selected - offset)
                    {
                        Utils.ClearLine(LIST_START_VPOS + i, 1);
                        using (var _ = Utils.SetTemporaryColors(Context.Instance.SelectionBgColor, Context.Instance.SelectionFgColor))
                        {                            
                            Utils.DrawCentered(Options.ElementAt(i + offset), LIST_START_VPOS + i);
                        }
                    }
                    else
                    {
                        Utils.ClearLine(LIST_START_VPOS + i, 1);
                        Utils.DrawCentered(Options.ElementAt(i + offset), LIST_START_VPOS + i);
                    }
                }
            }

            int offset = 0;
            int selected = 0;
            bool selectionFinished = false;
            while(!selectionFinished)
            {
                int loopIndex = 0;
                while (!Console.KeyAvailable)
                {
                    if (loopIndex++ % 2 == 0)
                    {
                        drawAllBlink(selected, offset);
                    }
                    else
                    {
                        drawNormal(offset);
                    }
                    Thread.Sleep(Context.Instance.BlinkSpeedMs);
                }

                switch(Console.ReadKey().Key)
                {
                    case ConsoleKey.DownArrow:
                        selected += 1;
                        if (selected >= Options.Count())
                        {
                            selected = Options.Count() - 1;
                        }
                        else if (selected >= maxDisplayable)
                        {
                            offset = Math.Min(maxOffset, offset + 1);
                        }
                        break;
                    case ConsoleKey.UpArrow:
                        selected -= 1;
                        if(selected < 0)
                        {
                            selected = 0;
                        }
                        else if(selected < offset)
                        {
                            offset = Math.Max(0, offset - 1);
                        }
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
