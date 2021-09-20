using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DoAnFramework
{
    class CodeHelpers
    {
        public static bool ExecuteWithTimeLimit(TimeSpan timeSpan, Action codeBlock)
        {
            try
            {
                var task = Task.Factory.StartNew(codeBlock);
                task.Wait(timeSpan);
                return task.IsCompleted;
            }
            catch (AggregateException ae)
            {
                throw ae.InnerExceptions[0];
            }
        }
    }
}
