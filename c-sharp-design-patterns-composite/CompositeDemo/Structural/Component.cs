using System;
using System.Collections.Generic;
using System.Text;

namespace CompositeDemo.Structural
{
    public abstract class Component
    {
        public Component(string name)
        {
            this.Name = name;
        }

        public string Name { get; }

        public abstract void PrimaryOperation(int depth);

        //public abstract void Add(Component c);
        //public abstract void Remove(Component c);
    }
}
