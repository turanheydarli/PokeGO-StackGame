using Code.Scripts.Managers;
using DG.Tweening;
using UnityEngine;

namespace Code.Scripts.Mechanics
{
    public class BallCollector : MonoBehaviour
    {
        [SerializeField] private float lerpDuration;
        [SerializeField] private float stackOffset;
        
        private Sequence _sequence;

        void OnEnable()
        {
            EventHolder.Instance.OnBallCollected += CollectBall;
            EventHolder.Instance.OnPokeCardCollided += CollectPokeCard;
        }

        private void FixedUpdate()
        {
            StackFollow();
        }

        private void CollectBall(Transform ball)
        {
            SoundManager.Instance.Play("CollectBallSound");
            
            ball.tag = "Collected";
            StackHolder.Instance.pokeBalls.Add(ball);
            ball.gameObject.AddComponent<CollectedBall>();
            ball.gameObject.AddComponent<Rigidbody>().isKinematic = true;

            _sequence = DOTween.Sequence();
            _sequence.Kill();
            _sequence = DOTween.Sequence();
            for (int i = StackHolder.Instance.PokeBallsCount - 1; i > 0; i--)
            {
                _sequence.Join(StackHolder.Instance.pokeBalls[i].DOScale(1.5f, 0.1f));
                _sequence.AppendInterval(0.05f);
                _sequence.Join(StackHolder.Instance.pokeBalls[i].DOScale(1f, 0.1f));
            }
        }

        private void StackFollow()
        {
            float lerpSpeed = lerpDuration;

            for (int i = 1; i < StackHolder.Instance.PokeBallsCount; i++)
            {
                Vector3 previousPos = StackHolder.Instance.pokeBalls[i - 1].transform.position +
                                      Vector3.forward * stackOffset;
                Vector3 currentPos = StackHolder.Instance.pokeBalls[i].transform.position;
                StackHolder.Instance.pokeBalls[i].transform.position =
                    Vector3.Lerp(currentPos, previousPos, lerpSpeed * Time.fixedDeltaTime);

                lerpSpeed += lerpDuration * Time.fixedDeltaTime;
            }
        }

        private void CollectPokeCard(Transform card, Transform ball)
        {
            CollectedBall ballScript = ball.GetComponent<CollectedBall>();

            if (!ballScript.isStored)
            {
                ballScript.storedPokemon = card;
                ballScript.isStored = true;
                var sequence = DOTween.Sequence();
                sequence.Append(card.transform.DOScale(2, 0.2f));
                card.parent = ball;
                sequence.Append(card.transform.DOLocalJump(Vector3.zero, 0.3f,1, 0.2f));
                sequence.Insert(0.3f,card.transform.DOScale(0, 0.2f));
            }
        }
    }
}