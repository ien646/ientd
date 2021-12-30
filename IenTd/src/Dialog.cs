using System;
using System.Collections.Generic;
using System.Text;

namespace IenTD
{
    public class Dialog
    {      
        public string Title { get; private set; }

        public Dialog(string title)
        {
            Title = title;
        }

        public virtual int ShowDialog()
        {
            Console.Clear();

            Utils.DrawBorders(
                Context.Instance.BorderCharacterHorizontal,
                Context.Instance.BorderCharacterVertical,
                Context.Instance.BorderCharacterCorner
            );

            DrawTitle();

            return 0;
        }

        public void SetTitle(string title)
        {
            Title = title;
            DrawTitle();
        }

        protected void DrawTitle()
        {
            Utils.DrawCentered(Title, Context.Instance.DialogTitleVpos);
        }
    }
}
