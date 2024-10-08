using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using static ResourcesType;
using DG.Tweening;
using Unity.VisualScripting;

public class transportnode : MonoBehaviour
{
    public int inputs;
    public int output;
    public int maxOutputs = 1;
    public int CurResHold;
    public int resCapacity = 5;
    public ResourcesType resource;
    public LineRenderer line;
    public float searchRadius = 5f;
    public List<transportnode> nearbyNodesScripts = new List<transportnode>();
    public List<GameObject> resourcesSprites;
    public List<GameObject> resourcesSpritesExamples;
    public transportnode connectedWith;
    public ResourcesType buildingResNeed;
    public GameObject ConnectZone;
    public int counter = 0;
    public bool NodeConstruction = false;

    void Start()
    {
        StartCoroutine(Unload());
        line.positionCount = 2;
        line.SetPosition(0, transform.position);
        line.SetPosition(1, transform.position);
        ConnectZone.SetActive(false);
        ConnectZone.transform.localScale = new Vector2(searchRadius, searchRadius);
    }

    private bool AllScriptsInactive()
    {
        return nearbyNodesScripts.All(script => !script.NodeConstruction);
    }

    void Update()
    {
        if (NodeConstruction)
        {
            ConnectZone.SetActive(true);
        }
        else
        {
            ConnectZone.SetActive(false);
        }


        if(CurResHold <= 0)
        {
            CurResHold = 0;
        }
    }

    void OnMouseDown()
    {
        var curPos = transform.position;
        FindObjectsInRadius();

        if (!NodeConstruction && AllScriptsInactive())
        {
            NodeConstruction = true;
            line.SetPosition(0, curPos);
            line.SetPosition(1, curPos);
        }
        else
        {
            foreach (var nearbyNode in nearbyNodesScripts)
            {
                if (nearbyNode.NodeConstruction)
                {
                    nearbyNode.line.SetPosition(1, curPos);
                    nearbyNode.output++;
                    nearbyNode.connectedWith = this;
                    nearbyNode.NodeConstruction = false;
                    break;
                }
            }
            inputs++;
        }
    }

    void FindObjectsInRadius()
    {
        nearbyNodesScripts.Clear();
        var allNodes = FindObjectsOfType<GameObject>();

        foreach (var node in allNodes)
        {
            if (node == this)
                continue;

            if (Vector2.Distance(transform.position, node.transform.position) <= searchRadius && node.tag == "node")
            {
                nearbyNodesScripts.Add(node.GetComponent<transportnode>());
            }
        }
    }

    IEnumerator Unload()
    {
        while (true)
        {
            if (connectedWith != null)
            {
                if (connectedWith.CurResHold < connectedWith.resCapacity)
                {

                    StartCoroutine(AddRes());

                    if (connectedWith.resource.currRes == ResourceType1.None)
                    {
                        connectedWith.resource.currRes = resource.currRes;
                    }
                }
            }
            else if (buildingResNeed != null && HasCommonFlags(resource))
            {
                StartCoroutine(AddRes());
            }

            yield return new WaitForSeconds(1);
        }
    }

    private int ConvertPowerOfTwoToSequenceNumber(int x)
    {
        return x > 0 ? (int)(Math.Log(x, 2) + 1) : 0; // Добавляем проверку на 0
    }

    public bool HasCommonFlags(ResourcesType other)
    {
        return buildingResNeed.activeFlags.Intersect(other.activeFlags).Any();
    }

    IEnumerator AddRes()
    {
        while (true)
        {
            if(CurResHold > 0)
            {
                int resourceIndex = ConvertPowerOfTwoToSequenceNumber((int)resource.currRes) - 1;
                if (buildingResNeed != null)
                {
                    if (buildingResNeed.resAmmount[resourceIndex] < buildingResNeed.maxResAmmount)
                    {
                        CurResHold--;
                        if (counter >= resourcesSprites.Count)
                        {
                            counter = 0;
                        }

                        resourcesSprites[counter] = Instantiate(resourcesSpritesExamples[resourceIndex], transform.position, Quaternion.identity);
                        resourcesSprites[counter].transform.DOMove(buildingResNeed.transform.position, 1);
                        resourcesSprites[counter] = null;
                        buildingResNeed.resAmmount[resourceIndex]++;
                        counter++;
                    }
                }
                else if (connectedWith != null)
                {
                    if (connectedWith.CurResHold < resCapacity)
                    {
                        CurResHold--;
                        if (counter >= resourcesSprites.Count)
                        {
                            counter = 0;
                        }

                        resourcesSprites[counter] = Instantiate(resourcesSpritesExamples[resourceIndex], transform.position, Quaternion.identity);
                        resourcesSprites[counter].transform.DOMove(connectedWith.resource.transform.position, 1);
                        resourcesSprites[counter] = null;
                        connectedWith.CurResHold++;
                        counter++;
                    }
                }
            }
            yield return new WaitForSeconds(1);
        }
    }

    
}
