using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Xuanji
    {
        public int x, y;
        public List<int> list;

        public Xuanji()
        {
            list = new List<int>();
        }

        public void Set(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public void Add(int s)
        {
            list.Add(s);
        }

        public void Remove(int s)
        {
            list.Remove(s);
        }

        public int Count()
        {
            return list.Count;
        }

        public bool Contains(int s)
        {
            return list.Contains(s);
        }

    }
}
