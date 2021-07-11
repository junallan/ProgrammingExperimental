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
        event EventHandler<EntityAddedEventArgs<T>> EntityAdded;
    }

    public class EntityAddedEventArgs<T> : EventArgs
    {
        public EntityAddedEventArgs(T entityAdded)
        {
            EntityAdded = entityAdded;
        }

        public T EntityAdded { get; }
    }

    public interface IReadRepository<T>
    {
        IEnumerable<T> List();
    }

    //public class Repository<T> : IWriteRepository<T>, IReadRepository<T>
    //{
    //    private static List<T> _data = new();

    //    public IEnumerable<T> List()
    //    {
    //        return _data.AsEnumerable();
    //    }

    //    public void Add(T entity)
    //    {
    //        _data.Add(entity);
    //        OnEntityAdded(new EntityAddedEventArgs<T>(entity));
    //    }

    //    public event EventHandler<EntityAddedEventArgs<T>> EntityAdded;

    //    protected virtual void OnEntityAdded(EntityAddedEventArgs<T> entityAdded)
    //    {
    //        EntityAdded?.Invoke(this, entityAdded);
    //    }

       
    //}
}
