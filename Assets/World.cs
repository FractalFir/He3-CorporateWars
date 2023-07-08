using System.Collections;
using System.Collections.Generic;
using UnityEngine;
///Should be only one instance at a time!
public class World : MonoBehaviour
{
    public static GameObject GetRockPrefab(RockType type){
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
    public static GameObject GetHabitatPrefab() => _HabitatPrefab;
    public static GameObject GetHe3DepoPrefab() => _He3DepoPrefab;
    public static GameObject GetConstructionSitePrefab() => _ConstructionSitePrefab;
    public float GetHeightAt(float x, float y){
        return worldData.GetHeightAt(x,y);
    }
    private static GameObject _He3DepoPrefab;
    private static GameObject _ConstructionSitePrefab;
    private static GameObject _HabitatPrefab;
    private static GameObject[] _rockPrefabs;
    WorldData worldData;
    public GameObject he3DepoPrefab;
    public GameObject constructionSitePrefab;
    public GameObject habitatPrefab;
    public GameObject worldBorder;
    public Material highlightMaterial;
    public GameObject[] buildingPrefabs;
    public GameObject[] rockPrefabs;
    GameObject[,] buildingGOs;
    uint turn;
    // Start is called before the first frame update
    void Start()
    {
        _rockPrefabs = rockPrefabs;
        _He3DepoPrefab = he3DepoPrefab;
        _HabitatPrefab = habitatPrefab;
        _ConstructionSitePrefab = constructionSitePrefab;
        worldData = new WorldData(128,0xFF4F23F);
        MeshFilter renderer = gameObject.GetComponent<MeshFilter>();
        renderer.mesh = worldData.GenerateMesh();
        MeshCollider collider = gameObject.GetComponent<MeshCollider>();
        collider.sharedMesh = renderer.mesh;
        buildingGOs = new GameObject[worldData.sideSize,worldData.sideSize];
        float halfOfWorldSize = (float)worldData.sideSize / 2.0f;
        worldBorder.transform.position = new Vector3(halfOfWorldSize,0.5f,halfOfWorldSize);
        worldBorder.transform.localScale = new Vector3(halfOfWorldSize,1.0f,halfOfWorldSize);
    }
    public uint GetCurrentTurn() => worldData.turn;
    public void NextTurn()=>worldData.NextTurn();
    // Update is called once per frame
    void Update()
    {
        
        
    }
    public bool PlaceBuilding(int x, int y,Building type){
        return worldData.PlaceBuilding(x,y,type);
    }
}
