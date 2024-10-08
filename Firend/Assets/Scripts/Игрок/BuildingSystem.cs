using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BuildingSystem : MonoBehaviour
{
    public List<GameObject> buildings;
    private GameObject buidingToPlace;
    public GameObject cursor;
    public PlayerHealth plHealth;
    public Movement movement;
    public Bite bite;
    private Vector3 startPoint;
    private Vector3 endPoint;
    public GameObject selectionBox;
    public Camera mainCamera;
    public LayerMask enemyLayer;
    public List<GameObject> buildingsToDelete;
    public TextMeshProUGUI buildingText;
    public CustomCursor CUrsor;
    public bool canBuild = false;
    private bool BuildingModOn = false;

    void Start()
    {
        buildingText.text = "Строительство:Off";
    }

    void Update()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        cursor.transform.position = mousePosition;

        if(Input.GetKeyDown(KeyCode.F) && !BuildingModOn)
        {
            BuildingModOn = true;
            movement.enabled = false;
            bite.enabled = false;
            buildingText.text = "Строительство:On";
        }
        else if(Input.GetKeyDown(KeyCode.F) && BuildingModOn)
        {
            BuildingModOn = false;
            movement.enabled = true;
            bite.enabled = true;
            buildingText.text = "Строительство:Off";
        }

        if (Input.GetMouseButtonDown(0) && buidingToPlace != null && CUrsor.CanBuild)
        {
            GameObject building = Instantiate(buidingToPlace, cursor.transform.position, Quaternion.identity);
            /*if(building.transform.childCount > 0)
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
            }*/
            plHealth.TakeDamage(1);
        }
        else if (Input.GetMouseButtonDown(1) && BuildingModOn)
        {
            buidingToPlace = null;
            cursor.gameObject.SetActive(false);
            Cursor.visible = true;
        }

        if (Input.GetMouseButtonDown(1) && BuildingModOn)
        {
            startPoint = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            selectionBox.SetActive(true);
        }

        if (Input.GetMouseButton(1) && BuildingModOn)
        {
            endPoint = Input.mousePosition;
            UpdateSelectionBox();
        }

        if (Input.GetMouseButtonUp(1) && BuildingModOn)
        {
            endPoint = Input.mousePosition;
            ApplyDeliting();
            selectionBox.SetActive(false);
        }
    }

    public void ConstructingBuilding(GameObject building)
    {
        cursor.gameObject.SetActive(true);
        cursor.GetComponent<SpriteRenderer>().sprite = building.GetComponent<SpriteRenderer>().sprite;
        cursor.transform.localScale = building.transform.localScale;
        buidingToPlace = building;
        Cursor.visible = false;
    }

    public void ChooseBuilding(int i)
    {
        if (BuildingModOn)
        {
            ConstructingBuilding(buildings[i]);
        }
    }

    private void UpdateSelectionBox()
    {
        Vector3 worldEnd = mainCamera.ScreenToWorldPoint(new Vector3(endPoint.x, endPoint.y, mainCamera.nearClipPlane));

        Vector2 size = worldEnd - startPoint;

        selectionBox.transform.position = new Vector3(startPoint.x + size.x / 2, startPoint.y + size.y / 2, 0);
        selectionBox.transform.localScale = new Vector3(Mathf.Abs(size.x), Mathf.Abs(size.y), 1);
    }

    private void ApplyDeliting()
    {
        Vector3 worldEnd = mainCamera.ScreenToWorldPoint(new Vector3(endPoint.x, endPoint.y, mainCamera.nearClipPlane));

        Collider2D[] buildings = Physics2D.OverlapAreaAll(startPoint, worldEnd, enemyLayer);
        foreach (Collider2D building in buildings)
        {
            buildingsToDelete.Add(building.gameObject);
        }
        
        for(int i = 0; i < buildingsToDelete.Count; i++)
        {
            plHealth.Heal(0.5f);
            Destroy(buildingsToDelete[i]);
        }

        buildingsToDelete.Clear();
    }
}
