using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIViewer : MonoBehaviour
{
    [SerializeField] Image _healthBar;
    [SerializeField] Image _armorBar;
    [SerializeField] Text _healthText;
    [SerializeField] GameObject _deathOverlay;
    [SerializeField] GameObject _winOverlay;
    [SerializeField] Transform UpgradeHolder;
    [SerializeField] GameObject UpgradeViewerPrefab;


    private void Awake()
    {
        FindObjectOfType<PlayerController>().SetUi(this);
    }
    private void OnEnable()
    {
        UpdateHealthBar();
        UpdateUpgrades();
        GameSession.Instance.OnStatsChange += UpdateHealthBar;
        GameSession.Instance.OnStatsChange += UpdateUpgrades;

    }
    private void OnDisable()
    {
        GameSession.Instance.OnStatsChange -= UpdateHealthBar;
        GameSession.Instance.OnStatsChange -= UpdateUpgrades;
    }

    public void EnableDeathOverlay() =>_deathOverlay.SetActive(true);
    public void EnableWinOverlay() => _winOverlay.SetActive(true);

    public void UpdateUpgrades()
    {
        for (int i = 0; i < UpgradeHolder.childCount; i++)
        {
            Destroy(UpgradeHolder.GetChild(i).gameObject);
        }
        for (int i = 0; i < GameSession.Instance.sessionData.GetUpgrades().Count; i++)
        {
            Sprite upgradeSprite = ReferenceLibrary.Instance.GetUpgrade(GameSession.Instance.sessionData.GetUpgrades()[i]).GetSprite();
            Instantiate(UpgradeViewerPrefab, UpgradeHolder).GetComponent<Image>().sprite = upgradeSprite;
        }
    }
    public void UpdateHealthBar()
    {
        _healthBar.fillAmount = (float)GameSession.Instance.sessionData.VesselHP / (float)GameSession.Instance.sessionData.MaxVesselHP;
        _healthText.text = $"{GameSession.Instance.sessionData.VesselHP} / {GameSession.Instance.sessionData.MaxVesselHP}";
    }
    public void SwitchToMainMenu() => SceneManager.LoadScene("MissionSelect", LoadSceneMode.Single);
}
