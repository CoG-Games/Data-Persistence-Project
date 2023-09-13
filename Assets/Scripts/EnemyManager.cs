using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [Header("Movement Fields")]
    [SerializeField] private Vector2 initialPosition;
    [SerializeField] private float initialHorizontalMovementSpeed = 1f;
    [SerializeField] private float xThreshold = 5f;
    [SerializeField] private float verticalDropDistance = 0.5f;

    [Header("Spawning Fields")]
    [SerializeField] private int rows = 4;
    [SerializeField] private int columns = 7;
    [SerializeField] private float rowSeparation = 2f;
    [SerializeField] private float colSeparation = 2f;
    [SerializeField] private List<WaveEnemyListSO> waveList;
    [SerializeField] private FloatVariableSO wave;
    [SerializeField] private FloatVariableSO isWaveActive;
    [SerializeField] private FloatVariableSO waveCount;

    [Header("Event Fields")]
    [SerializeField] private VoidEventSO waveCompleted;
    [SerializeField] private VoidEventSO preWaveStart;
    [SerializeField] private VoidEventSO waveStart;

    private GameObject[,] enemyMatrix;
    private Dictionary<int, List<GameObject>> enemyPool;
    private float movementSpeed;
    private int leftmostCol;
    private int rightmostCol;
    private Transform rightmostTransform;
    private Transform leftmostTransform;

    private void Start()
    {
        waveCount.value = waveList.Count;
        isWaveActive.value = 0;
        enemyPool = new Dictionary<int, List<GameObject>>();
    }

    private void OnEnable()
    {
        preWaveStart.OnEventRaised += Reset;
        waveStart.OnEventRaised += StartWave;
    }

    private void OnDisable()
    {
        preWaveStart.OnEventRaised -= Reset;
        waveStart.OnEventRaised -= StartWave;
    }

    private void Reset()
    {
        if(wave.value > waveList.Count)
        {
            return;
        }
        if (enemyMatrix == null)
        {
            enemyMatrix = new GameObject[rows, columns];
        }
        StartCoroutine(ResetRoutine());
    }

    private IEnumerator ResetRoutine()
    {
        int waveIndex = (int)wave.value - 1;
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                int enemyNumber;
                if (enemyMatrix[i, j] != null)
                {
                    enemyNumber = int.Parse(enemyMatrix[i, j].name.Substring(0, 2));
                    TryCheckEnemyPool(enemyNumber);
                    enemyPool[enemyNumber].Add(enemyMatrix[i, j]);
                    enemyMatrix[i, j] = null;
                }
                GameObject enemyPrefab = waveList[waveIndex].GetRandomEnemy();
                enemyNumber = int.Parse(enemyPrefab.name.Substring(0, 2));
                TryCheckEnemyPool(enemyNumber);
                if (enemyPool[enemyNumber].Count == 0)
                {
                    enemyMatrix[i, j] = Instantiate(enemyPrefab, transform.position + GetLocalPositionFromGridIndex(i, j), enemyPrefab.transform.rotation, transform);
                }
                else
                {
                    enemyMatrix[i, j] = enemyPool[enemyNumber][0];
                    enemyPool[enemyNumber].RemoveAt(0);
                    enemyMatrix[i, j].transform.localPosition = GetLocalPositionFromGridIndex(i, j);
                    enemyMatrix[i, j].transform.rotation = enemyPrefab.transform.rotation;
                    enemyMatrix[i, j].SetActive(true);
                }
                yield return null;
            }
        }

        transform.position = initialPosition;
        leftmostCol = 0;
        rightmostCol = columns - 1;
        FindLeftmostTransform();
        FindRightmostTransform();
    }

    private void StartWave()
    {
        if (wave.value > waveList.Count)
        {
            return;
        }
        isWaveActive.value = 1;
        movementSpeed = initialHorizontalMovementSpeed;
    }

    private void TryCheckEnemyPool(int enemyNumber)
    {
        if (!enemyPool.ContainsKey(enemyNumber))
        {
            enemyPool[enemyNumber] = new List<GameObject>();
        }
    }

    private Vector3 GetLocalPositionFromGridIndex(int i, int j)
    {
        return (i * rowSeparation - (rows - 1) * rowSeparation / 2) * Vector3.up + (j * colSeparation - (columns - 1) * colSeparation / 2) * Vector3.right;
    }

    private void Update()
    {
        if(isWaveActive.value == 1)
        {
            if(EnemyBase.EnemyCount > 0)
            {
                if (!rightmostTransform.gameObject.activeInHierarchy)
                {
                    FindRightmostTransform();
                }
                if (!leftmostTransform.gameObject.activeInHierarchy)
                {
                    FindLeftmostTransform();
                }
            }
            else
            {
                isWaveActive.value = 0;
                movementSpeed = 0;
                waveCompleted.RaiseEvent();
            }

            float xCheckPosition = (movementSpeed > 0 ? rightmostTransform.position.x : leftmostTransform.position.x);
            if (Mathf.Abs(xCheckPosition + movementSpeed * Time.deltaTime) > xThreshold)
            {
                Vector3 newPosition = transform.position;
                newPosition.y -= verticalDropDistance;
                transform.position = newPosition;
                float speedUpFactor = -1.05f;
                movementSpeed *= speedUpFactor;
            }
            else
            {
                transform.position += movementSpeed * Time.deltaTime * Vector3.right;
            }
        }
    }

    private void FindRightmostTransform()
    {
        for(int j = rightmostCol; j >= leftmostCol; j--)
        {
            rightmostCol = j;
            for (int i = rows - 1; i >= 0; i--)
            {
                if(enemyMatrix[i,j].activeInHierarchy)
                {
                    rightmostTransform = enemyMatrix[i, j].transform;
                    return;
                }
            }
        }
    }
    private void FindLeftmostTransform()
    {
        for(int j = leftmostCol; j <= rightmostCol; j++)
        {
            leftmostCol = j;
            for(int i = rows-1; i >= 0; i--)
            {
                if(enemyMatrix[i,j].activeInHierarchy)
                {
                    leftmostTransform = enemyMatrix[i, j].transform;
                    return;
                }
            }
        }
    }
}
