using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Sudu1
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 3)
            {
                //makesudu
                if (args[1] == "-c" & Regex.IsMatch(args[2], @"^[+-]?/d*$"))
                {

                }

                //dealsudu
                else if (args[1] == "-s")
                {
                    if (File.Exists(args[2]))
                    {
                        Dealsudu ds = new Dealsudu(args[2]);
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
