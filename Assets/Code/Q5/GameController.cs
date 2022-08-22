using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Q5
{
    public class GameController : MonoBehaviour
    {
        public static GameController instance;
        public float timeElapsed;
        public Transform[] spawnPoints;
        public GameObject[] asteroidPrefabs;
        public GameObject explosionPrefab;
        public float minAsteroidDelay = 0.2f;
        public float maxAsteroidDelay = 2f;
        public float asteroidDelay;
        public TMP_Text textScore;
        public int score;
        public TMP_Text textMoney;
        public int money;
        public TMP_Text missileSpeedUpgradeText;
        public float missileSpeed = 2f;
        public TMP_Text bonusUpgradeText;
        public float bonusMultiplier = 1f;

        void Awake()
        {
            instance = this;
        }

        void Start()
        {
            StartCoroutine("AsteroidSpawnTimer");
            score = 0;
            money = 0;
        }

        void Update()
        {
            timeElapsed += Time.deltaTime;
            float decreaseDelayOverTime = maxAsteroidDelay - ((maxAsteroidDelay - minAsteroidDelay) / 30f * timeElapsed);
            asteroidDelay = Mathf.Clamp(decreaseDelayOverTime, minAsteroidDelay, maxAsteroidDelay);
            UpdateDisplay();
        }

        void UpdateDisplay()
        {
            textScore.text = score.ToString();
            textMoney.text = money.ToString();
        }

        public void EarnPoints(int pointAmount)
        {
            score += Mathf.RoundToInt(pointAmount * bonusMultiplier);
            money += Mathf.RoundToInt(pointAmount * bonusMultiplier);
        }

        void SpawnAsteroid()
        {
            Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            GameObject randomAsteroidPrefabs = asteroidPrefabs[Random.Range(0, asteroidPrefabs.Length)];
            Instantiate(randomAsteroidPrefabs, randomSpawnPoint.position, Quaternion.identity);
        }

        IEnumerator AsteroidSpawnTimer()
        {
            yield return new WaitForSeconds(asteroidDelay);
            SpawnAsteroid();
            StartCoroutine("AsteroidSpawnTimer");
        }

        public void UpgradeMissileSpeed()
        {
            int cost = Mathf.RoundToInt(25 * missileSpeed);
            if (cost <= money)
            {
                money -= cost;
                missileSpeed += 1f;
                missileSpeedUpgradeText.text = "Missile Speed $" + Mathf.RoundToInt(25 * missileSpeed);
            }
        }

        public void UpgradeBonus()
        {
            int cost = Mathf.RoundToInt(100 * bonusMultiplier);
            if (cost <= money)
            {
                money -= cost;
                bonusMultiplier += 1f;
                bonusUpgradeText.text = "Multiplier $" + Mathf.RoundToInt(100 * bonusMultiplier);
            }
        }
    }
}
