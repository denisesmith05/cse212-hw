using Microsoft.VisualStudio.TestTools.UnitTesting;

// TODO Problem 2 - Write and run test cases and fix the code to match requirements.

[TestClass]
public class PriorityQueueTests
{
    private PriorityQueue _queue;

    [TestInitialize]
    public void SetUp()
    {
        _queue = new PriorityQueue();
    }

    [TestMethod]
    // Scenario: Enqueing multiple items with varying priorities into an empty queue.
    // Expected Result: Items should be added to queue, maintaining order of insertion, regardless of their priority.
    // Defect(s) Found: none
    public void Enqueue_AddsItemsToQueue()
    {
        _queue.Enqueue("value1", 1);
        _queue.Enqueue("value2", 2);
        _queue.Enqueue("value3", 3);
        
        // Assert: Check if the queue contains the expected items
        var expectedOutput = "[value1 (Pri:1), value2 (Pri:2), value3 (Pri:3)]";
        Assert.AreEqual(expectedOutput, _queue.ToString());
    }

    [TestMethod]
    // Scenario: Dequeing each item in the proper order regardless of priority.
    // Expected Result: Items with the highest priority should be dequed and return their value.
    // Defect(s) Found: Error Message: Assert.AreEqual failed. Expected:<value2>. Actual:<value3>. 
                    // 1. Changed the comparative operator <= to < 
                    // 2. Took away -1 from _queue.Count 
                    // 3. Added _queue.RemoveAt(highPriorityIndex) to remove the item from the queue.

    public void Dequeue_TakesItemsAway()
    {
        // Arrange: First, enqueue some items
        _queue.Enqueue("value1", 1);
        _queue.Enqueue("value2", 2);
        _queue.Enqueue("value3", 3);

        Assert.AreEqual("value3", _queue.Dequeue()); // value3 has the highest priority
        Assert.AreEqual("value2", _queue.Dequeue()); // value2 has the next highest priority
        Assert.AreEqual("value1", _queue.Dequeue()); // value1 is the last item in the queue
    }
   
    [TestMethod]
    // Scenario: Tests to see if there are more than one item with the highest priority.
    // Expected Result: Item closest to the front of the queue will be removed and it's value returned.
    // Defect(s) Found: Error Message: Assert.AreEqual failed. Expected:<value2>. Actual:<value1>. 
                    // Error fixed by changes made in the Deque method. (changes listed in Enqueue_AddsItemsToQueue() test)
    public void Test_PriorityItem() 
    {
        // Arrange: Enqueue items with the same priority
        _queue.Enqueue("value1", 3); // Same priority
        _queue.Enqueue("value2", 3); // Same priority
        _queue.Enqueue("value3", 3); // Same priority

        // Act & Assert: The first item enqueued with the highest priority should be dequeued first
        Assert.AreEqual("value1", _queue.Dequeue()); // value1 is closest to the front
        Assert.AreEqual("value2", _queue.Dequeue()); // value2 is next closest
        Assert.AreEqual("value3", _queue.Dequeue()); // value3 is last
    }

    [TestMethod]
    // Scenario: Tests the behavior when there are items with mixed priorities in the queue,and some items have the same priority.
    // Expected Result: The item with the highest priority will be removed first. If there are 
    // multiple items with the same priority, the item closest to the front of the queue will 
    // be dequeued first.
    // Defect(s) Found: Error Message:Assert.AreEqual failed. Expected:<value2>. Actual:<value1>". 
    // This was caused by an issue in the Dequeue method, where it wasn't handling multiple items with the same priority 
    // correctly. The issue was fixed by correcting the loop condition in the Dequeue method (as detailed in the Enqueue_AddsItemsToQueue() test).

    public void Dequeue_WithMixedPriorities_RemovesCorrectItem()
    {
        // Arrange: Enqueue items with mixed priorities
        _queue.Enqueue("lowPriority1", 1);
        _queue.Enqueue("highPriority", 5);
        _queue.Enqueue("lowPriority2", 1);
        _queue.Enqueue("mediumPriority", 3);

        // Act & Assert: Ensure that the highest priority item is dequeued first
        Assert.AreEqual("highPriority", _queue.Dequeue()); // Highest priority (5)
        Assert.AreEqual("mediumPriority", _queue.Dequeue()); // Next highest (3)
        // Then ensure the first of the equal priority items is dequeued
        Assert.AreEqual("lowPriority1", _queue.Dequeue()); // Closest to front
        Assert.AreEqual("lowPriority2", _queue.Dequeue()); // Remaining low-priority item
    }


    [TestMethod]
    public void Enqueue_And_Dequeue_SingleItem()
    {
        // Arrange: Enqueue a single item
        _queue.Enqueue("singleItem", 1);

        // Act & Assert: Ensure that the single item is dequeued correctly
        Assert.AreEqual("singleItem", _queue.Dequeue());
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void Test_Exception() 
    {
        // Act: Try to dequeue from an empty queue, which should throw an exception
        _queue.Dequeue();
    }
}