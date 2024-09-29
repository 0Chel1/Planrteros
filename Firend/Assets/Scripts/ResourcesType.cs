using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesType : MonoBehaviour
{
    public ResourceType1 currRes = ResourceType1.None;
    public List<ResourceType1> activeFlags;
    public bool LoadIn = false;
    public bool isFactory = false;
    public List<int> resAmmount;
    public int maxResAmmount = 5;
    public List<transportnode> nearbyNodesScripts;
    public List<GameObject> nearNodesObj;
    public transportnode node;
    public GameObject nodeObj;
    public Factory factory;
    public float searchRadius = 1f;

    [Flags]
    public enum ResourceType1
    {
        Copper = 1 << 0, //0
        Sand = 1 << 1, //1
        Kwartz = 1 << 2, //2
        None = 4 << 3
    }

    void Start()
    {
        if (!LoadIn && transform.tag != "node" || LoadIn && isFactory)
        {
            StartCoroutine(UnloadInNode());
        }
        StartCoroutine(CheckFlags());
    }

    public void FindObjectsInRadius()
    {
        nearNodesObj.Clear();
        nearbyNodesScripts.Clear();
        var allNodes = FindObjectsOfType<GameObject>();

        foreach(var node in allNodes)
        {
            if (node == this)
                continue;

            if (Vector2.Distance(transform.position, node.transform.position) <= searchRadius && node.tag == "node")
            {
                nearNodesObj.Add(node);
                nearbyNodesScripts.Add(node.GetComponent<transportnode>());
            }
        }
    }

    public IEnumerator UnloadInNode()
    {
        while (true)
        {
            FindObjectsInRadius();
            if(nearbyNodesScripts.Count > 0 || nearNodesObj.Count > 0)
            {
                nodeObj = nearNodesObj[0];
                node = nearbyNodesScripts[0];

                if (node != null && nodeObj.tag == "node" && node.CurResHold < node.resCapacity)
                {
                    if (!isFactory)
                    {
                        resAmmount[0]--;
                        node.CurResHold++;
                        var nodeResourcesScript = nodeObj.GetComponent<ResourcesType>();
                        nodeResourcesScript.currRes = transform.GetComponent<ResourcesType>().currRes;
                    }
                    else if(isFactory && resAmmount[factory.resourceToAdd] > 0)
                    {
                        resAmmount[factory.resourceToAdd]--;
                        node.CurResHold++;
                        var nodeResourcesScript = nodeObj.GetComponent<ResourcesType>();
                        nodeResourcesScript.currRes = (ResourceType1)factory.resourceToAdd + 1;
                        
                    }
                }
            }
            
            /*if(nodeObj.tag == "Sentry" && nodeObj.GetComponent<ResourcesType>().resAmmount < nodeObj.GetComponent<ResourcesType>().maxResAmmount)
            {
                ResourcesType buildingResourcesScript = nodeObj.GetComponent<ResourcesType>();
                resAmmount--;
                buildingResourcesScript.resAmmount++;
            }*/
            yield return new WaitForSeconds(1);
        }
    }

    void OnMouseDown()
    {
        if (LoadIn)
        {
            var curPos = transform.position;
            nearbyNodesScripts.Clear();
            FindObjectsInRadius();
            for (int i = 0; i < nearbyNodesScripts.Count; i++)
            {
                if (nearbyNodesScripts[i].NodeConstruction == true)
                {
                    nearbyNodesScripts[i].line.SetPosition(1, curPos);
                    nearbyNodesScripts[i].output++;
                    nearbyNodesScripts[i].buildingResNeed = transform.GetComponent<ResourcesType>();
                    nearbyNodesScripts[i].NodeConstruction = false;
                    break;
                }
            }
        }
    }

    public static List<ResourceType1> GetActiveFlags(ResourceType1 flags)
    {
        List<ResourceType1> activeFlags = new List<ResourceType1>();

        foreach (ResourceType1 flag in Enum.GetValues(typeof(ResourceType1)))
        {
            if (flag != ResourceType1.None && flags.HasFlag(flag))
            {
                activeFlags.Add(flag);
            }
        }

        return activeFlags;
    }

    IEnumerator CheckFlags()
    {
        while (true)
        {
            activeFlags = GetActiveFlags(currRes);
            yield return new WaitForSeconds(1);
        }
    }
}
