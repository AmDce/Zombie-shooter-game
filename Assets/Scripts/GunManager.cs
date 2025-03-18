using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace ZombieShooter.Guns
{
    public class GunManager : MonoBehaviour
    {
        public static GunManager Instance;

        public List<GunData> gunList = new List<GunData>();
        private Dictionary<string, GunData> gunsData = new Dictionary<string, GunData>();

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadGuns();
        }

        private void LoadGuns()
        {
            foreach (GunData gun in gunList)
            {
                if (!gunsData.ContainsKey(gun.GunName))
                {
                    gunsData.Add(gun.GunName, gun);
                }
            }
        }

        public GunData GetGun(string gunName)
        {
            gunsData.TryGetValue(gunName, out GunData gun);
            return gun;
        }

        public Dictionary<string, GunData> GetAllGuns()
        {
            return gunsData;
        }
    }
}


[System.Serializable]
public class GunData
{
    public string GunName;
    public Sprite GunSprite;
    public int Damage;
    public float BulletSpeed;
    public float FireRate;
    public float ReloadTime;
    public int AmmoCapacity;
}

