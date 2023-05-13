using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace DSA.Library.DataStructures
{
    public class DoublyLinkedListNode<T> : IComparable<T>, IComparer<T>, IEquatable<T>, ICloneable
    {
        public T Value { get; set; }
        public DoublyLinkedListNode<T> Next { get; set; }
        public DoublyLinkedListNode<T> Previous { get; set; }

        public DoublyLinkedListNode(T value)
        {
            Value = value;
            Next = null;
            Previous = null;
        }

        public int CompareTo(T other)
        {
            if (Value is IComparable<T> comparable)
            {
                return comparable.CompareTo(other);
            }
            throw new InvalidOperationException("Type is not comparable.");
        }

        public int Compare(T x, T y)
        {
            if (x is IComparable<T> comparable)
            {
                return comparable.CompareTo(y);
            }
            throw new InvalidOperationException("Type is not comparable.");
        }

        public bool Equals(T other)
        {
            if (Value is IEquatable<T> equatable)
            {
                return equatable.Equals(other);
            }
            throw new InvalidOperationException("Type is not equatable.");
        }

        public object Clone()
        {
            return new DoublyLinkedListNode<T>(Value)
            {
                Next = Next?.Clone() as DoublyLinkedListNode<T>,
                Previous = Previous?.Clone() as DoublyLinkedListNode<T>
            };
        }
    }

    [Serializable]
    public class DoublyLinkedList<T> : ICollection<T>, IEnumerable<T>, IEnumerable, IReadOnlyCollection<T>, ICollection, IDeserializationCallback, ISerializable
    {
        #region Testing internals 
        // for testing project visibility
        internal DoublyLinkedListNode<T> Head => head;
        internal DoublyLinkedListNode<T> Tail => tail;
        //
        #endregion

        private DoublyLinkedListNode<T> head;
        private DoublyLinkedListNode<T> tail;

        public int Count { get; private set; }
        public bool IsReadOnly => false;
        public object SyncRoot => this;
        public bool IsSynchronized => false;

        public DoublyLinkedList()
        {
            head = null;
            tail = null;
            Count = 0;
        }

        public void Add(T item)
        {
            var node = new DoublyLinkedListNode<T>(item);
            if (head == null)
            {
                head = node;
                tail = node;
            }
            else
            {
                node.Previous = tail;
                tail.Next = node;
                tail = node;
            }
            Count++;
        }

        public void Clear()
        {
            head = null;
            tail = null;
            Count = 0;
        }

        public bool Contains(T item)
        {
            var node = head;
            while (node != null)
            {
                if (node.Value.Equals(item))
                {
                    return true;
                }
                node = node.Next;
            }
            return false;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array == null)
            {
                throw new ArgumentNullException(nameof(array));
            }
            if (arrayIndex < 0 || arrayIndex > array.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(arrayIndex));
            }
            if (array.Length - arrayIndex < Count)
            {
                throw new ArgumentException("The number of elements in the source collection is greater than the available space from the index to the end of the destination array.");
            }

            var node = head;
            while (node != null)
            {
                array[arrayIndex++] = node.Value;
                node = node.Next;
            }
        }

        public bool Remove(T item)
        {
            var node = head;
            while (node != null)
            {
                if (node.Value.Equals(item))
                {
                    if (node.Previous != null)
                    {
                        node.Previous.Next = node.Next;
                    }
                    else
                    {
                        head = node.Next;
                    }
                    if (node.Next != null)
                    {
                        node.Next.Previous = node.Previous;
                    }
                    else
                    {
                        tail = node.Previous;
                    }
                    Count--;
                    return true;
                }
                node = node.Next;
            }
            return false;
        }

        public IEnumerator<T> GetEnumerator()
        {
            var node = head;
            while (node != null)
            {
                yield return node.Value;
                node = node.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
            {
                throw new ArgumentNullException(nameof(info));
            }
            info.AddValue("Count", Count);
            var index = 0;
            var array = new T[Count];
            var node = head;
            while (node != null)
            {
                array[index++] = node.Value;
                node = node.Next;
            }
            info.AddValue("Items", array);
        }

        void IDeserializationCallback.OnDeserialization(object sender)
        {
            // no-op
        }

        void ICollection<T>.Add(T item)
        {
            Add(item);
        }

        bool ICollection<T>.Remove(T item)
        {
            return Remove(item);
        }

        public void CopyTo(Array array, int index)
        {
            CopyTo(array, index);
        }
    }
}
