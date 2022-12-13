using System;
using UnityEngine;

namespace Code.Scripts
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
        public Action<Transform> OnBallCollected;
        public Action<Transform> OnThornCollider;
        public Action<Transform> OnGuillotineCollider;
        public Action<Transform> OnSellBall;

        public void BallCollected(Transform ball)
        {
            OnBallCollected?.Invoke(ball);
        }
        
        public void ThornCollider(Transform ball)
        {
            OnThornCollider?.Invoke(ball);
        }
        public void GuillotineCollider(Transform ball)
        {
            OnGuillotineCollider?.Invoke(ball);
        }
        public void SellBallCollider(Transform ball)
        {
            OnSellBall?.Invoke(ball);
        }
        public void FinishCollider()
        {
            OnFinishCollider?.Invoke();
        }
    }
}