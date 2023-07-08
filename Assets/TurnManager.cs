using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TurnManager : MonoBehaviour
{
    public World world;
    public Text trunDisplay;
    public Text factionDisplay;
    bool hasRefreshedAfterStart;
    public void NextTurn(){
        world.NextTurn();
        //Debug.Log("Next turn!");
        RefreshUI();
    }
    void RefreshUI(){
        uint currTurn = world.GetCurrentTurn();
        trunDisplay.text = $"Turn {currTurn}";
    }
    void Update(){
        if(!hasRefreshedAfterStart){
            RefreshUI();
            hasRefreshedAfterStart = true;
        }
    }
}
