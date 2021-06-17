using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZUtility.Unity;
[RequireComponent(typeof(Animator))]
public class MissionSelectionCell : MonoBehaviour
{
    MissionData _data;
    Animator animator;
    public void Init(MissionData data, ref ZGrid grid)
    {
        _data = data;
        transform.position = grid.GetWorldPosition(_data.position.x, _data.position.y);
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void OnMouseEnter()
    {
        animator.SetBool("OnHover", true);
    }
    private void OnMouseDown()
    {
        GameSession.Instance.sessionData.MissionProgression.SetPlayerPos(_data.position);
        GameSession.Instance.currentMission = "";
        MissionSelectionManager.Instance.SelectMission(_data);
    }
    private void OnMouseExit()
    {
        animator.SetBool("OnHover", false);
    }
}
