using Plugins.Scripts.DataAccount;

namespace DataAccount
{
    public static class DataAccountPlayer
    {
        private static PlayerMoney _playerMoney;
        private static Player1GreenRedLight _player1GreenRedLight;
        
        
        private static PlayerChallenger1PowerUp _playerChallenger1PowerUp;
        private static Challenger1Spin _challenger1Spin;
        private static PlayerChallenger2PowerUp _playerChallenger2PowerUp;
        private static PlayerChallenger3 _playerChallenger3;
        private static PlayerChallenger4PowerUp _playerChallenger4PowerUp;
        private static Challenger4Spin _challenger4Spin;
        private static PlayerChallenger5PowerUp _playerChallenger5PowerUp;
        private static BotChallenger5PowerUp _botChallenger5PowerUp;
        private static PlayerChallenger6PowerUp _playerChallenger6PowerUp;
        private static PlayerChallenger7PowerUp _playerChallenger7PowerUp;
        private static PlayerDailyCheckIn _playerDailyCheckIn;
        private static PlayerSettings _playerSettings;
        private static PlayerCharacter _playerCharacter;
        private static PlayerSkins _playerSkins;
        
        #region Getters

        public static PlayerMoney PlayerMoney
        {
            get
            {
                if (_playerMoney != null)
                {
                    return _playerMoney;
                }

                _playerMoney = ES3.Load(DataAccountPlayerConstants.PlayerMoney, new PlayerMoney());
                return _playerMoney;
            }
        }
        
        public static PlayerSkins PlayerSkins
        {
            get
            {
                if (_playerSkins != null)
                    return _playerSkins;

                var playerSkins = new PlayerSkins();
                _playerSkins = ES3.Load(DataAccountPlayerConstants.PlayerSkins, playerSkins);
                return _playerSkins;
            }
        }

        public static Player1GreenRedLight Player1GreenRedLight
        {
            get
            {
                if (_player1GreenRedLight != null)
                    return _player1GreenRedLight;
                
                var player = new Player1GreenRedLight();
                _player1GreenRedLight = ES3.Load(DataAccountPlayerConstants.Player1GreenRedLight, player);
                return _player1GreenRedLight;
            }
        }
        
        public static Challenger1Spin Challenger1Spin
        {
            get
            {
                if (_challenger1Spin != null)
                    return _challenger1Spin;

                var challenger1Spin = new Challenger1Spin();
                _challenger1Spin = ES3.Load(DataAccountPlayerConstants.Challenger1Spin, challenger1Spin);
                return _challenger1Spin;
            }
        }
        
        public static Challenger4Spin Challenger4Spin
        {
            get
            {
                if (_challenger4Spin != null)
                    return _challenger4Spin;

                var challenger4Spin = new Challenger4Spin();
                _challenger4Spin = ES3.Load(DataAccountPlayerConstants.Challenger4Spin, challenger4Spin);
                return _challenger4Spin;
            }
        }

        public static PlayerChallenger1PowerUp PlayerChallenger1PowerUp
        {
            get
            {
                if (_playerChallenger1PowerUp != null)
                {
                    return _playerChallenger1PowerUp;
                }

                _playerChallenger1PowerUp = ES3.Load(DataAccountPlayerConstants.PlayerChallenger1PowerUp,
                    new PlayerChallenger1PowerUp());
                return _playerChallenger1PowerUp;
            }
        }

        public static PlayerChallenger2PowerUp PlayerChallenger2PowerUp
        {
            get
            {
                if (_playerChallenger2PowerUp != null)
                {
                    return _playerChallenger2PowerUp;
                }

                _playerChallenger2PowerUp = ES3.Load(DataAccountPlayerConstants.PlayerChallenger2PowerUp, 
                    new PlayerChallenger2PowerUp());
                return _playerChallenger2PowerUp;
            }
        }
        
        public static PlayerChallenger3 PlayerChallenger3
        {
            get
            {
                if (_playerChallenger3 != null)
                {
                    return _playerChallenger3;
                }

                _playerChallenger3 = ES3.Load(DataAccountPlayerConstants.PlayerChallenger3, 
                    new PlayerChallenger3());
                return _playerChallenger3;
            }
        }

        public static PlayerChallenger4PowerUp PlayerChallenger4PowerUp
        {
            get
            {
                if (_playerChallenger4PowerUp != null)
                {
                    return _playerChallenger4PowerUp;
                }
                var playerChallenger4PowerUp = new PlayerChallenger4PowerUp();
                _playerChallenger4PowerUp = ES3.Load(DataAccountPlayerConstants.PlayerChallenger4PowerUp,
                    playerChallenger4PowerUp);
                return _playerChallenger4PowerUp;
            }
        }
        
        public static PlayerChallenger5PowerUp PlayerChallenger5PowerUp
        {
            get
            {
                if (_playerChallenger5PowerUp != null)
                {
                    return _playerChallenger5PowerUp;
                }

                var playerChallenger5PowerUp = new PlayerChallenger5PowerUp();
                _playerChallenger5PowerUp = ES3.Load(DataAccountPlayerConstants.PlayerChallenger5PowerUp,
                    playerChallenger5PowerUp);
                return _playerChallenger5PowerUp;
            }
        }
        
        public static BotChallenger5PowerUp BotChallenger5PowerUp
        {
            get
            {
                if (_botChallenger5PowerUp != null)
                {
                    return _botChallenger5PowerUp;
                }

                _botChallenger5PowerUp = ES3.Load(DataAccountPlayerConstants.BotChallenger5PowerUp,
                    new BotChallenger5PowerUp());
                return _botChallenger5PowerUp;
            }
        }

        public static PlayerChallenger6PowerUp PlayerChallenger6PowerUp
        {
            get
            {
                if (_playerChallenger6PowerUp != null)
                {
                    return _playerChallenger6PowerUp;
                }

                var playerChallenger6PowerUp = new PlayerChallenger6PowerUp();
                _playerChallenger6PowerUp = ES3.Load(DataAccountPlayerConstants.PlayerChallenger6PowerUp,
                    playerChallenger6PowerUp);
                return _playerChallenger6PowerUp;
            }
        }
        
        public static PlayerChallenger7PowerUp PlayerChallenger7PowerUp
        {
            get
            {
                if (_playerChallenger7PowerUp != null)
                {
                    return _playerChallenger7PowerUp;
                }

                var playerChallenger7PowerUp = new PlayerChallenger7PowerUp();
                _playerChallenger7PowerUp = ES3.Load(DataAccountPlayerConstants.PlayerChallenger7PowerUp,
                    playerChallenger7PowerUp);
                return _playerChallenger7PowerUp;
            }
        }
        
        public static PlayerDailyCheckIn PlayerDailyCheckIn
        {
            get
            {
                if (_playerDailyCheckIn != null)
                {
                    return _playerDailyCheckIn;
                }

                var playerDailyCheckIn = new PlayerDailyCheckIn();
                _playerDailyCheckIn = ES3.Load(DataAccountPlayerConstants.PlayerDailyCheckIn, playerDailyCheckIn);
                return _playerDailyCheckIn;
            }
        }

        public static PlayerSettings PlayerSettings
        {
            get
            {
                if (_playerSettings != null)
                {
                    return _playerSettings;
                }

                var playerSettings = new PlayerSettings();
                _playerSettings = ES3.Load(DataAccountPlayerConstants.PlayerSettings, playerSettings);
                return _playerSettings;
            }
        }

        public static PlayerCharacter PlayerCharacter
        {
            get
            {
                if (_playerCharacter != null)
                    return _playerCharacter;

                var playerCharacter = new PlayerCharacter();
                _playerCharacter = ES3.Load(DataAccountPlayerConstants.PlayerCharacter, playerCharacter);
                return _playerCharacter;
            }
        }
        
        #endregion

        #region Save

        public static void SavePlayerMoney()
        {
            ES3.Save(DataAccountPlayerConstants.PlayerMoney, _playerMoney);
        }
        
        public static void SavePlayerSkins()
        {
            ES3.Save(DataAccountPlayerConstants.PlayerSkins, _playerSkins);
        }

        public static void SavePlayer1GreenRedLight()
        {
            ES3.Save(DataAccountPlayerConstants.Player1GreenRedLight, _player1GreenRedLight);
        }

        public static void SavePlayerChallenger1PowerUp()
        {
            ES3.Save(DataAccountPlayerConstants.PlayerChallenger1PowerUp, _playerChallenger1PowerUp);
        }
        
        public static void SaveChallenger1Spin()
        {
            ES3.Save(DataAccountPlayerConstants.Challenger1Spin, _challenger1Spin);
        }

        public static void SavePlayerChallenger2PowerUp()
        {
            ES3.Save(DataAccountPlayerConstants.PlayerChallenger2PowerUp, _playerChallenger2PowerUp);
        }
        
        public static void SavePlayerChallenger3()
        {
            ES3.Save(DataAccountPlayerConstants.PlayerChallenger3, _playerChallenger3);
        }
        
        public static void SavePlayerChallenger4PowerUp()
        {
            ES3.Save(DataAccountPlayerConstants.PlayerChallenger4PowerUp, _playerChallenger4PowerUp);
        }
        
        public static void SaveChallenger4Spin()
        {
            ES3.Save(DataAccountPlayerConstants.Challenger4Spin, _challenger4Spin);
        }
        
        public static void SavePlayerChallenger5PowerUp()
        {
            ES3.Save(DataAccountPlayerConstants.PlayerChallenger5PowerUp, _playerChallenger5PowerUp);
        }
        
        public static void SaveBotChallenger5PowerUp()
        {
            ES3.Save(DataAccountPlayerConstants.BotChallenger5PowerUp, _botChallenger5PowerUp);
        }

        public static void SavePlayerChallenger6PowerUp()
        {
            ES3.Save(DataAccountPlayerConstants.PlayerChallenger6PowerUp, _playerChallenger6PowerUp);
        }
        
        public static void SavePlayerChallenger7PowerUp()
        {
            ES3.Save(DataAccountPlayerConstants.PlayerChallenger7PowerUp, _playerChallenger7PowerUp);
        }
        
        public static void SavePlayerDailyCheckIn()
        {
            ES3.Save(DataAccountPlayerConstants.PlayerDailyCheckIn, _playerDailyCheckIn);
        }

        public static void SavePlayerSettings()
        {
            ES3.Save(DataAccountPlayerConstants.PlayerSettings, _playerSettings);
        }

        public static void SavePlayerCharacter()
        {
            ES3.Save(DataAccountPlayerConstants.PlayerCharacter, _playerCharacter);
        }
        
        #endregion
    }
}