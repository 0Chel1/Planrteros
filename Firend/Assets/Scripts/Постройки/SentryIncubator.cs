using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SentryIncubator : MonoBehaviour
{
    public GameObject sentry;
    public float IncubationTime;
    void Start()
    {
        StartCoroutine(Incubation());
    }

    IEnumerator Incubation()
    {
        yield return new WaitForSeconds(IncubationTime);
        Instantiate(sentry, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
