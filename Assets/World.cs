using System.Collections;
using System.Collections.Generic;
using UnityEngine;
///Should be only one instance at a time!
public class World : MonoBehaviour
{
    public static GameObject GetRockGO(RockType type){
        switch(type){
            case RockType.Rocks1:
                return _rockPrefabs[0];
            case RockType.Rocks2:
                return _rockPrefabs[1];
            case RockType.Rocks3:
                return _rockPrefabs[2];
            default:
                return _rockPrefabs[0];
        }
    }
    public static GameObject GetHabitatPrefab(){
        return _HabitatPrefab;
    }
    public static GameObject GetHe3DepoPrefab(){
        return _He3DepoPrefab;
    }
    private static GameObject _He3DepoPrefab;
    private static GameObject _HabitatPrefab;
    private static GameObject[] _rockPrefabs;
    WorldData data;
    public GameObject he3DepoPrefab;
    public GameObject habitatPrefab;
    public Material highlightMaterial;
    public GameObject[] buildingPrefabs;
    public GameObject[] rockPrefabs;
    GameObject[,] buildingGOs;
    // Start is called before the first frame update
    void Start()
    {
        _rockPrefabs = rockPrefabs;
        _He3DepoPrefab = he3DepoPrefab;
        _HabitatPrefab = habitatPrefab;
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
    public bool PlaceBuilding(int x, int y,Building type){
        return data.PlaceBuilding(x,y,type);
    }
}
