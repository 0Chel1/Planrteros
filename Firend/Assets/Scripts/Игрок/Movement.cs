using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Movement : MonoBehaviour
{
    public Rigidbody2D rb;
    public List<LineRenderer> lines;
    public List<GameObject> LinesEnds;
    public Transform firePoint;
    public List<Vector2> hitPoints;
    public LayerMask whatIsWall;

    public float pullSpeed = 5f;

    public int currPointUsed = 0;

    private Vector2 targetPosition;
    private bool isPulling = false;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                TrailCloser();
            }
            else
            {
                Trail();
            }
        }
        

        if (isPulling)
        {
            rb.MovePosition(Vector2.Lerp(rb.position, targetPosition, pullSpeed * Time.deltaTime));
            if (Vector2.Distance(rb.position, targetPosition) < 0.01f)
            {
                isPulling = false;
            }
        }

        for(int i = 0; i < lines.Count; i++)
        {
            lines[i].SetPosition(0, transform.position);
            lines[i].SetPosition(1, hitPoints[i]);
            LinesEnds[i].transform.position = hitPoints[i];
            Vector2 curPos = transform.position;
            Vector2 diff = hitPoints[i] - curPos;
            float rotateZ = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            LinesEnds[i].transform.rotation = Quaternion.Euler(0f, 0f, rotateZ);
        }

    }

    void Trail()
    {
        Vector2 currPos = transform.position;
        RaycastHit2D hit = Physics2D.Raycast(currPos, firePoint.right, 200f, whatIsWall);

        if (hit.collider != null)
        {
            Vector2 hitPoint = hit.point;
            hitPoints[currPointUsed] = hitPoint;
            float distance = Vector2.Distance(currPos, hitPoint);
            Vector2 direction = (hitPoint - currPos).normalized;
            targetPosition = currPos + direction * (distance * 0.5f);
            isPulling = true;
            currPointUsed += 1;
            if(currPointUsed >= hitPoints.Count)
            {
                currPointUsed = 0;
            }
        }
    }

    void TrailCloser()
    {
        Vector2 currPos = transform.position;
        RaycastHit2D hit = Physics2D.Raycast(currPos, firePoint.right, 200f, whatIsWall);

        if (hit.collider != null)
        {
            Vector2 hitPoint = hit.point;
            hitPoints[currPointUsed] = hitPoint;
            float distance = Vector2.Distance(currPos, hitPoint);
            Vector2 direction = (hitPoint - currPos).normalized;
            targetPosition = currPos + direction * (distance * 0.9f);
            isPulling = true;
            currPointUsed += 1;
            if (currPointUsed >= hitPoints.Count)
            {
                currPointUsed = 0;
            }
        }
    }
}
