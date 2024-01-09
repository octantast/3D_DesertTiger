using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace._Lights.DataBases
{
    public class DataBaseRivers : MonoBehaviour
    {
        [SerializeField] private List<string> _tokenRiver;
        [SerializeField] private List<string> _nameRiver;

        public List<string> TokenRiver => _tokenRiver;
        public List<string> NameRiver => _nameRiver;
    }
}