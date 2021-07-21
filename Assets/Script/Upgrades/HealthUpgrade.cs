using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUpgrade : IUpgradable
{
    public int amount = 10;
    private Sprite _sprite;

    public HealthUpgrade(Sprite sprite)
    {
        _sprite = sprite;
    }
    public Sprite GetSprite() => _sprite;
    public string GetDescription() => $"Increase Max Health by {amount}";

    public void ApplyUpgrade(SessionDataContainer sessionData)
    {
        sessionData.MaxAdditionalHP += amount;
        sessionData.VesselHP += amount;

    }
    public void RevokeUpgrade(SessionDataContainer sessionData)
    {
        sessionData.MaxAdditionalHP -= amount;
    }
}