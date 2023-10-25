using System;
using System.Collections;
using System.Threading.Tasks;

namespace Dominoes.Core.Extensions
{
    internal static class TaskExtensions
    {
        public static IEnumerator WaitForTaskCompleteRoutine(this Task task, Action<Task> taskCompleted)
        {
            while (!task.IsCompleted)
            {
                yield return null;
            }
            taskCompleted?.Invoke(task);
        }

        public static IEnumerator WaitForTaskCompleteRoutine<TResult>(this Task<TResult> task, Action<Task<TResult>> taskCompleted)
        {
            while (!task.IsCompleted)
            {
                yield return null;
            }
            taskCompleted?.Invoke(task);
        }
    }
}
