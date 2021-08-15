using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ProjectileLookup
{
    private static Dictionary<string, Projectile> _lookupTable = new Dictionary<string, Projectile>();
    public static Projectile Get(string attachment)
    {
        if (_lookupTable.Count == 0)
        {
            InitTable();
        }
        return _lookupTable[attachment];
    }
    private static void InitTable()
    {
        foreach (Projectile projectile in ReferenceLibrary.Instance.projectilePrefabs)
        {
            _lookupTable.Add(projectile.ToString(), projectile);
        }
    }
}