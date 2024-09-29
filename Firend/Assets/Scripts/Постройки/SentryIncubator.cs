using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SentryIncubator : Sounds
{
    public GameObject sentry;
    public float IncubationTime;
    public bool playSound = true;
    void Start()
    {
        if (playSound)
        {
            PlaySound(0, 2, destroyed: true, p1: 1, p2: 1);
        }
        StartCoroutine(Incubation());
    }

    IEnumerator Incubation()
    {
        yield return new WaitForSeconds(IncubationTime);
        Instantiate(sentry, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
