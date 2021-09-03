using System;
using System.Collections.Generic;
using System.Text;

namespace CompositeDemo.Structural
{
    public class Composite : Component
    {
        private List<Component> children = new List<Component>();

        public Composite(string name) : base(name)
        {

        }

        public override void Add(Component c)
        {
            this.children.Add(c);
        }

        public override void PrimaryOperation(int depth)
        {
            Console.WriteLine(new String('-', depth) + this.Name);

            foreach(var component in this.children)
            {
                component.PrimaryOperation(depth + 2);
            }
        }

        public override void Remove(Component c)
        {
            this.children.Remove(c);
        }
    }
}
