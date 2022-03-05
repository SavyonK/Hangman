using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using static Hangman.Difficulty;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Hangman
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GameScreen : Page
    {
        private string key;
        GameManager gameManager;
        string levelChosen;

        private RadioButton[] radioButtonArr;
        public GameScreen()
        {
            this.InitializeComponent();
            gameManager = new GameManager(grid, Keyboard_Click);

            //detecting keyboard 
            Window.Current.CoreWindow.KeyDown += CoreWindow_KeyDown;
            Window.Current.CoreWindow.KeyUp += CoreWindow_KeyUp;


            AdjustRadioButtonContent();
        }

        private void AdjustRadioButtonContent()
        {
            //Initializing array and making sure its content will fit the Difficulty enum
            radioButtonArr = new RadioButton[4];
            radioButtonArr[0] = RB_All;
            radioButtonArr[0].Content = Difficulty.All.ToString();
            radioButtonArr[1] = RB_Easy;
            radioButtonArr[1].Content = Difficulty.Easy.ToString();
            radioButtonArr[2] = RB_Intermediate;
            radioButtonArr[2].Content = Difficulty.Medium.ToString();
            radioButtonArr[3] = RB_Difficult;
            radioButtonArr[3].Content = Difficulty.Hard.ToString();

            for (int i = 0; i < radioButtonArr.Length; i++)
            {
                radioButtonArr[i].Height = 30;
                radioButtonArr[i].Foreground = new SolidColorBrush(Colors.Black);
                radioButtonArr[i].VerticalAlignment = VerticalAlignment.Center;
                radioButtonArr[i].VerticalContentAlignment = VerticalAlignment.Center;
                radioButtonArr[i].FontSize = 15;
            }
        }

        private void CoreWindow_KeyUp(CoreWindow sender, KeyEventArgs args)
        {
            key = "";
        }

        private void Keyboard_Click(object sender, RoutedEventArgs e) //this event is connected only to keyboard keys
        {
            if (sender is Button)
            {
                Button btn = (Button)sender; //downcasting

                //sending button content to method
                char _letterPressed = char.Parse(btn.Content.ToString());
                gameManager.CheckLetter(_letterPressed);
            }

        }

        private void CoreWindow_KeyDown(CoreWindow sender, KeyEventArgs args)
        {
            key = args.VirtualKey.ToString();


            if (key == "Escape")   //exit game
            {
                Application.Current.Exit();
            }

        }

        private void RB_Checked(object sender, RoutedEventArgs e) //updates levelChosen variable eveytime user checks an option
        {
            if (sender is RadioButton)
            {
                RadioButton btn1 = (RadioButton)sender; //downcasting
                levelChosen = btn1.Content.ToString();
            }

        }

        private void NewGame_Click(object sender, RoutedEventArgs e)
        {
            Difficulty difficulty;

            if (levelChosen == null)
            {
                //if difficulty not chosen by user - word will be drawn from general unfiltered list
                levelChosen = Difficulty.All.ToString();
            }


            difficulty = (Difficulty)Enum.Parse(typeof(Difficulty), levelChosen);

            gameManager.NewGame(difficulty);

        }


    }
}
