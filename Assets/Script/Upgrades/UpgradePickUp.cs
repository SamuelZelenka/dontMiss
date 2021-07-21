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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            GameSession.Instance.sessionData.AddUpgrade(ReferenceLibrary.Instance.GetUpgrade(_upgradeName));
            Destroy(gameObject);
        }
    }
}
