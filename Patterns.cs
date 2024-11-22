using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModbusWPFproject
{
    internal class Patterns
    {
        static string G2 = @"^.*\bG2\b.*\bG3\b.*$|^.*\bG2\b.*\bG1\b.*$|^.*\bG2\b.*\bG0\b.*$|^.*\bG2\b.*\bG72\b.*$|^.*\bG2\b.*\bG73\b.*$|^.*\bG2\b.*\bG68\b.*$|^.*\bG2\b.*\bG69\b.*$";
        static string G1 = @"^.*\bG1\b.*\bG3\b.*$|^.*\bG1\b.*\bG2\b.*$|^.*\bG1\b.*\bG0\b.*$|^.*\bG1\b.*\bG72\b.*$|^.*\bG1\b.*\bG73\b.*$|^.*\bG1\b.*\bG68\b.*$|^.*\bG1\b.*\bG69\b.*$";
        static string G3 = @"^.*\bG3\b.*\bG0\b.*$|^.*\bG3\b.*\bG2\b.*$|^.*\bG3\b.*\bG1\b.*$|^.*\bG3\b.*\bG72\b.*$|^.*\bG3\b.*\bG73\b.*$|^.*\bG3\b.*\bG68\b.*$|^.*\bG3\b.*\bG69\b.*$";
        static string G0 = @"^.*\bG0\b.*\bG1\b.*$|^.*\bG0\b.*\bG2\b.*$|^.*\bG0\b.*\bG3\b.*$|^.*\bG0\b.*\bG72\b.*$|^.*\bG0\b.*\bG73\b.*$|^.*\bG0\b.*\bG68\b.*$|^.*\bG0\b.*\bG69\b.*$";
        static string G72 = @"^.*\bG72\b.*\bG1\b.*$|^.*\bG72\b.*\bG2\b.*$|^.*\bG72\b.*\bG3\b.*$|^.*\bG72\b.*\bG0\b.*$|^.*\bG72\b.*\bG73\b.*$|^.*\bG72\b.*\bG68\b.*$|^.*\bG72\b.*\bG69\b.*$";
        static string G73 = @"^.*\bG73\b.*\bG1\b.*$|^.*\bG73\b.*\bG2\b.*$|^.*\bG73\b.*\bG3\b.*$|^.*\bG73\b.*\bG0\b.*$|^.*\bG73\b.*\bG72\b.*$|^.*\bG73\b.*\bG68\b.*$|^.*\bG73\b.*\bG69\b.*$";
        static string G68 = @"^.*\bG68\b.*\bG1\b.*$|^.*\bG68\b.*\bG2\b.*$|^.*\bG68\b.*\bG3\b.*$|^.*\bG68\b.*\bG0\b.*$|^.*\bG68\b.*\bG73\b.*$|^.*\bG68\b.*\bG69\b.*$|^.*\bG68\b.*\bG72\b.*$";
        static string G69 = @"^.*\bG69\b.*\bG1\b.*$|^.*\bG69\b.*\bG2\b.*$|^.*\bG69\b.*\bG3\b.*$|^.*\bG69\b.*\bG0\b.*$|^.*\bG69\b.*\bG73\b.*$|^.*\bG69\b.*\bG68\b.*$|^.*\bG69\b.*\bG72\b.*$";



        public string patternSearchErrorsFiles = G0 + G1 + G2 + G3 + G68 + G69 + G72 + G73;

        public string patternSearchCircle = @"^.*\bG3\b(?!.*\bR\d+\b).*$|^.*\bG2\b(?!.*\bR\d+\b).*$";

        public string patternSearchLineCircle = @"^.*\bG3\b(?!.*\b[IJ]\d+).*$|^.*\bG2\b(?!.*\b[IJ]\d+\b).*$";


        public string patternSerchBitMask = @"(?:[G,g]{1,3}-?\d+|X|x|Y|y|Z|z|A|a|B|b|C|c|F|f)";

        public string patternSearchCommentFiles = @"^(?!;).+$";

        public string pattermSearchCirCode = @"^.*\bG3\b.||\bG2\b.*$";
    }
}
