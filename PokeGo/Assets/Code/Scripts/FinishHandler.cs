using System;
using System.Collections;
using Code.Scripts.Managers;
using DG.Tweening;
using UnityEngine;

namespace Code.Scripts
{
    public class FinishHandler : MonoBehaviour
    {
        [SerializeField] private Transform pokePipe;
        [SerializeField] private Transform pipe;
        [SerializeField] private Transform arrow;
        [SerializeField] private Transform jumpTransform;
        [SerializeField] private Transform pokemonPrefab;
        [SerializeField] private Transform particlePrefab;
        [SerializeField] private ParticleSystem vacuumParticle;

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
                    Transform pokemon = Instantiate(pokemonPrefab, ball);
                    pokemon.parent = pokePipe;
                    ball.DOScale(2f, 0.2f).OnComplete(() =>
                    {
                        //Transform particle = Instantiate(particlePrefab, pokemon);
                        ball.gameObject.SetActive(false);
                        pokemon.DOLocalJump(Vector3.zero, 0.5f, 1, 0.5f)
                            .Insert(0.01f, pokemon.DOScale(5, 0.2f))
                            .Insert(0.01f, pokemon.DORotate(Vector3.up * 180, 0.01f))
                            .Insert(0.3f, pokemon.DOScale(1, 0.2f));
                    });
                }));

                yield return new WaitForSeconds(0.3f);
            }

            sequence.OnComplete(() =>
            {
                pipe.DORotate(new Vector3(0, 0), 1.5f);
                vacuumParticle.Stop();
                            CameraManager.Instance.OpenCamera("ShowScore");

            });
        }
    }
}