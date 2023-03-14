using System.Threading.Tasks;
using System;

namespace Zacks_Outlining_Tool.Utilities;

public static class TaskExtensions
{

    public static async Task WithTimeout(this Task task, TimeSpan timeout)
    {
        if (task == await Task.WhenAny(task, Task.Delay(timeout)))
        {
            await task;
        }
        else
        {
            throw new TimeoutException();
        }
    }

    public static async Task<T> WithTimeout<T>(this Task<T> task, TimeSpan timeout)
    {
        if (task == await Task.WhenAny(task, Task.Delay(timeout)))
        {
            return await task;
        }
        else
        {
            throw new TimeoutException();
        }
    }


}
