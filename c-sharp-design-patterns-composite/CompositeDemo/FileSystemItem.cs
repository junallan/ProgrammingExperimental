using System;
using System.Collections.Generic;
using System.Text;

namespace CompositeDemo
{
    public abstract class FileSystemItem
    { 
        public FileSystemItem(string name)
        {
            this.Name = name;
        }

        public string Name { get; }

        public abstract decimal GetSizeInKB();
    }
}
