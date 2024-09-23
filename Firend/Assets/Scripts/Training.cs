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
        text.text = "����� ������������� ���������� �� �����, ���� �� ������ �������������, � ������� ���. ������� [Enter] ����� ����������.";
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (TextCurrentCount == 0)
            {
                text.text = "����� ���������� ����� ����� ���������� ��� ������� � ������� ����, � ����� ������ ���. ������� [Enter] ����� ����������.";
                TextCurrentCount += 1;
            }
            else if (TextCurrentCount == 1)
            {
                text.text = "�� ������ ������� ���������� ������. ��� ����� ����� ������ �� ������ [1], � ����� � �������� ����� ���. ������� [Enter] ����� ����������.";
                TextCurrentCount += 1;
            }
            else if (TextCurrentCount == 2)
            {
                text.text = "����� ��������, ��� ���������� ����������� ������ ���� ��������. �� ��� ����� ��������������� ������ ������. ������� [Enter] ����� ������.";
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
