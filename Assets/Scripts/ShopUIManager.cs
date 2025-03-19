using System.Collections.Generic;
using UnityEngine;
using ZombieShooter.Guns;
using ZombieShooter.ShopView;
using TMPro;
using System;

namespace ZombieShooter.ShopUIManager
{
    public class ShopUIManager : MonoBehaviour
    {
        public static Action<bool, GunData> OnEnableStats;

        [SerializeField] GameObject _shopItemPrefab;
        [SerializeField] Transform _shopPanelHolder;

        public GameObject _shopStatsPanel;

        private Dictionary<string, GunData> _guns;

        void Start()
        {
            PopulateShopItem();
        }

        private void OnEnable()
        {
            OnEnableStats += EnableStatsPanel;
        }

        private void OnDisable()
        {
            OnEnableStats -= EnableStatsPanel;
        }

        private void PopulateShopItem()
        {
            _guns = GunManager.Instance.GetAllGuns();
            foreach (var gun in _guns.Values)
            {
                GameObject gunItem = Instantiate(_shopItemPrefab, _shopPanelHolder);
                ShopViewItem shopViewItem = gunItem.GetComponent<ShopViewItem>();
                shopViewItem.OnSetGunData(gun);
            }
        }

        private void EnableStatsPanel(bool active, GunData gunData)
        {
            if (gunData != null)
            {
                _shopStatsPanel.GetComponent<WeaponStatsVIew>().GunDatas = gunData;
            }
            _shopStatsPanel.SetActive(active);
        }
    }
}
