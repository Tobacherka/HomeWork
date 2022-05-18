using System;
using System.Linq;
using System.Text;

namespace Postfix.src
{
    class Node<T>
    {
        public Node<T> Next;
        public T data;
    }

    class Stack<T>
    {
        private Node<T> head;

        public Node<T> Pop()
        {
            if (head == null)
                return null;

            var last = head;
            head = head.Next;
            return last;
        }

        public void Push(T data)
        {
            var node = new Node<T> { Next = head, data = data };
            head = node;
        }

        public Node<T> GetHead()
        {
            return head;
        }

        public bool IsEmpty()
        {
            return head == null;
        }
    }

    class Program
    {
        static int GetPriority(char x)
        {
            switch (x)
            {
                case '(': return 1;
                case ')': return 2;
                case '=': return 3;
                case '+': case '-': return 4;
                case '*': case '/': return 5;
            }
            return -1;
        }


        static string ConvertToPostfix(string infix)
        {
            Stack<char> stack = new Stack<char>();
            string output = "";

            infix = infix.Replace(" ", "");


            for (int i = 0; i < infix.Length; i++)
            {
                var x = infix[i];

                if (char.IsLetterOrDigit(x))
                {
                    string str = "";
                    while (i < infix.Length && char.IsLetterOrDigit(x))
                    {
                        str += x;
                        i++;
                        x = infix[i];
                    }

                    output += str;
                    output += ' ';
                }

                if (x == '(')
                {
                    stack.Push(x);
                }
                else if (x == ')')
                {
                    while (stack.GetHead().data != '(')
                    {
                        output += stack.Pop().data;
                        output += ' ';
                    }
                    stack.Pop();
                }
                else
                {
                    while (!stack.IsEmpty() && GetPriority(x) <= GetPriority(stack.GetHead().data))
                    {
                        output += stack.Pop().data;
                        output += ' ';
                    }
                    stack.Push(x);
                }
            }

            while (!stack.IsEmpty())
            {
                output += stack.Pop().data;
                output += ' ';
            }

            return output;
        }

        static string ConvertToInfix(string postfix)
        {
            Stack<string> stack = new Stack<string>();
            char x;

            for (int i = 0; i < postfix.Length; i++)
            {
                x = postfix[i];

                if (x == ' ')
                    continue;

                if (char.IsLetterOrDigit(x))
                {
                    string str = "";
                    while (x != ' ' && i < postfix.Length && char.IsLetterOrDigit(x))
                    {
                        str += x;
                        i++;
                        x = postfix[i];
                    }

                    if (i < postfix.Length)
                    {
                        i--;
                    }
                    stack.Push(str);
                }
                else
                {
                    string first = stack.Pop().data;
                    string second = stack.Pop().data;
                    stack.Push($"({second} {postfix[i]} {first})");
                }
            }

            return stack.GetHead().data;
        }

        public static void Main()
        {
            string input = Console.ReadLine();//например a*b-c/(d+e)
            string output = ConvertToPostfix(input);
            string back = ConvertToInfix(output);
            Console.WriteLine(output);
            Console.WriteLine(back);

            Console.ReadKey();
        }
    }
}
