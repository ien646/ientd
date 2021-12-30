using System;
using System.Collections.Generic;
using System.Text;

namespace IenTD
{
    public enum ProgressMode
    {
        Finite,
        Undefined
    }

    public class ProgressDialog : Dialog
    {
        public ProgressMode Mode { get; private set; }
        public string Message { get; private set; }
        public int MaxProgress { get; private set; }
        public int Progress { get; private set; }

        const int PROGRESSBAR_PADDING = 2;

        const int MESSAGE_OFFSET = -2;

        const int COUNT_OFFSET = 1;

        public ProgressDialog(ProgressMode mode, string title, string message, int maxProgress, int progress)
            : base(title)
        {
            Mode = mode;
            Message = message;
            MaxProgress = maxProgress;
            Progress = progress;
        }

        public override int ShowDialog()
        {
            base.ShowDialog();

            DrawProgressMessage();

            DrawProgressBar();

            DrawProgressCount();

            return 0;
        }

        public void SetMode(ProgressMode mode)
        {
            Mode = mode;
            ShowDialog();
        }

        public void SetMaxProgress(int v)
        {
            MaxProgress = v;
            if(Progress > MaxProgress)
            {
                Progress = v;
            }
            DrawProgressBar();
            DrawProgressCount();
        }

        public void SetProgress(int v, bool clearBar = true)
        {
            Progress = v;
            DrawProgressBar(clearBar);
            DrawProgressCount();
        }

        public void SetMessage(string msg)
        {
            Message = msg;
            DrawProgressMessage();
        }

        private void DrawProgressBar(bool clear = true)
        {
            switch (Mode)
            {
                case ProgressMode.Undefined:
                    DrawProgressBarUndefined(clear);
                    break;
                case ProgressMode.Finite:
                    DrawProgressBarFinite(clear);                    
                    break;
            }
        }

        private void DrawProgressMessage()
        {
            Utils.ClearLine((Console.BufferHeight / 2) + MESSAGE_OFFSET, 1);
            Utils.DrawCentered(Message, (Console.BufferHeight / 2) + MESSAGE_OFFSET);
        }

        private void DrawProgressBarFinite(bool clear = true)
        {
            int pbarLength = Console.BufferWidth - ((PROGRESSBAR_PADDING + 1) * 2);
            float proportion = (float)Progress / MaxProgress;
            if(float.IsNaN(proportion) || float.IsInfinity(proportion))
            {
                proportion = 0;
            }
            int currentLen = (int)(proportion * pbarLength);
            int pendingLen = pbarLength - currentLen;

            string pbarText =
                new string(Context.Instance.ProgressBarFillCharacter, currentLen) +
                new string(' ', pendingLen);

            if (clear)
            {
                Utils.ClearLine((Console.BufferHeight / 2), 1);
            }
            using (var _ = Utils.SetTemporaryColors(Context.Instance.ProgressBarBgColor, Context.Instance.ProgressBarFgColor))
            {
                Utils.DrawCentered(pbarText, (Console.BufferHeight / 2));
            }
        }

        private void DrawProgressBarUndefined(bool clear = true)
        {
            int pbarLength = Console.BufferWidth - ((PROGRESSBAR_PADDING + 1) * 2);
            string pbarText = "";
            for(int i = 0; i < pbarLength; ++i)
            {
                if(i % 3 == 0)
                {
                    pbarText += Context.Instance.ProgressBarUndefinedCharacter;
                }
                else
                {
                    pbarText += ' ';
                }
            }

            if (clear)
            {
                Utils.ClearLine((Console.BufferHeight / 2), 1);
            }
            using (var _ = Utils.SetTemporaryColors(Context.Instance.ProgressBarBgColor, Context.Instance.ProgressBarFgColor))
            {
                Utils.DrawCentered(pbarText, (Console.BufferHeight / 2));
            }
        }

        private void DrawProgressCount()
        {
            Utils.ClearLine((Console.BufferHeight / 2) + COUNT_OFFSET, 1);
            if (Mode == ProgressMode.Finite)
            {
                string text = "[" + Progress + "/" + MaxProgress + "]";                
                Utils.DrawCentered(text, (Console.BufferHeight / 2) + COUNT_OFFSET);
            }
        }
    }
}
