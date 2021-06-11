using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationEndTrigger : MonoBehaviour
{
    public delegate void AnimationEndHandler();
    public AnimationEndHandler OnAnimationEnd;
    [SerializeField] float delay = 0f;

    void Start()
    {
        StartCoroutine("AfterAnimationTrigger");
    }
    IEnumerator AfterAnimationTrigger()
    {
        yield return new WaitForSeconds(GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length + delay);
        OnAnimationEnd?.Invoke();
        StartCoroutine("AfterAnimationTrigger");
    }
}