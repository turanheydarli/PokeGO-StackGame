using System;
using System.Collections;
using Code.Scripts.Mechanics;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Scripts.Managers
{
    public class UIManager : MonoBehaviour
    {
        [Header("Home Panel")] [SerializeField]
        private GameObject homePanel;

        [SerializeField] private Button tapToPlayButton;

        [SerializeField] private Button inventoryButton;
        [SerializeField] private GameObject inventory;

        [SerializeField] private Button settingsButton;
        [SerializeField] private GameObject settings;

        [SerializeField] private Button pokeBattleButton;
        [SerializeField] private GameObject pokeBattle;

        [Header("Home Panel / Profile Card")] [SerializeField]
        private GameObject profileCard;

        [SerializeField] private TMP_Text playerName;
        [SerializeField] private TMP_Text pokeCardCount;
        [SerializeField] private TMP_Text levelNumber;

        [Header("Settings Panel")] [SerializeField]
        private GameObject settingsPanel;

        [SerializeField] private Button backButton;

        [Header("Loading Panel")] [SerializeField]
        private GameObject loadingPanel;

        [SerializeField] private Transform pokemonEar;
        [SerializeField] private Transform pokemonTrail;


        private void Awake()
        {
            OpenLoadingPanel();

            tapToPlayButton.onClick.AddListener(CloseHomePanel);
            tapToPlayButton.onClick.AddListener(EventHolder.Instance.PlayTabbed);
            settingsButton.onClick.AddListener(OpenSettingsPanel);
            backButton.onClick.AddListener(CloseSettingsPanel);
        }

        private void Start()
        {
            FirstInitialize();
        }

        #region Methods

        void OpenLoadingPanel()
        {
            loadingPanel.SetActive(true);
            StartCoroutine(LoadingAnimation());
        }

        void CloseLoadingPanel()
        {
            loadingPanel.SetActive(false);
        }

        private void OpenHomePanel()
        {
            Sequence sequence = DOTween.Sequence();

            sequence.Join(inventory.transform.DOMoveX(165, 0));
            sequence.Join(pokeBattle.transform.DOMoveX(160, 0));
            sequence.Join(settings.transform.DOMoveX(922, 0));
            sequence.Join(profileCard.transform.DOMoveY(1620, 0));

            sequence.OnComplete(() => { homePanel.SetActive(true); });

            tapToPlayButton.transform.DOScale(1.7f, 1).SetLoops(-1, LoopType.Yoyo);
        }

        private void CloseHomePanel()
        {
            Sequence sequence = DOTween.Sequence();

            sequence.Join(inventory.transform.DOMoveX(-150, 0.3f));
            sequence.Join(pokeBattle.transform.DOMoveX(-100, 0.3f));
            sequence.Join(profileCard.transform.DOMoveY(2300, 0.3f));
            sequence.Join(settings.transform.DOMoveX(1350, 0.3f));

            sequence.OnComplete(() => { homePanel.SetActive(false); });
        }

        private void OpenSettingsPanel()
        {
            settingsPanel.SetActive(true);
        }

        private void CloseSettingsPanel()
        {
            settingsPanel.SetActive(false);
        }

        #endregion

        IEnumerator LoadingAnimation()
        {
            pokemonEar.DORotate(Vector3.forward * 30, .4f).SetLoops(5, LoopType.Yoyo);
            pokemonTrail.DORotate(Vector3.forward * -10, .5f).SetLoops(-1, LoopType.Yoyo);
            yield return new WaitForSeconds(3f);
            CloseLoadingPanel();
            OpenHomePanel();
        }

        private void FirstInitialize()
        {
            playerName.text = ESDataManager.Instance.gameData.playerName;
            levelNumber.text = $"Stage: {ESDataManager.Instance.gameData.levelIndex + 1}";
            pokeCardCount.text = $"x{ESDataManager.Instance.gameData.pokeCards.Count}";
        }
    }
}