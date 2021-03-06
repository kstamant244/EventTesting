using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTCV.CorruptCore.Coroutines
{
    //public delegate IEnumerator<Yielder> CoroutineReturn();

    /// <summary>
    /// Used to manage and run coroutines
    /// </summary>
    public class CoroutineRunner
    {
        LinkedList<Coroutine> coroutines = new LinkedList<Coroutine>();
        public void StopAndClearAll()
        {
            foreach (var cor in coroutines)
            {
                cor.Stop();
            }
            coroutines.Clear();
        }

        public bool RemoveCoroutine(Coroutine coroutine)
        {
            return coroutines.Remove(coroutine);
        }
        public void AddCoroutineLast(Coroutine coroutine)
        {
            coroutines.AddLast(coroutine);
        }

        /// <summary>
        /// Starts a coroutine. IMPORTANT: coroutines always execute to the first yield when starting, add a "yield return null;" to skip this
        /// </summary>
        /// <param name="enumerator"></param>
        /// <returns></returns>
        public Coroutine StartCoroutine(IEnumerator<Yielder> enumerator)
        {
            Coroutine res = new Coroutine(enumerator);
            coroutines.AddLast(res);
            return res;
        }

        //public Coroutine StartCoroutineNoAuto(CoroutineReturn enumerator)
        //{
        //    Coroutine res = new Coroutine(enumerator);
        //    coroutines.AddLast(res);
        //    return res;
        //}


        public void Update()
        {
            var curCoroutineNode = coroutines.First;
            while (curCoroutineNode != null)
            {
                Coroutine curCoroutine = curCoroutineNode.Value;
                curCoroutine.DoCycle();
                var nextNode = curCoroutineNode.Next;
                if (curCoroutine.IsComplete)
                {
                    curCoroutineNode.Value = null;
                    coroutines.Remove(curCoroutineNode);
                }
                curCoroutineNode = nextNode;
            }
        }

    }
}
