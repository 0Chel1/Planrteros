using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class Bite : MonoBehaviour
{
    public List<GameObject> EnemysToBite;
    public List<EnemyHealth> EnemyHealths;
    public PlayerHealth playerHealth;
    public float damage;
    public float TimeToBite = 3;
    public float TimeTilNextBite;
    public Image HungerBar;
    public Animator animator;

    void Start()
    {
        TimeTilNextBite = 0;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision != null)
        {
            if (collision.transform.tag == "Enemy")
            {
                for (int i = 0; i < EnemysToBite.Count; ++i)
                {
                    if (EnemysToBite[i] == null)
                    {
                        EnemysToBite[i] = collision.gameObject;
                        EnemyHealths[i] = EnemysToBite[i].GetComponent<EnemyHealth>();
                        break;
                    }
                }
            }
        }
    }

    /*public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (collision.transform.tag == "Enemy")
            {
                for (int i = 0; i < EnemysToBite.Count; ++i)
                {
                    if (EnemysToBite[i] == null)
                    {
                        EnemysToBite[i] = collision.gameObject;
                        EnemyHealths[i] = EnemysToBite[i].GetComponent<EnemyHealth>();
                        break;
                    }
                }
            }
        }
    }*/

    public void OnTriggerExit2D(Collider2D collision)
    {
        if(collision != null)
        {
            if (collision.transform.tag == "Enemy")
            {
                for (int i = 0; i < EnemysToBite.Count; ++i)
                {
                    if (EnemysToBite[i] != null)
                    {
                        EnemysToBite[i] = null;
                        EnemyHealths[i] = null;
                        break;
                    }
                }
            }
        }
    }

    public void Update()
    {
        if(TimeTilNextBite > 0)
        {
            TimeTilNextBite -= Time.deltaTime;
        }
        else if(TimeTilNextBite <= 0)
        {
            animator.SetBool("Chewing", false);
            TimeTilNextBite = 0;
        }

        HungerBar.fillAmount = TimeTilNextBite / TimeToBite;

        if (Input.GetMouseButtonDown(0) && TimeTilNextBite <= 0)
        {
            StartCoroutine(Chewing());
        }
    }

    IEnumerator Chewing()
    {
        int nonZeroCount = 0;
        TimeTilNextBite = TimeToBite;
        if (EnemysToBite.Any(item => item != null))
        {
            animator.SetBool("Chewing", true);
            for (int i = 0; i < EnemyHealths.Count; ++i)
            {
                if (EnemysToBite.All(item => item == null))
                {
                    break;
                }

                foreach(GameObject value in EnemysToBite)
                {
                    if (value != null)
                    {
                        nonZeroCount++;
                    }
                }
                EnemyHealths[i].TakeDamage(damage);
                playerHealth.Heal(nonZeroCount);
                nonZeroCount = 0;
            }
            yield return new WaitForSeconds(3);
            animator.SetBool("Chewing", false);
        }
    }
}
