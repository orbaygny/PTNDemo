using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WorldCreator : MonoBehaviour
{
    public static WorldCreator Instance { get; private set; }

    [SerializeField] private int witdth,height;
    [SerializeField] private Tile tilePrefab;
    [Space]
    [SerializeField] private Camera mainCam;

    Queue<Vector3> cellPositions;
    int pointer;
   
    private int xCellSize;
    private int yCellSize;
    
    private void Awake()
    {
        Instance = this;
        pointer = 1;
        CalculateAspectRatio();
    }
    // Start is called before the first frame update
    void Start()
    {
        cellPositions = new Queue<Vector3>();
        GenerateGrid();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void GenerateGrid()
    {
        for(int x=0; x <(int)(witdth * 2 * (9f / height)) ; x++)
        {
            for (int y = 0; y < (int)(height * 2 * (9f / height)); y++)
            {
                cellPositions.Enqueue(new Vector3(x * 8, y * 8, 0));

                 var tile = Instantiate(tilePrefab, new Vector3(x*8, y*8), Quaternion.identity,transform);
                 tile.name = $"Tile {x} {y}";

                 var isOffset = (x % 2 == 0 && y % 2 != 0) || (x % 2 != 0 && y % 2 == 0);
                 tile.Init(isOffset);
            }
        }

        mainCam.transform.position = new Vector3(((float)witdth * 8 * (9f / height)) -4, ((float)height*8 * (9f / height)) -4 ,-10);
    }

    public void CalculateAspectRatio()
    {
        float tmp = Camera.main.aspect * pointer;
        if(tmp-(int)tmp == 0)
        {   if(tmp == 8)
            {
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
        if(t.CompareTo("x") ==0)
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

    /*private void OnDrawGizmos()
    {
        if(cellPositions.Count != 0)
        {
            foreach (Vector3 pos in cellPositions)
            {
                var isOffset = ((pos.x/8) % 2 == 0 &&(pos.y/8) % 2 != 0) || ((pos.x/8) % 2 != 0 && (pos.y/8) % 2 == 0);
               Gizmos.color = isOffset ? Color.white : Color.black;
                Gizmos.DrawCube(pos, new Vector3(8, 8, 0));
            }
        }*/
    }
