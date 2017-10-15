using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace WindowsFormsApp1
{
    
    
    public partial class Form1 : Form
    {
        double best=-1;
        int difficulty = 1;
        double time = 0;
        int[] resultlist;


        public Form1()
        {
            InitializeComponent();

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }


        private void sudoku_init(int[] l)
        {
            resultlist = result(l);
            time = 0;
            timer1.Stop();
            timer1.Start();
            
            for(int i=0;i<9 ;i++)
            {
                for(int j=0;j<9 ;j++)
                {
                    if(l[i*9+j]!=0)
                    {
                        string s = "textBox" + (i * 9 + j+1);
                        TextBox tb = (TextBox)this.Controls.Find(s, false)[0];
                        tb.Text = l[i*9+j].ToString();
                        tb.BackColor = Color.DodgerBlue;
                        tb.ReadOnly = true;
                    }
                    else
                    {
                        string s = "textBox" + (i * 9 + j + 1);
                        TextBox tb = (TextBox)this.Controls.Find(s, false)[0];
                        tb.BackColor = Color.WhiteSmoke;
                        tb.Text = "";
                        tb.ReadOnly = false;
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //暂时这样生成
            int[] list = initial();
            sudoku_init(list);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int[] list = resultlist;
            textBox_hint(list);
        }
        private void textBox_MouseEnter(string s)
        {
            TextBox tb = (TextBox)this.Controls.Find(s, false)[0];
            Color color = Color.Yellow;
            tb.BackColor = color;
        }


        private void textBox_changecolor(object sender, EventArgs e)
        {

            foreach (Control c in Controls)
            {
                if (c is TextBox)
                {
                    if (c.Focused & (((TextBox)c).ReadOnly == false)) c.BackColor = Color.Yellow;
                    else if (c.BackColor == Color.Yellow)
                    {
                        if (string.IsNullOrWhiteSpace(c.Text)) c.BackColor = Color.WhiteSmoke;
                        else c.BackColor = Color.Green;
                    }
                }
            }
        }

        private void textBox_hint(int[] l)
        {
            foreach (Control c in Controls)
            {
                if (c is TextBox)
                {
                    if (c.BackColor == Color.Yellow)
                    {
                        //c.Name.Remove(0, 7);
                        int a = int.Parse(c.Name.Remove(0, 7))-1;
                        c.Text = l[a].ToString();
                    }
                }
            }
        }

        private void 简单ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.difficulty = 1;
            label2.Text = "简单";
        }

        private void 中等ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.difficulty = 2;
            label2.Text = "中等";
        }

        private void 困难ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.difficulty = 3;
            label2.Text = "困难";
        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            time += 0.1;
            string s = time.ToString("f1") + "s";
            label1.Text = s;
        }

        private void textBox_TextChanged(object sender, EventArgs e)
        {
            //对输入处理
            foreach (Control c in Controls)
            {
                if (c is TextBox)
                {
                    if (c.Focused & (((TextBox)c).ReadOnly == false))
                    {
                        string s=c.Text;
                        if (s.Length == 0) return;
                        if (s.Length != 1) s = s.Remove(0, s.Length - 1);
                        if (!char.IsNumber(s[0]) | s[0].Equals('0'))
                        {
                            s = "";
                            printfalse();
                        }
                        c.Text = s;
                    }
                    
                }
            }
            //判断是否填完填对
            if(check()==true)
            {
                timer1.Stop();
                win();
            }
        }

        private void printfalse()
        {

        }

        private void win()
        {
            if (best == -1) best = time;
            else if (time < best) best = time;
            Form2 F = new Form2();
            F.Show();
        }

        
        private int[] initial()//外部调用生成
        {
            int[] list = {  0, 0, 0, 0, 0, 0, 0, 1, 2,  0, 0, 3, 6, 0, 0, 0, 0, 0 ,  0, 0, 0, 0, 0, 7, 0, 0, 0,  4, 1, 0, 0, 2, 0, 0, 0, 0 ,  0, 0, 0, 5, 0, 0, 3, 0, 0 ,  7, 0, 0, 0, 0, 0, 6, 0, 0 ,  2, 8, 0, 0, 0, 0, 0, 4, 0,  0, 0, 0, 3, 0, 0, 5, 0, 0 ,  0, 0, 0, 0, 0, 0, 0, 0, 0  };
            /*int[,] lists = new int[1,81];
            int[] list=new int[81];
            Console.WriteLine(this.difficulty);
            generate(1, this.difficulty,ref lists[0,0]);
            for(int x=0;x<81; x++)
            {
                list[x] = lists[0, x];
            }*/
            return list;
        }

        private int[] result(int[] l)//外部调用结果
        {
            int[,] list = { { 0, 0, 0, 0, 0, 0, 0, 1, 2 }, { 0, 0, 3, 6, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 7, 0, 0, 0 }, { 4, 1, 0, 0, 2, 0, 0, 0, 0 }, { 0, 0, 0, 5, 0, 0, 3, 0, 0 }, { 7, 0, 0, 0, 0, 0, 6, 0, 0 }, { 2, 8, 0, 0, 0, 0, 0, 4, 0 }, { 0, 0, 0, 3, 0, 0, 5, 0, 0 }, { 0, 0, 0, 0, 0, 0, 0, 0, 0 } };
            /*int[] rl= new int[81];
            solve(l, rl);
            return rl;*/
            return l;
        }

        private bool check()//调用内部判断
        {
            bool b = true;
            foreach (Control c in Controls)
            {
                if (c is TextBox)
                {
                    int num = int.Parse(c.Name.Remove(0, 7)) - 1;
                    if (c.Text == resultlist[num].ToString()) continue;
                    else return false;
                }
            }
            return b;
        }

        private void 排行榜ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("最快速度： " + best + "s", "最快");
        }

        [DllImport(@"sudoku.dll",EntryPoint = "?generate@core@@QAGXHHQAY0FB@H@Z",CallingConvention = CallingConvention.Cdecl)]
        extern static  void generate(int number, int mode,ref int r);

        [DllImport(@"sudoku.dll",EntryPoint ="?solve@core@@QAG_NQAH0@Z", CallingConvention = CallingConvention.Cdecl)]
        extern static  bool solve(int[] puzzle, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] int[] solution);
    }
}
