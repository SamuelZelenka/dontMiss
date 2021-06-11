using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamageEffect : MonoBehaviour
{
    const float DAMAGE_EFFECT_RATE = 0.5f;
    public static IEnumerator DamageEffect(SpriteRenderer renderer)
    {
        while (renderer.color.r < 1 ||
            renderer.color.g < 1 ||
            renderer.color.b < 1)
        {
            renderer.color += new Color(DAMAGE_EFFECT_RATE, DAMAGE_EFFECT_RATE, DAMAGE_EFFECT_RATE) * Time.deltaTime;
            yield return null;
        }
    }
}
