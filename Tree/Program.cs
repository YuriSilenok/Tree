using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tree
{
    class Program
    {
        static void Main(string[] args)
        {
            Tree tree = new Tree();
            int[] indexes = new int[] { -1, 0, 1, 1, 2, 4, 3, 3, 0 };
            foreach (var item in indexes)
                tree.AddNode(item, item);
            foreach (var item in tree.GetData())
                Console.Write($"{item}\t");
            Console.WriteLine();
            foreach (var item in tree.GetTransfers())
                Console.WriteLine($"{item}\t");
            Console.WriteLine();
            tree.DelNode(3);
            foreach (var item in tree.GetTransfers())
                Console.WriteLine($"{item}\t");
            tree.AddFull();
            Console.WriteLine();
            foreach (var item in tree.GetTransfers())
                Console.WriteLine($"{item}\t");
            Console.WriteLine();
            Console.WriteLine(tree.SummChetLvl());
            Console.ReadLine();
        }
    }
}
