using System.Collections.Generic;
using DG.Tweening;
using Spine;
using UnityEngine;

namespace GamePlay._1_GreenRedLight.Characters
{
    public class CharacterGreenRedController : MonoBehaviour
    {
        public float moveSpeed = 5f;

        [SerializeField] private GameObject bloodEffect;
        [SerializeField] private List<GameObject> soulEffects;

        private CharacterSpine _characterSpine;
        private CharacterStage _currentStage;
        private Transform _myTransform;

        private Vector3 _startPosition;
        private float _targetPositionX;

        private Vector3 _direction = Vector3.left;

        protected ManagerModeGreenRed managerMode;

        public CharacterStage CurrentStage => _currentStage;

        protected float LastMoveSpeed { get; set; }

        private void Awake()
        {
            _characterSpine = GetComponentInChildren<CharacterSpine>();
            _myTransform = transform;
        }

        public void InitData(Vector3 startPos, float endPosX, ManagerModeGreenRed mode)
        {
            managerMode = mode;
            _startPosition = startPos;
            _targetPositionX = endPosX;
            ChangeStage(CharacterStage.Init);
        }

        private void Update()
        {
            float dt = Time.deltaTime;
            OnUpdateStage(dt);
        }

        public void ChangeStage(CharacterStage nextStage)
        {
            if (nextStage == _currentStage)
                return;

            ExitCurrentStage();
            EnterNewStage(nextStage);
        }

        protected virtual void ExitCurrentStage()
        {
        }

        protected virtual void EnterNewStage(CharacterStage nextStage)
        {
            _currentStage = nextStage;

            switch (nextStage)
            {
                case CharacterStage.Init:
                    _characterSpine.PlayRun();
                    break;
                case CharacterStage.Prepare:
                    _characterSpine.PlayIdle();
                    break;
                case CharacterStage.Stop:
                    _characterSpine.PauseAnim();
                    break;
                case CharacterStage.Run:
                    _characterSpine.ResumeAnim();
                    _characterSpine.PlayRun();
                    break;
                case CharacterStage.Win:
                    OnWin();
                    break;
                case CharacterStage.Die:
                    OnDie();
                    break;
                case CharacterStage.Shoot:
                    _characterSpine.PlayShoot();
                    break;
                default:
                    break;
            }
        }

        protected virtual void OnUpdateStage(float dt)
        {
            switch (_currentStage)
            {
                case CharacterStage.Init:
                    GoToStartPosition(dt);
                    break;
                case CharacterStage.Prepare:
                    break;
                case CharacterStage.Stop:
                    break;
                case CharacterStage.Run:
                    GoToTargetPosition(dt);
                    break;
                case CharacterStage.Win:
                    break;
                case CharacterStage.Die:
                    break;
                case CharacterStage.Shoot:
                    break;
                default:
                    break;
            }
        }

        private void GoToStartPosition(float dt)
        {
            if (CustomUtilities.isCloseTo(_myTransform.position, _startPosition, 0.1f))
            {
                EnterNewStage(CharacterStage.Prepare);
                return;
            }

            var direction = _startPosition - _myTransform.position;
            direction.Normalize();
            _myTransform.Translate(CustomUtilities.Multiply(direction, moveSpeed, dt));
        }

        private void GoToTargetPosition(float dt)
        {
            _myTransform.Translate(CustomUtilities.Multiply(_direction, LastMoveSpeed, dt));

            if (_myTransform.position.x <= _targetPositionX)
            {
                EnterNewStage(CharacterStage.Win);
            }
        }

        private void OnWin()
        {
            float xRandom = Random.Range(1, 3f);
            _myTransform.DOMoveX(_myTransform.position.x - xRandom, 1f)
                .OnComplete(() =>
                {
                    _characterSpine.PlayWin();
                });
        }

        private void OnDie()
        {
            bloodEffect.SetActive(true);
            var trackEntry = _characterSpine.PlayDie();
            if (trackEntry is null)
                return;

            trackEntry.Complete += OnDieComplete;
        }

        private void OnDieComplete(TrackEntry trackEntry)
        {
            var soulRandomIndex = Random.Range(0, soulEffects.Count);
            soulEffects[soulRandomIndex].SetActive(true);
        }
    }
}