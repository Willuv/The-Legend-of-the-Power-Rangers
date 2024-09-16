using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Legend_of_the_Power_Rangers
{
    public interface IController<T>
    {
        void Update();

        void RegisterCommand(T input, ICommand command);
    }
}
