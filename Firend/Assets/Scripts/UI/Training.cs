using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class Training : MonoBehaviour
{
    public TextMeshProUGUI text;
    public WavesSystem wavesSystem;
    public PlayerHealth plHealth;
    private int TextCurrentCount = 0;
    private int TextsCount = 3;
    void Start()
    {
        TextCurrentCount = PlayerPrefs.GetInt("TextsCount");
        text.text = "Чтобы передвигаться наведитесь на место, куда вы хотите переместиться, и нажмите ПКМ. Нажмите [Enter] чтобы продолжить.";
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (TextCurrentCount == 0)
            {
                text.text = "Чтобы уничтожить врага нужно подпустить его поближе в красной зоне, а потом нажать ЛКМ. Нажмите [Enter] чтобы продолжить.";
                TextCurrentCount += 1;
            }
            else if (TextCurrentCount == 1)
            {
                text.text = "Вы можете ставить инкубаторы юнитов. Для этого нужно нажать на кнопку [1], а потом в выбраном месте ЛКМ. Нажмите [Enter] чтобы продолжить.";
                TextCurrentCount += 1;
            }
            else if (TextCurrentCount == 2)
            {
                text.text = "Стоит заметить, что размещение инкубаторов тратит ваше здоровье. Но его можно восстонавливать сьедая врагов. Нажмите [Enter] чтобы начать.";
                TextCurrentCount += 1;
            }
            else if(TextCurrentCount == 3)
            {
                text.text = "";
                plHealth.StartLoseHealth();
                PlayerPrefs.SetInt("TextsCount", TextCurrentCount);
                wavesSystem.time = 1;
            }
        }

        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            PlayerPrefs.DeleteKey("TextsCount");
        }
    }

    /*
    public TextMeshProUGUI text;
    public WavesSystem wavesSystem;
    public PlayerHealth plHealth;
    private int TextCurrentCount = 0;
    private int TextsCount = 3;
    void Start()
    {
        TextCurrentCount = PlayerPrefs.GetInt("TextsCount");
        text.text = "×òîáû ïåðåäâèãàòüñÿ íàâåäèòåñü íà ìåñòî, êóäà âû õîòèòå ïåðåìåñòèòüñÿ, è íàæìèòå ÏÊÌ. Íàæìèòå [Enter] ÷òîáû ïðîäîëæèòü.";
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (TextCurrentCount == 0)
            {
                text.text = "×òîáû óíè÷òîæèòü âðàãà íóæíî ïîäïóñòèòü åãî ïîáëèæå â êðàñíîé çîíå, à ïîòîì íàæàòü ËÊÌ. Íàæìèòå [Enter] ÷òîáû ïðîäîëæèòü.";
                TextCurrentCount += 1;
            }
            else if (TextCurrentCount == 1)
            {
                text.text = "Âû ìîæåòå ñòàâèòü èíêóáàòîðû þíèòîâ. Äëÿ ýòîãî íóæíî íàæàòü íà êíîïêó [1], à ïîòîì â âûáðàíîì ìåñòå ËÊÌ. Íàæìèòå [Enter] ÷òîáû ïðîäîëæèòü.";
                TextCurrentCount += 1;
            }
            else if (TextCurrentCount == 2)
            {
                text.text = "Ñòîèò çàìåòèòü, ÷òî ðàçìåùåíèå èíêóáàòîðîâ òðàòèò âàøå çäîðîâüå. Íî åãî ìîæíî âîññòîíàâëèâàòü ñüåäàÿ âðàãîâ. Íàæìèòå [Enter] ÷òîáû íà÷àòü.";
                TextCurrentCount += 1;
            }
            else if(TextCurrentCount == 3)
            {
                text.text = "";
                plHealth.StartLoseHealth();
                PlayerPrefs.SetInt("TextsCount", TextCurrentCount);
                wavesSystem.time = 1;
            }
        }

        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            PlayerPrefs.DeleteKey("TextsCount");
        }
    }*/
}
