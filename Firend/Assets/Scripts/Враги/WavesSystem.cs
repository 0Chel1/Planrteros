using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WavesSystem : MonoBehaviour
{
    public WaveSettings[] Enemys;
    public int CurrentWaavesCount = 0;
    public Transform SpawnZone;
    public List<float> TimesToDeal;
    public float time;
    public TextMeshProUGUI WavesText;
    public TextMeshProUGUI WavesCountText;
    public GameObject BossHealth;

    void Start()
    {
        BossHealth.SetActive(false);
        if (TimesToDeal.Count > 0)
        {
            time = TimesToDeal[0];
        }
    }

    void Update()
    {
        if (CurrentWaavesCount < Enemys.Length)
        {
            time -= Time.deltaTime;
            WavesText.text = Mathf.Round(time).ToString();

            if (time <= 0)
            {
                SpawnEnemy();
                CurrentWaavesCount++;

                if (CurrentWaavesCount < TimesToDeal.Count)
                {
                    time = TimesToDeal[CurrentWaavesCount];
                }
                else
                {
                    time = 0;
                }
            }
        }
        else if (CurrentWaavesCount == Enemys.Length)
        {
            BossHealth.SetActive(true);
            SpawnEnemy();
            WavesText.text = "Одолей Босса";
        }

        WavesCountText.text = CurrentWaavesCount + "/" + Enemys.Length;
    }

    void SpawnEnemy()
    {
        if (CurrentWaavesCount < Enemys.Length)
        {
            for (int j = 0; j < Enemys[CurrentWaavesCount].Enemy_.Length; j++)
            {
                if (j >= Enemys[CurrentWaavesCount].Enemy_.Length)
                {
                    break;
                }
                Instantiate(Enemys[CurrentWaavesCount].Enemy_[j], SpawnZone.position, Quaternion.identity);
            }
        }
    }

    [System.Serializable]
    public class WaveSettings
    {
        [SerializeField] private GameObject[] _enemy;
        public GameObject[] Enemy_ { get => _enemy; }
    }
}
