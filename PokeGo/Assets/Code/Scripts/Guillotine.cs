using DG.Tweening;
using UnityEngine;

namespace Code.Scripts
{
    public class Guillotine : MonoBehaviour
    {
        [SerializeField] float guillotineDuration = 2;
        [SerializeField] float goTo = 45;
        
        private void Start()
        {
            transform.DOLocalRotate(new Vector3(0, 0, goTo), guillotineDuration).SetLoops(-1, LoopType.Yoyo);
        }
    }
}