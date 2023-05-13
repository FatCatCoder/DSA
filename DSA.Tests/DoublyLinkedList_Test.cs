using NUnit.Framework;
using DSA.Library;
using DSA.Library.DataStructures;

namespace DSA.Tests
{
    [TestFixture]
    public class DoublyLinkedList_Test
    {
        [Test]
        public void Add_AddsElementToList()
        {
            // Arrange
            var list = new DoublyLinkedList<int>();

            // Act
            list.Add(1);

            // Assert
            Assert.AreEqual(1, list.Count);
            Assert.AreEqual(1, list.Head.Value);
            Assert.AreEqual(1, list.Tail.Value);
        }

        [Test]
        public void Remove_RemovesElementFromList()
        {
            // Arrange
            var list = new DoublyLinkedList<int>();
            list.Add(1);
            list.Add(2);
            list.Add(3);

            // Act
            var removed = list.Remove(2);

            // Assert
            Assert.IsTrue(removed);
            Assert.AreEqual(2, list.Count);
            Assert.AreEqual(1, list.Head.Value);
            Assert.AreEqual(3, list.Tail.Value);
        }

        [Test]
        public void Contains_ReturnsTrueIfElementExists()
        {
            // Arrange
            var list = new DoublyLinkedList<int>();
            list.Add(1);
            list.Add(2);
            list.Add(3);

            // Act
            var contains = list.Contains(2);

            // Assert
            Assert.IsTrue(contains);
        }

        [Test]
        public void Contains_ReturnsFalseIfElementDoesNotExist()
        {
            // Arrange
            var list = new DoublyLinkedList<int>();
            list.Add(1);
            list.Add(2);
            list.Add(3);

            // Act
            var contains = list.Contains(4);

            // Assert
            Assert.IsFalse(contains);
        }

        [Test]
        public void CopyTo_CopiesElementsToArray()
        {
            // Arrange
            var list = new DoublyLinkedList<int>();
            list.Add(1);
            list.Add(2);
            list.Add(3);
            var array = new int[3];

            // Act
            list.CopyTo(array, 0);

            // Assert
            Assert.AreEqual(1, array[0]);
            Assert.AreEqual(2, array[1]);
            Assert.AreEqual(3, array[2]);
        }
    }
}