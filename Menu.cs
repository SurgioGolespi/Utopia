using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Menu : MonoBehaviour{
    public Overworld OverworldObj;
    public Transform MenuScreen;
    public TextMeshProUGUI AppText;
    public Transform[] MenuButtons;
    public int Prisms;
    public void MenuOn(){
        if(MenuScreen.gameObject.activeSelf){
            OverworldObj.MoveOn = true;
            MenuScreen.gameObject.SetActive(false);}
        else{
            AppText.text = "";
            foreach(Transform i in MenuButtons){
                i.gameObject.SetActive(true);}
            MenuScreen.gameObject.SetActive(true);
            OverworldObj.MoveOn = false;}}
    public void Party(){
        foreach(Transform i in MenuButtons){
            i.gameObject.SetActive(false);}
        foreach(SpriteRenderer i in OverworldObj.Party){
                AppText.text += i.sprite.name + " ";}}
     public void Pull(){
        foreach(Transform i in MenuButtons){
            i.gameObject.SetActive(false);}
        if(Prisms >= 1000){
            Prisms -= 1000;
            AppText.text = "Pulled";}
        else{
            AppText.text = "Not Enough Prisms";}}
    public void Path(){
        foreach(Transform i in MenuButtons){
            i.gameObject.SetActive(false);}
        AppText.text = "Reach the Black Tower";}}