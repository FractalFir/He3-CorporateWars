using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Habitat : Building
{
    const float HAB_ROT_SEED = 114.24433553265f;
    //Value in range 0-3
    public static Habitat PlaceAt(int x, int y,WorldData data){
        int rotation = data.PlacementNoise(x,y,3,HAB_ROT_SEED);
        Habitat habitat = new Habitat(x,y,(byte)rotation);
        habitat.height = data.GetHeightAt((float)x + 0.5f,(float)y + 0.5f);
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
    private Habitat(int x, int y,byte rotation){
        this.rotation = rotation;
        this.x = x;
        this.y = y;
    }
    public override GameObject GetPrefab(){
        return World.GetHabitatPrefab();
    }
    public override void Tick(WorldData data){
        //If Visuals invliad, reset them!
        RefreshVisuals();
        
    }
}
