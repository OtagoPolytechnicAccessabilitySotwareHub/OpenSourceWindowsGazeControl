using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GazeToolBar
{
    class Node
    {
        internal char m_char;
        public List<Node> children;
        internal bool m_wordEnd;
        internal string m_cased = null;

        public Node(char ch, bool wordEnd)
        {
            m_char = ch;
            children = new List<Node>();
            m_wordEnd = wordEnd;

        }

        public Node findChild(Char chara, Boolean endofword)
        {
            Node newParent = new Node('k', false);
            if (children != null)
            {
                Boolean exist = false;
                foreach (Node child in children)
                {
                    if ((child.m_char == chara) && (child.m_wordEnd == endofword))
                    {
                        newParent = child;
                        exist = true;
                    }
                }
                if (!exist)
                {
                    children.Add(new Node(chara, endofword));
                    newParent = children.Last();
                }
            }
            else
            {
                newParent = new Node(chara, endofword);
                children.Add(newParent);
            }
            return newParent;
        }


        public Node findNode(char chara, Boolean endofword)
        {
            foreach (Node child in children)
            {
                if ((child.m_char == chara) && (child.m_wordEnd == endofword))
                {
                    return child;
                }
            }
            return null;
        }
    }
}
