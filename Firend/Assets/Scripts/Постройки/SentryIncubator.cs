using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using static ResourcesType;

public class SentryIncubator : Sounds
{
    public GameObject sentry;
    public float IncubationTime;
    private float time;
    public Image slider;
    public Tilemap tilemap;
    public Vector3Int gridPosition;
    public bool playSound = true;
    void Start()
    {
        var tolemap = GameObject.Find("TilemapWalls");
        tilemap = tolemap.GetComponent<Tilemap>();
        Vector3Int tilePos = tilemap.WorldToCell(transform.position);
        TileBase tile = tilemap.GetTile(tilePos);
        if (tile != null)
        {
            for(int i = 0; i < 15; i++)
            {
                if (tile.name == "Walls_" + i)
                {
                    Destroy(gameObject);
                }
            }
        }

        time = IncubationTime;
        if (playSound)
        {
            PlaySound(0, 2, destroyed: true, p1: 1, p2: 1);
        }
        StartCoroutine(Incubation());
    }

    private void Update()
    {
        time -= Time.deltaTime;
        slider.fillAmount = time / IncubationTime;
    }

    IEnumerator Incubation()
    {
        yield return new WaitForSeconds(IncubationTime);
        GameObject building = Instantiate(sentry, transform.position, Quaternion.identity);
        if (building.transform.childCount > 0)
        {
            foreach (Transform child in building.transform)
            {
                child.gameObject.layer = 9;
            }
            building.layer = 8;
        }
        else
        {
            building.layer = 9;
        }
        Destroy(gameObject);
    }
}
