using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Sudu1
{
    class Dealsudu
    {
        string filename;
        FileStream F;
        StreamReader R;

        public Dealsudu(string s)
        {
            filename = s; 
            F = new FileStream(filename,FileMode.Open);
            R = new StreamReader(F);
        }

        public void Deal()
        {
            string line;
            while ((line = R.ReadLine()) != null)
            {
                char[] chars = line.ToCharArray();
                int[,] juzhen=new int[9,9];
                for(int i=1;i<82 ;i++)
                {
                    juzhen[i % 9,i / 9] = Convert.ToInt32(chars[i]);
                }
                //juzhen is already
                
            }
        }

        private void Solve()
        {

        }

    }
}
