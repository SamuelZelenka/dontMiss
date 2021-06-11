using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIViewer : MonoBehaviour
{
    [SerializeField] Image _healthBar;
    [SerializeField] Image _armorBar;
    [SerializeField] GameObject _deathOverlay;
    [SerializeField] GameObject _winOverlay;


    private static UIViewer _instance;

    public static UIViewer Instance => _instance;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    private void OnEnable()
    {
        UpdateHealthBar();
        GameSession.Instance.OnStatsChange += UpdateHealthBar;
    }
    private void OnDisable()
    {
        GameSession.Instance.OnStatsChange -= UpdateHealthBar;
    }

    public static void EnableDeathOverlay() => _instance._deathOverlay.SetActive(true);
    public static void EnableWinOverlay() => _instance._winOverlay.SetActive(true);


    public void UpdateHealthBar()
    {
        _healthBar.fillAmount = (float)GameSession.Instance.sessionData.VesselHP / (float)GameSession.Instance.sessionData.MaxVesselHP;
    }
    public void SwitchToMainMenu() => SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
}
