using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generic.Commands
{
    public interface ICommand
    {
        object Execute();
    }

    public interface ICommand<TResult> : ICommand
    {
        new TResult Execute();
    }
}
