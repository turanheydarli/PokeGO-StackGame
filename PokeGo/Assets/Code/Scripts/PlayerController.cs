using UnityEngine;

namespace Code.Scripts
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float verticalSpeed;
        [SerializeField] private float speedMultiplier;


        private Vector3 _direction;
        private Animator _animator;
        private Animation _animation;
        private Transform _transform;
        private bool _isMoving = true;

        void Start()
        {
            _transform = transform;
            _animator = GetComponent<Animator>();
            StackHolder.Instance.pokeBalls.Add(transform.GetChild(0));
        }

        void FixedUpdate()
        {
            if (_isMoving)
                Movement();
        }

        private void Movement()
        {
            _direction = new Vector3(InputManager.Horizontal * speedMultiplier, 0, verticalSpeed) * Time.fixedDeltaTime;
            _transform.Translate(_direction.x, 0, _direction.z);
            _animator.SetFloat($"Running", _direction.magnitude);

            var localPosition = _transform.localPosition;
            localPosition = new Vector3(Mathf.Clamp(localPosition.x, -1f, 1f), localPosition.y, localPosition.z);
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
                _isMoving = false;
                EventHolder.Instance.FinishCollider();
            }
        }
    }
}