using Challenger4.Gameplay.Managers;
using Challenger4.UI.ProcessBar;
using DG.Tweening;
using UnityEngine;

namespace Challenger4.TugRope
{
    public class RopeMovement : MonoBehaviour
    {
        private ProcessBarController _processBarController;
        private GameManagerChallenger4 _gameManagerChallenger4;
        private void Start()
        {
            _processBarController = FindObjectOfType<ProcessBarController>();
            _gameManagerChallenger4 = FindObjectOfType<GameManagerChallenger4>();
        }
        
        private void Update()
        {
            if (_gameManagerChallenger4.isWin)
            {
                transform.Translate(new Vector3(1, 0, 0) * (20 * Time.deltaTime));
                DOVirtual.DelayedCall(0.125f, delegate
                {
                    _gameManagerChallenger4.isWin = false;
                });
            }
            else if (_gameManagerChallenger4.isBotTugWin)
            {
                transform.Translate(new Vector3(-1, 0, 0) * (20 * Time.deltaTime));
                DOVirtual.DelayedCall(0.125f, delegate
                {
                    _gameManagerChallenger4.isBotTugWin = false;
                });
            }
            else if(_processBarController.isPower)
            {
                transform.Translate(new Vector3(1, 0, 0) * (20 * Time.deltaTime));
                DOVirtual.DelayedCall(0.125f, delegate
                {
                    _processBarController.isPower = false;
                });
            }
            
            if (_processBarController.moveRight)
            {
                transform.Translate(new Vector3(1, 0, 0) * (1.5f * Time.deltaTime));
                DOVirtual.DelayedCall(0.2f, delegate
                {
                    _processBarController.moveRight = false;
                });
            }
            else if (_processBarController.moveLeft)
            {
                transform.Translate(new Vector3(-1, 0, 0) * (1.5f * Time.deltaTime));
                DOVirtual.DelayedCall(0.2f, delegate
                {
                    _processBarController.moveLeft = false;
                });
            }
        }
    }
}
