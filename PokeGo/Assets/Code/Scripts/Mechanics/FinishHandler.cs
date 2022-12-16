using System.Collections;
using Code.Scripts.Classes;
using Code.Scripts.Managers;
using DG.Tweening;
using UnityEngine;

namespace Code.Scripts.Mechanics
{
    public class FinishHandler : MonoBehaviour
    {
        [SerializeField] private Transform pokePipe;
        [SerializeField] private Transform pipe;
        [SerializeField] private Transform arrow;
        [SerializeField] private Transform jumpTransform;
        [SerializeField] private Transform particlePrefab;
        [SerializeField] private ParticleSystem vacuumParticle;

        private float _collectedBallCount;

        void OnEnable()
        {
            EventHolder.Instance.OnFinishCollider += FinishAnimationHandler;
        }

        private void FinishAnimationHandler()
        {
            CameraManager.Instance.OpenCamera("FinishCamera");
            StartCoroutine(StartAnimation());
        }

        IEnumerator StartAnimation()
        {
            var sequence = DOTween.Sequence();

            sequence.Join(pipe.DORotate(new Vector3(0, 25), 1));


            for (int i = 1; i < StackHolder.Instance.PokeBallsCount; i++)
            {
                Transform ball = StackHolder.Instance.pokeBalls[i];
                StackHolder.Instance.RemoveBallFromList(ball);

                ball.GetComponent<Animator>().SetTrigger($"OpenBall");

                sequence.Join(ball.DOJump(jumpTransform.position, 0.5f, 1, 0.5f));
                sequence.Insert(0.3f, ball.DORotate(Vector3.up * 90, 0.5f).SetLoops(4, LoopType.Incremental));
                sequence.Insert(0.2f, ball.DOScale(1.5f, 0.5f).OnComplete(() =>
                {
                    vacuumParticle.Play();
                    CollectedBall collectedBall = ball.GetComponent<CollectedBall>();
                    ball.DOScale(2f, 0.2f).OnComplete(() =>
                    {
                        ball.gameObject.SetActive(false);
                        if (collectedBall.isStored)
                        {
                            HandlePokeCard(collectedBall.storedPokemon.name.Split(" ")[0]);
                            SoundManager.Instance.Play("VacuumSound");

                            _collectedBallCount++;
                            collectedBall.storedPokemon.parent = pokePipe;
                            //Transform particle = Instantiate(particlePrefab, pokemon);
                            collectedBall.storedPokemon.DOLocalJump(Vector3.zero, 0.5f, 1, 0.5f)
                                .Insert(0.01f, collectedBall.storedPokemon.DOScale(3.5f, 0.2f))
                                .Insert(0.01f, collectedBall.storedPokemon.DORotate(Vector3.up * 180, 0.01f))
                                .Insert(0.3f, collectedBall.storedPokemon.DOScale(1, 0.2f));
                        }
                    });
                }));
                i--;
                yield return new WaitForSeconds(0.3f);
            }

            sequence.OnComplete(() =>
            {
                pipe.DORotate(new Vector3(0, 0), 1.5f);
                vacuumParticle.Stop();
                CameraManager.Instance.OpenCamera("ShowScore");
                arrow.DORotate(Vector3.forward * (_collectedBallCount * -10), 1.2f).OnComplete(() =>
                {
                    ESDataManager.Instance.Save();
                    EventHolder.Instance.OpenRewards();
                });
            });
        }

        void HandlePokeCard(string cardName)
        {
            switch (cardName)
            {
                case "Squirtle":
                    ESDataManager.Instance.gameData.squirtleCount++;
                    break;
                case "Charmander":
                    ESDataManager.Instance.gameData.charmanderCount++;
                    break;
                case "Charmeleon":
                    ESDataManager.Instance.gameData.charmeleonCount++;
                    break;
                case "Bulbasaur":
                    ESDataManager.Instance.gameData.bulbasaurCount++;
                    break;
            }
        }
    }
}