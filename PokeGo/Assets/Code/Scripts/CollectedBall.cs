using UnityEngine;

namespace Code.Scripts
{
    public class CollectedBall : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Collectable"))
            {
                EventHolder.Instance.BallCollected(other.transform);
            }
            if (other.CompareTag("Thorn"))
            {
                EventHolder.Instance.ThornCollider(transform);
            }
            if (other.CompareTag("Guillotine"))
            {
                EventHolder.Instance.GuillotineCollider(transform);
            }
        }
    }
}