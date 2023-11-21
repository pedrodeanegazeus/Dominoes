using System;
using System.Collections;
using System.Threading.Tasks;

namespace Dominoes.Core.Extensions
{
    internal static class TaskExtensions
    {
        public static IEnumerator WaitForTaskCompleteRoutine(this Task task, Action taskCompleted)
        {
            while (!task.IsCompleted)
            {
                yield return null;
            }
            taskCompleted?.Invoke();
        }

        public static IEnumerator WaitForTaskCompleteRoutine<TResult>(this Task<TResult> task, Action<TResult> taskCompleted)
        {
            while (!task.IsCompleted)
            {
                yield return null;
            }
            taskCompleted?.Invoke(task.Result);
        }
    }
}
