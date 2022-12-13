using System.Collections.Generic;
using UnityEngine;

namespace Code.Scripts
{
    public class StackHolder : MonoBehaviour
    {
        [SerializeField] public List<Transform> pokeBalls;
        public int PokeBallsCount => pokeBalls.Count;
        public static StackHolder Instance { get; private set; }

        private void Awake()
        {
            Instance ??= this;
        }

        public void RemoveBallFromList(Transform removeTransform)
        {
            if (pokeBalls.FindIndex(x => x == removeTransform) != -1)
            {
                pokeBalls.Remove(removeTransform);
            }
        }
    }
}