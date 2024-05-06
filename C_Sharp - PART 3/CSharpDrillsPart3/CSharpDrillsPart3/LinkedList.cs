using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpDrillsPart3
{
    // ==================== EXERCISE 6 ====================
    /// <summary>
    /// Represents a singly linked list of integers with various operations.
    /// </summary>
    public class LinkedList
    {
        private Node<int> head;
        private Node<int> tail;
        private Node<int> minNode;
        private Node<int> maxNode;

        /// <summary>
        /// Appends a new node with the specified value to the end of the list.
        /// </summary>
        /// <param name="value">The value to be appended.</param>
        public void Append(int value)
        {
            Node<int> newNode = new Node<int> { Value = value };

            // If list is empty, initialize head, tail, minNode, and maxNode to the new node
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

                // Update minNode and maxNode if necessary
                if (value < minNode.Value)
                    minNode = newNode;
                else if (value > maxNode.Value)
                    maxNode = newNode;
            }
        }

        /// <summary>
        /// Prepends a new node with the specified value to the beginning of the list.
        /// </summary>
        /// <param name="value">The value to be prepended.</param>
        public void Prepend(int value)
        {
            Node<int> newHead = new Node<int> { Value = value, Next = head };
            head = newHead;

            if (tail == null)
            {
                tail = head;
            }

            // Update minNode and maxNode if necessary
            if (value < minNode.Value)
                minNode = newHead;
            else if (value > maxNode.Value)
                maxNode = newHead;
        }

        /// <summary>
        /// Removes and returns the last element from the list.
        /// </summary>
        /// <returns>The value of the removed element.</returns>
        public int Pop()
        {
            if (head == null)
                throw new InvalidOperationException("List is empty");

            // If there's only one element in the list, reset all references
            if (head == tail)
            {
                int value = head.Value;
                head = null;
                tail = null;
                minNode = null;
                maxNode = null;
                return value;
            }

            // Get to the last of the list and remove
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

        /// <summary>
        /// Removes and returns the first element from the list.
        /// </summary>
        /// <returns>The value of the removed element.</returns>
        public int Unqueue()
        {
            if (head == null)
                throw new InvalidOperationException("List is empty");

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

        /// <summary>
        /// Returns an enumerable collection of all values in the list.
        /// </summary>
        /// <returns>An enumerable collection of integers.</returns>
        public IEnumerable<int> ToList()
        {
            Node<int> current = head;
            while (current != null)
            {
                yield return current.Value;
                current = current.Next;
            }
        }

        /// <summary>
        /// Checks whether the list contains a cycle.
        /// </summary>
        /// <returns>True if the list contains a cycle, otherwise false.</returns>
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
                   
                // We want the second to move faster than first, so it could catch up if the List is Circular
                first = first.Next;
                second = second.Next.Next;
            }
            return false;
        }

        /// <summary>
        /// Sorts the elements of the list in non-decreasing order. (Bubble Sort)
        /// </summary>
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

        /// <summary>
        /// Gets the node with the maximum value in the list.
        /// </summary>
        /// <returns>The node with the maximum value.</returns>
        public Node<int> GetMaxNode()
        {
            if (maxNode == null)
                throw new InvalidOperationException("List is empty");

            return maxNode;
        }

        /// <summary>
        /// Gets the node with the minimum value in the list.
        /// </summary>
        /// <returns>The node with the minimum value.</returns>
        public Node<int> GetMinNode()
        {
            if (minNode == null)
                throw new InvalidOperationException("List is empty");

            return minNode;
        }

        /// <summary>
        /// Updates the minNode reference to the node with the minimum value in the list.
        /// </summary>
        private void UpdateMinNode()
        {
            if (head == null)
            {
                minNode = null;
                return;
            }

            Node<int> current = head;
            Node<int> min = head;

            // Iterate over list, check if there is a number that is smaller than head.value;
            // If so, update min
            while (current != null)
            {
                if (current.Value < min.Value)
                    min = current;

                current = current.Next;
            }

            minNode = min;
        }

        /// <summary>
        /// Updates the maxNode reference to the node with the maximum value in the list.
        /// </summary>
        private void UpdateMaxNode()
        {
            if (head == null)
            {
                maxNode = null;
                return;
            }

            Node<int> current = head;
            Node<int> max = head;

            // Iterate over list, check if there is a number that is bigger than head.value;
            // If so, update max
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


