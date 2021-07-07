using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZUtility.Unity;
[RequireComponent(typeof(Animator))]
public class MissionSelectionCell : MonoBehaviour
{
    MissionData _data;
    Animator _animator;
    public void Init(MissionData data, ref ZGrid grid)
    {
        _data = data;
        transform.position = grid.GetWorldPosition(_data.position.x, _data.position.y);
    }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    private void OnMouseEnter()
    {
        _animator.SetBool("OnHover", true);
    }
    private void OnMouseDown()
    {
        if (MissionSelectionManager.Instance.player.moveState.Equals(typeof(OnLocationState)))
        {
            GameSession.Instance.currentMission = _data;
            MissionSelectionManager.Instance.SelectMission(_data);
            if (MissionSelectionManager.Instance.player.PlayerPos != _data.position)
            {
                MissionSelectionManager.Instance.player.SetPathTo(_data.position);
            }
        }
    }
    private void OnMouseExit()
    {
        _animator.SetBool("OnHover", false);
    }
}
