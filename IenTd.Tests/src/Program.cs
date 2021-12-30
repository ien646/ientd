using System;
using System.Collections.Generic;
using System.Threading;

namespace IenTD.Tests
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            Utils.SetSize(80, 24);
            Utils.SetColors(ConsoleColor.DarkBlue, ConsoleColor.White);
            Context.Instance.BlinkSpeedMs = 100;

            List<string> options = new List<string>()
            {
                "Pupucaca",
                "Cacapupu",
                "BumbuKenke",
                "Pupucaca",
                "Cacapupu",
                "BumbuKenke",
                "Pupucaca",
                "Cacapupu",
                "BumbuKenke",
                "Pupucaca",
                "Cacapupu",
                "BumbuKenke",
                "Pupucaca",
                "Cacapupu",
                "BumbuKenke",
                "Pupucaca",
                "Cacapupu",
                "BumbuKenke",
                "Pupucaca",
                "Cacapupu",
                "BumbuKenke",
                "Pupucaca",
                "Cacapupu",
                "BumbuKenke",
                "Pupucaca",
                "Cacapupu",
                "BumbuKenke",
                "Pupucaca",
                "Cacapupu",
                "BumbuKenke",
                "Pupucaca",
                "Cacapupu",
                "BumbuKenke",
            };

            options.AddRange(options);
            options.AddRange(options);
            options.AddRange(options);

            ProgressDialog pdiag = new ProgressDialog(ProgressMode.Finite, "Progress Test", "...", options.Count, 0);
            pdiag.ShowDialog();

            for(int i = 0; i < options.Count; i++)
            {
                string option = options[i];
                pdiag.SetMessage(option);
                pdiag.SetProgress(i+1);
                Console.Out.Flush();
                Thread.Sleep(50);
            }

            return;
        }
    }
}
