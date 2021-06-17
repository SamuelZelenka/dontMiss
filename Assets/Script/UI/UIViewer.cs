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

    private void Awake()
    {
        FindObjectOfType<PlayerController>().SetUi(this);
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

    public void EnableDeathOverlay() =>_deathOverlay.SetActive(true);
    public void EnableWinOverlay() => _winOverlay.SetActive(true);

    public void UpdateHealthBar()
    {
        _healthBar.fillAmount = (float)GameSession.Instance.sessionData.VesselHP / (float)GameSession.Instance.sessionData.MaxVesselHP;
    }
    public void SwitchToMainMenu() => SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
}
