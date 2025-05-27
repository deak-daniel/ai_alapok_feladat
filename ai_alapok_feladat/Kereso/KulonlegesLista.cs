using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ai_alapok_feladat
{
    internal class KulonlegesLista : IEnumerable
    {
        #region Fields
        private List<Node> nodes;
        #endregion

        #region Constructor
        public KulonlegesLista()
        {
            nodes = new List<Node>();
        }
        #endregion

        #region Public methods
        public void Enqueue(Node node)
        {
            nodes.Add(node);
            nodes.Sort();
        }
        public Node Dequeue()
        {
            if (nodes.Count > 0)
            {
                Node ret = new Node();
                ret = nodes[0];
                nodes.RemoveAt(0);
                return ret;
            }
            return null;
        }
        public bool Contains(Node node)
        {
            return nodes.Contains(node);
        }
        public IEnumerator GetEnumerator()
        {
            return new KulonlegesListaEnumerator(nodes);
        }
        public Node Find(Node item)
        {
            int index = nodes.IndexOf(item);
            return nodes[index];
        }
        #endregion

        #region Properties
        public int Count { get => nodes.Count; }
        #endregion
    }
    class KulonlegesListaEnumerator : IEnumerator
    {
        private List<Node> list;
        private int index;
        public object Current => list[index];
        public KulonlegesListaEnumerator(List<Node> inList)
        {
            list = inList;
            index = -1;
        }
        public bool MoveNext()
        {
            if (index >= list.Count)
            {
                return false;
            }
            else 
            {
                index++;
                return true;
            }
        }
        public void Reset()
        {
            index = -1;
        }
    }
}
