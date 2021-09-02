using System;
using System.Collections.Generic;
using System.Text;

namespace CompositeDemo.Structural
{
    public class Leaf : Component
    {
        public Leaf(string name) : base(name)
        {

        }

        public override void PrimaryOperation(int depth)
        {
            Console.WriteLine(new String('-', depth) + this.Name);
        }
    }
}
