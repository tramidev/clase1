using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GridManager : MonoBehaviour {
    [SerializeField] private int _width, _height;
    [SerializeField] private GameObject _tilePrefab;

    public Material mat1;
    public Material mat2;
    public Material mat_wall_1;
    public Material mat_wall_2;
    public Material mat4_obstacle;

    public int obstacles;

    public List<GameObject> Floor = new List<GameObject>();
    public List<GameObject> Walls = new List<GameObject>();
    public List<GameObject> InternalWalls = new List<GameObject>();
    public GameObject goal;
    
    void Start() {
        
    }

    [ContextMenu("Generate Random obstacles")]
    void GenerateRandomObstacles()
    {
        if (InternalWalls.Count > 0) RemoveWalls();
        float xOffset = (int)(_width/2);
        float yOffset = (int)(_height/2);
        for (int i = 0; i < obstacles; i++)
        {
            Vector3 position = Vector3.zero;
            position.x = Random.Range(0,_width)-xOffset;
            position.y = Random.Range(0,_height)-yOffset;
            var spawnedWall = Instantiate(_tilePrefab, position, Quaternion.identity);
            spawnedWall.tag = "WALL";
            InternalWalls.Add(spawnedWall);
            spawnedWall.GetComponent<MeshRenderer>().material = mat_wall_1;
            spawnedWall.transform.parent = transform;
            spawnedWall.name = $"Wall Tile {position.x} {position.y}";
        }
    }

    [ContextMenu("GenerateGrid")]
    void GenerateGrid()
    {
        float xOffset = (int)(_width/2);
        float yOffset = (int)(_height/2);
        for (int x = 0; x < _width; x++) {
            for (int y = 0; y < _height; y++) {
                var spawnedTile = Instantiate(_tilePrefab, new Vector3(x-xOffset, y-yOffset), Quaternion.identity);
                Floor.Add(spawnedTile);
                spawnedTile.transform.parent = transform;
                var pos = spawnedTile.transform.localPosition;
                pos.z = 0;
                spawnedTile.transform.localPosition = pos;
                spawnedTile.name = $"Tile {x} {y}";
                
                var isOffset = (x % 2 == 0 && y % 2 != 0) || (x % 2 != 0 && y % 2 == 0);
                
                if(isOffset)
                    spawnedTile.GetComponent<MeshRenderer>().material = mat1;
                else
                {
                    spawnedTile.GetComponent<MeshRenderer>().material = mat2;
                }

                bool leftWall = x == 0;
                bool rightWall = x==_width-1;
                bool topWall = y==_height-1;
                bool bottomWall = y == 0;

                Walls = new List<GameObject>();
                void CreateWall(Vector3 pos, bool isOffset)
                {
                    
                    var spawnedWall = Instantiate(_tilePrefab, pos, Quaternion.identity);
                    Walls.Add(spawnedWall);
                    spawnedWall.GetComponent<MeshRenderer>().material = mat_wall_1;
                    spawnedWall.tag = "WALL";
                    spawnedWall.transform.parent = transform;
                    spawnedWall.name = $"Wall Tile {x} {y}";
                }
                
                if (leftWall || rightWall || topWall || bottomWall)
                {
                    Vector3 wallPos = new Vector3(x - xOffset, y - yOffset);
                    if (leftWall) CreateWall(wallPos+new Vector3(-1,0,0),isOffset);
                    if (rightWall) CreateWall(wallPos+new Vector3(1,0,0),isOffset);
                    if (topWall) CreateWall(wallPos+new Vector3(0,1,0),isOffset);
                    if (bottomWall) CreateWall(wallPos+new Vector3(0,-1,0),isOffset);
                }
                
                
            }
        }
    }
    
    [ContextMenu("RemoveGrid")]
    void RemoveGrid()
    {
        foreach (var obj in Floor)
        {
            DestroyImmediate(obj);
        }

        Floor = new List<GameObject>();

        foreach (var wall in Walls)
        {
            DestroyImmediate(wall);
        }
        Walls = new List<GameObject>();
        
        foreach (var wall in InternalWalls)
        {
            DestroyImmediate(wall);
        }
        InternalWalls = new List<GameObject>();
    }
    
    void RemoveWalls()
    {
        foreach (var wall in Walls)
        {
            Destroy(wall);
        }
        Walls = new List<GameObject>();
    }
    
    
}
