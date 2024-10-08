using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCursor : MonoBehaviour
{
    public bool CanBuild;
    public GameObject cursor;
    public Color ColorCantBuild;
    public Color ColorCanBuild;
    public SpriteRenderer Sprite;

    void Update()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = mousePosition;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        CanBuild = false;
        Sprite.color = ColorCantBuild;
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        CanBuild = false;
        Sprite.color = ColorCantBuild;
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        CanBuild = true;
        Sprite.color = ColorCanBuild;
    }
}
