using System.Collections.Generic;
using UnityEngine;
using ZombieShooter.Guns;
using ZombieShooter.ShopView;
using TMPro;

namespace ZombieShooter.ShopUIManager
{
    public class ShopUIManager : MonoBehaviour
    {
        [SerializeField] GameObject _shopItemPrefab;
        [SerializeField] Transform _shopPanelHolder;

        [SerializeField] GameObject _stateItemPrefab;
        [SerializeField] Transform _statePanelHolder;

        private Dictionary<string, GunData> _guns;

        void Start()
        {
            PopulateShopItem();
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
        
    }
}
