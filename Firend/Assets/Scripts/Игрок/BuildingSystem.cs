using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSystem : MonoBehaviour
{
    public List<GameObject> buildings;
    private GameObject buidingToPlace;
    public GameObject cursor;
    public PlayerHealth plHealth;
    public bool canBuild = false;

    void Update()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        cursor.transform.position = mousePosition;

        if (Input.GetMouseButtonDown(0) && buidingToPlace != null)
        {
            Instantiate(buidingToPlace, cursor.transform.position, Quaternion.identity);
            buidingToPlace = null;
            cursor.gameObject.SetActive(false);
            Cursor.visible = true;
            plHealth.TakeDamage(1);
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ConstructingBuilding(buildings[0]);
        }
    }

    public void ConstructingBuilding(GameObject building)
    {
        cursor.gameObject.SetActive(true);
        cursor.GetComponent<SpriteRenderer>().sprite = building.GetComponent<SpriteRenderer>().sprite;
        buidingToPlace = building;
        Cursor.visible = false;
    }
}
