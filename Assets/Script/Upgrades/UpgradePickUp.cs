using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradePickUp : MonoBehaviour
{
    [SerializeField] private string _upgradeName;
    [SerializeField] private SpriteRenderer _upgradeSprite;
    private void Awake()
    {
        _upgradeSprite.sprite = ReferenceLibrary.Instance.GetUpgrade(_upgradeName).GetSprite();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameSession.Instance.sessionData.AddUpgrade(ReferenceLibrary.Instance.GetUpgrade(_upgradeName));
            Destroy(gameObject);
        }
    }
}
