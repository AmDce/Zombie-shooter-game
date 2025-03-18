using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using ZombieShooter.MenuEvents;

namespace ZombieShooter.MenuControll
{

    public class MenuController : MonoBehaviour
    {
        [SerializeField] GameObject _shopPanel;

        [SerializeField] Button _pruchaseBtn;
        [SerializeField] Button _playBtn;

        private void Start()
        {
            _playBtn.onClick.AddListener(OnClickGamePlay);

            _pruchaseBtn.onClick.AddListener(() =>
            {
                MenuEventController.RaisePurchasePanel(true);
            });
        }

        private void OnEnable()
        {
            MenuEventController.TogglePurchasePanel += OnClickPurchase;
        }

        private void OnDisable()
        {
            MenuEventController.TogglePurchasePanel -= OnClickPurchase;
        }

        private void OnClickGamePlay()
        {
            SceneManager.LoadScene(1);
        }

        private void OnClickPurchase(bool active)
        {
            _shopPanel.SetActive(active);
        }
    }

}

