using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsDelegates
{
    public interface IWriteRepository<T>
    {
        void Add(T entity);
        event EventHandler<T> EntityAdded;
    }

    public interface IReadRepository<T>
    {
        IEnumerable<T> List();
    }

    public class Repository<T> : IWriteRepository<T>//, IReadRepository<T>
    {
        private static List<T> _data = new();

        public IEnumerable<T> List()
        {
            return _data.AsEnumerable();
        }

        public void Add(T entity)
        {
            _data.Add(entity);
            OnEntityAdded(entity);
        }

        public event EventHandler<T> EntityAdded;

        protected virtual void OnEntityAdded(T entityAdded)
        {
            EntityAdded?.Invoke(this, entityAdded);
        }

       
    }
}
