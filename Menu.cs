using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Menu : MonoBehaviour{
    public Overworld OverworldObj;
    public Transform MenuScreen;
    public void MenuOn(){
        if(MenuScreen.gameObject.activeSelf){
            OverworldObj.MoveOn = true;
            MenuScreen.gameObject.SetActive(false);}
        else{
            MenuScreen.gameObject.SetActive(true);
            OverworldObj.MoveOn = false;}}}
