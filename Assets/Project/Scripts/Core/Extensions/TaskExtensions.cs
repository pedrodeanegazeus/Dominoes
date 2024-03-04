using System;
using System.Collections;
using System.Threading.Tasks;

namespace Dominoes.Core.Extensions
{
    internal static class TaskExtensions
    {
        public static IEnumerator WaitTask(this Task task)
        {
            while (!task.IsCompleted)
            {
                yield return null;
            }

            if (task.IsFaulted)
            {
                throw task.Exception;
            }
        }

        public static IEnumerator WaitTask(this Task task, Action taskCompleted)
        {
            yield return WaitTask(task);
            taskCompleted?.Invoke();
        }

        public static IEnumerator WaitTask<TResult>(this Task<TResult> task, Action<TResult> taskCompleted)
        {
            yield return WaitTask(task);
            taskCompleted?.Invoke(task.Result);
        }
    }
}
