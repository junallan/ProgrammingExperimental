using CompositeDemo.Structural;
using System;

namespace CompositeDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var root = new DirectoryItem("development");
            var proj1 = new DirectoryItem("project1");
            var proj2 = new DirectoryItem("project2");
            root.Add(proj1);
            root.Add(proj2);

            proj1.Add(new FileItem("p1f1.txt", 2100));
            proj1.Add(new FileItem("p1f2.txt", 3100));
            var subDir1 = new DirectoryItem("sub_dir1");
            subDir1.Add(new FileItem("p1f3.txt", 4100));
            subDir1.Add(new FileItem("p1f4.txt", 5100));
            proj1.Add(subDir1);

            proj2.Add(new FileItem("p2f1.txt", 6100));
            proj2.Add(new FileItem("p2f2.txt", 7100));

            Console.WriteLine($"Total size (proj2): {proj2.GetSizeInKB()}");
            Console.WriteLine($"Total size (proj1): {proj1.GetSizeInKB()}");
            Console.WriteLine($"Total size (root): {root.GetSizeInKB()}");
        }

        private static void StructuralExample()
        {
            var root = new Composite("root");
            root.Add(new Leaf("Leaf B"));

            var comp1 = new Composite("Composite C1");
            comp1.Add(new Leaf("Leaf C1-A"));
            comp1.Add(new Leaf("Leaf C1-B"));
            var comp2 = new Composite("Composite C2");
            comp2.Add(new Leaf("Leaf C2-A"));
            comp1.Add(comp2);

            root.Add(comp1);
            root.Add(new Leaf("Leaf C"));

            var leaf = new Leaf("Leaf D");
            //leaf.Add(new Leaf("X"));
            root.Add(leaf);
            root.Remove(leaf);

            root.PrimaryOperation(1);
        }
    }
}
