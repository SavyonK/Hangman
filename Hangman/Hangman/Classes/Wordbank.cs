using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hangman
{
    public enum Difficulty { Easy, Medium, Hard, All }

    class Wordbank
    {
        public List<string> WordList { get; private set; }
        public List<string> EasyLevelList { get; private set; }
        public List<string> MediumLevelList { get; private set; }
        public List<string> HardLevelList { get; private set; }
        private Random rnd;
        public Wordbank()
        {
            rnd = new Random();

            WordList = new List<string>();
            EasyLevelList = new List<string>();
            MediumLevelList = new List<string>();
            HardLevelList = new List<string>();

            GetDefaultList();
            SetEachLevelsList();
        }
        public string GetNewWord(Difficulty level)
        {
            //return random word which fits specific level
                      
            string newWord = "";
            switch (level)
            {
                case Difficulty.All:
                    newWord = GetWordFromList(WordList);
                    break;

                case Difficulty.Easy:
                    newWord = GetWordFromList(EasyLevelList);
                    break;

                case Difficulty.Medium:
                    newWord = GetWordFromList(MediumLevelList);
                    break;

                case Difficulty.Hard:
                    newWord = GetWordFromList(HardLevelList);
                    break;

                default:
                    break;
            }
          
            return newWord;
          
        }

        private string GetWordFromList(List<string> levelList)
        {
            string newWord = "";

            //if list is empty
            if (levelList.Count() == 0)
            { throw new Exception("list of words for this level is empty"); }


            int rndIndex = rnd.Next(levelList.Count);
            newWord = levelList[rndIndex];
            return newWord;
        }
        private void SetEachLevelsList() //this method regulates each difficulty parameters
        {
            for (int i = 0; i < WordList.Count; i++)
            {
                if (WordList[i].Length <= 4) //4 letters or less
                {
                    EasyLevelList.Add(WordList[i]);
                }
                else if (WordList[i].Length >= 5 && WordList[i].Length <= 7) //5-7 letters
                {
                    MediumLevelList.Add(WordList[i]);
                }
                else if (WordList[i].Length <= 8) //8 letters or more
                {
                    HardLevelList.Add(WordList[i]);
                }
            }
           
        }
        private void GetDefaultList()
        {
            WordList.Add("Cat");
            WordList.Add("Dog");
            WordList.Add("Italy");
            WordList.Add("Audio");
            WordList.Add("Paper");
            WordList.Add("Wallet");
            WordList.Add("Glasses");
            WordList.Add("Tree");
            WordList.Add("Measurement");
            WordList.Add("Differential");
            WordList.Add("Cloud");
            WordList.Add("Toothbrush");
            WordList.Add("Roadtrip");
            WordList.Add("Bike");
            WordList.Add("Duck");
            WordList.Add("Sandman");
            WordList.Add("Mat");
            WordList.Add("Lamp");
            WordList.Add("Championship");
            WordList.Add("Taperecorder");
            WordList.Add("Children");
            WordList.Add("Cheers");

        }

    }
}
