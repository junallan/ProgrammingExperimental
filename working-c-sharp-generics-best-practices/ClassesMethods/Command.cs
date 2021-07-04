using Ardalis.GuardClauses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generic.Commands
{
    public class Command : ICommand
    {
        protected Func<ICommand, object> ExecFunc { get; }

        public object Execute()
        {
            return ExecFunc(this);
        }

        public Command(Func<ICommand, object> execFunc)
        {
            ExecFunc = Guard.Against.Null(execFunc, nameof(execFunc));
        }
    }

    public class Command<TResult> : Command, ICommand<TResult> where TResult : class
    {
        new protected Func<ICommand<TResult>, TResult> ExecFunc => (ICommand<TResult> cmd) => (TResult)base.ExecFunc(cmd);

        TResult ICommand<TResult>.Execute()
        {
            return ExecFunc(this);
        }

        public Command(Func<ICommand<TResult>, TResult> execFunc) : base((ICommand c) => (object)execFunc((ICommand<TResult>)c))
        {

        }
    }

    public class ConcatCommand : Command<string>
    {
        public IEnumerable<String> Inputs { get; }

        public ConcatCommand(IEnumerable<String> inputs) : base((ICommand<string> c) => (string)String.Concat(((ConcatCommand)c).Inputs))
        {
            Inputs = Guard.Against.Null(inputs, nameof(inputs));
        }

    }

}
