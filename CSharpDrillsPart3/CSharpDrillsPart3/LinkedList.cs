using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpDrillsPart3
{
    public class LinkedList
    {
        private Node<int> head;
        private Node<int> tail;
        private Node<int> minNode;
        private Node<int> maxNode;

        public void Append(int value)
        {
            Node<int> newNode = new Node<int> { Value = value };

            if (head == null)
            {
                head = newNode;
                tail = newNode;
                minNode = newNode;
                maxNode = newNode;
            }
            else
            {
                tail.Next = newNode;
                tail = newNode;

                if (value < minNode.Value)
                    minNode = newNode;
                else if (value > maxNode.Value)
                    maxNode = newNode;
            }
        }

        public void Prepend(int value)
        {
            Node<int> newHead = new Node<int> { Value = value, Next = head };
            head = newHead;

            if (tail == null)
            {
                tail = head;
            }

            if (value < minNode.Value)
                minNode = newHead;
            else if (value > maxNode.Value)
                maxNode = newHead;
        }

        public int Pop()
        {
            if (head == null)
                throw new Exception("List is empty");

            if (head == tail)
            {
                int value = head.Value;
                head = null;
                tail = null;
                minNode = null;
                maxNode = null;
                return value;
            }

            Node<int> current = head;
            while (current.Next != tail)
            {
                current = current.Next;
            }

            int lastValue = tail.Value;
            current.Next = null;
            tail = current;

            // Update minNode and maxNode if necessary
            if (lastValue == minNode.Value)
                UpdateMinNode();
            else if (lastValue == maxNode.Value)
                UpdateMaxNode();

            return lastValue;
        }

        public int Unqueue()
        {
            if (head == null)
                throw new Exception("List is empty");

            int firstValue = head.Value;
            head = head.Next;
            if (head == null)
            {
                tail = null;
                minNode = null;
                maxNode = null;
            }

            // Update minNode and maxNode if necessary
            if (firstValue == minNode.Value)
                UpdateMinNode();
            else if (firstValue == maxNode.Value)
                UpdateMaxNode();

            return firstValue;
        }

        public IEnumerable<int> ToList()
        {
            Node<int> current = head;
            while (current != null)
            {
                yield return current.Value;
                current = current.Next;
            }
        }

        public bool IsCircular()
        {
            if (head == null)
                return false;

            Node<int> first = head;
            Node<int> second = head.Next;

            while (second != null && second.Next != null)
            {
                if (first == second)
                    return true;

                first = first.Next;
                second = second.Next.Next;
            }
            return false;
        }

        public void Sort()
        {
            if (head == null || head == tail)
                return; // Nothing to sort

            bool swapped;
            do
            {
                swapped = false;
                Node<int> current = head;
                Node<int> previous = null;

                while (current != tail)
                {
                    if (current.Value > current.Next.Value)
                    {
                        // Swap the values
                        int temp = current.Value;
                        current.Value = current.Next.Value;
                        current.Next.Value = temp;
                        swapped = true;
                    }

                    // Move to the next nodes
                    previous = current;
                    current = current.Next;
                }
                // Update the tail after each pass
                tail = previous;
            } while (swapped);

            // Restore head and tail references after sorting
            Node<int> newTail = head;
            while (newTail.Next != null)
            {
                newTail = newTail.Next;
            }
            tail = newTail;
        }

        public Node<int> GetMaxNode()
        {
            if (maxNode == null)
                throw new Exception("List is empty");

            return maxNode;
        }

        public Node<int> GetMinNode()
        {
            if (minNode == null)
                throw new Exception("List is empty");

            return minNode;
        }

        private void UpdateMinNode()
        {
            if (head == null)
            {
                minNode = null;
                return;
            }

            Node<int> current = head;
            Node<int> min = head;

            while (current != null)
            {
                if (current.Value < min.Value)
                    min = current;

                current = current.Next;
            }

            minNode = min;
        }

        private void UpdateMaxNode()
        {
            if (head == null)
            {
                maxNode = null;
                return;
            }

            Node<int> current = head;
            Node<int> max = head;

            while (current != null)
            {
                if (current.Value > max.Value)
                    max = current;

                current = current.Next;
            }

            maxNode = max;
        }
    }
}


