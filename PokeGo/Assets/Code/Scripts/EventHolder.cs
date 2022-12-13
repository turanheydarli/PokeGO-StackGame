﻿using System;
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
        public Action<Transform> OnThornCollided;
        public Action<Transform> OnGuillotineCollided;
        public Action<Transform> OnPokeCardCollided;
        public Action<Transform> OnSellBall;

        public void BallCollected(Transform ball)
        {
            OnBallCollected?.Invoke(ball);
        }
        public void PokeCardCollected(Transform ball)
        {
            OnPokeCardCollided?.Invoke(ball);
        }
        
        public void ThornCollided(Transform ball)
        {
            OnThornCollided?.Invoke(ball);
        }
        public void GuillotineCollided(Transform ball)
        {
            OnGuillotineCollided?.Invoke(ball);
        }
        public void SellBallCollided(Transform ball)
        {
            OnSellBall?.Invoke(ball);
        }
        public void FinishCollider()
        {
            OnFinishCollider?.Invoke();
        }
    }
}