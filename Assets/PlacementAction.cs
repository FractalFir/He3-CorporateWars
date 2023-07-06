using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementAction : Action
{
    static Dictionary<BuildingType,GameObject> highlightChache = new Dictionary<BuildingType,GameObject>(); 
    GameObject highligthGO;
    BuildingType type;
    public PlacementAction(World w, BuildingType type){
        GameObject chached;
        highlightChache.TryGetValue(type, out chached);
        if(chached == null){
            chached = GameObject.Instantiate(w.GetBuildingPrefab(type));
            MeshRenderer renderer = chached.GetComponent<MeshRenderer>();
            renderer.material = w.highlightMaterial;
            foreach(Collider c in chached.GetComponents<Collider> ()) {
                c.enabled = false;
            }
            renderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
            highlightChache.Add(type,chached);
        }
        chached.SetActive(true);
        highligthGO = chached;
        this.type = type;
    }
    public override void Display(int x, int y){
        highligthGO.transform.position = new Vector3((float)x + 0.5f,0.0f,(float)y + 0.5f);
    }
    public override void Stop(){
        highligthGO.SetActive(false);
    }
    public override Action Enact(int x,int y, World world){
        if(world.PlaceBuilding(x,y,type)){
            Stop();
            return null;
        }
        else{
            return this;
        }
    }
}
