using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tree
{
    public class Tree
    {
        private int index = 0;
        Node Head;
        public class Node
        {
            public Node Left, Right;
            private int index = 0;
            public int Value;

            public int Index
            {
                get { return index; }
            }

            public Node(int index, int value)
            {
                this.index = index;
                Value = value;
            }
        }

        public void AddFull(Node node = null)
        {
            if (node == null) node = Head;
            if (node.Left == null)
            {
                if (node.Right != null)
                    node.Left = new Node(index++, node.Value * 2);
            }
            else
                AddFull(node.Left);
            if (node.Right == null)
            {
                if (node.Left != null)
                    node.Right = new Node(index++, node.Value * 2);
            }
            else
                AddFull(node.Right);
        }

        /// <summary>
        /// собирает данные по алгоритму ЛПК
        /// </summary>
        /// <param name="node">Узел от которого удкт собираться данные</param>
        /// <returns></returns>
        public int[] GetData(Node node = null)
        {
            List<int> result = new List<int>();
            //если узел не указан, то данные собирают от головы
            if (node == null) node = Head;
            if (node.Left == null)
                if (node.Right == null)
                    ///если левый и правый переходы пустые,
                    ///то берем значение текущего узла
                    result.Add(node.Index);
                else
                {
                    ///если левый пусто, а правый не пустой,
                    ///то собираем данные из подветви 
                    ///и записывает значение текущего узла
                    result.AddRange(GetData(node.Right));
                    result.Add(node.Index);
                }
            else
            {
                ///если левый переход существует,
                ///то собираем сведения из левой подветви
                result.AddRange(GetData(node.Left));
                if (node.Right == null)
                    ///Если в правый переход пустой,
                    ///то добавляем значение текущего узла
                    result.Add(node.Index);
                else
                {
                    ///если правый переход существует,
                    ///то собираем данные из правой подветски
                    result.AddRange(GetData(node.Right));
                    result.Add(node.Index);
                }
            }

            return result.ToArray();
        }

        public string[] GetTransfers(Node node = null)
        {
            List<string> result = new List<string>();
            if (node == null) node = Head;

            if(node.Left != null)
            {
                result.Add($"{node.Index}|{node.Value}->L->{node.Left.Index}|{node.Left.Value}");
                result.AddRange(GetTransfers(node.Left));
            }
            if (node.Right != null)
            {
                result.Add($"{node.Index}|{node.Value}->R->{node.Right.Index}|{node.Right.Value}");
                result.AddRange(GetTransfers(node.Right));
            }
            return result.ToArray();
        }

        public int SummChetLvl(Node node = null, int level = 1)
        {
            int result = 0;
            if (node == null) node = Head;
            if (level % 2 != 0)
                result += node.Value;
            if(node.Left != null)
                result += SummChetLvl(node.Left, level + 1);
            if (node.Right != null)
                result += SummChetLvl(node.Right, level + 1);
            return result;
        }

        public void DelNode(int index)
        {
            Node node = Find(index, Head);
            Node parent = GetParent(node);
            if (parent == null) throw new Exception("Элемент не найден");
            if (parent.Left == node) parent.Left = null;
            if (parent.Right == node) parent.Right = null;
        }

        public Node GetParent(Node node, Node current = null)
        {
            if (current == null) current = Head;
            if (Head == node) return null;
            if (current.Left == node || current.Right == node) return current;
            Node result = GetParent(node, current.Left);
            if (result != null) return result;
            result = GetParent(node, current.Right);
            if (result != null) return result;
            return null;
        }

        public void AddNode(int index, int Value)
        {
            if (Head == null)
            {
                Head = new Node(this.index++, Value);
                return;
            }
            Node node = Find(index, Head);
            if (node == null) throw new Exception("Не найден узел в который нужно добавить элемент");
            if(node.Left == null)
            {
                node.Left = new Node(this.index++, Value);
                return;
            }
            if (node.Right == null)
            {
                node.Right = new Node(this.index++, Value);
                return;
            }
            throw new Exception("Все ссылки этого узла уже заняты дочерними элементами");
        }

        public Node Find(int index, Node node)
        {
            if (node.Index == index) return node;
            if(node.Left != null)
            {
                Node result = Find(index, node.Left);
                if (result != null) return result;
            }
            if (node.Right != null)
            {
                Node result = Find(index, node.Right);
                if (result != null) return result;
            }
            return null;
        }
    }
}
