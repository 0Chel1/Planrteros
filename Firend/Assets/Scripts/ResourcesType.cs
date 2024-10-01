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
    public List<ResourcesType> nearNodesRes;
    public transportnode node;
    public ResourcesType nodeRes;
    public Factory factory;
    public float searchRadius = 1f;

    [Flags]
    public enum ResourceType1
    {
        Copper = 1 << 0, //0
        Sand = 1 << 1, //1
        Kwartz = 1 << 2, //2
        Pestan = 1 << 3, //3 Sand + Copper
        Quadran = 1 << 4, //4 Copper + Kwartz
        None = 2 << 5
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
        nearNodesRes.Clear();
        nearbyNodesScripts.Clear();
        var allNodes = FindObjectsOfType<GameObject>();
        float nearestDist = Mathf.Infinity;

        foreach (var nodE in allNodes)
        {
            float distance = Vector2.Distance(transform.position, nodE.transform.position);
            if (nodE == this)
                continue;

            if (distance <= searchRadius && nodE.tag == "node" && distance < nearestDist)
            {
                nearestDist = distance;
                nodeRes = nodE.GetComponent<ResourcesType>();
                node = nodE.GetComponent<transportnode>();
            }

            if(distance <= searchRadius && nodE.tag == "node")
            {
                nearNodesRes.Add(nodE.GetComponent<ResourcesType>());
                nearbyNodesScripts.Add(nodE.GetComponent<transportnode>());
            }
        }
    }

    public IEnumerator UnloadInNode()
    {
        while (true)
        {
            if (node != null && nodeRes.tag == "node" &&  node.CurResHold < node.resCapacity)
            {
                if (!isFactory)
                {
                    resAmmount[0]--;
                    node.CurResHold++;
                    nodeRes.currRes = currRes;
                }
                else if (isFactory && resAmmount[factory.ConvertPowerOfTwoToSequenceNumber((int)factory.resourceToAdd)] > 0)
                {
                    nodeRes.currRes = factory.resourceToAdd;
                    resAmmount[factory.ConvertPowerOfTwoToSequenceNumber((int)factory.resourceToAdd)]--;
                    node.CurResHold++;

                }
            }
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
            FindObjectsInRadius();
            yield return new WaitForSeconds(1);
        }
    }
}
