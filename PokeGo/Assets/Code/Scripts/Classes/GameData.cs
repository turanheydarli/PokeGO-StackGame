using System.Collections.Generic;
using UnityEngine;

namespace Code.Scripts.Classes
{
    [System.Serializable]
    public class GameData
    {
        public int levelIndex;
        public string playerName;
        [Range(0, 1)] public float soundLevel;
        public List<PokeCard> pokeCards;
        public int squirtleCount;
        public int charmeleonCount;
        public int bulbasaurCount;
        public int charmanderCount;
    }
}