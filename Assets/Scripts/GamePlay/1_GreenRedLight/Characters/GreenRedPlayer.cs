namespace GamePlay._1_GreenRedLight.Characters
{
    public class GreenRedPlayer : CharacterGreenRedController
    {
        private GreenRedInputChecking _inputChecking;

        private void Start()
        {
            _inputChecking = FindObjectOfType<GreenRedInputChecking>();
            LastMoveSpeed = moveSpeed;
        }

        private void FixedUpdate()
        {
            if (!managerMode.IsStarted)
                return;
            
            CheckInput();
        }

        private void CheckInput()
        {
            if (CurrentStage == CharacterStage.Die)
                return;

            if (CurrentStage == CharacterStage.Win)
                return;

            if (_inputChecking.IsHolding)
            {
                ChangeStage(CharacterStage.Run);
            }
            else
            {
                ChangeStage(CharacterStage.Stop);
            }
        }

        protected override void EnterNewStage(CharacterStage nextStage)
        {
            base.EnterNewStage(nextStage);
            if (nextStage == CharacterStage.Prepare)
            {
                this.PostEvent(EventID.GreenRedPlayerReachedStartLine);
            }

            if (nextStage == CharacterStage.Win)
            {
                this.PostEvent(EventID.GreenRedPlayerReachedFinishLine);
            }
        }
    }
}