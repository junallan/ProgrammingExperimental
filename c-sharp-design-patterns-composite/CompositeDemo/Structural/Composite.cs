using System;
using System.Collections.Generic;
using System.Text;

namespace CompositeDemo.Structural
{
    public class Composite : Component
    {
        public List<Component> children = new List<Component>();

        public Composite(string name) : base(name)
        {

        }

        public override void PrimaryOperation(int depth)
        {
            throw new NotImplementedException();
        }
    }
}
