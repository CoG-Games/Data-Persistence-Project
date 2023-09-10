using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Scriptable Object/Wave Spawn List")]
public class WaveEnemyListSO : ScriptableObject
{
    [System.Serializable]
    public struct EnemySpawnStruct
    {
        public GameObject enemyPrefab;
        public int spawnLikelihood;
    }
    [SerializeField] private List<EnemySpawnStruct> spawnableEnemyList;

    private int totalLikelihoodSum;
    private void Awake()
    {
        spawnableEnemyList = new List<EnemySpawnStruct>();
    }

    private void OnEnable()
    {
        totalLikelihoodSum = 0;
        foreach (EnemySpawnStruct enemy in spawnableEnemyList)
        {
            totalLikelihoodSum += enemy.spawnLikelihood;
        }
    }

    public GameObject GetRandomEnemy()
    {
        int selectedIndex = Random.Range(0, totalLikelihoodSum);
        for(int i = 0; i < spawnableEnemyList.Count-1; i++)
        {
            if(selectedIndex < spawnableEnemyList[i].spawnLikelihood)
            {
                return spawnableEnemyList[i].enemyPrefab;
            }
            else
            {
                selectedIndex -= spawnableEnemyList[i].spawnLikelihood;
            }
        }
        int last = spawnableEnemyList.Count - 1;
        return spawnableEnemyList[last].enemyPrefab;
    }
}

