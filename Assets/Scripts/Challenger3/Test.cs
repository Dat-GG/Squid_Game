// using DG.Tweening;
// using UnityEngine;
// using UnityEngine.UI;
//
//
//     public class PowerIndicator : MonoBehaviour
//     {
//         [SerializeField] private Button clickOnFillBtn;
//         [SerializeField] private Button clickThrowBtn;
//         [SerializeField] private Image fill;
//         [SerializeField] private float rotZ;
//         
//         [SerializeField] private Transform posRange1;
//         [SerializeField] private Transform posRange2;
//         [SerializeField] private Transform headPos;
//         
//         [SerializeField] private GameObject angleThrow;
//         [SerializeField] private Transform posMoveTrue;
//         [SerializeField] private Transform posMoveFalse;
//         
//         private int _dir = -1;
//         private float _fillCurren;
//         private float _fillMax = 1f;
//         private float _fillZero = 0f;
//         
//         private bool _isFill = true;
//         private bool _isOnFill;
//         private bool _isRot =true;
//         private bool _isThrow;
//
//         private PlayerController _playerController;
//         internal float FillCurren => _fillCurren;
//         internal Button ClickOnFill => clickOnFillBtn;
//         internal Transform HeadPos => headPos;
//         internal bool IsOnFill => _isOnFill;
//         public GameObject AngleThrow => angleThrow; 
//
//         public bool IsThrow
//         {
//             get => _isThrow;
//             set => _isThrow = value;
//         }
//
//         private void Awake()
//         {
//             clickOnFillBtn.onClick.AddListener(OnCLickOnFill);
//             clickThrowBtn.onClick.AddListener(OnclickThrowBtn);
//             _playerController = FindObjectOfType<PlayerController>();
//         }
//         private void Update()
//         {
//             if (_isOnFill) ChangeFill();
//             if(_isRot) ArrowRotation();
//             if(MarblesPlayer.IsTurn == 2) ResetIndicator();
//           
//             
//         }
//
//         private void ArrowRotation()
//         {
//             
//             if (headPos.position.y < posRange1.position.y || headPos.position.y > posRange2.position.y)
//                 _dir *= -1;
//             transform.Rotate(0, 0, rotZ* _dir);
//         }
//
//         private void ChangeFill()
//         {
//            
//                 if (_fillCurren < 0)
//                 {
//                     _fillCurren = _fillMax;
//                     _isFill = !_isFill;
//                 }
//
//                 if (_fillZero > 1)
//                 {
//                     _fillZero = 0;
//                     _isFill = !_isFill;
//                 }
//
//                 fill.fillAmount = _isFill ? _fillCurren / _fillMax : _fillZero / _fillMax;
//                 if (_isFill)
//                     _fillCurren -= Time.deltaTime;
//                 else
//                     _fillZero += Time.deltaTime;
//            
//         }
//
//         private void OnCLickOnFill()
//         {
//             clickThrowBtn.gameObject.SetActive(true);
//             clickOnFillBtn.gameObject.SetActive(false);
//             _isRot = false;
//             _isOnFill = true;
//         }
//
//         private void OnclickThrowBtn()
//         {
//             clickThrowBtn.gameObject.SetActive(false);
//            MarblesPlayer.IsTurn = 1;
//             _isOnFill = false;
//             _isThrow = true;
//         }
//
//         private void ResetIndicator()
//         {
//             _isRot = true;
//             _isOnFill = false;
//             fill.fillAmount = 0;
//         }
//         
//         public void ThrowRangeMove() => angleThrow.transform.DOMoveX(posMoveTrue.position.x, 0.5f);
//         public void ThrowRangeBack() =>  angleThrow.transform.DOMoveX(posMoveFalse.position.x, 0.5f);
//             
//     }
