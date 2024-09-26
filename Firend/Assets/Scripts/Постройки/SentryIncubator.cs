using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SentryIncubator : Sounds
{
    public GameObject sentry;
    public float IncubationTime;
    void Start()
    {
        PlaySound(0, 2, destroyed: true, p1: 1, p2: 1);
        StartCoroutine(Incubation());
    }

    IEnumerator Incubation()
    {
        yield return new WaitForSeconds(IncubationTime);
        Instantiate(sentry, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
