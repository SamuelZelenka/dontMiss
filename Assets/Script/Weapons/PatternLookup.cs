using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PatternLookup
{
    private static Dictionary<string, Pattern> _lookupTable = new Dictionary<string, Pattern>();
    public static Pattern Get(string attachment)
    {
        if (_lookupTable.Count == 0)
        {
            InitTable();
        }
        return _lookupTable[attachment];
    }
    //Include all patterns in game
    private static void InitTable()
    {
            _lookupTable.Add("AlternateMuzzle", new AlternateMuzzle());
    }
}
