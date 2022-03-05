using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using static Hangman.Difficulty;

namespace Hangman
{
    class GameManager
    {
        private int CounterHit { get; set; }
        private int CounterMiss { get; set; }
        private const int MAX_LOSING_ATTEMPS = 4;
        private string CurrentWord { get; set; }
        private Wordbank WordGenerator { get; set; }
        private Grid _grid;
        private GameVisuals gameVisuals;

        public GameManager(Grid grid, RoutedEventHandler clickEvent)
        {
            CounterMiss = 0;
            CounterHit = 0;
            _grid = grid;

            WordGenerator = new Wordbank();

            CurrentWord = GetValidLengthWord();

            gameVisuals = new GameVisuals(_grid, CurrentWord);
            gameVisuals.AddKeyboardEvent(clickEvent);
            gameVisuals.UpdateChancesBlock(CounterMiss, MAX_LOSING_ATTEMPS);

        }

        private string GetValidLengthWord(Difficulty difficulty = All)  //gets new word from Wordbank according to GameVisuals' restrictions

        {
            CurrentWord = WordGenerator.GetNewWord(difficulty);

            //check if grid is not build for length of current word OR if current word is null or empty
            while (!GameVisuals.IsWordLengthValid(CurrentWord, _grid))
            {
                //replace with new word
                CurrentWord = WordGenerator.GetNewWord(difficulty);
            }

            return CurrentWord;
        }


        public void CheckLetter(char letter)   //check if current word contains letter pressed
        {
            bool correctLetter = false;

            gameVisuals.DisableKeyboardLetter(letter);

            for (int i = 0; i < CurrentWord.Length; i++)
            {
                bool upper = CurrentWord[i].ToString().Equals(letter.ToString().ToUpper());
                bool lower = CurrentWord[i].ToString().Equals(letter.ToString().ToLower());

                if (upper || lower)
                {
                    correctLetter = true;
                    ++CounterHit;
                }
            }

            if (correctLetter)
            {
                gameVisuals.MakeLetterVisible(letter); //show letter on textblock
            }
            else if (!correctLetter)
            {
                ++CounterMiss;
                gameVisuals.UpdateChancesBlock(CounterMiss, MAX_LOSING_ATTEMPS);
            }

            CheckIfGameEnded();
        }


        private void CheckIfGameEnded()   //check if win or lose
        {
            if (CounterHit == CurrentWord.Length)
            { EndGameWin(); }

            else if (CounterMiss == MAX_LOSING_ATTEMPS)
            { EndGameLose(); }
        }


        private async void EndGameWin()
        {
            gameVisuals.EndGame();
            await new MessageDialog("You guessed it, good job!").ShowAsync();
        }

        private async void EndGameLose()
        {
            gameVisuals.EndGame();
            await new MessageDialog("Uh oh, better luck next time").ShowAsync();
        }

        public void NewGame(Difficulty difficulty)
        {
            gameVisuals.EndGame();
            CounterHit = 0;
            CounterMiss = 0;
            CurrentWord = GetValidLengthWord(difficulty);
            gameVisuals.NewGame(CurrentWord, CounterMiss, MAX_LOSING_ATTEMPS);
        }
    }
}
