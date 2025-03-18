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

        [SerializeField] Button _applyBtn;

        [SerializeField] TextMeshProUGUI _statsText;

        private string _gunName;

        private void Start()
        {
            _applyBtn?.onClick.AddListener(OnClickPurchase);
        }

        public void OnSetGunData(GunData gunData)
        {
            _gunName = gunData.GunName;
            _name.text = gunData.GunName;
            _statsText.text = $"Damage: {gunData.Damage}\nFire Rate: {gunData.FireRate}\nReload: {gunData.ReloadTime}\nAmmo: {gunData.AmmoCapacity}";

        }

        private void OnClickPurchase()
        {
            Debug.Log($"Purchase Completed");
            PlayerModular.Instance.UpdateCurrentGun(_gunName);
            MenuEventController.RaisePurchasePanel(false);
        }
    }
}

