using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Code.Scripts
{
    public class ObstacleHandler : MonoBehaviour
    {
        private void OnEnable()
        {
            EventHolder.Instance.OnThornCollided += SmashBall;
            EventHolder.Instance.OnGuillotineCollided += SplitBalls;
        }

        private void SmashBall(Transform ball)
        {
            StackHolder.Instance.pokeBalls.Remove(ball);
            ball.DOJump(Vector3.forward * 2, 1, 1, 1);
        }

        
        private void SplitBalls(Transform ball)
        {
            int colliderBall = StackHolder.Instance.pokeBalls.FindIndex(x => x == ball);

            if (colliderBall != -1)
            {
                List<Transform> droppedBalls = StackHolder.Instance.pokeBalls.GetRange(colliderBall, StackHolder.Instance.PokeBallsCount - colliderBall);

                foreach (var droppedBall in droppedBalls)
                {
                    //droppedBall.parent = null;
                    //droppedBall.tag = "Collectable";
                    StackHolder.Instance.pokeBalls.Remove(droppedBall);
                    droppedBall.DOJump(
                        droppedBall.position + new Vector3(Random.Range(-3f, 3f), 0, Random.Range(2f, 3f)), 0.5f, 1,
                        0.5f);
                    droppedBall.GetComponent<Rigidbody>().isKinematic = false;
                    droppedBall.GetComponent<Collider>().isTrigger = false;
                }
            }
        }
    }
}