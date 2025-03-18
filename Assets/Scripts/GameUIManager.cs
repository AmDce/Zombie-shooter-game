using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameUIManager : MonoBehaviour
{
    public static GameUIManager Instance;

    [Header("UI Elements")]
    [SerializeField] TextMeshProUGUI ammoText;
    [SerializeField] TextMeshProUGUI reloadText;
    [SerializeField] TextMeshProUGUI gameDescText;

    [SerializeField]
    GameObject _gameOverPanel;

    [SerializeField]
    Button _exitBtn;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        _exitBtn.onClick.AddListener(() =>
        {
            LoadScene(0);
        });
    }

    private void OnEnable()
    {
        GameEvents.OnAmmoUpdate += UpdateAmmo;
        GameEvents.OnReload += ShowReloading;
        GameEvents.OnGameOver += UpdateGameOverPanel;
    }

    private void OnDisable()
    {
        GameEvents.OnAmmoUpdate -= UpdateAmmo;
        GameEvents.OnReload -= ShowReloading;
        GameEvents.OnGameOver -= UpdateGameOverPanel;
    }

    public void UpdateAmmo(int currentAmmo, int maxAmmo)
    {
        ammoText.text = $"Ammo: {currentAmmo} / {maxAmmo}";
    }

    public void ShowReloading(bool isReloading)
    {
        reloadText.gameObject.SetActive(isReloading);
    }

    public void UpdateGameOverPanel(bool active, string text)
    {
        _gameOverPanel.SetActive(active);
        gameDescText.text = text;
    }

    private void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }
}
