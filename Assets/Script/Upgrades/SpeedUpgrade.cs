using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUpgrade : Upgradable
{
    public float amount = 0.2f;
    private Sprite _sprite;
    public SpeedUpgrade(Sprite sprite)
    {
        _sprite = sprite;
    }
    public override Sprite GetSprite() => _sprite;
    public override string GetDescription() => $"Increase Speed by {amount}";

    public override void ApplyUpgrade(SessionDataContainer sessionData)
    {
        sessionData.MovementSpeed += amount;
    }
    public override void RevokeUpgrade(SessionDataContainer sessionData)
    {
        sessionData.MovementSpeed -= amount;
    }
}
