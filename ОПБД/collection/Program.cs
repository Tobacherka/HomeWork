using System;
using System.Collections;
using System.Collections.Generic;

namespace collection
{
    class Program
    {

        public class MyArray<T> : ICollection<KeyValuePair<int, T>>
        {
            private Hashtable arr = new Hashtable();


            public IEnumerator<KeyValuePair<int, T>> GetEnumerator()
            {
                var enumerator = arr.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    yield return new KeyValuePair<int, T>((int)enumerator.Key, (T)enumerator.Value);
                }
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            public T this[int index]
            {
                get { return (T)arr[index.GetHashCode()]; }
                set { arr[index.GetHashCode()] = value; }
            }

            public T this[string index]
            {
                get { return (T)arr[index.GetHashCode()]; }
                set { arr[index.GetHashCode()] = value; }
            }


            public void Add(KeyValuePair<int, T> item)
            {
                arr.Add(item.Key, item.Value);
            }

            public void Clear()
            {
                arr.Clear();
            }

            public bool Contains(KeyValuePair<int, T> item)
            {
                return arr.Contains(item.Key);
            }

            public void CopyTo(KeyValuePair<int, T>[] array, int arrayIndex)
            {
                arr.CopyTo(array, arrayIndex);
            }

            public bool Remove(KeyValuePair<int, T> item)
            {
                if (!arr.Contains(item.Key)) return false;
                arr.Remove(item.Key);
                return true;
            }


            public int Count
            {
                get { return arr.Count; }
            }

            public bool IsReadOnly
            {
                get { return arr.IsReadOnly; }
            }

            
        }
        static void Main(string[] args)
        {
            MyArray<int> testArray = new MyArray<int>();
            testArray[0] = 0;
            testArray["1"] = 1;

            Console.WriteLine(testArray[0]);
            Console.WriteLine(testArray["1"]);

            foreach (var pair in testArray)
            {
                Console.WriteLine(pair.Value);
            }
        }
    }
}