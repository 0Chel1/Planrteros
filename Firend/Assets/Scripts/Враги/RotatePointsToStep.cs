using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePointsToStep : MonoBehaviour
{
    public Vector2 lastPos;
    public float offset;
    void Start()
    {
        lastPos = transform.position;
        StartCoroutine(UpdatePointsRot());
    }

    IEnumerator UpdatePointsRot()
    {
        while (true)
        {
            
            Vector2 currentPos = transform.position;
            Vector2 diff = lastPos - currentPos;
            if(diff != new Vector2(0, 0))
            {
                float rotateZ = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0f, 0f, rotateZ + offset);
                lastPos = transform.position;
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
}
