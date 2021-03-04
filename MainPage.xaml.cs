using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace MatchGame
{

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        DispatcherTimer timer = new DispatcherTimer();
        int tenthsOfSecondsElapsed;
        int matchesFound;

        List<string> animalEmoji  = new List<string>()
        {
            "❤","❤",
            "🧡","🧡",
            "💚","💚",
            "💙","💙",
            "💜","💜",
            "🤎","🤎",
            "🖤","🖤",
            "🤍","🤍",
            "💔","💔",
            "❣","❣"
        };
        public MainPage()
        {
            this.InitializeComponent();
            timer.Interval = TimeSpan.FromSeconds(.1);
            timer.Tick += DispatcherTimer_Tick;
            SetUpGame();
        }

        private void DispatcherTimer_Tick(object sender, object e)
        {
            tenthsOfSecondsElapsed++;
            timetick.Text = (tenthsOfSecondsElapsed / 10F).ToString("0.0s");
            if(matchesFound == 10)
            {
                timer.Stop();
                timetick.Text = $"{timetick.Text}  -Play Again ?";
            }
        }

        private void SetUpGame()
        {
            Random random = new Random();
            foreach (TextBlock textBlock in mainGrid.Children.OfType<TextBlock>())
            {
                if (textBlock.Name != "timetick")
                {
                    textBlock.Visibility = Visibility.Visible;
                    int index = random.Next(animalEmoji.Count);
                    string nextEmojo = animalEmoji[index];
                    textBlock.Text = nextEmojo;
                    matchesFound++;
                    animalEmoji.RemoveAt(index);
                }
               

            }
            timer.Start();
            tenthsOfSecondsElapsed = 0;
            matchesFound = 0;
                 
        }
        TextBlock lastTextBlockeClicked;
        bool findingMatch = false;

        private void TextBlock_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            TextBlock textBlock = sender as TextBlock;
            if (findingMatch == false)
            {
                textBlock.Visibility = Visibility.Collapsed;
                lastTextBlockeClicked = textBlock;
                findingMatch = true;
            }
            else if (textBlock.Text == lastTextBlockeClicked.Text)
            {
                matchesFound++;
                textBlock.Visibility = Visibility.Collapsed;
                findingMatch = false;
            }
             else
            {
                textBlock.Visibility = Visibility.Visible;
                findingMatch = false;
            }
        }

        private void TimeTick_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            if (matchesFound == 10)
                matchesFound=  0;
    
            //animalEmoji.Clear();
            SetUpGame();
        }
    }
}
