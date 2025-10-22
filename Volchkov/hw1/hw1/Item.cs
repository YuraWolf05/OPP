namespace hw1
{
    public class Item
    {
        public string Name { get; set; }
        public int Priority { get; set; }

        public Item() { }

        public Item(string name, int priority)
        {
            Name = name;
            Priority = priority;
        }

        public override string ToString()
        {
            return $"{Name} (priority: {Priority})";
        }
    }

  
    public class ItemPriorityComparer : IComparer<Item>
    {
        public int Compare(Item? x, Item? y)
        {
            if (x == null || y == null) return 0;
            return x.Priority.CompareTo(y.Priority);
        }
    }
}
