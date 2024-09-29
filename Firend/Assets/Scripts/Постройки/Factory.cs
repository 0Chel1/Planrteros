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
            if (canConsume && resources.resAmmount[resourceToAdd] < resources.maxResAmmount)
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
                if (resourceToAdd >= 0 && resourceToAdd < resources.resAmmount.Count)
                {
                    resources.resAmmount[resourceToAdd]++;
                }
            }
            yield return new WaitForSeconds(1);
        }
    }

}
