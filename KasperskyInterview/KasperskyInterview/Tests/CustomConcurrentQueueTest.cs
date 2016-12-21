using System.Threading.Tasks;
using KasperskyInterview.Tasks;
using NUnit.Framework;

namespace KasperskyInterview.Tests
{
    [TestFixture]
    public class CustomConcurrentQueueTest
    {
        [Test]
        public void Queue_SingleThreadPopPush_ReturnValue()
        {
            // Arrange
            var concurrentQueue = new CustomConcurrentQueue<int>();

            // Act             
            concurrentQueue.Push(5);
            concurrentQueue.Push(6);
            var result = concurrentQueue.Pop();

            // Assert
            Assert.AreEqual(5, result);
        }

        [Test]
        public void Init_FromEmptyCopy_WithOutExcpetion()
        {
            // Arrange
            var concurrentQueue = new CustomConcurrentQueue<int>(null);

            // Act             
            concurrentQueue.Push(5);
            concurrentQueue.Push(6);
            var result = concurrentQueue.Pop();

            // Assert
            Assert.AreEqual(5, result);
        }

        [Test]
        public async Task Queue_MultiThreadPop_ReturnValues()
        {
            // Arrange
            var concurrentQueue = new CustomConcurrentQueue<int>();

            // Act             
            concurrentQueue.Push(5);
            concurrentQueue.Push(6);

            var firstTask = Task.Run(() => concurrentQueue.Pop());
            var secondTask = Task.Run(() => concurrentQueue.Pop());

            var result = await Task.WhenAll(firstTask, secondTask);;

            // Assert
            Assert.AreEqual(result.Length, 2);
            Assert.That(result[0], Is.EqualTo(5).Or.EqualTo(6));
            Assert.That(result[1], Is.EqualTo(5).Or.EqualTo(6));
        }

        [Test]
        public void Queue_MultiThreadPopPush_ReturnValues()
        {
            // Arrange
            var concurrentQueue = new CustomConcurrentQueue<int>();

            // Act             
            var firstTask = Task.Run(() => concurrentQueue.Pop());
            var secondTask = Task.Run(() => concurrentQueue.Pop());
            var thirdTask = Task.Run(() =>
            {
                concurrentQueue.Push(5);
                concurrentQueue.Push(6);
                concurrentQueue.Push(7);
            });

           Task.WaitAll(firstTask, secondTask, thirdTask);

            // Assert
            Assert.That(firstTask.Result, Is.EqualTo(5).Or.EqualTo(6));
            Assert.That(secondTask.Result, Is.EqualTo(5).Or.EqualTo(6));
        }
    }
}
