using System;
using Code.Scripts.Managers;
using UnityEngine;

namespace Code.Scripts.Mechanics
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] public float verticalSpeed;
        [SerializeField] public float speedMultiplier;
        [SerializeField] private float _speed = 0.001f;


        private Vector3 _direction;
        private Animator _animator;
        private Animation _animation;
        private Transform _transform;

        private Touch _touch;

        void Start()
        {
            CameraManager.Instance.OpenCamera("VirtualCamera");
            CameraManager.Instance.SetFollow("VirtualCamera", transform);
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
            // transform.position += Vector3.forward * verticalSpeed * Time.deltaTime;
            //
            // if (Input.touchCount > 0)
            // {
            //     _touch = Input.GetTouch(0);
            //     if (_touch.phase == TouchPhase.Moved)
            //     {
            //         var position = transform.position;
            //         position = new Vector3(position.x + _touch.deltaPosition.x * _speed, transform.position.y, position.z);
            //         position = new Vector3(Mathf.Clamp(position.x,-0.8f, 0.8f), position.y, position.z);
            //         transform.position = position;
            //     }
            // }
            //
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
            SoundManager.Instance.Play("StartSound");
            SoundManager.Instance.Stop("BackgroundMusic");
            verticalSpeed = 3;
            speedMultiplier = 2;
        }
    }
}