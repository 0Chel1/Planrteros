using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;
using static System.Net.Mime.MediaTypeNames;

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
}
