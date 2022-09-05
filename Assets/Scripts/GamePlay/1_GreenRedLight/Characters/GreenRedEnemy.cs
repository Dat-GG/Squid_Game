using Core.Common;
using UnityEngine;

namespace GamePlay._1_GreenRedLight.Characters
{
    public class GreenRedEnemy : CharacterGreenRedController
    {
        [SerializeField] private float minSpeedOffset = -0.25f;
        [SerializeField] private float maxSpeedOffset = 0.25f;

        [SerializeField] private float minDelayOffset = 0f;
        [SerializeField] private float maxDelayOffset = 0.5f;

        public bool CanLive { get; set; }

        private void Start()
        {
            this.RegisterListener(EventID.GreenRedCanMove, (sender, param) =>
            {
                OnCanMove();
            });
            this.RegisterListener(EventID.GreenRedStopMove, (sender, param) =>
            {
                OnStopMove();
            });

            RandomMoveSpeed();
            CanLive = true;
        }

        private void RandomMoveSpeed()
        {
            float speedOffset = Random.Range(minSpeedOffset, maxSpeedOffset);
            LastMoveSpeed = moveSpeed + speedOffset;
        }

        private void OnStopMove()
        {
            if (CurrentStage == CharacterStage.Win || CurrentStage == CharacterStage.Die)
                return;

            if (CanLive)
            {
                EnterNewStage(CharacterStage.Stop);
            }
            else
            {
                float delay = Random.Range(0.2f, 0.5f);
                this.StartDelayMethod(delay, () =>
                {
                    this.PostEvent(EventID.GreenRedPoliceNeedShot);
                    EnterNewStage(CharacterStage.Die);
                });
            }
        }

        private void OnCanMove()
        {
            if (CurrentStage == CharacterStage.Win || CurrentStage == CharacterStage.Die)
                return;

            float delayOffset = Random.Range(minDelayOffset, maxDelayOffset);
            this.StartDelayMethod(delayOffset, () =>
            {
                EnterNewStage(CharacterStage.Run);
            });
        }
    }
}