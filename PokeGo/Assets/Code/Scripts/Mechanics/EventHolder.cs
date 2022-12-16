using System;
using UnityEngine;

namespace Code.Scripts.Mechanics
{
    public class EventHolder : MonoBehaviour
    {
        private static EventHolder _instance;

        public static EventHolder Instance
        {
            get
            {
                _instance = FindObjectOfType<EventHolder>();
                return _instance;
            }
            set { _instance = value; }
        }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
        }

        public Action OnFinishCollider;
        public Action OnTabPlay;
        public Action OnOpenRewards;
        public Action<string> OnNewCardFound;
        public Action<Transform> OnBallCollected;
        public Action<Transform> OnThornCollided;
        public Action<Transform> OnGuillotineCollided;
        public Action<Transform, Transform> OnPokeCardCollided;

       
        public void PlayTabbed()
        {
            OnTabPlay?.Invoke();
        }
        public void BallCollected(Transform ball)
        {
            OnBallCollected?.Invoke(ball);
        }

        public void PokeCardCollected(Transform card, Transform ball)
        {
            OnPokeCardCollided?.Invoke(card, ball);
        }

        public void ThornCollided(Transform ball)
        {
            OnThornCollided?.Invoke(ball);
        }

        public void GuillotineCollided(Transform ball)
        {
            OnGuillotineCollided?.Invoke(ball);
        }

        public void OpenRewards()
        {
            OnOpenRewards?.Invoke();
        }
        
        public void NewCardFound(string cardName)
        {
            OnNewCardFound?.Invoke(cardName);
        }


        public void FinishCollider()
        {
            OnFinishCollider?.Invoke();
        }
    }
}