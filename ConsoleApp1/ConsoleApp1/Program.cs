
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        
        static void Main(string[] args)
        {
            Outputfile OP = new Outputfile();
            if (args.Length == 3)
            {
                //makesudu
                if (args[1] == "-c" )
                {
                    Makesudu ms = new Makesudu(int.Parse(args[2]),OP);
                    ms.Make();
                }

                //dealsudu
                else if (args[1] == "-s")
                {
                    if (File.Exists(args[2]))
                    {
                        Dealsudu ds = new Dealsudu(args[2],OP);
                        ds.Deal();

                    }
                    else return;
                }
            }
            

            else
            {
                Console.WriteLine("else");
                
                return;
            }

        }
    }
}
