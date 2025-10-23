using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public static class CoroutineHelper
    {
        public static IEnumerator WaitTimeAndUse(float time, params Action[] actions)
        {
            yield return new WaitForSeconds(time);
            UseActions(actions);
        }

        public static IEnumerator WaitCorutineAndUse(Coroutine coroutine, params Action[] actions)
        {
            yield return coroutine;
            UseActions(actions);
        }

        public static IEnumerator WaitUntilAndUse(Func<bool> action, params Action[] actions)
        {
            yield return new WaitUntil(action);
            UseActions(actions);
        }

        public static IEnumerator WaitWhileAndUse(Func<bool> action, params Action[] actions)
        {
            yield return new WaitWhile(action);
            UseActions(actions);
        }

        private static void UseActions(Action[] actions)
        {
            foreach (var action in actions)
                action();
        }
    }
}
