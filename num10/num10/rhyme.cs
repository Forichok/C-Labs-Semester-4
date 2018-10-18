using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace num10
{
    public static class Rhyme
    {        
        private static readonly HashSet<string> Points;
        private static readonly SortedDictionary<int, List<string>> Dictionary;
        private static readonly HashSet<char> Vowels;

        static Rhyme()
        {
            Dictionary = new SortedDictionary<int, List<string>>();
            Vowels = new HashSet<char>() { 'а', 'я', 'у', 'ю', 'о', 'ё', 'е', 'э', 'и', 'ы' };
            Points = new HashSet<string>() { ".", ",", "!", "?", "...", ":", ";" };
        }


        private static int GetVowelsCount(string word)
        {
            int count=0;
            word=word.ToLower();
            foreach (char letter in word)
            {
                if (Vowels.Contains(letter))
                {
                    count++;
                }
            }
            return count;
        }
        public static void FillBaseFromFile(string file = "../../lib.txt")
        {
            StreamReader sr = new StreamReader(file, Encoding.UTF8);
            while (sr.Peek() != -1)
            {
                var word = sr.ReadLine();
                int vowelsCount = GetVowelsCount(word);
                if (!Dictionary.ContainsKey(vowelsCount))
                {
                    List<string> list = new List<string>();
                    list.Add(word); 
                    Dictionary.Add(vowelsCount, list);
                } 
                else
                {
                    Dictionary[vowelsCount].Add(word);
                }
            }
            sr.Close();
        }
        public static void ChangePoemFromFile(string fileIN = "../../poemIN.txt", string fileOUT = "../../poemOUT.txt")
        {
            var rnd = new Random();
            var sw = new StreamWriter(fileOUT);
            var sr = new StreamReader(fileIN);

            char[] delimiterChars = { ' ', '\n', '\t' };
            while (sr.Peek() != -1)
            {
                var line = sr.ReadLine();
                foreach (var c in Points)
                {
                    line = line.Replace(c, " "+c);
                }
                
                var words = new List<string>(line.Split(delimiterChars));
         
                for (var i=0; i<words.Count;i++)
                {
                    var vowelsCount = GetVowelsCount(words[i]);

                    if (words[i].Length==1)
                    {
                       sw.Write(words[i]+" ");
                       continue;
                    }
                    
                    bool noVowels = GetVowelsCount(words[i]) == 0;

                    if (noVowels) { sw.Write(words[i] + " "); continue; }

                    bool isLastWord = ((i + 1 <= words.Count - 1) && (Points.Contains(words[i + 1])))||
                                      ((i == words.Count - 1) && (!Points.Contains(words[i])));
                    if (isLastWord) 
                    {
                        var len = (int) (words[i].Length * 0.3);
                        
                        string str = words[i].Substring(words[i].Length - len);
                     
                        for (var j = 0; j < Dictionary[vowelsCount].Count; j++)
                        {
                            var randomWordId = rnd.Next(Dictionary[vowelsCount].Count);

                            string newWord = Dictionary[vowelsCount][randomWordId];

                            if (newWord.Substring(newWord.Length - len) == str)
                            {
                                sw.Write(newWord);
                                break;
                            }
                        }
                    }
                    else
                       sw.Write(Dictionary[GetVowelsCount(words[i])][rnd.Next(vowelsCount)] +" ");
                }
                sw.Write("\n");
            }
            sw.Close();
            sr.Close(); 
        }
    }
}
