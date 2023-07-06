using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum BuildingType{
    TestCube,
    HousingLVL1,
}
///Should be only one instance at a time!
public class World : MonoBehaviour
{
    public static GameObject GetRockGO(RockType type){
        switch(type){
            case RockType.Rocks1:
                return _rockPrefabs[0];
            case RockType.Rocks2:
                return _rockPrefabs[1];
            default:
                return _rockPrefabs[0];
        }
    }
    private static GameObject[] _rockPrefabs;
    WorldData data;
    public Material highlightMaterial;
    public GameObject[] buildingPrefabs;
    public GameObject[] rockPrefabs;
    GameObject[,] buildingGOs;
    // Start is called before the first frame update
    void Start()
    {
        _rockPrefabs = rockPrefabs;
        data = new WorldData(128,0xFFFFFFFF);
        MeshFilter renderer = gameObject.GetComponent<MeshFilter>();
        renderer.mesh = data.GenerateMesh();
        MeshCollider collider = gameObject.GetComponent<MeshCollider>();
        collider.sharedMesh = renderer.mesh;
        buildingGOs = new GameObject[data.sideSize,data.sideSize];
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }
    public GameObject GetBuildingPrefab(BuildingType type){
        switch(type){
            case BuildingType.TestCube:
                return buildingPrefabs[0];
            case BuildingType.HousingLVL1:
                return buildingPrefabs[1];
            default:
                return buildingPrefabs[0];
        }
    }
    public bool PlaceBuilding(int x, int y,BuildingType type){
        if(buildingGOs[x,y]){
            return false;
        }
        GameObject prefab = GetBuildingPrefab(type);
        buildingGOs[x,y] = GameObject.Instantiate(prefab,new Vector3(x + 0.5f,0.0f, y + 0.5f),prefab.transform.rotation);
        return true;
    }
}
