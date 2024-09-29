using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using static ResourcesType;

public class Factory : MonoBehaviour
{
    public ResourcesType resources;
    public int resourceToAdd;
    void Start()
    {
        StartCoroutine(CreateMatireals());
    }

    IEnumerator CreateMatireals()
    {
        while (true)
        {
            // ���������, ����� �� ������� ���� �������.
            bool canConsume = true;

            // ��������� ������� ����������� ��������
            for (int i = 0; i < System.Enum.GetValues(typeof(ResourceType1)).Length; i++)
            {
                // ���������, ��������������� �� ���� ���� � ������������
                ResourceType1 currentResource = (ResourceType1)(1 << i);
                if (resources.currRes.HasFlag(currentResource))
                {
                    if (resources.resAmmount[i] <= 0)
                    {
                        canConsume = false; // ���� ���� �� ���� ������ �� ����� ���� ������, ������� �� �����.
                        break;
                    }
                }
            }

            // ���� ��� ����������� ������� ����, �������� � ��������� ����� ������
            if (canConsume && resources.resAmmount[resourceToAdd] < resources.maxResAmmount)
            {
                for (int i = 0; i < System.Enum.GetValues(typeof(ResourceType1)).Length; i++)
                {
                    ResourceType1 currentResource = (ResourceType1)(1 << i);
                    if (resources.currRes.HasFlag(currentResource))
                    {
                        resources.resAmmount[i]--; // ��������� ������� �� 1
                    }
                }

                // ���������� ������ ������� ���������� �������
                if (resourceToAdd >= 0 && resourceToAdd < resources.resAmmount.Count)
                {
                    resources.resAmmount[resourceToAdd]++;
                }
            }
            yield return new WaitForSeconds(1);
        }
    }

}
