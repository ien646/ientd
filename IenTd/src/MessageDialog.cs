using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IenTD
{
    public class MessageDialog : Dialog
    {
        public string Message { get; private set; }

        public MessageDialog(string title, string message)
            : base(title)
        {
            Message = message;
        }

        public override int ShowDialog()
        {
            base.ShowDialog();

            int vpos = (Console.BufferHeight / 2) - 1;
            foreach(string line in Message.Split('\n'))
            {
                Utils.DrawCentered(line, vpos++);
            }

            Action drawOkButtonNormal = () =>
            {                
                Utils.DrawCentered("[  OK  ]", Console.BufferHeight - 3);
            };

            Action drawOkButtonBlink = () =>
            {
                using var _ = Utils.SetTemporaryColors(Context.Instance.SelectionBgColor, Context.Instance.SelectionFgColor);
                Utils.DrawCentered("[  OK  ]", Console.BufferHeight - 3);
            };

            int loopIndex = 0;
            while (true)
            {
                while(!Console.KeyAvailable)
                {
                    if(loopIndex++ % 2 ==0)
                    {
                        drawOkButtonNormal();
                    }
                    else
                    {
                        drawOkButtonBlink();
                    }
                    Thread.Sleep(Context.Instance.BlinkSpeedMs);
                }
                if (Console.ReadKey(true).Key == ConsoleKey.Enter)
                {
                    break;
                }
            }

            return 0;
        }
    }
}
