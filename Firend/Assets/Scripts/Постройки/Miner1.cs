using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using static ResourcesType;

public class Miner1 : MonoBehaviour
{
    public ResourcesType resource;
    public int resourceAmmount;
    public int maxResourceAmmount;
    public Tilemap tilemap;
    public Vector3Int gridPosition;
    void Start()
    {
        var tolemap = GameObject.Find("Ores");
        tilemap = tolemap.GetComponent<Tilemap>();
        Vector3Int tilePos = tilemap.WorldToCell(transform.position);
        TileBase tile = tilemap.GetTile(tilePos);
        if (tile != null)
        {
            if(tile.name == "Copper")
            {
                resource.currRes = ResourceType1.Copper;
            }
            else if(tile.name == "Sand")
            {
                resource.currRes = ResourceType1.Sand;
            }
            else if(tile.name == "Kvartz")
            {
                resource.currRes = ResourceType1.Kwartz;
            }
            StartCoroutine(Mining());
            StartCoroutine(resource.UnloadInNode());
        }
        else
        {
            resource.currRes = ResourceType1.None;
        }
    }


    IEnumerator Mining()
    {
        while(true)
        {
            if(resourceAmmount < maxResourceAmmount)
            {
                resourceAmmount++;
            }

            if(resource.resAmmount[0] < resource.maxResAmmount)
            {
                resourceAmmount--;
                resource.resAmmount[0]++;
            }
            yield return new WaitForSeconds(1);
        }
    }
}
