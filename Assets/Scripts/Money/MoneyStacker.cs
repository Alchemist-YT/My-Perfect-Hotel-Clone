using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MoneyStacker : MonoBehaviourSingleton<MoneyStacker>
{
    public Transform stackStartPoint;
    public Vector3 spacing = new Vector3(1.0f, 0.1f, 1.0f);

    [SerializeField] int xStacks = 10;
    [SerializeField] int yStacks = 10;
    [SerializeField] int zStacks = 10;

    [SerializeField] MoneyStackPoint moneyStackPrefab;
    [SerializeField] List<MoneyStackPoint> moneyStackPoints = new();

    Vector3 nextStackingPoint;
    int currentStacks = 0;

    void Start()
    {
        MakeStack();
    }
    [Button()]
    void MakeStack()
    {
        nextStackingPoint = stackStartPoint.position;

        CreateGrid();
    }
    void CreateGrid()
    {
        for (int z = 0; z < zStacks; z++)
        {
            for (int x = 0; x < xStacks; x++)
            {
                Vector3 pos = nextStackingPoint + new Vector3(x * spacing.x, 0f, z * spacing.z);
                SpawnStackPoint(pos);
            }
        }

        currentStacks++;
        if (currentStacks >= yStacks)
        {
            Debug.Log("Maximum stacks reached!");
            return;
        }

        nextStackingPoint.y += spacing.y;
        CreateGrid();
    }
    void SpawnStackPoint(Vector3 position)
    {
        var point = Instantiate(moneyStackPrefab, transform);
        point.transform.position = position;
        moneyStackPoints.Add(point);
    }

    public MoneyStackPoint GetEmptyMoneyStackPoint()
    {
        return moneyStackPoints.Find((point) => !point.HasMoney());
    }
}
