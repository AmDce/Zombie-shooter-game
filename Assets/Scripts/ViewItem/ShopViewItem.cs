using UnityEngine;
using UnityEngine.UI;
using TMPro;
using ZombieShooter.MenuEvents;
using ZombieShooter.Player;

namespace ZombieShooter.ShopView
{
    public class ShopViewItem : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI _name;

        [SerializeField] Image _logo;

        [SerializeField] Button _applyBtn;

        private string _gunName;

        private GunData _gunData;

        private void Start()
        {
            _applyBtn?.onClick.AddListener(OnClickPurchase);
        }

        public void OnSetGunData(GunData gunData)
        {
            _gunData = gunData;
            _gunName = gunData.GunName;
            _name.text = gunData.GunName;
            _logo.sprite = gunData.GunSprite;
        }

        private void OnClickPurchase()
        {
            ShopUIManager.ShopUIManager.OnEnableStats(true, _gunData);
        }
    }
}

