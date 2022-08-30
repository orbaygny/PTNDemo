using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WorldCreator : MonoBehaviour
{
    public static WorldCreator Instance { get; private set; }

    [SerializeField] private int witdth, height;
    [SerializeField] private Tile tilePrefab;
    [Space]
    [SerializeField] private Camera mainCam;

    int pointer;

    private void Awake()
    {
        Application.targetFrameRate = 60;
        Instance = this;
        pointer = 1;
        CalculateAspectRatio();
    }
    // Start is called before the first frame update
    void Start()
    {
        //cellPositions = new Queue<Vector3>();
        GenerateGrid();
    }
    public int MaxSize
    {
        get
        {
            return (int)(witdth * 2 * (9f / height)) * (int)(height * 2 * (9f / height));
        }
    }
    void GenerateGrid()
    {
        //Calculations based on 16:9 default aspect ratio

        for (int x = 0; x < (int)(witdth * 2 * (9f / height)); x++)
        {
            for (int y = 0; y < (int)(height * 2 * (9f / height)); y++)
            {
                var tile = Instantiate(tilePrefab, new Vector3(x * 8, y * 8), Quaternion.identity, transform);
                tile.name = $"Tile {x} {y}";
                tile.gridX = x;
                tile.gridY = y;
                var isOffset = (x % 2 == 0 && y % 2 != 0) || (x % 2 != 0 && y % 2 == 0);
                tile.Init(isOffset);
            }
        }

        mainCam.transform.position = new Vector3(((float)witdth * 8 * (9f / height)) - 4, ((float)height * 8 * (9f / height)) - 4, -10);
    }

    public void CalculateAspectRatio()
    {
        // Converting aspect float to aspect ratio
        float tmp = Camera.main.aspect * pointer;
        if (tmp - (int)tmp == 0)
        {
            if (tmp == 8)
            {
                // 16:10 is exceptional for this calculation, this part for fix that
                tmp = 16;
                pointer = 10;
            }
            height = pointer;
            witdth = (int)tmp;

        }
        else
        {
            pointer++;
            CalculateAspectRatio();
        }
    }

    public int GetHeight()
    {
        return height;
    }

    public int GetWitdth()
    {
        return witdth;
    }

    public int ReturnCellCize(string t)
    {
        if (t.CompareTo("x") == 0)
        {
            var tmp = (int)(witdth * 2 * (9f / height));
            return tmp;
        }

        else if (t.CompareTo("y") == 0)
        {
            var tmp = (int)(height * 2 * (9f / height));
            return tmp;
        }

        return 0;
    }

   
}