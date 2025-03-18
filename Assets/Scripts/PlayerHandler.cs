using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using ZombieShooter.bullet;
using ZombieShooter.Guns;
using ZombieShooter.Player;
using TMPro;
using UnityEngine.UI;

namespace ZombieShooter.PlayerController
{
    public class PlayerHandler : CharacterBase
    {
        [SerializeField] private Transform firePoint;
        [SerializeField] private SpriteRenderer GunLogo;

        public GunData gunData;

        private int currentAmmo;
        private bool isReloading = false;
        private float nextFireTime = 0f;

        private void Awake()
        {
            Debug.Log($"gunName :{PlayerModular.Instance.CurrentGunName}");
            gunData = GunManager.Instance.GetGun(PlayerModular.Instance.CurrentGunName);
            GunLogo.sprite = gunData.GunSprite;
            currentAmmo = gunData.AmmoCapacity;
        }

        protected override void Start()
        {
            MaxHealth = 100;
            base.Start();
        }

        private void Update()
        {
            if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W)) && Time.time >= nextFireTime && !isReloading)
            {
                if (currentAmmo > 0)
                {
                    Attack();

                }
                else
                {
                    StartCoroutine(Reload());
                }
            }
        }

        public override void Die()
        {
            Debug.Log("Player Died! Reloading Level...");
            GameEvents.TriggerGameOver(true, "Level Failed");
        }

        public override void Attack()
        {
            nextFireTime = Time.time + (1f / gunData.FireRate);
            UpdateUI();
            Debug.Log($"Next Fire Time: {nextFireTime} (Current Time: {Time.time})");

            currentAmmo--;

            GameObject bullet = ObjectPool.ObjectPool.Instance.GetObject(PoolType.Bullet, firePoint.position, firePoint.rotation);
            Bullet bulletScript = bullet.GetComponent<Bullet>();
            bulletScript.Initialize(gunData.BulletSpeed, gunData.Damage);

            Debug.Log($"{gunData.GunName} fired! Ammo Left: {currentAmmo}");

            if (currentAmmo <= 0)
            {
                StartCoroutine(Reload());
            }
        }

        private IEnumerator Reload()
        {
            isReloading = true;
            GameEvents.TriggerReload(true);
            Debug.Log($"Reloading {gunData.GunName}...");
            yield return new WaitForSeconds(gunData.ReloadTime);

            currentAmmo = gunData.AmmoCapacity;
            isReloading = false;
            Debug.Log($"{gunData.GunName} Reloaded!");
            GameEvents.TriggerReload(false);
            UpdateUI();
        }

        private void UpdateUI()
        {
            GameEvents.UpdateAmmo(currentAmmo, gunData.AmmoCapacity);
        }
    }
}

