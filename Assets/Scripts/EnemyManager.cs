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
    [SerializeField] private List<GameObject> enemySpawnList;
    [SerializeField] private FloatVariableSO wave;

    private GameObject[,] enemyMatrix;
    private float movementSpeed;
    private int leftmostCol;
    private int rightmostCol;
    private Transform rightmostTransform;
    private Transform leftmostTransform;

    private void Start()
    {
        Reset();
    }

    private void Reset()
    {
        wave.value++;
        if(enemyMatrix == null)
        {
            enemyMatrix = new GameObject[rows, columns];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    GameObject enemyPrefab = enemySpawnList[Random.Range(0, enemySpawnList.Count)];
                    enemyMatrix[i, j] = Instantiate(enemyPrefab, transform.position + (i * rowSeparation - (rows - 1) * rowSeparation / 2) * Vector3.up + (j * colSeparation - (columns - 1) * colSeparation / 2) * Vector3.right, enemyPrefab.transform.rotation, transform);
                }
            }
        }
        else
        {
            transform.position = initialPosition;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    enemyMatrix[i, j].SetActive(true);
                }
            }
        }

        leftmostCol = 0;
        rightmostCol = columns - 1;
        FindLeftmostTransform();
        FindRightmostTransform();

        movementSpeed = initialHorizontalMovementSpeed;
    }

    private void Update()
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
            Reset();
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
