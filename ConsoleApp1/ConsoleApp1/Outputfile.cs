using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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
            sw = new StreamWriter("sudoku.txt",true);
        }

        public void Out(string c)
        {
            //if(sw.) sw = new StreamWriter("D:/repository/SE/sudoku.txt", true);
            sw.WriteLine(c);
        }

        public void Close()
        {
            sw.Flush();
            sw.Close();
        }
    }
}

