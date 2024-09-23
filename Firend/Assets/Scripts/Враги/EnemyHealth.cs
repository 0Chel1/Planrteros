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

    public void Start()
    {
        var hpObj = GameObject.FindGameObjectWithTag("BossHP");
        BossHealth = GameObject.FindGameObjectWithTag("BossHPObject");
        if(curShow == ShowHP.Yes)
            HPBar = hpObj.GetComponent<Image>();

    }
    public void TakeDamage(float damage)
    {
        health -= damage;
        if(health <= 0)
        {
            if (curShow == ShowHP.Yes)
                HPBar.fillAmount = 0;
            Destroy(BossHealth);
            Destroy(Collection);
        }
    }

    void Update()
    {
        if(curShow == ShowHP.Yes)
        {
            HPBar.fillAmount = health / maxHealth;
        }
    }
}
