using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUpgrade : Upgradable
{
    public int amount = 10;
    private Sprite _sprite;

    public HealthUpgrade(Sprite sprite)
    {
        _sprite = sprite;
    }
    public override Sprite GetSprite() => _sprite;
    public override string GetDescription() => $"Increase Max Health by {amount}";

    public override void ApplyUpgrade(SessionDataContainer sessionData)
    {
        sessionData.MaxAdditionalHP += amount;
        sessionData.VesselHP += amount;

    }
    public override void RevokeUpgrade(SessionDataContainer sessionData)
    {
        sessionData.MaxAdditionalHP -= amount;
    }
}