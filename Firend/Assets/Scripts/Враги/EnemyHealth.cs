using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float health;
    public float maxHealth;
    public GameObject Collection;
    public Image HPBar;
    public GameObject BossHealth;
    public ShowHP curShow = ShowHP.No;
    public enum ShowHP
    {
        Yes,
        No
    }
    public void TakeDamage(float damage)
    {
        health -= damage;
        if(health <= 0)
        {
            if (curShow == ShowHP.Yes)
            {
                HPBar.fillAmount = 0;
                Destroy(BossHealth);
            }
            Destroy(Collection);
        }
    }

    void Update()
    {
        BossHealth = GameObject.FindGameObjectWithTag("BossHPObject");
        var hpObj = GameObject.FindGameObjectWithTag("BossHP");
        if (curShow == ShowHP.Yes)
        {
            HPBar = hpObj.GetComponent<Image>();
            HPBar.fillAmount = health / maxHealth;
        }
    }
}
