using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpDrillsPart3
{

    internal class Program
    {
        static void Main(string[] args)
        {
            // Create a new sorted linked list
            LinkedList list = new LinkedList();

            // Append some elements
            list.Append(10);
            list.Append(30);
            list.Append(20);
            list.Append(40);

            // Prepend an element
            list.Prepend(5);

            // Display the sorted linked list
            Console.WriteLine("Sorted Linked List:");
            list.Sort();
            foreach (int value in list.ToList())
            {
                Console.Write(value + " ");
            }
            Console.WriteLine();

            // Get the minimum and maximum nodes
            Node<int> minNode = list.GetMinNode();
            Node<int> maxNode = list.GetMaxNode();

            Console.WriteLine("Minimum Value: " + minNode.Value);
            Console.WriteLine("Maximum Value: " + maxNode.Value);

            // Pop an element from the end
            int poppedValue = list.Pop();
            Console.WriteLine("Popped Value: " + poppedValue);

            // Unqueue an element from the beginning
            int unqueuedValue = list.Unqueue();
            Console.WriteLine("Unqueued Value: " + unqueuedValue);

            // Display the updated sorted linked list
            Console.WriteLine("Updated Sorted Linked List:");
            foreach (int value in list.ToList())
            {
                Console.Write(value + " ");
            }
            Console.WriteLine();

            minNode = list.GetMinNode();
            maxNode = list.GetMaxNode();
            Console.WriteLine("Minimum Value: " + minNode.Value);
            Console.WriteLine("Maximum Value: " + maxNode.Value);
        }
    }
}
