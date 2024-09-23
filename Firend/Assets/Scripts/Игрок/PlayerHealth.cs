using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public float health;
    public float maxHealth;
    public TextMeshProUGUI healthText;
    public Image HPBar;

    public void Start()
    {

    }

    public void Update()
    {
        healthText.text = health.ToString();
        HPBar.fillAmount = health / maxHealth;
        health = Mathf.Floor(health * 10) / 10;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            SceneManager.LoadScene(1);
        }
    }

    public void Heal(float heal)
    {
        health += heal;
        if(health >= maxHealth)
        {
            health = maxHealth;
        }
    }

    public void StartLoseHealth()
    {
        StartCoroutine(HealthDecrease());
    }

    IEnumerator HealthDecrease()
    {
        while (true)
        {
            health -= 0.5f;
            yield return new WaitForSeconds(5);
        }
    }
}
