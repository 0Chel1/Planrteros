using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Sentry : Sounds
{
    public Transform firePoint;
    public GameObject bullet;
    public int ammoAmount;
    public int WasteAmmoPerShoot;
    public bool enemyInZone = false;
    public GameObject target;
    public float offset;
    private bool started = false;
    private ResourcesType resource;
    public FriendlyOrNot currState = FriendlyOrNot.Friendly;
    public enum FriendlyOrNot
    {
        Enemy,
        Friendly
    }

    private void Start()
    {
        resource = GetComponent<ResourcesType>();
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
            if(target != null && ammoAmount > 0)
            {
                Instantiate(bullet, firePoint.position, firePoint.rotation);
                ammoAmount -= 1;
                PlaySound(0, 0.2f);
                yield return new WaitForSeconds(2);
            }
            else
            {
                yield return null;
            }
        }
    }
}
