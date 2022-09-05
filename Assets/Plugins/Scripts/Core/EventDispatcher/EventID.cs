public enum EventID
{
    EarnMoney,
    SpendMoney,
    EarnReward,
    QuitLuckySpin,

    PlayerHealthAdd,
    PlayerHealthSub,
    PlayerManaAdd,
    PlayerManaSub,

    // Gameplay
    PlayerHealthCharge,
    PlayerManaCharge,
    TriggerCheckPoint,
    PlayerDie,
    KeyGet,
    PlayerReachEndGate,
    EarnGoldInGamePlay,
    EnemyDie,

    // GreenRedLight
    GreenRedPlayerReachedStartLine,
    GreenRedPlayerReachedFinishLine,
    GreenRedDollyFinishRotate,
    GreenRedCanMove,
    GreenRedStopMove,
    GreenRedPoliceNeedShot,


    //Challenger1
    CollectPowerUpChallenger1,
    UsePowerUpChallenger1,

    //Challenger2
    CollectPowerUpChallenger2,
    UsePowerUpChallenger2,

    //Challenger3
    ThrowMarbleFirst,
    ThrowMarbleSecond,

    //Challenger4
    CollectPowerUpChallenger4,
    UsePowerUpChallenger4,

    //Challenger5
    BotHavePowerUpChallenger5,
    BotUsePowerUpChallenger5,
    CollectPowerUpChallenger5,
    UsePowerUpChallenger5,

    //Challenger7
    CollectGold,
    TimesUp,
    PlayerLose,
    PlayerWin,
    CollectPowerUpChallenger7,
    UsePowerUpChallenger7,

    // Settings
    OnSoundChange,
    OnMusicChange,
    OnVibrationChange,
    OnCountryChange,

    InHouse,

    //UI
    SkinElementSelect,
    SkinToggleSelect,
    SkinUsingSelected,
    SkinUnlockSuccess,
}