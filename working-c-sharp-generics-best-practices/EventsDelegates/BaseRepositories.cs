using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsDelegates
{
    public abstract class RepositoryBase<T> : IWriteRepository<T>
    {
        public event EventHandler<EntityAddedEventArgs<T>> EntityAdded;

        public abstract void Add(T entity);

        protected virtual void OnEntityAdded(EntityAddedEventArgs<T> eventArgs)
        {
            EntityAdded?.Invoke(this, eventArgs);
        }
    }

    public abstract class RepositoryBase2<T> : IWriteRepository<T>
    {
        public event EventHandler<EntityAddedEventArgs<T>> EntityAdded;

        public virtual void Add(T entity)
        {
            OnEntityAdded(new EntityAddedEventArgs<T>(entity));
        }

        protected virtual void OnEntityAdded(EntityAddedEventArgs<T> eventArgs)
        {
            EntityAdded?.Invoke(this, eventArgs);
        }
    }

    public abstract class RepositoryBase3<T> : IWriteRepository<T>
    {
        public event EventHandler<EntityAddedEventArgs<T>> EntityAdded;

        protected abstract void DoAdd(T entity);

        public void Add(T entity)
        {
            DoAdd(entity);
           
        }

        protected virtual void OnEntityAdded(EntityAddedEventArgs<T> eventArgs)
        {
            EntityAdded?.Invoke(this, eventArgs);
        }
    }

    public class Reposity3<T> : RepositoryBase3<T>
    {
        private static List<T> _data = new();

        protected override void DoAdd(T entity)
        {
            _data.Add(entity);
        }
    }


    public class Repository<T> : RepositoryBase<T>
    {
        private static List<T> _data = new();

        public IEnumerable<T> List()
        {
            return _data.AsEnumerable();
        }


        public override void Add(T entity)
        {
            _data.Add(entity);
            OnEntityAdded(new EntityAddedEventArgs<T>(entity));
        }
    }

    public class Repository2<T> : RepositoryBase2<T>
    {
        private static List<T> _data = new();

        public override void Add(T entity)
        {
            _data.Add(entity);

            base.Add(entity);
        }
    }

}
