using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WavesSystem : MonoBehaviour
{
    public WaveSettings[] Enemys;
    public int CurrentWaavesCount = 0;
    public int wavesCount;
    public Transform SpawnZone;
    public List<float> TimesToDeal;
    public float time;
    public TextMeshProUGUI WavesText;
    public GameObject BossHealth;
    void Start()
    {
        time = TimesToDeal[0];
        BossHealth.SetActive(false);
    }

    public void Update()
    {
        if(CurrentWaavesCount < wavesCount)
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
        else if(CurrentWaavesCount == wavesCount)
        {
            SpawnEnemy();
            CurrentWaavesCount++;
            BossHealth.SetActive(true);
            WavesText.text = "Defeat Mr Big";
        }
    }

    void SpawnEnemy()
    {
        for(int j = 0; j < Enemys[CurrentWaavesCount].Enemy_.Length; j++)
        {
            Instantiate(Enemys[CurrentWaavesCount].Enemy_[j], SpawnZone.position, Quaternion.identity);
        }
    }

    [System.Serializable]
    public class WaveSettings
    {
        [SerializeField] private GameObject[] _enemy;
        public GameObject[] Enemy_ { get => _enemy; }
    }
}
