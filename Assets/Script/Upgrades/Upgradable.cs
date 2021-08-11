using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Upgradable
{
    public enum UpgradeTier { Common, Uncommon, Rare, Epic, Legendary }
    public abstract Sprite GetSprite();
    public abstract string GetDescription();
    public abstract void ApplyUpgrade(SessionDataContainer sessionData);
    public abstract void RevokeUpgrade(SessionDataContainer sessionData);

}