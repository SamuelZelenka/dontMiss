using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUpgrade : IUpgradable
{
    public float amount = 0.2f;
    private Sprite _sprite;
    public SpeedUpgrade(Sprite sprite)
    {
        _sprite = sprite;
    }
    public Sprite GetSprite() => _sprite;
    public string GetDescription() => $"Increase Speed by {amount}";

    public void ApplyUpgrade(SessionDataContainer sessionData)
    {
        sessionData.MovementSpeed += amount;
    }
    public void RevokeUpgrade(SessionDataContainer sessionData)
    {
        sessionData.MovementSpeed -= amount;
    }
}
