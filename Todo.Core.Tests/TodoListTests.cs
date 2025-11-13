namespace Todo.Core.Tests
{
    [TestClass]
    public class TodoListTests
    {
        [TestMethod]
        public void AddIncrementsCount()
        {
            var list = new TodoList();
            _ = list.Add("task");
            Assert.AreEqual(1, list.Count);
        }

        [TestMethod]
        public void RemoveByIdWorks()
        {
            var list = new TodoList();
            var i = list.Add("a");
            Assert.IsTrue(list.Remove(i.Id));
        }
    }
}