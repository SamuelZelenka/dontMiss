using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradePickUp : MonoBehaviour
{
    public string upgradeName;
    [SerializeField] private SpriteRenderer _upgradeSprite;
    private void Awake()
    {
        _upgradeSprite.sprite = ReferenceLibrary.Instance.GetUpgrade(upgradeName).GetSprite();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameSession.Instance.sessionData.AddUpgrade(ReferenceLibrary.Instance.GetUpgrade(upgradeName));
            Destroy(gameObject);
        }
    }
}
