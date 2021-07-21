using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUpgradable
{
    public Sprite GetSprite();
    public string GetDescription();
    public void ApplyUpgrade(SessionDataContainer sessionData);
    public void RevokeUpgrade(SessionDataContainer sessionData);

}