using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using ConsoleApp1;

namespace ConsoleApp1
{
    class Dealsudu
    {
        string filename;
        FileStream F;
        StreamReader R;
        Outputfile OP;
        Boolean M = false;

        public Dealsudu(string s,Outputfile OP)
        {
            filename = s; 
            F = new FileStream(filename,FileMode.Open);
            R = new StreamReader(F);
            this.OP = OP;
        }

        public void Deal()
        {
            string line;
            while ((line = R.ReadLine()) != null)
            {
                M = false;
                char[] chars = line.ToCharArray();
                int[,] juzhen=new int[9,9];//存入开始状态的数独
                for(int i=0;i<81 ;i++)
                {
                    juzhen[i / 9,i % 9] = (int)chars[i]-48;
                }
                //juzhen is already
               Xuanji[,] kexuan=new Xuanji[9,9];//存入可选数
                for(int i=0;i<9 ;i++)
                {
                    for(int j=0;j<9 ;j++)
                    {
                        kexuan[i, j] = new Xuanji();
                        for (int k=1;k<10 ;k++)
                        {
                            kexuan[i, j].Set(i, j);
                            kexuan[i, j].Add(k);
                        }
                    }
                }
                //可选数初始化
                for(int i=0;i<9 ;i++)
                {
                    for(int j=0;j<9;j++)
                    {
                        if (juzhen[i, j] != 0) Saichu(i, j, juzhen[i, j], kexuan);
                    }
                }
                //进行解
                Solve(juzhen, kexuan);
                string sss = "";
                for (int i = 0; i < 9; i++)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        //Console.Write(juzhen[i, j]);
                        sss += juzhen[i,j];
                        //sss += " ";
                    }
                }
                OP.Out(sss);
                //OP.Out("\n");
                
                //Console.Read();
            }
            OP.Close();
        }

        private void Solve(int[,] sudu,Xuanji[,] kexuan)
        {
            if (M) return;
            M = Tianman(sudu);
            Xuanji X = Findshortest(sudu, kexuan);
            int length = X.list.Count;
            int j;
            for(j=0;j<length ;j++)
            {
                int i = X.list[0];//因为每次add都加到末端
                sudu[X.x, X.y] = i;
                //Console.WriteLine("in" + (X.x+1) + " " + (X.y+1)+" "+i);
                List<Xuanji> change= Saichu(X.x, X.y, i, kexuan);
                Solve(sudu, kexuan);
                if (M) break;
                //Console.WriteLine("out" + (X.x+1) + " " + (X.y+1)+" "+i);
                Jiahui(change,i);
            }
            if (!M) sudu[X.x, X.y] = 0;
        }

        private List<Xuanji> Saichu(int x,int y,int s, Xuanji[,] L)//筛除可选数
        {
            List<Xuanji> gaidong =new List<Xuanji>();
            L[x, y].Remove(s);
            gaidong.Add(L[x, y]);
            for (int i=0;i<9;i++)
            {
                if (L[x, i].Contains(s))
                {
                    L[x, i].Remove(s);
                    gaidong.Add(L[x, i]);
                }
                if (L[i, y].Contains(s))
                {
                    L[i, y].Remove(s);
                    gaidong.Add(L[i,y]);
                }
            }
            Addin9(x, y, gaidong, L, s);
            return gaidong;
        }

        private void Addin9(int x,int y,List<Xuanji> gaidong,Xuanji[,] L,int s)
        {
            int i, imax;
            int j, jmax;
            if (x < 3)
            {
                i = 0;
                imax = 3;
            }
            else if (x < 6)
            {
                i = 3;
                imax = 6;
            }
            else
            {
                i = 6;
                imax = 9;
            }

            if (y < 3)
            {
                j = 0;
                jmax = 3;
            }
            else if (y < 6)
            {
                j = 3;
                jmax = 6;
            }
            else
            {
                j = 6;
                jmax = 9;
            }
            for (int ii=i; ii < imax; ii++)
            {
                for (int jj=j; jj < jmax; jj++)
                {
                    if (L[ii, jj].Contains(s))
                    {
                        L[ii, jj].Remove(s);
                        gaidong.Add(L[ii, jj]);
                    }
                }
            }
        }

        private Xuanji Findshortest(int[,] sudu,Xuanji[,] L)//找到可填入最少的点
        {
            int sl = 9;
            int x=new int();
            int y=new int();
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (sudu[i, j] == 0)
                    {
                        if (L[i, j].Count() < sl)
                        {
                            sl = L[i, j].Count();
                            x = i;
                            y = j;
                        }
                    }
                }
            }
            return L[x, y];
        }

        private void Jiahui(List<Xuanji> change,int s)//加回可选数
        {
            foreach(Xuanji i in change)
            {
                i.Add(s);
            }
        }

        private bool Tianman(int[,] sudu)
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (sudu[i,j]==0) return false;
                }
            }
            return true;
        }

    }
}
