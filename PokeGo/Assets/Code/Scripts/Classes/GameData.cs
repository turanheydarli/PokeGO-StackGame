using System.Collections.Generic;

namespace Code.Scripts.Classes
{
    [System.Serializable]
    public class GameData
    {
        public int levelIndex;
        public string playerName;
        public int soundLevel;
        public List<PokeCard> pokeCards;
    }
}