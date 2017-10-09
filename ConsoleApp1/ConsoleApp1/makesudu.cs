using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Makesudu
    {
        int t;
        int S = 500000000;
        Outputfile OP;

        public Makesudu(int s, Outputfile OP)
        {
            t = s;
            this.OP = OP;
        }

        public void Make()
        {
            
            int y = S;
            string ss = y.ToString();

            char[] chars = ss.ToCharArray();
            int[,] juzhen = new int[9, 9];//存入开始状态的数独
            for (int i = 0; i < 9; i++)
            {
                juzhen[i / 9, i % 9] = (int)chars[i] - 48;
            }
            for (int i = 9; i < 81; i++)
            {
                juzhen[i / 9, i % 9] = 0;
            }
            //juzhen is already
            Xuanji[,] kexuan = new Xuanji[9, 9];//存入可选数
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    kexuan[i, j] = new Xuanji();
                    for (int k = 1; k < 10; k++)
                    {
                        kexuan[i, j].Set(i, j);
                        kexuan[i, j].Add(k);
                    }
                }
            }
            //可选数初始化
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (juzhen[i, j] != 0) Saichu(i, j, juzhen[i, j], kexuan);
                }
            }
            //进行解
            Solve(juzhen, kexuan);
            //print
            //Printjuzhen(juzhen);
            int tt = 0;
            //Duoprint(t, tt, 4096, juzhen);
            //Console.Read();
            List<int> l = new List<int>();
            l.Add(1);
            l.Add(2);
            l.Add(3);
            l.Add(4);
            //l.Add(5);
            l.Add(6);
            l.Add(7);
            l.Add(8);
            l.Add(9);
            for (int a=0;a<8;a++)
            {
                List<int> la= new List<int>(l.ToArray());
                List<int> l1 = new List<int>();
                l1.Add(la[a]);
                la.Remove(la[a]);
                for (int b = 0; b < 7; b++)
                {
                    List<int> lb = new List<int>(la.ToArray());
                    List<int> l2 = new List<int>(l1.ToArray());
                    l2.Add(lb[b]);
                    lb.Remove(lb[b]);
                    for (int c = 0; c < 6; c++)
                    {
                        List<int> lc = new List<int>(lb.ToArray());
                        List<int> l3 = new List<int>(l2.ToArray());
                        l3.Add(lc[c]);
                        lc.Remove(lc[c]);
                        for (int d = 0; d < 5; d++)
                        {
                            List<int> ld = new List<int>(lc.ToArray());
                            List<int> l4 = new List<int>(l3.ToArray());
                            l4.Add(lb[b]);
                            ld.Remove(ld[d]);
                            for (int e = 0; e < 4; e++)
                            {
                                List<int> le = new List<int>(ld.ToArray());
                                List<int> l5 = new List<int>(l4.ToArray());
                                l5.Add(le[e]);
                                le.Remove(le[e]);
                                for (int f = 0; f < 3; f++)
                                {
                                    List<int> lf = new List<int>(le.ToArray());
                                    List<int> l6 = new List<int>(l5.ToArray());
                                    l6.Add(lf[f]);
                                    lf.Remove(lf[f]);
                                    for (int g = 0; g < 2; g++)
                                    {
                                        List<int> lg = new List<int>(lf.ToArray());
                                        List<int> l7 = new List<int>(l6.ToArray());
                                        l7.Add(lg[g]);
                                        lg.Remove(lg[g]);
                                        for (int h = 0; h < 1; h++)
                                        {
                                            List<int> lh = new List<int>(lg.ToArray());
                                            List<int> l8 = new List<int>(l7.ToArray());
                                            l8.Add(lh[h]);//l8为产生的8个数的排列
                                            lh.Remove(lh[h]);

                                            //交换矩阵中的数
                                            int[,] newjuzhen = Jiaohuan(l8,juzhen);
                                            Duoprint(t,tt, 4096, newjuzhen);
                                            tt++;
                                            if (tt * 4096 > t)
                                            {
                                                OP.Close();
                                                return;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            //OP.Close();
        
        }

        private void Solve(int[,] sudu, Xuanji[,] kexuan)
        {
            if (Tianman(sudu)) return;
            Xuanji X = Findshortest(sudu, kexuan);
            int length = X.list.Count;
            int j;
            for (j = 0; j < length; j++)
            {
                int i = X.list[0];//因为每次add都加到末端
                sudu[X.x, X.y] = i;
                List<Xuanji> change = Saichu(X.x, X.y, i, kexuan);
                Solve(sudu, kexuan);
                if (Tianman(sudu)) break;
                Jiahui(change, i);

            }
            if ((!Tianman(sudu))) sudu[X.x, X.y] = 0;
        }

        private List<Xuanji> Saichu(int x, int y, int s, Xuanji[,] L)//筛除可选数
        {
            List<Xuanji> gaidong = new List<Xuanji>();
            L[x, y].Remove(s);
            gaidong.Add(L[x, y]);
            for (int i = 0; i < 9; i++)
            {
                if (L[x, i].Contains(s))
                {
                    L[x, i].Remove(s);
                    gaidong.Add(L[x, i]);
                }
                if (L[i, y].Contains(s))
                {
                    L[i, y].Remove(s);
                    gaidong.Add(L[i, y]);
                }
            }
            Addin9(x, y, gaidong, L, s);
            return gaidong;
        }

        private void Addin9(int x, int y, List<Xuanji> gaidong, Xuanji[,] L, int s)
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
            for (int ii = i; ii < imax; ii++)
            {
                for (int jj = j; jj < jmax; jj++)
                {
                    if (L[ii, jj].Contains(s))
                    {
                        L[ii, jj].Remove(s);
                        gaidong.Add(L[ii, jj]);
                    }
                }
            }
        }

        private Xuanji Findshortest(int[,] sudu, Xuanji[,] L)//找到可填入最少的点
        {
            int sl = 9;
            int x = new int();
            int y = new int();
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

        private void Jiahui(List<Xuanji> change, int s)//加回可选数
        {
            foreach (Xuanji i in change)
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
                    if (sudu[i, j] == 0) return false;
                }
            }
            return true;
        }

        /*private int Zaoshu(int x)
        {
            //不能有5，不能有重复数字
            int y=0;
            while (x>0)
            {
                y++;
                if (!Chongfu(y)) x--;
            }
            return y;
        }*/

        /*private bool Chongfu(int x)
        {
            int a = x / 100;
            int b = (x % 100) / 10;
            int c = x / 10;
            return (a == 5 | b == 5 | c == 5 | a == b | a == c | b == c | a==0 | b==0 |c==0);
        }*/

        private int[,] Jiaohuan(List<int> L,int[,] juzhen)
        {
            int[,] juzheno = new int[9, 9];
            for(int i=0;i<9 ;i++)
            {
                for(int j=0;j<9 ;j++)
                { 
                    if(juzhen[i,j]==5)//5是一开始选定的
                    {
                        juzheno[i, j] = 5;
                    }
                    else if(juzhen[i,j]<5)
                    {
                        juzheno[i, j] = L[juzhen[i, j]-1];
                    }
                    else
                    {
                        juzheno[i, j] = L[juzhen[i, j] - 2];//因为L中只有8个数
                    }
                }
            }
            return juzheno;
        }

        private void Duoprint(int t,int tt,int b,int[,] juzhen)
        {
            int a = ((tt+1)*b)<t?b:t -tt*b;
            for (int aa = 0; aa < a; aa++)
            {
                int[,] juzheno = Huanhang(juzhen,1,1,1,1);

                int x = aa / 64;
                int y = aa % 64;
                int x1 = x / 32, x2 = (x % 32) / 16, x3 = (x % 16) / 4, x4 = x % 4;
                int y1 = y / 32, y2 = (y % 32) / 16, y3 = (y % 16) / 4, y4 = y % 4;

                //对于行操作
                if (x1 == 1)
                {
                    juzheno = Huanhang(juzheno, 1, 2, 1, 1);
                }
                else
                {
                    juzheno = Huanhang(juzheno, 1, 1, 1, 1);
                }

                if (x2 == 1)
                {
                    juzheno = Huanhang(juzheno, 3, 6, 1, 1);
                    juzheno = Huanhang(juzheno, 4, 7, 1, 1);
                    juzheno = Huanhang(juzheno, 5, 8, 1, 1);
                }
                else
                {
                    juzheno = Huanhang(juzheno, 1, 1, 1, 1);
                }

                if (x3 == 1)
                {
                    juzheno = Huanhang(juzheno, 3, 4, 1, 1);
                }
                else if (x3 == 2)
                {
                    juzheno = Huanhang(juzheno, 3, 5, 1, 1);
                }
                else if(x3 == 3)
                {
                    juzheno = Huanhang(juzheno, 4, 5, 1, 1);
                }
                else
                {
                    juzheno = Huanhang(juzheno, 3, 3, 1, 1);
                }

                if (x4 == 1)
                {
                    juzheno = Huanhang(juzheno, 6, 7, 1, 1);
                }
                else if (x4 == 2)
                {
                    juzheno = Huanhang(juzheno, 6, 8, 1, 1);
                }
                else if (x4 == 3)
                {
                    juzheno = Huanhang(juzheno, 7, 8, 1, 1);
                }
                else
                {
                    juzheno = Huanhang(juzheno, 6, 6, 1, 1);
                }
                //对于列操作
                if (y1 == 1)
                {
                    juzheno = Huanhang(juzheno, 1, 1, 1, 2);
                }
                else
                {
                    juzheno = Huanhang(juzheno, 1, 1, 1, 1);
                }

                if (y2 == 1)
                {
                    juzheno = Huanhang(juzheno, 1, 1, 3, 6);
                    juzheno = Huanhang(juzheno, 1, 1, 4, 7);
                    juzheno = Huanhang(juzheno, 1, 1, 5, 8);
                }
                else
                {
                    juzheno = Huanhang(juzheno, 1, 1, 1, 1);
                }

                if (y3 == 1)
                {
                    juzheno = Huanhang(juzheno, 1, 1, 3, 4);
                }
                else if (y3 == 2)
                {
                    juzheno = Huanhang(juzheno, 1, 1, 3, 5);
                }
                else if (y3 == 3)
                {
                    juzheno = Huanhang(juzheno, 1, 1, 4, 5);
                }
                else
                {
                    juzheno = Huanhang(juzheno, 1, 1, 3, 3);
                }

                if (y4 == 1)
                {
                    juzheno = Huanhang(juzheno, 1, 1, 6, 7);
                }
                else if (y4 == 2)
                {
                    juzheno = Huanhang(juzheno, 1, 1, 6, 8);
                }
                else if (y4 == 3)
                {
                    juzheno = Huanhang(juzheno, 1, 1, 7, 8);
                }
                else
                {
                    juzheno = Huanhang(juzheno, 1, 1, 6, 6);
                }
                //print
                Printjuzhen(juzheno);
            }
            

        }

        private int[,] Huanhang(int[,] juzhen,int x1,int x2,int y1,int y2)
        {
            int[,] juzhen2 = new int[9, 9];
            for (int i = 0;i < 9 ;i++)
            {
                for(int j=0;j<9 ;j++)
                {
                    juzhen2[i, j] = juzhen[i, j];
                }
            }
            if (x1 != x2)
            {
                for(int i=0;i<9;i++)
                {
                    int temp = juzhen2[x1, i];
                    juzhen2[x1, i] = juzhen2[x2, i];
                    juzhen2[x2, i] = temp;
                }
            }
            if (y1 != y2)
            {
                for (int i = 0; i < 9; i++)
                {
                    int temp = juzhen2[x1, i];
                    juzhen2[i,y1] = juzhen2[i,y2];
                    juzhen2[i,y2] = temp;
                }
            }
            return juzhen2;
        }

        private void Printjuzhen(int[,] juzhen)
        {
            string sss = "";
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    //Console.Write(juzhen[i, j]);
                    sss += juzhen[i, j];
                    //sss += " ";
                }
            }
            OP.Out(sss);
            //OP.Out("\n");
        }

    }


}
