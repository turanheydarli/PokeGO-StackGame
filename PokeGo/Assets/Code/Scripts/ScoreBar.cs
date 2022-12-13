using DG.Tweening;
using UnityEngine;

namespace Code.Scripts
{
    public class ScoreBar : MonoBehaviour
    {
        [SerializeField] private float cogWheelSpeed = 3f;
        [SerializeField] private Transform[] cogWheels;

        void Start()
        {
            cogWheels[0].DORotate(Vector3.forward * 90, cogWheelSpeed).SetLoops(-1, LoopType.Incremental);
            cogWheels[1].DORotate(Vector3.forward * -90, cogWheelSpeed).SetLoops(-1, LoopType.Incremental);
        }
    }
}