using System;
using System.Collections;
using System.Threading.Tasks;

namespace Dominoes.Core.Extensions
{
    internal static class TaskExtensions
    {
        public static IEnumerator WaitForTaskCompleteRoutine(this Task task)
        {
            while (!task.IsCompleted)
            {
                yield return null;
            }
        }

        public static IEnumerator WaitForTaskCompleteRoutine(this Task task, Action taskCompleted)
        {
            yield return WaitForTaskCompleteRoutine(task);
            taskCompleted?.Invoke();
        }

        public static IEnumerator WaitForTaskCompleteRoutine<TResult>(this Task<TResult> task, Action<TResult> taskCompleted)
        {
            yield return WaitForTaskCompleteRoutine(task);
            taskCompleted?.Invoke(task.Result);
        }
    }
}
