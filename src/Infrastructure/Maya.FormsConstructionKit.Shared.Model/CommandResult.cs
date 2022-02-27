using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maya.FormsConstructionKit.Shared.Model
{
    public class CommandResult
    {
        public bool IsSuccess { get; set; }

        public string Message { get; set; } = null!;

        public static CommandResult Success()
        {
            return new CommandResult
            {
                IsSuccess = true
            };
        }

        public static CommandResult Failure(string message)
        {
            return new CommandResult
            {
                IsSuccess = false,
                Message = message
            };
        }
    }
}
