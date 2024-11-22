using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Joins;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.IO;


namespace ModbusWPFproject
{
    public partial class Window1 : Window
    {
        float codeF = 0;

        string[] arraySetting;

        public List<string> CreateSettingsArray(string[] arrayE)
        {
            List<string> listSet = new List<string>();

            for (int i = 0; i < arrayE.Length; i++)
            {
                listSet.Add(arrayE[i]);
                arrayDates = arrayDates.Where(val => val != arrayE[i]).ToArray();
                if (arrayE[i] == "%")
                {
                    listSet.Remove(arrayE[i]);
                    break;
                }
            }
            return listSet;
        }
        public string[] CreateArray(string path)
        {
            string[] arrayslov = File.ReadAllLines(path);

            return arrayslov;
        }
        public SplitFile DoSplitFile(string path)
        {
            string[] arrayD = File.ReadAllLines(path);

            SplitFile splitFile = new SplitFile();
            List<string> listE = new List<string>();
            for (int i = 0; i < arrayD.Length; i++)
            {
                listE.Add(arrayD[i]);
                arrayD = arrayD.Where(val => val != arrayD[i]).ToArray();
                if (arrayD[i] == "%")
                {
                    listE.Remove(arrayD[i]);
                    break;
                }
            }
            splitFile.arraySet = new string[listE.Count];
            splitFile.arraySet = listE.ToArray();
            splitFile.arrayGcode = new string[arrayD.Length];
            splitFile.arrayGcode = arrayD;
            return splitFile;
        }
        public ProcessedDataFile ProcessDataInFile(string[] arrayDatesFile, ModeGenerator modeG)
        {
            ProcessedDataFile processedDataFile = new ProcessedDataFile();

            processedDataFile.Listkadrov = new Listkadrov();

            processedDataFile.Listkadrov.BitMask_1 = new List<int>();

            processedDataFile.Listkadrov.BitMask_2 = new List<int>();

            processedDataFile.Listkadrov.F = new List<float>();

            processedDataFile.listCurrentString = new List<string>();
            processedDataFile.list_error_code = new List<int>();
            processedDataFile.statusCheckFile = new bool();
            processedDataFile.Listkadrov.X = new List<float>();
            processedDataFile.Listkadrov.Y = new List<float>();
            processedDataFile.Listkadrov.Z = new List<float>();
            processedDataFile.Listkadrov.A = new List<float>();
            processedDataFile.Listkadrov.B = new List<float>();
            processedDataFile.Listkadrov.C = new List<float>();

            processedDataFile.Listkadrov.R = new List<float>();
            processedDataFile.Listkadrov.I = new List<float>();
            processedDataFile.Listkadrov.J = new List<float>();
            processedDataFile.Listkadrov.N = new List<int>();
            processedDataFile.Listkadrov.E = new List<Int16>();
            processedDataFile.Listkadrov.GMove = new List<Int16>();
            processedDataFile.statusCheckFile = false;

            Int16 codeG = 0;
            float codeF = 0;
            Int16 codeE = 0;
            Int16 code64 = 0;
            Int16 code643 = 0;
            Int16 code901 = 0;

            ThreeMainParametrs threeMainParametrs = new ThreeMainParametrs();

            string currentSlovo;

            Int16 flagStatus = 0;
            Int16 flagStatus1 = 0;

            List<string> listCurrentStringFile = new List<string>();

            processedDataFile.listCurrentString = listCurrentStringFile;

            string[] arrayCheck = new string[ArrayNotCommit(arrayDatesFile).Length];

            arrayCheck = ArrayNotCommit(arrayDatesFile);

            for (int g = 0; g < arrayCheck.Length; g++)
            {
                currentSlovo = arrayCheck[g];

                threeMainParametrs = FullTreeMainParametrs(currentSlovo);

                if (threeMainParametrs.paramF.Count > 0)
                {
                    codeF = threeMainParametrs.paramF[0];
                    //processedDataFile.Listkadrov.BitMask_1[g] |= 0x1;
                }
                if (threeMainParametrs.moveG.Count == 1)
                {
                    codeG = threeMainParametrs.moveG[0];
                }
                if (threeMainParametrs.paramE.Count > 0)
                {
                    codeE = threeMainParametrs.paramE[0];
                }
                if (threeMainParametrs.adaptiveFeed.Count > 0)
                {
                    if (threeMainParametrs.adaptiveFeed[0] == 64)
                        code643 = 0;
                    if (threeMainParametrs.adaptiveFeed[0] == 63)
                        code643 = 1;
                }
                if (threeMainParametrs.programType.Count > 0)
                {
                    if (threeMainParametrs.programType[0] == 90)
                        code901 = 0;
                    if (threeMainParametrs.programType[0] == 91)
                        code901 = 1;
                }
                if (threeMainParametrs.adaptiveFeed.Count>0)
                {
                    code64 = threeMainParametrs.adaptiveFeed[0];
                }
                flagStatus1 = CheckKadrInString(threeMainParametrs, codeF, codeG, codeE, modeG);
                processedDataFile.listCurrentString.Add(currentSlovo);
                processedDataFile.list_error_code.Add(flagStatus1);

                /// 
                /// для проверки передавать codeF codeG
                //   flagStatus = CheckKadrInString(threeMainParametrs.paramF, threeMainParametrs.paramG, threeMainParametrs.paramXYZABC, threeMainParametrs.paramR, threeMainParametrs.paramJI, currentSlovo);
                //  processedDataFile.list_error_code[g]=flagStatus; //код ошибки из метода поиска ошибок
                if (flagStatus1 == 0)
                {
                    processedDataFile.Listkadrov.N.Insert(g, g + 1);
                    processedDataFile.Listkadrov.BitMask_1.Add(0);

                    processedDataFile.Listkadrov.BitMask_2.Add(0);

                    processedDataFile.Listkadrov.BitMask_1[g] |= code643 << 7;
                    processedDataFile.Listkadrov.BitMask_1[g] |= code901 << 9;

                    if (g == arrayCheck.Length - 1)
                    {
                        processedDataFile.Listkadrov.BitMask_1[g] |= 0x8000;
                    }

                    if (threeMainParametrs.paramF.Count > 0)
                    {
                        processedDataFile.Listkadrov.BitMask_1[g] |= 0x1;
                    }
                    processedDataFile.Listkadrov.F.Insert(g, codeF);
                    processedDataFile.Listkadrov.GMove.Insert(g, codeG);
                    processedDataFile.Listkadrov.E.Insert(g, codeE);
                    if (threeMainParametrs.paramX.Count > 0)
                    {
                        processedDataFile.Listkadrov.X.Insert(g, threeMainParametrs.paramX[0]);
                        processedDataFile.Listkadrov.BitMask_1[g] |= 0x2;
                        //processedDataFile.Listkadrov.BitMask_1.Insert(g,fil|=0x2);
                        ////здесь можно добавить несколь иксов если потребуется
                    }
                    else
                    {
                        processedDataFile.Listkadrov.X.Insert(g, 0);
                    }
                    if (threeMainParametrs.paramY.Count > 0)
                    {
                        processedDataFile.Listkadrov.Y.Insert(g, threeMainParametrs.paramY[0]);
                        processedDataFile.Listkadrov.BitMask_1[g] |= 0x4;
                        ////здесь можно добавить несколь иксов если потребуется
                    }
                    else
                    {
                        processedDataFile.Listkadrov.Y.Insert(g, 0);
                    }
                    if (threeMainParametrs.paramZ.Count > 0)
                    {
                        processedDataFile.Listkadrov.Z.Insert(g, threeMainParametrs.paramZ[0]);
                        processedDataFile.Listkadrov.BitMask_1[g] |= 0x8;
                        ////здесь можно добавить несколь иксов если потребуется
                    }
                    else
                    {
                        processedDataFile.Listkadrov.Z.Insert(g, 0);
                    }
                    if (threeMainParametrs.paramA.Count > 0)
                    {
                        processedDataFile.Listkadrov.A.Insert(g, threeMainParametrs.paramA[0]);
                        processedDataFile.Listkadrov.BitMask_1[g] |= 0x10;
                        ////здесь можно добавить несколь иксов если потребуется
                    }
                    else
                    {
                        processedDataFile.Listkadrov.A.Insert(g, 0);
                    }
                    if (threeMainParametrs.paramB.Count > 0)
                    {
                        processedDataFile.Listkadrov.B.Insert(g, threeMainParametrs.paramB[0]);
                        processedDataFile.Listkadrov.BitMask_1[g] |= 0x20;
                        ////здесь можно добавить несколь иксов если потребуется
                    }
                    else
                    {
                        processedDataFile.Listkadrov.B.Insert(g, 0);
                    }
                    if (threeMainParametrs.paramC.Count > 0)
                    {
                        processedDataFile.Listkadrov.C.Insert(g, threeMainParametrs.paramC[0]);
                        processedDataFile.Listkadrov.BitMask_1[g] |= 0x40;
                        ////здесь можно добавить несколь иксов если потребуется
                    }
                    else
                    {
                        processedDataFile.Listkadrov.C.Insert(g, 0);
                    }
                    if (threeMainParametrs.paramR.Count > 0)
                    {
                        processedDataFile.Listkadrov.R.Insert(g, threeMainParametrs.paramR[0]);
                        ////здесь можно добавить несколь иксов если потребуется
                    }
                    else
                    {
                        processedDataFile.Listkadrov.R.Insert(g, 0);
                    }
                    if (threeMainParametrs.paramI.Count > 0)
                    {
                        processedDataFile.Listkadrov.I.Insert(g, threeMainParametrs.paramI[0]);
                        ////здесь можно добавить несколь иксов если потребуется
                    }
                    else
                    {
                        processedDataFile.Listkadrov.I.Insert(g, 0);
                    }
                    if (threeMainParametrs.paramJ.Count > 0)
                    {
                        processedDataFile.Listkadrov.J.Insert(g, threeMainParametrs.paramJ[0]);
                        ////здесь можно добавить несколь иксов если потребуется
                    }
                    else
                    {
                        processedDataFile.Listkadrov.J.Insert(g, 0);
                    }
                    //processedDataFile.Listkadrov.Y = threeMainParametrs.paramY;
                    // processedDataFile.Listkadrov.Z = threeMainParametrs.paramZ;
                    //выполняет какой-то метод  листов с датой  наполняет регистры 0-29
                    if (threeMainParametrs.interpolationPlane.Count > 0)
                    {
                        if (threeMainParametrs.interpolationPlane[0] == 17)
                            processedDataFile.Listkadrov.BitMask_1[g] |= 0x800;
                        if (threeMainParametrs.interpolationPlane[0] == 18)
                            processedDataFile.Listkadrov.BitMask_1[g] |= 0x1000;
                        if (threeMainParametrs.interpolationPlane[0] == 19)
                            processedDataFile.Listkadrov.BitMask_1[g] |= 0x2000;
                    }
                }
                else
                {
                    //выполняет только заполнение  0 - регистра с знач. -2
                    processedDataFile.Listkadrov.GMove.Insert(g, -2);

                    processedDataFile.Listkadrov.F.Add(0);

                    processedDataFile.Listkadrov.X.Add(0);
                    processedDataFile.Listkadrov.Y.Add(0);
                    processedDataFile.Listkadrov.Z.Add(0);
                    processedDataFile.Listkadrov.A.Add(0);
                    processedDataFile.Listkadrov.B.Add(0);
                    processedDataFile.Listkadrov.C.Add(0);
                    processedDataFile.Listkadrov.R.Add(0);
                    processedDataFile.Listkadrov.I.Add(0);
                    processedDataFile.Listkadrov.J.Add(0);
                    processedDataFile.Listkadrov.BitMask_1.Add(0);
                    processedDataFile.Listkadrov.BitMask_2.Add(0);
                    processedDataFile.Listkadrov.N.Add(0);
                    processedDataFile.statusCheckFile |= true;
                }
//flagStatus = 0;
            }

            return processedDataFile;
        }

        public List<int> FillMcode(ThreeMainParametrs mainParametrs, ProcessedDataFile dataFile, int m)
        {
            dataFile.Listkadrov.BitMask_2 = new List<int>();
            dataFile.Listkadrov.BitMask_2.Add(0);

            if (mainParametrs.paramM.Count > 0)
            {

                for (int i = 0; i < mainParametrs.paramM.Count; i++)
                {
                    switch (Convert.ToInt32(mainParametrs.paramM[i]))
                    {
                        case 0:
                            dataFile.Listkadrov.BitMask_2[m - 1] |= 0x1;
                            break;
                        case 1:
                            dataFile.Listkadrov.BitMask_2[m - 1] |= 0x2;
                            break;
                        case 2:
                            dataFile.Listkadrov.BitMask_2[m - 1] |= 0x4;
                            break;
                        case 3:
                            dataFile.Listkadrov.BitMask_2[m - 1] |= 0x8;
                            break;
                        case 4:
                            dataFile.Listkadrov.BitMask_2[m - 1] |= 0x10;
                            break;
                        case 5:
                            dataFile.Listkadrov.BitMask_2[m - 1] |= 0x20;
                            break;
                        case 6:
                            dataFile.Listkadrov.BitMask_2[m - 1] |= 0x40;
                            break;
                        case 7:
                            dataFile.Listkadrov.BitMask_2[m - 1] |= 0x80;
                            break;
                        case 8:
                            dataFile.Listkadrov.BitMask_2[m - 1] |= 0x100;
                            break;
                        case 9:
                            dataFile.Listkadrov.BitMask_2[m - 1] |= 0x200;
                            break;
                        case 20:
                            dataFile.Listkadrov.BitMask_2[m - 1] |= 0x400;
                            break;
                        case 21:
                            dataFile.Listkadrov.BitMask_2[m - 1] |= 0x800;
                            break;
                        case 30:
                            dataFile.Listkadrov.BitMask_2[m - 1] |= 0x1000;
                            break;
                        case 31:
                            dataFile.Listkadrov.BitMask_2[m - 1] |= 0x2000;
                            break;
                        case 50:
                            dataFile.Listkadrov.BitMask_2[m - 1] |= 0x4000;
                            break;
                        case 51:
                            dataFile.Listkadrov.BitMask_2[m - 1] |= 0x8000;
                            break;

                    }
                }
            }
            if (mainParametrs.paramM.Count == 0)
            {
                dataFile.Listkadrov.BitMask_2[m] = 0;
            }
            return dataFile.Listkadrov.BitMask_2;
        }
        public Int16 CheckKadrInString(ThreeMainParametrs threeMain, float cdeF, Int16 cdeG, Int16 cdeE, ModeGenerator modeGenerator)
        {

            Int16 Gx = 0;
            string inputStr = "";
            Int16 numE = cdeE;
            Int16 numM = 0;
            if (threeMain.paramM.Count > 0)
            {
                numM = threeMain.paramM[0];
            }
            List<Int16> listG = new List<Int16>();
            if (threeMain.unknownFunction.Count > 0)
            {
                Gx |= 0x1;
            }
            if (threeMain.moveG.Count > 1 | threeMain.startPoint.Count > 1 | threeMain.adaptiveFeed.Count > 1 | threeMain.interpolationPlane.Count > 1 | threeMain.programType.Count > 1)
            {
                Gx |= 0x4;
            }
            else
            {
                if ((cdeG == 0 | cdeG == 1 | cdeG == 3 | cdeG == 2) & (cdeF == 0) & (threeMain.paramX.Count > 0 | threeMain.paramY.Count > 0 | threeMain.paramZ.Count > 0 | threeMain.paramA.Count > 0 | threeMain.paramB.Count > 0 | threeMain.paramC.Count > 0))
                {
                    Gx |= 0x2;
                }
                if ((cdeG == 2 | cdeG == 3) & ((threeMain.paramR.Count == 0) & (threeMain.paramJ.Count == 0 | threeMain.paramI.Count == 0)))
                {
                    Gx |= 0x2;
                }
                if (threeMain.paramX.Count > 1 | threeMain.paramY.Count > 1 | threeMain.paramZ.Count > 1 | threeMain.paramA.Count > 1 | threeMain.paramB.Count > 1 | threeMain.paramC.Count > 1 | threeMain.paramF.Count > 1)
                {
                    Gx |= 0x10;
                }
                if (threeMain.paramM.Count > 4)
                {
                    Gx |= 0x20;
                }
                if (!modeGenerator.numSetSetting.Contains(numE) & numE != 0)
                {
                    Gx |= 0x40;
                }
                if (numM == 20 & numE == 0)
                {
                    Gx |= 0x80;
                }
            }

            return Gx;
        }


        public ThreeMainParametrs FullTreeMainParametrs(string currentSlovoString)
        {
            ThreeMainParametrs threeMainParametrs = new ThreeMainParametrs();

            string currentX = null;

            string currentY = null;

            string currentZ = null;

            string currentA = null;

            string currentB = null;

            string currentC = null;

            string currentI = null;

            string currentJ = null;

            string currentF = null;

            string currentR = null;

            string currentE = null;

            string currentM = null;

            List<Int16> Gcoll = new List<Int16>();

            List<float> Fcoll = new List<float>();

            List<float> Acoll = new List<float>();
            List<float> Bcoll = new List<float>();
            List<float> Ccoll = new List<float>();
            List<float> Xcoll = new List<float>();
            List<float> Ycoll = new List<float>();
            List<float> Zcoll = new List<float>();
            List<float> Icoll = new List<float>();
            List<float> Jcoll = new List<float>();
            List<float> Rcoll = new List<float>();
            List<Int16> Ecoll = new List<Int16>();
            List<Int16> Mcoll = new List<Int16>();
            List<Int16> G_move_coll = new List<Int16>();
            List<Int16> programTypeColl = new List<Int16>();
            List<Int16> adaptFeedColl = new List<Int16>();
            List<Int16> staptPontColl = new List<Int16>();
            List<Int16> interpolationPlaneColl = new List<Int16>();
            List<Int16> UnknowFunctionColl = new List<Int16>();


            MatchCollection G = Regex.Matches(currentSlovoString, @"(?:[Gg]{1,3}-?\d+)");

            MatchCollection F = Regex.Matches(currentSlovoString, @"[F]{1,3}-?\d+(\.\d+)?|F-?\d+(\.\d+)?");

            MatchCollection R = Regex.Matches(currentSlovoString, @"[R]{1,3}-?\d+(\.\d+)?|R-?\d+(\.\d+)?");

            MatchCollection X = Regex.Matches(currentSlovoString, @"[X]{1,3}-?\d+(\.\d+)?|X-?\d+(\.\d+)?");

            MatchCollection Y = Regex.Matches(currentSlovoString, @"[Y]{1,3}-?\d+(\.\d+)?|Y-?\d+(\.\d+)?");

            MatchCollection Z = Regex.Matches(currentSlovoString, @"[Z]{1,3}-?\d+(\.\d+)?|Z-?\d+(\.\d+)?");

            MatchCollection A = Regex.Matches(currentSlovoString, @"[A]{1,3}-?\d+(\.\d+)?|A-?\d+(\.\d+)?");

            MatchCollection B = Regex.Matches(currentSlovoString, @"[B]{1,3}-?\d+(\.\d+)?|B-?\d+(\.\d+)?");

            MatchCollection C = Regex.Matches(currentSlovoString, @"[C]{1,3}-?\d+(\.\d+)?|C-?\d+(\.\d+)?");

            MatchCollection I = Regex.Matches(currentSlovoString, @"[I]{1,3}-?\d+(\.\d+)?|I-?\d+(\.\d+)?");

            MatchCollection J = Regex.Matches(currentSlovoString, @"[J]{1,3}-?\d+(\.\d+)?|J-?\d+(\.\d+)?");

            MatchCollection E = Regex.Matches(currentSlovoString, @"[E]{1,3}-?\d+(\.\d+)?|E-?\d+(\.\d+)?");

            MatchCollection M = Regex.Matches(currentSlovoString, @"[M]{1,3}-?\d+(\.\d+)?|M-?\d+(\.\d+)?");

            Match G54 = Regex.Match(currentSlovoString, @"([G][5][4]|[G][5][5]|[G][5][6]|[G][5][7]|[G][5][8])");

            Match JI = Regex.Match(currentSlovoString, @"([JI])");


            foreach (Match math in G)
            {
                Gcoll.Add(Convert.ToInt16(math.ToString().Trim(new char[] { 'G' })));
            }
            foreach (Match math in G)
            {
                Int16 g = Convert.ToInt16(math.ToString().Trim(new char[] { 'G' }));

                if (g != 1 & g != 2 & g != 3 & g != 0 & g != 72 & g != 73 & g != 68 & g != 69 & g != 54 & g != 55 & g != 56 & g != 57 & g != 58 & g != 17 & g != 18 & g != 19 & g != 90 & g != 91 & g != 79 & g != 63 & g != 64)
                {
                    UnknowFunctionColl.Add(g);
                }
                else
                {
                    if (g == 1 | g == 2 | g == 3 | g == 0 | g == 72 | g == 73 | g == 68 | g == 69)
                    {
                        G_move_coll.Add(g);
                    }
                    if (g == 54 | g == 55 | g == 56 | g == 57 | g == 58)
                    {
                        staptPontColl.Add(g);
                    }
                    if (g == 17 | g == 18 | g == 19)
                    {
                        interpolationPlaneColl.Add(g);
                    }
                    if (g == 64 | g == 63)
                    {
                        adaptFeedColl.Add(g);
                    }
                    if (g == 90 | g == 91 | g == 79)
                    {
                        programTypeColl.Add(g);
                    }
                }

            }

            foreach (Match mathR in R)
            {
                currentR = Convert.ToString(mathR.ToString());
                currentR = currentR.Trim(new char[] { 'R' });
                currentR = currentR.Replace(".", ",");
                Rcoll.Add(Convert.ToSingle(currentR));
            }
            foreach (Match math in F)
            {
                currentF = Convert.ToString(math.ToString());
                currentF = currentF.Trim(new char[] { 'F' });
                currentF = currentF.Replace(".", ",");
                Fcoll.Add(Convert.ToSingle(currentF));
            }
            foreach (Match mathX in X)
            {
                currentX = Convert.ToString(mathX.ToString());
                currentX = currentX.Trim(new char[] { 'X' });
                currentX = currentX.Replace(".", ",");
                Xcoll.Add(Convert.ToSingle(currentX));
            }
            foreach (Match mathY in Y)
            {
                currentY = Convert.ToString(mathY.ToString());
                currentY = currentY.Trim(new char[] { 'Y' });
                currentY = currentY.Replace(".", ",");
                Ycoll.Add(Convert.ToSingle(currentY));
            }
            foreach (Match mathZ in Z)
            {
                currentZ = Convert.ToString(mathZ.ToString());
                currentZ = currentZ.Trim(new char[] { 'Z' });
                currentZ = currentZ.Replace(".", ",");
                Zcoll.Add(Convert.ToSingle(currentZ));
            }
            foreach (Match mathA in A)
            {
                currentA = Convert.ToString(mathA.ToString());
                currentA = currentA.Trim(new char[] { 'A' });
                currentA = currentA.Replace(".", ",");
                Acoll.Add(Convert.ToSingle(currentA));
            }
            foreach (Match mathB in B)
            {
                currentB = Convert.ToString(mathB.ToString());
                currentB = currentB.Trim(new char[] { 'B' });
                currentB = currentB.Replace(".", ",");
                Bcoll.Add(Convert.ToSingle(currentA));
            }
            foreach (Match mathC in C)
            {
                currentC = Convert.ToString(mathC.ToString());
                currentC = currentC.Trim(new char[] { 'C' });
                currentC = currentC.Replace(".", ",");
                Ccoll.Add(Convert.ToSingle(currentC));
            }
            foreach (Match mathI in I)
            {
                currentI = Convert.ToString(mathI.ToString());
                currentI = currentI.Trim(new char[] { 'I' });
                currentI = currentI.Replace(".", ",");
                Icoll.Add(Convert.ToSingle(currentI));
            }
            foreach (Match mathJ in J)
            {
                currentJ = Convert.ToString(mathJ.ToString());
                currentJ = currentJ.Trim(new char[] { 'J' });
                currentJ = currentJ.Replace(".", ",");
                Jcoll.Add(Convert.ToSingle(currentJ));
            }
            foreach (Match mathE in E)
            {
                currentE = Convert.ToString(mathE.ToString());
                currentE = currentE.Trim(new char[] { 'E' });
                Ecoll.Add(Convert.ToInt16(currentE));
            }
            foreach (Match mathM in M)
            {
                currentM = Convert.ToString(mathM.ToString());
                currentM = currentM.Trim(new char[] { 'M' });
                Mcoll.Add(Convert.ToInt16(currentM));
            }

            threeMainParametrs.paramF = Fcoll;
            threeMainParametrs.paramX = Xcoll;
            threeMainParametrs.paramY = Ycoll;
            threeMainParametrs.paramZ = Zcoll;
            threeMainParametrs.paramA = Acoll;
            threeMainParametrs.paramB = Bcoll;
            threeMainParametrs.paramC = Ccoll;
            threeMainParametrs.paramR = Rcoll;
            threeMainParametrs.paramI = Icoll;
            threeMainParametrs.paramJ = Jcoll;
            threeMainParametrs.paramE = Ecoll;
            threeMainParametrs.paramM = Mcoll;
            threeMainParametrs.moveG = G_move_coll;
            threeMainParametrs.startPoint = staptPontColl;
            threeMainParametrs.interpolationPlane = interpolationPlaneColl;
            threeMainParametrs.adaptiveFeed = adaptFeedColl;
            threeMainParametrs.programType = programTypeColl;
            threeMainParametrs.unknownFunction = UnknowFunctionColl;

            return threeMainParametrs;
        }
        public string[] ArrayNotCommit(string[] array)
        {

            Patterns patterns = new Patterns();
            string patternCommit = patterns.patternSearchCommentFiles;

            List<string> preArray = new List<string>();

            Regex regCommit = new Regex(patternCommit);

            for (int c = 0; c < array.Length; c++)
            {
                MatchCollection MatColl = regCommit.Matches(array[c]);

                foreach (Match m in MatColl)
                {
                    preArray.Add(m.Value);
                }
            }

            string[] postArray = new string[preArray.Count];
            postArray = preArray.ToArray();

            return postArray;

        }
        public class ThreeMainParametrs
        {
            public List<Int16> adaptiveFeed { get; set; }

            public List<Int16> unknownFunction { get; set; }
            public List<Int16> moveG { get; set; }
            public List<Int16> programType { get; set; }

            public List<Int16> interpolationPlane { get; set; }
            public List<Int16> startPoint { get; set; }

            public List<float> paramF { get; set; }

            public string paramWorkCoord { get; set; }

            public List<float> paramX { get; set; }

            public List<float> paramY { get; set; }
            public List<float> paramZ { get; set; }

            public List<float> paramA { get; set; }

            public List<float> paramB { get; set; }

            public List<float> paramC { get; set; }

            public List<float> paramJ { get; set; }
            public List<float> paramI { get; set; }

            public List<float> paramR { get; set; }

            public List<Int16> paramE { get; set; }

            public List<Int16> paramM { get; set; }

        }
        public class ProcessedDataFile
        {
            public List<string> listCurrentString { get; set; }
            public Listkadrov Listkadrov { get; set; }

            public List<int> list_error_code { get; set; }

            public bool statusCheckFile { get; set; }

        }
        public class SplitFile
        {
            public string[] arrayGcode { get; set; }
            public string[] arraySet { get; set; }
        }
        public class Listkadrov
        {

            public List<Int16> GMove { get; set; }
            public List<int> BitMask_1 { get; set; }

            public List<int> BitMask_2 { get; set; }

            public List<float> F { get; set; }

            public List<float> X { get; set; }

            public List<float> Y { get; set; }

            public List<float> Z { get; set; }

            public List<float> A { get; set; }
            public List<float> B { get; set; }

            public List<float> C { get; set; }

            public List<float> R { get; set; }

            public List<float> I { get; set; }

            public List<float> J { get; set; }

            public List<int> N { get; set; }
            public List<Int16> E { get; set; }
        }
        public class ModeGenerator
        {
            public List<ListModeGenerator> listModeGenerators { get; set; }

            public bool statusSetSettting { get; set; }
            public List<int> codeError { get; set; }

            public List<Int16> numSetSetting { get; set; }
        }

        public class ListModeGenerator
        {
            public Int16 ustavkaVolts { get; set; }
            public float gainFactor { get; set; }
            public Int16 frequancyPulse { get; set; }
            public float dutyCycle { get; set; }
            public Int16 operatingCurrent { get; set; }
            public Int16 arsonCurrent { get; set; }
            public Int16 proctectiveCurrent { get; set; }
            public Int16 packageDuration { get; set; }
            public Int16 pauseDuration { get; set; }
            public Int16 titaniumPulseDuration { get; set; }
            public Int16 capacitorCapacity { get; set; }
            public float relaxationDistance { get; set; }
            public Int16 relaxationDuration { get; set; }
        }
        public bool CheckTemporaryNum(string[] arrayTemp) // надо добавить коэф усил !!!!!!!!!! :О
        {
            Int16 modeVolt = Convert.ToInt16(arrayTemp[0]);
            Int16 frequancyp = Convert.ToInt16(arrayTemp[1]);
            float dutyCycle = Convert.ToSingle(arrayTemp[2].Replace(".", ","));
            Int16 operatingCurrent = Convert.ToInt16(arrayTemp[3]);
            Int16 arsonCurrent = Convert.ToInt16(arrayTemp[4]);
            Int16 proctectiveCurrent = Convert.ToInt16(arrayTemp[5]);
            Int16 packageDuration = Convert.ToInt16(arrayTemp[6]);
            Int16 pauseDuration = Convert.ToInt16(arrayTemp[7]);
            Int16 titaniumPulseDuration = Convert.ToInt16(arrayTemp[8]);
            Int16 capacitorCapacity = Convert.ToInt16(arrayTemp[9]);
            float relaxationDistance = Convert.ToSingle(arrayTemp[10].Replace(".", ","));
            Int16 relaxationDuration = Convert.ToInt16(arrayTemp[11]);
            bool statusTemporaryNum = false;

            if (CheckAllRanges(0, 250, modeVolt, 1, 20000, frequancyp) &
                CheclAllFloatRanges(1.05f, 6.6f, dutyCycle, 0.1f, 20, relaxationDistance)
                & CheckAllRanges(0, 100, operatingCurrent, 0, 15, arsonCurrent) &
                CheckAllRanges(10, 20000, packageDuration, 10, 20000, pauseDuration)
                & CheckAllRanges(0, 100, titaniumPulseDuration, 0, 3, capacitorCapacity) &
                CheckAllRanges(0, 10000, relaxationDuration, 0, 20, proctectiveCurrent))
            {
                statusTemporaryNum = false;
            }
            else
            {
                statusTemporaryNum = true;
            }
            return statusTemporaryNum;
        }


        bool CheckRange(double min, double max, double value)
        {
            return (value < min | value > max);
        }
        bool CheckAllRanges(int min1, int max1, int val1, int min2, int max2, int val2)
        {
            return CheckRange(min1, max1, val1) & CheckRange(min2, max2, val2);
        }

        Int16 CheckFloatRanges(float value_f, float min, float max)
        {

            return Convert.ToInt16((value_f < min | value_f > max));
        }
        Int16 CheckInt16Ranges(Int16 value_i, Int16 min, Int16 max)
        {

            return Convert.ToInt16((value_i < min | value_i > max));
        }

        bool CheclAllFloatRanges(float min1, float max1, float val1, float min2, float max2, float val2)
        {
            return ((val1 >= min1 & val1 <= max1) & (val2 >= min2 & val2 <= max2));
        }

        public ModeGenerator FillModegenerator(List<string> listSetGen)
        {
            ModeGenerator modeGenerator = new ModeGenerator();
            modeGenerator.listModeGenerators = new List<ListModeGenerator>();

            modeGenerator.numSetSetting = new List<Int16>();

            modeGenerator.codeError = new List<int>();

            string[] patternSet = {
                "[U]{1,3}-?\\d+(\\.\\d+)?|C-?\\d+(\\.\\d+)?",
                "[Kv]{1,3}-?\\d+(\\.\\d+)?|C-?\\d+(\\.\\d+)?",
                "[F]{1,3}-?\\d+(\\.\\d+)?|C-?\\d+(\\.\\d+)?",
                "[S]{1,3}-?\\d+(\\.\\d+)?|C-?\\d+(\\.\\d+)?",
                "[Iw]{1,3}-?\\d+(\\.\\d+)?|C-?\\d+(\\.\\d+)?",
                "[Ip]{1,3}-?\\d+(\\.\\d+)?|C-?\\d+(\\.\\d+)?",
                "[Iz]{1,3}-?\\d+(\\.\\d+)?|C-?\\d+(\\.\\d+)?",
                "[Pc]{1,3}-?\\d+(\\.\\d+)?|C-?\\d+(\\.\\d+)?",
                "[Pz]{1,3}-?\\d+(\\.\\d+)?|C-?\\d+(\\.\\d+)?",
                "[Tti]{1,3}-?\\d+(\\.\\d+)?|C-?\\d+(\\.\\d+)?",
                "[C]{1,3}-?\\d+(\\.\\d+)?|C-?\\d+(\\.\\d+)?",
                "[Lr]{1,3}-?\\d+(\\.\\d+)?|C-?\\d+(\\.\\d+)?",
                "[Tr]{1,3}-?\\d+(\\.\\d+)?|C-?\\d+(\\.\\d+)?"
            };

            for (int i = 0; i < listSetGen.Count; i++)
            {
                ListModeGenerator InitParams = new ListModeGenerator();
                InitParams.ustavkaVolts = -1;
                InitParams.gainFactor = -1;
                InitParams.frequancyPulse = -1;
                InitParams.dutyCycle = -1;
                InitParams.proctectiveCurrent = -1;
                InitParams.arsonCurrent = -1;
                InitParams.operatingCurrent = -1;
                InitParams.packageDuration = -1;
                InitParams.pauseDuration = -1;
                InitParams.titaniumPulseDuration = -1;
                InitParams.capacitorCapacity = -1;
                InitParams.relaxationDistance = -1;
                InitParams.relaxationDuration = -1;

                modeGenerator.listModeGenerators.Add(InitParams);

                string element = listSetGen[i];

                string eNum = element.Substring(element.IndexOf("e"), element.IndexOf("[") - element.IndexOf("e")).Trim('e');

                if (!modeGenerator.numSetSetting.Contains(Convert.ToInt16(eNum)))
                {
                    modeGenerator.numSetSetting.Add(Convert.ToInt16(eNum));
                }

                element = (element.Substring(element.IndexOf("["), element.IndexOf("]") - element.IndexOf("["))).Trim('[');

                string[] arrayMode = element.Split(';');

                if (arrayMode.Length == 13)
                {
                    modeGenerator.codeError.Add(0);

                    for (int j = 0; j <= 12; j++)
                    {
                        Match matchSet = Regex.Match(arrayMode[j], patternSet[j]);

                        if (matchSet.Length != 0)
                        {
                            switch (j)
                            {
                                case 0:
                                    modeGenerator.listModeGenerators[i].ustavkaVolts = Convert.ToInt16(matchSet.ToString().Trim(new char[] { 'U' }));
                                    break;
                                case 1:
                                    modeGenerator.listModeGenerators[i].gainFactor = Convert.ToSingle(matchSet.ToString().Trim(new char[] { 'K', 'v' }).Replace(".", ","));
                                    break;
                                case 2:
                                    modeGenerator.listModeGenerators[i].frequancyPulse = Convert.ToInt16(matchSet.ToString().Trim(new char[] { 'F' }));
                                    break;
                                case 3:
                                    modeGenerator.listModeGenerators[i].dutyCycle = Convert.ToSingle(matchSet.ToString().Trim(new char[] { 'S' }).Replace(".", ","));
                                    break;
                                case 4:
                                    modeGenerator.listModeGenerators[i].operatingCurrent = Convert.ToInt16(matchSet.ToString().Trim(new char[] { 'I', 'w' }));
                                    break;
                                case 5:
                                    modeGenerator.listModeGenerators[i].arsonCurrent = Convert.ToInt16(matchSet.ToString().Trim(new char[] { 'I', 'p' }));
                                    break;
                                case 6:
                                    modeGenerator.listModeGenerators[i].proctectiveCurrent = Convert.ToInt16(matchSet.ToString().Trim(new char[] { 'I', 'z' }));
                                    break;
                                case 7:
                                    modeGenerator.listModeGenerators[i].packageDuration = Convert.ToInt16(matchSet.ToString().Trim(new char[] { 'P', 'c' }));
                                    break;
                                case 8:
                                    modeGenerator.listModeGenerators[i].pauseDuration = Convert.ToInt16(matchSet.ToString().Trim(new char[] { 'P', 'z' }));
                                    break;
                                case 9:
                                    modeGenerator.listModeGenerators[i].titaniumPulseDuration = Convert.ToInt16(matchSet.ToString().Trim(new char[] { 'T', 't', 'i' }));
                                    break;
                                case 10:
                                    modeGenerator.listModeGenerators[i].capacitorCapacity = Convert.ToInt16(matchSet.ToString().Trim(new char[] { 'C' }));
                                    break;
                                case 11:
                                    modeGenerator.listModeGenerators[i].relaxationDistance = Convert.ToSingle(matchSet.ToString().Trim(new char[] { 'L', 'r' }).Replace(".", ","));
                                    break;
                                case 12:
                                    modeGenerator.listModeGenerators[i].relaxationDuration = Convert.ToInt16(matchSet.ToString().Trim(new char[] { 'T', 'r' }));
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                }
                else
                {
                    modeGenerator.statusSetSettting |= true;
                    modeGenerator.codeError.Add(1);
                }
                // List<Int16> fas =  CheckArraySetting(arrayMode);

                modeGenerator.codeError[i] |= CheckInt16Ranges(modeGenerator.listModeGenerators[i].ustavkaVolts, 0, 250) << 1;
                modeGenerator.codeError[i] |= CheckFloatRanges(modeGenerator.listModeGenerators[i].gainFactor, 0, 6) << 2;
                modeGenerator.codeError[i] |= CheckInt16Ranges(modeGenerator.listModeGenerators[i].frequancyPulse, 1, 500) << 3;
                modeGenerator.codeError[i] |= CheckFloatRanges(modeGenerator.listModeGenerators[i].dutyCycle, 1.05f, 6.6f) << 4;
                modeGenerator.codeError[i] |= CheckInt16Ranges(modeGenerator.listModeGenerators[i].operatingCurrent, 0, 100) << 5;
                modeGenerator.codeError[i] |= CheckInt16Ranges(modeGenerator.listModeGenerators[i].arsonCurrent, 0, 15) << 6;
                modeGenerator.codeError[i] |= CheckInt16Ranges(modeGenerator.listModeGenerators[i].proctectiveCurrent, 0, 20) << 7;
                modeGenerator.codeError[i] |= CheckInt16Ranges(modeGenerator.listModeGenerators[i].packageDuration, 10, 20000) << 8;
                modeGenerator.codeError[i] |= CheckInt16Ranges(modeGenerator.listModeGenerators[i].pauseDuration, 10, 20000) << 9;
                modeGenerator.codeError[i] |= CheckInt16Ranges(modeGenerator.listModeGenerators[i].titaniumPulseDuration, 0, 100) << 10;
                modeGenerator.codeError[i] |= CheckInt16Ranges(modeGenerator.listModeGenerators[i].capacitorCapacity, 0, 3) << 11;
                modeGenerator.codeError[i] |= CheckFloatRanges(modeGenerator.listModeGenerators[i].relaxationDistance, 0.1f, 20f) << 12;
                modeGenerator.codeError[i] |= CheckInt16Ranges(modeGenerator.listModeGenerators[i].relaxationDuration, 0, 10000) << 13;
            }
            return modeGenerator;

        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            List<string> list = new List<string>();

            list = CreateSettingsArray(arrayDates);

            /* string se = list[0];
             se = se.Remove(0, 6);
             se = se.Trim(new char[] { '[', ']' });
             string[] arraySE = se.Split(new char[] { ';' });
             string sa = list[1];          



             sa = (sa.Substring(sa.IndexOf("["), sa.IndexOf("]")-sa.IndexOf("["))).Trim('[');*/

            ProcessedDataFile dataFile = new ProcessedDataFile();
            ProcessedDataFile dataFileInit = new ProcessedDataFile();

            ModeGenerator modeGenerator = new ModeGenerator();

            modeGenerator = FillModegenerator(list);
            ProcessDataInFile(arrayDates, modeGenerator);
            dataFile = ProcessDataInFile(arrayDates, modeGenerator);
        }
    }
}

