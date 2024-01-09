using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace _Lights.RiversController
{
    public class MountainForest:MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        public string ConcatenateStrings(List<string> stringsToConcatenate)
        {
            StringBuilder resultBuilder = new StringBuilder();
            foreach (var str in stringsToConcatenate)
            {
                resultBuilder.Append(str);
            }

            return resultBuilder.ToString();
        }
    }
}