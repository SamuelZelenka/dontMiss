using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static  class Upgrade
{
    public static UpgradePickUp GetRandomUpgradePrefab() => ReferenceLibrary.Instance.upgradePrefabs[Random.Range(0, ReferenceLibrary.Instance.upgradePrefabs.Count)];
}
