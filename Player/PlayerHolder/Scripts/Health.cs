using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

public class Health : NetworkBehaviour
{
    public const int maxHealth = 2000;
    public int HP = maxHealth;
    public TextMeshPro text;

    private void Start()
    {
        text.text = HP.ToString();
    }
    public void damage (int dmg)
    {
        HP = HP - dmg;
        if (HP <= 0)
            Destroy(gameObject);
        text.text = HP.ToString();
    }
}
