using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class SkipListNode<T>
        where T : IComparable<T>
    {
        public SkipListNode<T> Next;
        public SkipListNode<T> Lower;
        public SkipListNode<T> Upper;
        public T Key;
        public int Level;
    }
    public class SkipList<T>
        where T : IComparable<T>
    {
        public SkipListNode<T> Head;
        public SkipListNode<T> Tail;
        public void Add(T value)
        {
            var a = Find(value);
            var newNode = new SkipListNode<T> { Key = value, Next = a.Next };
            a.Next = newNode;
            var randKey = new Random();
            int coin = randKey.Next(2);
            while(coin!=0)
            {
                a = a.Lower;
                newNode = new SkipListNode<T> { Key = value, Next = a.Next };
                a.Next = newNode;
                coin = randKey.Next(2);
            }
        }

        public SkipListNode<T> Find(T value)
        {
            var a = Head;
            for (int i = 0; i < Head.Level; i++)
            {
                a = a.Lower;
            }
            while (a.Upper != null)
            {
                while (value.CompareTo(a.Key) > 0 && !a.Next.Key.Equals(default(T)))
                    a = a.Next;
                a = a.Upper;
            }
            while (value.CompareTo(a.Key) > 0 && !a.Next.Key.Equals(default(T)) && value.CompareTo(a.Next.Key) > 0)
                a = a.Next;
            return a;
        }

        public void Delete(T value)
        {
            var a = Find(value);
            var c = Head;
            while (a.Lower != null)
            {
                var b = c;
                while (b.Next != a)
                {
                    b = b.Next;
                }
                b.Next = a.Next;
                a = a.Lower;
                c = c.Lower;
            }
        }

        public SkipList(List<T> inputList)
        {
            int maximumLevel = (int)Math.Log(inputList.Count, 2);
            Head = new SkipListNode<T> { Key = default(T), Level = maximumLevel};
            var a = Head.Next;
            for(int i = 0; i < inputList.Count; i++)
            {
                a = new SkipListNode<T> { Key = inputList[i] };
                for(int j = 1; j <= maximumLevel; j++)
                {
                    if(i % Math.Pow(2,j) != 0)
                    {
                        a.Level = j - 1;
                    }
                }
                a = a.Next;
            }
            a = new SkipListNode<T> { Key = default(T), Level = maximumLevel, Next = null };
            var levelHead = Head;
            var levelIterator = Head;
            a = levelHead.Lower;
            for(int i = 2; i <= maximumLevel; i++)
            {
                a = new SkipListNode<T> { Key = default(T), Level = maximumLevel, Upper = a };
                while (levelIterator != null)
                {
                    if (levelIterator.Level >= i)
                    {
                        levelIterator.Lower = new SkipListNode<T> { Key = levelIterator.Key, Level = levelIterator.Level, Upper = levelIterator };
                        a.Next = levelIterator.Lower;
                        a = a.Next;
                    }
                    levelIterator = levelIterator.Next;
                }
                a.Next = new SkipListNode<T> { Key = default(T), Level = maximumLevel , Next = null, Upper = levelIterator };
                levelHead = levelHead.Lower;
                a = levelHead.Lower;
                levelIterator = levelHead;
            }
        }
    }
}
