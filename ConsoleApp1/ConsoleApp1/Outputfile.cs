using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Outputfile
    {
        //FileStream fs;
        StreamWriter sw;
        

        public Outputfile()
        {
            //fs = new FileStream("D:/repository/SE/sudoku.txt", FileMode.Open);
            FileStream fs = new FileStream("sudoku.txt", FileMode.Create);
            sw = new StreamWriter(fs);
        }

        public void Out(string c)
        {
            //if(sw.) sw = new StreamWriter("D:/repository/SE/sudoku.txt", true);
            //c.Replace(".{4}(?!$)", "$0 ");

            string result = Regex.Replace(c, @"(\d{9})", "$1\r\n");
            string result1 = Regex.Replace(result, @"(\d{1})", "$1 ");
            
            sw.WriteLine(result1);
        }

        public void Close()
        {
            sw.Flush();
            sw.Close();
        }
    }
}

