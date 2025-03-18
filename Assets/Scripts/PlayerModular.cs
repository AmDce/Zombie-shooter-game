using UnityEngine;

namespace ZombieShooter.Player
{
    public class PlayerModular
    {
        private static PlayerModular _instance;

        public static PlayerModular Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new PlayerModular();
                }
                return _instance;
            }
        }

        public string CurrentGunName = $"Pistol";
        public int RequiredKills = 10;

        private int _killCount;

        public void UpdateCurrentGun(string gName)
        {
            CurrentGunName = gName;
            Debug.Log($"Current GunName : {CurrentGunName}");
        }

        public void AddKill()
        {
            _killCount++;
            Debug.Log($"Kill Count: {_killCount}/{RequiredKills}");

            if (_killCount >= RequiredKills)
            {
                LevelComplete();
            }
        }

        private void LevelComplete()
        {
            GameEvents.TriggerGameOver(true, "Level Completed ...");
        }
    }
}

