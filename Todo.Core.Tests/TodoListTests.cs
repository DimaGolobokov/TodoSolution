using Xunit;

namespace Todo.Core.Tests
{
    [TestClass]
    public class TodoListTests
    {
        [Fact]
        public void AddIncrementsCount()
        {
            var list = new TodoList();
            _ = list.Add("task");
            Assert.Equals(1, list.Count);
        }
        [Fact]
        public void RemoveByIdWorks()
        {
            var list = new TodoList();
            var i = list.Add("a");
            Assert.IsTrue(list.Remove(i.Id));
        }

    }
}