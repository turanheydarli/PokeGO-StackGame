using System;
using System.Collections;
using Code.Scripts.Mechanics;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
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
        [SerializeField] private Slider soundSlider;

        [Header("Loading Panel")] [SerializeField]
        private GameObject loadingPanel;

        [SerializeField] private Image pokemonFill;

        [Header("Rewards Panel")] [SerializeField]
        private GameObject rewardsPanel;

        [SerializeField] private Button rewardsBackButton;
        [SerializeField] private TMP_Text squirtleCount;
        [SerializeField] private TMP_Text charmeleonCount;
        [SerializeField] private TMP_Text bulbasaurCount;
        [SerializeField] private TMP_Text charmanderCount;

        [Header("Card Found Sprites")] [SerializeField]
        private GameObject cardFoundPanel;

        [SerializeField] private Button cardFoundBackButton;
        [SerializeField] private Image cardFront;
        [SerializeField] private Sprite squirtleSprite;
        [SerializeField] private Sprite charmeleonSprite;
        [SerializeField] private Sprite bulbasaurSprite;
        [SerializeField] private Sprite charmanderSprite;


        private void Awake()
        {
            OpenLoadingPanel();

            tapToPlayButton.onClick.AddListener(CloseHomePanel);
            tapToPlayButton.onClick.AddListener(EventHolder.Instance.PlayTabbed);
            settingsButton.onClick.AddListener(OpenSettingsPanel);
            backButton.onClick.AddListener(CloseSettingsPanel);
            rewardsBackButton.onClick.AddListener(CloseRewardsPanel);
            soundSlider.onValueChanged.AddListener(ChangeSoundVolume);
            cardFoundBackButton.onClick.AddListener(CloseCardFoundPanel);
            rewardsBackButton.onClick.AddListener(RestartGame);
            EventHolder.Instance.OnOpenRewards += OpenRewardsPanel;
            EventHolder.Instance.OnNewCardFound += OpenCardFoundPanel;
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
            DOTween.To(() => pokemonFill.fillAmount, x => pokemonFill.fillAmount = x, 1, 3f);
            yield return new WaitForSeconds(3f);
            CloseLoadingPanel();
            OpenHomePanel();
        }

        private void FirstInitialize()
        {
            soundSlider.value = ESDataManager.Instance.gameData.soundLevel * 100 / 11.11111111f;
            playerName.text = ESDataManager.Instance.gameData.playerName;
            levelNumber.text = $"Stage: {ESDataManager.Instance.gameData.levelIndex + 1}";
            pokeCardCount.text = $"x{ESDataManager.Instance.gameData.pokeCards.Count}";
        }

        void ChangeSoundVolume(float volume)
        {
            SoundManager.Instance.ChangeVolume(volume / 100 * 11.11111111f);
        }

        void OpenRewardsPanel()
        {
            SoundManager.Instance.Stop("PlayModeMusic");

            rewardsPanel.SetActive(true);
            bulbasaurCount.text = $"{ESDataManager.Instance.gameData.bulbasaurCount}/10";
            squirtleCount.text = $"{ESDataManager.Instance.gameData.squirtleCount}/10";
            charmeleonCount.text = $"{ESDataManager.Instance.gameData.charmeleonCount}/10";
            charmanderCount.text = $"{ESDataManager.Instance.gameData.charmanderCount}/10";
            rewardsBackButton.interactable = false;
            StartCoroutine(ShowFoundCard());
        }

        void CloseRewardsPanel()
        {
            rewardsPanel.SetActive(false);
        }


        private void OpenCardFoundPanel(string cardName)
        {
            cardFoundPanel.SetActive(true);

            switch (cardName)
            {
                case "Bulbasaur":
                    cardFront.sprite = bulbasaurSprite;
                    break;
                case "Charmander":
                    cardFront.sprite = charmanderSprite;
                    break;
                case "Charmeleon":
                    cardFront.sprite = charmeleonSprite;
                    break;
                case "Squirtle":
                    cardFront.sprite = squirtleSprite;
                    break;
            }
        }

        void RestartGame()
        {
            CameraManager.Instance.OpenCamera("VirtualCamera");
            StackHolder.Instance.pokeBalls.Clear();
            DOTween.KillAll();
            SceneManager.LoadScene(0);
        }


        private void CloseCardFoundPanel()
        {
            cardFoundPanel.SetActive(false);
        }

        IEnumerator ShowFoundCard()
        {
            yield return new WaitForSeconds(2);

            var foundCards = ESDataManager.Instance.FoundCards();

            foreach (var foundCard in foundCards)
            {
                EventHolder.Instance.NewCardFound(foundCard);
            }

            rewardsBackButton.interactable = true;
        }
    }
}