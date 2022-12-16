using Code.Scripts.Managers;
using UnityEngine;

namespace Code.Scripts.Mechanics
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float verticalSpeed;
        [SerializeField] private float speedMultiplier;


        private Vector3 _direction;
        private Animator _animator;
        private Animation _animation;
        private Transform _transform;

        void Start()
        {
            _transform = transform;
            _animator = GetComponent<Animator>();
            StackHolder.Instance.pokeBalls.Add(transform.GetChild(0));
            EventHolder.Instance.OnTabPlay += PlayGame;
        }

        void FixedUpdate()
        {
            Movement();
        }

        private void Movement()
        {
            _direction = new Vector3(InputManager.Horizontal * speedMultiplier, 0, verticalSpeed) * Time.fixedDeltaTime;
            _transform.Translate(_direction.x, 0, _direction.z);
            _animator.SetFloat($"Running", _direction.magnitude);

            var localPosition = _transform.localPosition;
            localPosition = new Vector3(Mathf.Clamp(localPosition.x, -0.8f, 0.8f), localPosition.y, localPosition.z);
            _transform.localPosition = localPosition;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Collectable"))
            {
                EventHolder.Instance.BallCollected(other.transform);
            }

            if (other.CompareTag("Finish"))
            {
                verticalSpeed = 0;
                speedMultiplier = 0;
                EventHolder.Instance.FinishCollider();
            }
        }

        void PlayGame()
        {
            SoundManager.Instance.Play("PlayModeMusic");
            SoundManager.Instance.Stop("BackgroundMusic");
            verticalSpeed = 3;
            speedMultiplier = 2;
        }
    }
}