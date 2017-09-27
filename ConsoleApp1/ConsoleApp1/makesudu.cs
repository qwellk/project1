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
            int tt = t / 4096;//还可以有更多的
            for(int f=0;f<tt+1 ;f++)
            {
                int y = S + Zaoshu(f);
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
                Duoprint(t, tt, 4096, juzhen);
                //Console.Read();
            }
            OP.Close();
        
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
            if (x < 3)
            {
                if (y < 3)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            if (L[i, j].Contains(s))
                            {
                                L[i, j].Remove(s);
                                gaidong.Add(L[i, j]);
                            }
                        }
                    }
                }
                else if (y < 6)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        for (int j = 3; j < 6; j++)
                        {
                            if (L[i, j].Contains(s))
                            {
                                L[i, j].Remove(s);
                                gaidong.Add(L[i, j]);
                            }
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < 3; i++)
                    {
                        for (int j = 6; j < 9; j++)
                        {
                            if (L[i, j].Contains(s))
                            {
                                L[i, j].Remove(s);
                                gaidong.Add(L[i, j]);
                            }
                        }
                    }
                }
            }
            else if (x < 6)
            {
                if (y < 3)
                {
                    for (int i = 3; i < 6; i++)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            if (L[i, j].Contains(s))
                            {
                                L[i, j].Remove(s);
                                gaidong.Add(L[i, j]);
                            }
                        }
                    }
                }
                else if (y < 6)
                {
                    for (int i = 3; i < 6; i++)
                    {
                        for (int j = 3; j < 6; j++)
                        {
                            if (L[i, j].Contains(s))
                            {
                                L[i, j].Remove(s);
                                gaidong.Add(L[i, j]);
                            }
                        }
                    }
                }
                else
                {
                    for (int i = 3; i < 6; i++)
                    {
                        for (int j = 6; j < 9; j++)
                        {
                            if (L[i, j].Contains(s))
                            {
                                L[i, j].Remove(s);
                                gaidong.Add(L[i, j]);
                            }
                        }
                    }
                }
            }
            else
            {
                if (y < 3)
                {
                    for (int i = 6; i < 9; i++)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            if (L[i, j].Contains(s))
                            {
                                L[i, j].Remove(s);
                                gaidong.Add(L[i, j]);
                            }
                        }
                    }
                }
                else if (y < 6)
                {
                    for (int i = 6; i < 9; i++)
                    {
                        for (int j = 3; j < 6; j++)
                        {
                            if (L[i, j].Contains(s))
                            {
                                L[i, j].Remove(s);
                                gaidong.Add(L[i, j]);
                            }
                        }
                    }
                }
                else
                {
                    for (int i = 6; i < 9; i++)
                    {
                        for (int j = 6; j < 9; j++)
                        {
                            if (L[i, j].Contains(s))
                            {
                                L[i, j].Remove(s);
                                gaidong.Add(L[i, j]);
                            }
                        }
                    }
                }
            }
            return gaidong;
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

        private int Zaoshu(int x)
        {
            //不能有5，不能有重复数字
            int y=0;
            while (x>0)
            {
                y++;
                if (!Chongfu(y)) x--;
            }
            return y;
        }

        private bool Chongfu(int x)
        {
            int a = x / 100;
            int b = (x % 100) / 10;
            int c = x / 10;
            return (a == 5 | b == 5 | c == 5 | a == b | a == c | b == c | a==0 | b==0 |c==0);
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
                }
            }
            OP.Out(sss);
            OP.Out("\n");
        }

    }


}
