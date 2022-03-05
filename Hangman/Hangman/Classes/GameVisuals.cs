using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Gaming.Input;
using Windows.System;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Shapes;

namespace Hangman
{
    class GameVisuals
    {

        private TextBlock[] LettersArr { get; set; }
        private Rectangle[] RectArr { get; set; }

        private Button[] KeyboardArr { get; set; }

        private Image[] StagesImageArr { get; set; }
        private TextBlock TB_ChancesLeft { get; set; }


        private string CurrentWord { get; set; } //for followup if needed
        private Grid _grid;


        public GameVisuals(Grid grid, string word)
        {
            _grid = grid;
            _grid.Background = new SolidColorBrush(Colors.WhiteSmoke);
            CurrentWord = word;
            KeyboardArr = new Button[26];
            NewKeyboard();
            SetChancesBlock();
            CreateTextBlocks();
            SetWordOnGrid();

        }


        public static bool IsWordLengthValid(string word, Grid grid)
        {
            //check if variable has an actual word length
            if (string.IsNullOrEmpty(word))
            {
                return false;
            }

            //check if: is grid culomns capacity for letters (word length) maxed out
            else if (word.Length > (grid.ColumnDefinitions.Count))
            {
                return false;
            }

            return true;
        }

        #region initializing instance methods
        private void NewKeyboard()
        {
            int row = 1;
            int column = -1;

            for (int i = 0; i < KeyboardArr.Length; i++)
            {
                KeyboardArr[i] = new Button();
                KeyboardArr[i].Height = 60;
                KeyboardArr[i].Width = 60;
                KeyboardArr[i].FontSize = 30;


                #region placing keyboard on grid
                int startingRow = 1;
                if (i <= 9) //1st row
                {
                    row = startingRow;
                    column = 3 + i;
                    KeyboardArr[i].Margin = new Thickness(5, 0, -5, 0);
                }
                else if (i >= 10 && i <= 18) //2nd row
                {
                    row = startingRow + 1;
                    column = i - 7;
                    KeyboardArr[i].Margin = new Thickness(45, 0, -45, 0);
                }
                else if (i >= 19) //3rd row
                {
                    row = startingRow + 2;
                    column = i - 15;
                    KeyboardArr[i].Margin = new Thickness(45, 0, -45, 0);
                }

                _grid.Children.Add(KeyboardArr[i]);
                Grid.SetColumn(KeyboardArr[i], column);
                Grid.SetRow(KeyboardArr[i], row);
                #endregion

            }

            #region content
            this.KeyboardArr[0].Content = "Q";
            this.KeyboardArr[1].Content = "W";
            this.KeyboardArr[2].Content = "E";
            this.KeyboardArr[3].Content = "R";
            this.KeyboardArr[4].Content = "T";
            this.KeyboardArr[5].Content = "Y";
            this.KeyboardArr[6].Content = "U";
            this.KeyboardArr[7].Content = "I";
            this.KeyboardArr[8].Content = "O";
            this.KeyboardArr[9].Content = "P";
            this.KeyboardArr[10].Content = "A";
            this.KeyboardArr[11].Content = "S";
            this.KeyboardArr[12].Content = "D";
            this.KeyboardArr[13].Content = "F";
            this.KeyboardArr[14].Content = "G";
            this.KeyboardArr[15].Content = "H";
            this.KeyboardArr[16].Content = "J";
            this.KeyboardArr[17].Content = "K";
            this.KeyboardArr[18].Content = "L";
            this.KeyboardArr[19].Content = "Z";
            this.KeyboardArr[20].Content = "X";
            this.KeyboardArr[21].Content = "C";
            this.KeyboardArr[22].Content = "V";
            this.KeyboardArr[23].Content = "B";
            this.KeyboardArr[24].Content = "N";
            this.KeyboardArr[25].Content = "M";
            #endregion

        }
        public void AddKeyboardEvent(RoutedEventHandler someEvent)
        {
            for (int i = 0; i < KeyboardArr.Length; i++)
            {
                KeyboardArr[i].Click += someEvent;
            }
        }
        private void SetChancesBlock()
        {
            //set images
            int column = 12;
            int columnSpan = 3;
            int imageRow = 3;
            int imageRowSpan = 4;
            StagesImageArr = new Image[8];
            for (int i = 0; i < StagesImageArr.Length; i++)
            {
                StagesImageArr[i] = new Image();
                StagesImageArr[i].Source = new BitmapImage(new Uri($"ms-appx://Project/Assets/HM{i + 1}.png"));

                StagesImageArr[i].Visibility = Visibility.Collapsed;
                _grid.Children.Add(StagesImageArr[i]);
                Grid.SetColumn(StagesImageArr[i], column);
                Grid.SetRow(StagesImageArr[i], imageRow);
                Grid.SetColumnSpan(StagesImageArr[i], columnSpan);
                Grid.SetRowSpan(StagesImageArr[i], imageRowSpan);
            }

            //set textblock
            TB_ChancesLeft = new TextBlock();
            TB_ChancesLeft.TextAlignment = TextAlignment.Center;
            TB_ChancesLeft.VerticalAlignment = VerticalAlignment.Bottom;
            _grid.Children.Add(TB_ChancesLeft);
            Grid.SetColumn(TB_ChancesLeft, column);
            Grid.SetRow(TB_ChancesLeft, imageRow - 1);
            Grid.SetColumnSpan(TB_ChancesLeft, columnSpan);
        }
        #endregion

        private void CreateTextBlocks()
        {
            if (IsWordLengthValid(CurrentWord, _grid))
            {
                RectArr = new Rectangle[CurrentWord.Length];
                LettersArr = new TextBlock[CurrentWord.Length];

                int height = 85;
                int width = 60;

                for (int i = 0; i < LettersArr.Length; i++)
                {
                    //rectangles will act as background for each letter
                    RectArr[i] = new Rectangle();
                    RectArr[i].Height = height + 25;
                    RectArr[i].Width = width + 10;
                    RectArr[i].Fill = new SolidColorBrush(Colors.LightBlue);
                    RectArr[i].Visibility = Visibility.Visible;

                    //new textblock for each letter
                    LettersArr[i] = new TextBlock();
                    LettersArr[i].Text = CurrentWord[i].ToString();
                    LettersArr[i].Height = height;
                    LettersArr[i].Width = width;
                    LettersArr[i].FontSize = 50;
                    LettersArr[i].Foreground = new SolidColorBrush(Colors.Black);
                    LettersArr[i].VerticalAlignment = VerticalAlignment.Center;
                    LettersArr[i].HorizontalAlignment = HorizontalAlignment.Center;
                    LettersArr[i].TextAlignment = TextAlignment.Center;
                    LettersArr[i].HorizontalTextAlignment = TextAlignment.Center;
                    LettersArr[i].Margin = new Thickness(0, 15, 0, 0); //replacement for non-existent VerticalTextAlignment attribute


                    LettersArr[i].Visibility = Visibility.Collapsed;

                }
            }
        }

        private void SetWordOnGrid()
        {
            double startingPoint = (_grid.ColumnDefinitions.Count - LettersArr.Length) / 2;

            int row = 7;

            for (int i = 0; i < LettersArr.Length; i++) //place textblocks & rectangles on grid as one unit
            {
                _grid.Children.Add(RectArr[i]);
                Grid.SetColumn(RectArr[i], (int)startingPoint + i);
                Grid.SetRow(RectArr[i], row);

                _grid.Children.Add(LettersArr[i]);
                Grid.SetColumn(LettersArr[i], (int)startingPoint + i);
                Grid.SetRow(LettersArr[i], row);
            }
        }

        #region managing current game 
        public void DisableKeyboardLetter(char a)
        {
            for (int i = 0; i < KeyboardArr.Length; i++)
            {
                if (KeyboardArr[i].Content.ToString() == a.ToString())
                {
                    KeyboardArr[i].IsEnabled = false;
                }
            }
        }

        public void MakeLetterVisible(char a)
        {
            for (int i = 0; i < LettersArr.Length; i++)
            {
                bool upper = LettersArr[i].Text.Equals(a.ToString().ToUpper());
                bool lower = LettersArr[i].Text.Equals(a.ToString().ToLower());

                if (upper || lower)
                {
                    LettersArr[i].Visibility = Visibility.Visible;
                }
            }
        }

        public void UpdateChancesBlock(int missedChances, int maxChances)
        {
            TB_ChancesLeft.Text = $"You have {maxChances - missedChances} / {maxChances} chances left";

            if (missedChances != 0)
            {
                StagesImageArr[(missedChances * 2) - 1].Visibility = Visibility.Visible;
            }
        }
        #endregion

        #region game over methods
        public void EndGame()
        {
            DisableKeyboard();
            ShowHiddenLetters();
        }

        private void DisableKeyboard()
        {
            for (int i = 0; i < KeyboardArr.Length; i++)
            {
                KeyboardArr[i].IsEnabled = false;
            }
        }

        private void ShowHiddenLetters()
        {
            for (int i = 0; i < LettersArr.Length; i++)
            {
                if (LettersArr[i].Visibility == Visibility.Collapsed)
                {
                    LettersArr[i].Visibility = Visibility.Visible;
                    LettersArr[i].Foreground = new SolidColorBrush(Colors.Red);
                }
            }
        }
        #endregion

        #region new game methods
        public void NewGame(string word, int chancesMissed, int maxchances)
        {
            RemoveWord();
            UpdateWord(word);
            EnableKeyboard();
            ResetHangmanPics();
            UpdateChancesBlock(chancesMissed, maxchances);
        }
        private void RemoveWord() //remove arrays of textblocks(letters) & rectangles 
        {
            for (int i = 0; i < LettersArr.Length; i++)
            {
                _grid.Children.Remove(LettersArr[i]);
                _grid.Children.Remove(RectArr[i]);
            }

        }
        private void UpdateWord(string newWord) //put new word on screen
        {
            if (IsWordLengthValid(newWord, _grid))
            {
                CurrentWord = newWord;
                CreateTextBlocks();
                SetWordOnGrid();
            }
            else
            {
                throw new Exception("Word length is not valid");
            }
        }
        private void EnableKeyboard()
        {
            for (int i = 0; i < KeyboardArr.Length; i++)
            {
                KeyboardArr[i].IsEnabled = true;
            }
        }
        private void ResetHangmanPics()
        {
            for (int i = 0; i < StagesImageArr.Length; i++)
            {
                StagesImageArr[i].Visibility = Visibility.Collapsed;
            }
        }
        #endregion
    }
}
