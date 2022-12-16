using System.Collections.Generic;
using Code.Scripts.Classes;
using UnityEngine;

namespace Code.Scripts.Managers
{
    public class ESDataManager : MonoBehaviour
    {
        private static ESDataManager _instance;

        public static ESDataManager Instance
        {
            get
            {
                _instance = FindObjectOfType<ESDataManager>();
                return _instance;
            }
            set { _instance = value; }
        }

        public GameData gameData;

        public const string _dataKey = "gameData";

        private void Awake()
        {
            FirstInitialize();
            Load();
        }

        public void Load()
        {
            if (ES3.FileExists())
            {
                if (ES3.KeyExists(_dataKey)) gameData = ES3.Load(_dataKey, gameData);
            }
        }

        public void Save()
        {
            ES3.Save(_dataKey, gameData);
        }

        private void FirstInitialize()
        {
            if (!ES3.KeyExists(_dataKey))
            {
                gameData.levelIndex = 0;
                gameData.playerName = $"#cyberpoke{Random.Range(0, 99)}";
                gameData.pokeCards = new List<PokeCard>();
                Save();
            }
        }

        public string[] FoundCards()
        {
            List<string> cards = new List<string>();

            if (gameData.bulbasaurCount >= 10)
            {
                gameData.bulbasaurCount -= 10;
                cards.Add("Bulbasaur");
            }
            
            if (gameData.charmanderCount >= 10)
            {
                gameData.charmanderCount -= 10;
                cards.Add("Charmander");
            }
            
            if (gameData.charmeleonCount >= 10)
            {
                gameData.charmeleonCount -= 10;
                cards.Add("Charmeleon");
            }
            
            if (gameData.squirtleCount >= 10)
            {
                gameData.squirtleCount -= 10;
                cards.Add("Squirtle");
            }
            Instance.Save();
            return cards.ToArray();
        }
    }
}