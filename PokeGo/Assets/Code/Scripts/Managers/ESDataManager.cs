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
    }
}