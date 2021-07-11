using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsDelegates
{
    public class DataLoader<T>
    {
        private readonly IWriteRepository<T> _repo;
        private int counter = 0;

        public int Counter => counter;

        public DataLoader(IWriteRepository<T> repository)
        {
            _repo = repository;
            _repo.EntityAdded += _repo_EntityAdded;
        }

        private void _repo_EntityAdded(object sender, EntityAddedEventArgs<T> e)
        {
            counter++;
        }

        public void Load(IEnumerable<T> data)
        {
            foreach(var entity in data)
            {
                _repo.Add(entity);
            }
        }
    }
}
