using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionControler : MonoBehaviour
{
    Camera mainCamera;
    //public GameObject testPlaceGO;
    public World world;
    public Action action;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = gameObject.GetComponent<Camera>();
        //action = new PlacementAction(world,Habitat.GetBuilderInstance());
    }
    void SetAction(Action a){
        if(this.action != null){
            this.action.Stop();
        }
        this.action = a;
        a.Start();
    }
    Vector3 hitTarget;
    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Vector3 screenPosition = Input.mousePosition;
        //screenPosition.z = mainCamera.nearClipPlane;
        Ray ray = mainCamera.ScreenPointToRay(screenPosition);  
        //Vector3 rayStart = mainCamera.ScreenToWorldPoint(screenPosition);
        if (Input.GetKeyDown(KeyCode.Alpha1)){
            SetAction(new PlacementAction(world,Habitat.GetBuilderInstance()));
        }
        /*
        if (Input.GetKeyDown(KeyCode.Alpha2)){
            SetAction(new PlacementAction(world,BuildingType.HousingLVL1));
        }*/
        //Debug.Log($"screenPosition:{screenPosition},rayStart:{rayStart}");
        if (Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity))
        {
            Vector3 target = ray.origin + ray.direction * hit.distance;
            int x = (int)target.x;
            int y = (int)target.z;
            target = new Vector3(Mathf.Round(target.x),0.0f,Mathf.Round(target.z)) + new Vector3(0.5f,0.0f,0.5f);
            //Debug.DrawRay(ray.origin,target - ray.origin, Color.yellow);
            hitTarget = target; 
            if(Input.GetKeyDown(KeyCode.Mouse0)){
                if(action != null){
                    action = action.Enact(x,y,world);
                }
            }
            else{
                if(action != null){
                    action.Display(x,y);
                }
            }
            //Debug.Log("Did Hit");
        }
    }
    void OnDrawGizmos(){
        //Debug.Log($"Drawing at {hitTarget}!");
        //Gizmos.DrawCube(hitTarget,Vector3.one);
    }
}
