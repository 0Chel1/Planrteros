using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Sentry : Sounds
{
    public Transform firePoint;
    public GameObject thisObj;
    public GameObject bullet;
    public int ammoAmount;
    public int maxAmmoAmount = 10;
    public int WasteAmmoPerShot;
    public bool enemyInZone = false;
    public GameObject target;
    public float offset;
    private bool started = false;
    public ResourcesType resource;
    public bool HasAmmo = false;
    public FriendlyOrNot currState = FriendlyOrNot.Friendly;
    public enum FriendlyOrNot
    {
        Enemy,
        Friendly
    }

    public void Start()
    {
        StartCoroutine(LoadAmmo());
        resource = thisObj.GetComponent<ResourcesType>();
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
                if (HasAmmo)
                {
                    if(ammoAmount > 0)
                    {
                        Instantiate(bullet, firePoint.position, firePoint.rotation);
                        ammoAmount -= WasteAmmoPerShot;
                        PlaySound(0, 0.5f);
                    }
                }
                else
                {
                    Instantiate(bullet, firePoint.position, firePoint.rotation);
                    PlaySound(0, 0.5f);
                }
                yield return new WaitForSeconds(2);
            }
            else
            {
                yield return null;
            }
        }
    }

    IEnumerator LoadAmmo()
    {
        while (true)
        {
            int resourceIndex = ConvertPowerOfTwoToSequenceNumber((int)resource.currRes) - 1;
            if (HasAmmo && resource.resAmmount[resourceIndex] > 0 && ammoAmount < maxAmmoAmount)
            {
                resource.resAmmount[resourceIndex]--;
                ammoAmount++;
            }
            yield return new WaitForSeconds(1);
        }
    }

    private int ConvertPowerOfTwoToSequenceNumber(int x)
    {
        return x > 0 ? (int)(Math.Log(x, 2) + 1) : 0; // Добавляем проверку на 0
    }
}
