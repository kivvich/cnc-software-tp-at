using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.IO;
using System.Text;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using EasyModbus;
using System.Threading;

namespace ModbusWPFproject
{
    public partial class Window1 : Window
    {
        public static string[,] ArrayGenerateWords(string slovo)
        {
            string[] slovoArray = File.ReadAllLines(slovo);
            string[,] stringArray = new string[slovoArray.GetLength(0), slovoArray[0].Length];

            char[] stringChar;

            for (int i = 0; i < slovoArray.Length; i++)
            {
                for (int j = 0; j < slovoArray[i].Length; j++)
                {
                    stringArray = new string[slovoArray[i].Length, slovoArray.Length];
                    stringChar = slovoArray[i].ToCharArray();
                    stringArray[j, i] = stringChar[j].ToString() + " ";
                }
            }
            return stringArray;
        }
        public static List<int> FindListNumbers(string[] arrayWords)
        {
            List<int> findListNumbers = new List<int>();

            arrayWords = arrayWords.Where(line => !line.Trim().StartsWith(";")).ToArray();

            for (int i = 0; i < arrayWords.Length; i++)
            {
                int countG = 0x0;
                string pattern = @"^(?!;).+$";
                string patternTwo = @"(?:[G,g]{1,3}-?\d+|X|x|Y|y|Z|z|A|a|B|b|C|c|F|f)";
                string lastString = arrayWords.Last().ToString();
                int m = arrayWords.Count() - 1;
                Regex regex = new Regex(pattern);
                if (regex.IsMatch(arrayWords[i]))
                {
                    MatchCollection mathesSubstring = Regex.Matches(arrayWords[i], patternTwo);

                    for (int j = 0; j < mathesSubstring.Count; j++)
                    {
                        string matchErorr = mathesSubstring[j].ToString();

                        if (matchErorr == "X" || matchErorr == "x")
                        {
                            countG |= 0x2;
                        }
                        else if (matchErorr == "Y" || matchErorr == "y")
                        {
                            countG |= 0x4;
                        }
                        else if (matchErorr == "Z" || matchErorr == "z")
                        {
                            countG |= 0x8;
                        }
                        else if (matchErorr == "A" || matchErorr == "a")
                        {
                            countG |= 0x10;
                        }
                        else if (matchErorr == "B" || matchErorr == "b")
                        {
                            countG |= 0x20;
                        }
                        else if (matchErorr == "C" || matchErorr == "c")
                        {
                            countG |= 0x40;
                        }
                        else if (matchErorr == "G63" || matchErorr == "g63")
                        {
                            countG |= 0x80;
                        }
                        else if (matchErorr == "G64" || matchErorr == "g64")
                        {
                            countG |= 0x100;
                        }
                        else if (matchErorr == "G90" || matchErorr == "g90")
                        {
                            countG |= 0x200;
                        }
                        else if (matchErorr == "G91" || matchErorr == "g91")
                        {
                            countG |= 0x400;
                        }
                        else if (matchErorr == "G17" || matchErorr == "g17")
                        {
                            countG |= 0x800;
                        }
                        else if (matchErorr == "G18" || matchErorr == "g18")
                        {
                            countG |= 0x1000;
                        }
                        else if (matchErorr == "G19" || matchErorr == "g19")
                        {
                            countG |= 0x2000;
                        }
                        else if (matchErorr == "F" || matchErorr == "f")
                        {
                            countG |= 0x1;
                        }
                    }
                }
                if (m == i)
                {
                    countG |= 0x8000;

                }
                findListNumbers.Add(countG);
            }
            return findListNumbers;
        }
        static bool isNotComment(string n)
        {
            return !n.StartsWith(";");
        }
        public static List<int> FindErrorsInString(string[] arrayWords)
        {
            List<int> findGErorrsCode = new List<int>();
            int countG = -2;

            arrayWords = Array.FindAll(arrayWords, isNotComment).ToArray();
            for (int i = 0; i < arrayWords.Length; i++)
            {
                bool G1 = false;
                bool G2 = false;
                bool G3 = false;
                bool G5 = false;
                bool G7 = false;
                bool G0 = false;
                bool F = false;
                bool G17 = false;
                bool G18 = false;
                bool G19 = false;
                bool G63 = false;
                bool G64 = false;
                bool G90 = false;
                bool G91 = false;
                float numR = 0f;
                string pattern = @"^(?!;).+$";
                string patternTwo = @"(?:[GgRr]{1,3}-?\d+(\.\d+)?|R-?\d+(\.\d+)?|[Ff])";
                Regex regex = new Regex(pattern);
                if (regex.IsMatch(arrayWords[i]))
                {
                    MatchCollection mathesSubstring = Regex.Matches(arrayWords[i], patternTwo);

                    for (int j = 0; j < mathesSubstring.Count; j++)
                    {
                        string matchErorr = mathesSubstring[j].ToString();

                        if (matchErorr == "G0" || matchErorr == "G00")
                        {
                            G0 = true;
                        }
                        else if (matchErorr == "G1" || matchErorr == "G01")
                        {
                            G1 = true;
                        }
                        else if (matchErorr == "G2" || matchErorr == "G02")
                        {
                            G2 = true;
                        }
                        else if (matchErorr == "G3" || matchErorr == "G03")
                        {
                            G3 = true;
                        }
                        else if (matchErorr == "G5" || matchErorr == "G05")
                        {
                            G5 = true;
                        }
                        else if (matchErorr == "G7" || matchErorr == "G07")
                        {
                            G7 = true;
                        }
                        else if (matchErorr == "F")
                        {
                            F = true;
                        }
                        else if (matchErorr.StartsWith("R"))
                        {
                            numR = float.Parse(matchErorr.Substring(1), CultureInfo.InvariantCulture.NumberFormat);
                        }
                        else if (matchErorr == "G17")
                        {
                            G17 = true;
                        }
                        else if (matchErorr == "G18")
                        {
                            G18 = true;
                        }
                        else if (matchErorr == "G19")
                        {
                            G19 = true;
                        }
                        else if (matchErorr == "G63")
                        {
                            G63 = true;
                        }
                        else if (matchErorr == "G64")
                        {
                            G64 = true;
                        }
                        else if (matchErorr == "G90")
                        {
                            G90 = true;
                        }
                        else if (matchErorr == "G91")
                        {
                            G91 = true;
                        }
                        if (G0)
                        {
                            countG = 0;
                        }
                        if (G1)
                        {
                            countG = 1;
                        }
                        if (G2)
                        {
                            countG = 2;
                        }
                        if (G3)
                        {
                            countG = 3;
                        }
                        if (G5)
                        {
                            countG = 4;
                        }
                        if (G7)
                        {
                            countG = 5;
                        }
                        if (G17 && G18 || G17 && G19 || G18 && G19)
                        {
                            countG = -2;
                        }
                        if (G63 && G64 || G90 && G91)
                        {
                            countG = -2;
                        }

                        if (G0 && G1 || G1 && G2 || G1 && G3 || G0 && G2 || G0 && G3 || G3 && G2)
                        {
                            countG = -2;

                            if ((G1 || G2 || G3) && F)
                            {
                                countG = 0;
                            }
                            if (G2 || G3 && numR != 0)
                            {
                                countG = 0;
                            }
                        }
                    }
                }
                findGErorrsCode.Add(countG);
                //findGErorrsCode.Add(countG);
            }
            return findGErorrsCode;
        }

        public List<uint> Find_N_kadrs(string[] arrayWrd)
        {
            List<uint> findLowByteN = new List<uint>();


            uint n = 0;
            for (int q = 0; q < arrayWrd.Length; q++)
            {
                n++;

                findLowByteN.Add(n);
            }
            return findLowByteN;
        }

        public List<uint> FindLowByte(string[] arraySlova, char patterLowerCase, char patternCapitalCase)
        {
            List<uint> findLowLByteX = new List<uint>();

            for (int x = 0; x < arraySlova.Length; x++)
            {

                uint lowerValueX = 0;
                string patternX = $@"([{Regex.Escape(patterLowerCase.ToString())}{Regex.Escape(patternCapitalCase.ToString())}][-+]?\d+(?:[.,]\d+)?)";

                MatchCollection matches = Regex.Matches(arraySlova[x], patternX);

                foreach (Match match in matches)
                {
                    Console.WriteLine(match.ToString());
                }

                if (matches == null)
                {
                    lowerValueX = 0;
                }
                for (int k = 0; k < matches.Count; k++)
                {
                    string X = matches[k].Value;
                    X = X.Trim(new char[] { patternCapitalCase });
                    X = X.Trim(new char[] { patterLowerCase });

                    float Xfloat = Convert.ToSingle(X);

                    byte[] bytes = BitConverter.GetBytes(Xfloat);

                    int intValue = BitConverter.ToInt32(bytes, 0);

                    string binaryRepresentation = Convert.ToString(intValue, 2).PadLeft(32, '0');

                    string lower16Bits = binaryRepresentation.Substring(16, 16);

                    lowerValueX = Convert.ToUInt16(lower16Bits, 2);
                }
                findLowLByteX.Add(lowerValueX);
            }
            return findLowLByteX;
        }
        public List<float> FindCoordinates(string[] arraySlova, char patterLowerCase, char patternCapitalCase)
        {
            List<float> findCoordinates = new List<float>();

            for (int q = 0; q < arraySlova.Length; q++)
            {
                float Value = 0;
                string pattern = $@"([{Regex.Escape(patterLowerCase.ToString())}{Regex.Escape(patternCapitalCase.ToString())}][-+]?\d+(?:[.,]\d+)?)";
                MatchCollection matches = Regex.Matches(arraySlova[q], pattern);

                if (matches == null)
                {
                    Value = 0;
                }

                for (int k = 0; k < matches.Count; k++)
                {
                    string X = matches[k].Value;
                    X = X.Trim(new char[] { patternCapitalCase });
                    X = X.Trim(new char[] { patterLowerCase });

                    Value = Convert.ToSingle(X);
                }
                findCoordinates.Add(Value);
            }
            return findCoordinates;
        }

        internal class DatagridItem
        {
            public int Gcode { get; set; }
            public int BitMaskDirections { get; set; }
            public int HighByteX { get; set; }
            public int LowByteX { get; set; }
            public int HighByteY { get; set; }
            public int LowByteY { get; set; }
            public int HighByteZ { get; set; }
            public int LowByteZ { get; set; }
            public int HighByteA { get; set; }
            public int LowByteA { get; set; }
            public int HighByteB { get; set; }
            public int LowByteB { get; set; }
            public int HighByteC { get; set; }
            public int LowByteC { get; set; }
            public int HighByteR { get; set; }
            public int LowByteR { get; set; }
            public int HighByteF { get; set; }
            public int LowByteF { get; set; }
            public int HighByteN { get; set; }
            public int LowByteN { get; set; }
        }
        public class ListView
        {
            public List<float> ListX { get; set; }
            public List<float> ListY { get; set; }
            public List<float> ListZ { get; set; }
            public List<float> ListA { get; set; }
            public List<float> ListB { get; set; }
            public List<float> ListC { get; set; }
            public List<float> ListR { get; set; }
            public List<float> ListF { get; set; }
            public List<uint> ListN { get; set; }

            public List<int> ListGCode { get; set; }

            public List<int> BitMaskList { get; set; }

        }
        internal class DataGridCompilate
        {
            public int numPosition { get; set; }
            public string stringError { get; set; }
        }
    }
}
