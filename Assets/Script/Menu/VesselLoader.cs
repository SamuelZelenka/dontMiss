using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class VesselLoader : MonoBehaviour
{
    public Button vesselButton;
    public Text vesselName;
    public void LoadVessel()
    {
        GameSession.Instance.LoadData(vesselName.text);
    }
}
