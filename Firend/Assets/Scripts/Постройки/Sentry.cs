using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Sentry : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bullet;
    public bool enemyInZone = false;
    public GameObject target;
    public float offset;
    private bool started = false;
    public FriendlyOrNot currState = FriendlyOrNot.Friendly;
    public enum FriendlyOrNot
    {
        Enemy,
        Friendly
    }

    void Update()
    {
        if(target != null)
        {
            Vector2 diff = target.transform.position - transform.position;
            float rotateZ = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rotateZ + offset);
        }

        if(target == null)
        {
            enemyInZone = false;
        }
        else if(target != null && started == false)
        {
            StopCoroutine(Shoot());
            StartCoroutine(Shoot());
            started = true;
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(currState == FriendlyOrNot.Friendly)
        {
            if (collision.transform.tag == "Enemy" && target == null)
            {
                target = collision.gameObject;
                enemyInZone = true;
            }
        }
        else if(currState == FriendlyOrNot.Enemy)
        {
            if (collision.transform.tag == "Player" && target == null)
            {
                target = collision.gameObject;
                enemyInZone = true;
            }
        }
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (currState == FriendlyOrNot.Friendly)
        {
            if (collision.transform.tag == "Enemy" && target == null)
            {
                target = collision.gameObject;
                enemyInZone = true;
            }
        }
        else if (currState == FriendlyOrNot.Enemy)
        {
            if (collision.transform.tag == "Player" && target == null)
            {
                target = collision.gameObject;
                enemyInZone = true;
            }
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if(currState == FriendlyOrNot.Friendly)
        {
            if (collision.transform.tag == "Enemy" && target != null)
            {
                enemyInZone = false;
                target = null;
            }
        }
        else if(currState == FriendlyOrNot.Enemy)
        {
            if (collision.transform.tag == "Player" && target != null)
            {
                enemyInZone = false;
                target = null;
            }
        }
    }

    IEnumerator Shoot()
    {
        while(true)
        {
            if(target != null)
            {
                Instantiate(bullet, firePoint.position, firePoint.rotation);
                yield return new WaitForSeconds(2);
            }
            else
            {
                yield return null;
            }
        }
    }
}
