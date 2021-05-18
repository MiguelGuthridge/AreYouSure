using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System;
using System.ComponentModel;

namespace AreYouSure
{
    public class MyDataContext
    {
        private static readonly string[] mAdditions = 
        {  
            "absolutely",
            "positive that you are",
            "certain that you're",
            "sure that you're",
            "still",
            "decisively",
            "decidedly",
            "convinced that you are",
            "really",
            "100%",
        };

        private static readonly string[] mNegatives =
        {
            "not ",
            "not ",
            "un",
            "un",
            "not ",
            "not ",
            "not ",
            "un",
            "not ",
            "less than ",
        };

        private readonly int messageId;
        private static readonly string mStart = "Are you";
        private readonly string mFinish;
        public string Message
        {
            get
            {
                return mStart + " " + mFinish;
            }

        }

        private MainWindow? child;
        private MainWindow contextOwner;

        public MyDataContext(MainWindow owner, string oldMessage, bool doAppend, int negId=-1)
        {
            contextOwner = owner;
            mFinish = oldMessage;
            if (negId != -1) mFinish = mNegatives[negId] + mFinish;
            else if (doAppend)
            {
                Random gen = new();
                messageId = gen.Next() % mAdditions.Length;
                mFinish = mAdditions[messageId] + " " + mFinish;
            }
        }

        public void OnClickNo()
        {
            child = new MainWindow(mFinish, false, messageId);
            child.ShowDialog(contextOwner);
        }
        public void OnClickYes()
        {
            child = new MainWindow(mFinish, true);
            child.ShowDialog(contextOwner);
        }

    }

    public class MainWindow : Window
    {

        public MainWindow(string oldMessage, bool doAppend, int negId=-1)
        {
            DataContext = new MyDataContext(this, oldMessage, doAppend, negId);
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        public MainWindow()
        {
            DataContext = new MyDataContext(this, "sure that you want to open the program?", false, -1);
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}