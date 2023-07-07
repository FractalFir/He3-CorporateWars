using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Habitat : Building
{
    const float HE3_ROT_SEED = 914.24433553265f;
    [System.NonSerialized()] 
    GameObject HabitatGO;
    int x;
    int y;
    float height;
    //Value in range 0-3
    int rotation;
    public static Habitat PlaceAt(int x, int y,WorldData data){
        int rotation = data.PlacementNoise(x,y,3,HE3_ROT_SEED);
        Habitat habitat = new Habitat(x,y,rotation);
        habitat.height = data.GetHeightAt((float)x + 0.5f,(float)y + 0.5f);
        habitat.SetupVisuals();
        return habitat;
    }
    public override Building PlaceNewAt(int x, int y,WorldData data){
        return PlaceAt(x,y,data);
    }
    private static Habitat builderInstance;
    public static Habitat GetBuilderInstance(){
        if(builderInstance == null){
            builderInstance = new Habitat();
        }
        return builderInstance;
    }
    private Habitat(){}
    private Habitat(int x, int y,int rotation){
        this.rotation = rotation;
        this.x = x;
        this.y = y;
    }
    public override GameObject GetPrefab(){
        return World.GetHabitatPrefab();
    }
    public override void RefreshVisuals(){
        if(HabitatGO == null){
            SetupVisuals();
        }
    }
    void SetupVisuals(){
        GameObject prefab = World.GetHabitatPrefab();
        GameObject go = GameObject.Instantiate(prefab,new Vector3((float)x + 0.5f,height,(float)y + 0.5f),Quaternion.AngleAxis(rotation*90, Vector3.up));
    }
    public override void Tick(){
        //Visuals invliad, reset them!
        if(HabitatGO == null){
            SetupVisuals();
        }
    }
}
