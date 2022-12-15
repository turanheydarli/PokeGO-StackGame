using UnityEngine;

namespace Code.Scripts.Mechanics
{
    public class CollectedBall : MonoBehaviour
    {
        public bool isStored;
        public Transform storedPokemon;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Collectable"))
            {
                EventHolder.Instance.BallCollected(other.transform);
            }

            if (other.CompareTag("Thorn"))
            {
                EventHolder.Instance.ThornCollided(transform);
            }

            if (other.CompareTag("Guillotine"))
            {
                EventHolder.Instance.GuillotineCollided(transform);
            }

            if (other.CompareTag("PokeCard"))
            {
                EventHolder.Instance.PokeCardCollected(other.transform, transform);
            }
        }
    }
}