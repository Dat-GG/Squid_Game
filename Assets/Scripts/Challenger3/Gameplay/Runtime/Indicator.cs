using Challenger3.Player;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Challenger3.Gameplay.Runtime
{
    public class Indicator : MonoBehaviour
    {
        // [SerializeField] private Transform maxRangeTop;
        // [SerializeField] private Transform maxRangeBottom;

        [SerializeField] public Transform startPos;
        [SerializeField] public Transform endPos;

        // [SerializeField] private float rotZ;
        // [SerializeField] private float rotateSpeed;
        [SerializeField] private GameObject arrow;

        [SerializeField] private Transform startArrowPos;
        [SerializeField] private Transform endArrowPos;

        [SerializeField] public Image fill;
        [SerializeField] public Button fillFirstBtn;
        [SerializeField] public Button fillSecondBtn;
        [SerializeField] public Button throwFirstBtn;
        [SerializeField] public Button throwSecondBtn;
    
        [HideInInspector] public float minFill;
        
        [HideInInspector] public bool isFill;
        // [HideInInspector] public bool isRotation;
        [HideInInspector] public bool isChangeFill;
        [HideInInspector] public bool canThrow;
        [HideInInspector] public float currentFill;
        [HideInInspector] public Transform parent;

        // private bool _clockWiseRotation;
        // private int _indicatorDir;
        private Tweener _arrowRotate;
        private float _maxFill;
        private PlayerController3 _player;

        private void Awake()
        {
            throwFirstBtn.onClick.AddListener(ThrowFirst);
            throwSecondBtn.onClick.AddListener(ThrowSecond);
            fillFirstBtn.onClick.AddListener(IsFillFirst);
            fillSecondBtn.onClick.AddListener(IsFillSecond);
        }

        private void Start()
        {
            _player = FindObjectOfType<PlayerController3>();

            parent = transform.parent;
            parent.position = startArrowPos.position;
            // _indicatorDir = -1;
            _maxFill = 1;
            currentFill = _maxFill;
            // isRotation = true;

            _arrowRotate = ArrowRotate();
        }

        private void ThrowFirst()
        {
            throwFirstBtn.gameObject.SetActive(false);
            if (canThrow)
            {
                this.PostEvent(EventID.ThrowMarbleFirst);
                canThrow = false;
            }
        }

        private void ThrowSecond()
        {
            throwSecondBtn.gameObject.SetActive(false);
            if (canThrow)
            {
                this.PostEvent(EventID.ThrowMarbleSecond);
                canThrow = false;
            }
        }
        
        private void Update()
        {
            // Rotation();
            Fill();
        }

        private void IsFillFirst()
        {
            _player.PlayerSpine.PlayThrowStart();
            
            fillFirstBtn.gameObject.SetActive(false);
            throwFirstBtn.gameObject.SetActive(true);
            isChangeFill = true;
            // isRotation = false;
            _arrowRotate.Pause();
        }
        
        private void IsFillSecond()
        {
            _player.PlayerSpine.PlayThrowStart();
            
            fillSecondBtn.gameObject.SetActive(false);
            throwSecondBtn.gameObject.SetActive(true);
            isChangeFill = true;
            // isRotation = false;
            _arrowRotate.Pause();
        }

        // private void Rotation()
        // {
        //     if (isRotation)
        //     {
        //         if (endPos.position.y >= maxRangeTop.position.y || endPos.position.y <= maxRangeBottom.position.y)
        //         {
        //             _clockWiseRotation = !_clockWiseRotation;
        //         }
        //
        //         if (_clockWiseRotation)
        //         {
        //             rotZ += Time.deltaTime * rotateSpeed;
        //         }
        //         else
        //         {
        //             rotZ += -Time.deltaTime * rotateSpeed;
        //         }
        //     
        //         // transform.rotation = Quaternion.Euler(0,0,rotZ);
        //         transform.DOLocalRotateQuaternion(Quaternion.Euler(0, 0, rotZ), 0); 
        //     }
        // }

        private Tweener ArrowRotate()
        {
            var tween = transform.DORotateQuaternion(Quaternion.Euler(0, 0, -38), 1)
                .SetEase(Ease.InOutSine)
                .SetLoops(-1, LoopType.Yoyo);
            
            return tween;
        }
        
        private void Fill()
        {
            if (isChangeFill)
            {
                if (currentFill <= 0)
                {
                    currentFill = _maxFill;
                    isFill = !isFill;
                }

                if (minFill > 1)
                {
                    minFill = 0;
                    isFill = !isFill;
                }

                fill.fillAmount = isFill ? currentFill / _maxFill : minFill / _maxFill;

                if (isFill)
                    currentFill -= Time.deltaTime;
                else
                    minFill += Time.deltaTime;
            }
        }

        public void Reset()
        {
            isChangeFill = false;
            isFill = false;
            // isRotation = false;
            _arrowRotate.Pause();
            currentFill = _maxFill;
            minFill = 0;
            fillFirstBtn.gameObject.SetActive(false);
            fillSecondBtn.gameObject.SetActive(false);
            throwFirstBtn.gameObject.SetActive(false);
            throwSecondBtn.gameObject.SetActive(false);
        }

        public void WhenPlayerTurn()
        {
            arrow.transform.DOMoveX(endArrowPos.position.x, 0.5f);
            // isRotation = true;
            _arrowRotate.Play();
        }

        public void WhenEnemyTurn()
        {
            arrow.transform.DOMoveX(startArrowPos.position.x, 0.5f);
        }
    }
}