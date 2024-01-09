using UnityEngine;

namespace _Lights.Hands
{
    public class Appps:MonoBehaviour
    {
        public void Awake()
        {
            DontDestroyOnLoad(this);
        }
    }
}