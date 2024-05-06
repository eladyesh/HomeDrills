using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpDrillsPart3
{
    // ====================================================
    // ==================== EXERCISE 5 ====================
    // ====================================================
    /// <summary>
    /// Represents a node in a singly linked list.
    /// </summary>
    /// <typeparam name="T">The type of the value stored in the node.</typeparam>
    public class Node<T>
    {
        /// <summary>
        /// Gets or sets the value stored in the node.
        /// </summary>
        public T Value { get; set; }

        /// <summary>
        /// Gets or sets the reference to the next node in the linked list.
        /// </summary>
        public Node<T> Next { get; set; }
    }

}
