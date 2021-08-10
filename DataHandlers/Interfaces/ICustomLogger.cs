using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rokono_Control.DataHandlers.Interfaces
{
    public interface ICustomLogger
    {
        void Message(string message);
        void Error(string message, string errorCode);
        void Warning(string message, string warningCode);
        void Info(string message);
    }
}
