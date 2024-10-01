using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using static ResourcesType;

public class Factory : MonoBehaviour
{
    public ResourcesType resources;
    public ResourceType1 resourceToAdd = ResourceType1.None;
    void Start()
    {
        StartCoroutine(CreateMatireals());
    }

    public int ConvertPowerOfTwoToSequenceNumber(int x)
    {
        return x > 0 ? (int)(Math.Log(x, 2) + 1) : 0; // Добавляем проверку на 0
    }

    IEnumerator CreateMatireals()
    {
        while (true)
        {
            // Проверяем, могут ли ресурсы быть вычтены.
            bool canConsume = true;

            // Проверяем наличие необходимых ресурсов
            for (int i = 0; i < System.Enum.GetValues(typeof(ResourceType1)).Length; i++)
            {
                // Проверяем, устанавливается ли этот флаг в перечислении
                ResourceType1 currentResource = (ResourceType1)(1 << i);
                if (resources.currRes.HasFlag(currentResource))
                {
                    if (resources.resAmmount[i] <= 0)
                    {
                        canConsume = false; // Если хотя бы один ресурс не может быть вычтен, выходим из цикла.
                        break;
                    }
                }
            }

            // Если все необходимые ресурсы есть, вычитаем и добавляем новый ресурс
            if (canConsume && resources.resAmmount[ConvertPowerOfTwoToSequenceNumber((int)resourceToAdd)] < resources.maxResAmmount)
            {
                for (int i = 0; i < System.Enum.GetValues(typeof(ResourceType1)).Length; i++)
                {
                    ResourceType1 currentResource = (ResourceType1)(1 << i);
                    if (resources.currRes.HasFlag(currentResource))
                    {
                        resources.resAmmount[i]--; // Уменьшаем ресурсы на 1
                    }
                }

                // Добавление нового ресурса указанного индекса
                if (resourceToAdd >= 0 && ConvertPowerOfTwoToSequenceNumber((int)resourceToAdd) < resources.resAmmount.Count)
                {
                    resources.resAmmount[ConvertPowerOfTwoToSequenceNumber((int)resourceToAdd)]++;
                }
            }
            yield return new WaitForSeconds(1);
        }
    }

}
