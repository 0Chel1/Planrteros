using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTarget : MonoBehaviour
{
     public Camera cam;
     public Transform player;
     public float threshHold;

    void Update()
    {
        Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 targetPos = (player.position + mousePos) / 2f;

        targetPos.x = Mathf.Clamp(targetPos.x, -threshHold + player.position.x, threshHold + player.position.x);
        targetPos.y = Mathf.Clamp(targetPos.y, -threshHold + player.position.y, threshHold + player.position.y);
        this.transform.position = targetPos;
    }
}
