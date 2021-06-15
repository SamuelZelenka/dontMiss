using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZUtility.Unity;
[RequireComponent(typeof(Animator))]
public class MissionSelectionCell : MonoBehaviour
{
    Animator animator;
    public void Init(MissionData data, ref ZGrid grid)
    {
        transform.position = grid.GetWorldPosition(data.position.x, data.position.y);
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void OnMouseEnter()
    {
        animator.SetBool("OnHover", true);
    }
    private void OnMouseExit()
    {
        animator.SetBool("OnHover", false);
    }
}
