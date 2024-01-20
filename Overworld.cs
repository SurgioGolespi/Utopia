using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
public class Overworld : MonoBehaviour{
    public SpriteRenderer[] Party;
    public SpriteRenderer[] Foreground;
    public SpriteRenderer[] Build;
    public SpriteRenderer[] NPC;
    public Sprite[] PartySprite;
    public Sprite[] ForegroundSprite;
    public Sprite[] BuildSprite;
    public Sprite[] InsideSprite;
    public Menu MenuObj;
    public Camera MainCam;
    public float Horizontal;
    public float Vertical;
    public bool MoveOn;
    public int Area;
    void Update(){
        if(MoveOn){
            TouchScreen();}
        Party[0].transform.Translate(Horizontal * Time.deltaTime * 50, 0, 0);
        Party[1].transform.position = Party[0].transform.position + new Vector3(-2 * Party[0].transform.localScale.x, 0, 0);
        Party[2].transform.position = Party[0].transform.position + new Vector3(-4 * Party[0].transform.localScale.x, 0, 0);
        Party[0].transform.localScale = (Horizontal != 0) ? new Vector3(Horizontal, 1, 1) : new Vector3(Party[0].transform.localScale.x, 1, 1);
        Party[1].transform.localScale = new Vector3(Party[0].transform.localScale.x, 1, 1);
        Party[2].transform.localScale = new Vector3(Party[0].transform.localScale.x, 1, 1);
        MainCam.transform.position = Party[0].transform.position + new Vector3(4, 2.2f, -10);
        foreach(SpriteRenderer i in Foreground){
            i.transform.position = ((Party[0].transform.position.x - i.transform.position.x) * Horizontal >= 40) 
            ? new Vector3(i.transform.position.x + 66 * Horizontal, 2.68f, 0): i.transform.position;
        if(Party[0].transform.position.x > 500){
            AreaPlus();}
        if(Party[0].transform.position.x < 0){
            AreaMinus();}}}
    public void TouchScreen(){
        if(Input.touchCount > 0){
            if(Input.GetTouch(0).phase == TouchPhase.Moved){
                Horizontal = (Mathf.Abs(Input.GetTouch(0).deltaPosition.x) > 5) ? Mathf.Sign(Input.GetTouch(0).deltaPosition.x) : Horizontal;
                Vertical = (Mathf.Abs(Input.GetTouch(0).deltaPosition.y) > 5) ? Mathf.Sign(Input.GetTouch(0).deltaPosition.y) : Vertical;}
            if(Input.GetTouch(0).phase == TouchPhase.Began){
                if(Vector3.Distance(Camera.main.ScreenToWorldPoint(new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, 10)), 
                NPC[0].transform.position) < 1){
                    MenuObj.DialogueStart();}}}
        else{
            Horizontal = 0;
            Vertical = 0;}
        GoInside();}
    public void AreaPlus(){
         if(Area == 1){
            Build[0].sprite = null;
            NPC[0].sprite = null;}
        Area = (Area + 1) % 3;
        Party[0].transform.position = new Vector3(0,0,0);
        foreach(SpriteRenderer i in Foreground){
            i.transform.position += new Vector3(-500,0,0);
            i.sprite = ForegroundSprite[Area];}
        if(Area == 1){
            Build[0].sprite = BuildSprite[0];
            NPC[0].sprite = PartySprite[1];}}
    public void AreaMinus(){
        if(Area == 1){
            Build[0].sprite = null;
            NPC[0].sprite = null;}
        Area = (Area + 2) % 3;
        Party[0].transform.position = new Vector3(500,0,0);
        foreach(SpriteRenderer i in Foreground){
            i.transform.position += new Vector3(500,0,0);
            i.sprite = ForegroundSprite[Area];}
        if(Area == 1){
            Build[0].sprite = BuildSprite[0];
            NPC[0].sprite = PartySprite[1];}}
    public void GoInside(){
        if(Area == 1 && Vector3.Distance(Party[0].transform.position, Build[0].transform.position) <= 2.5f){
            if(Vertical == 1){
                Build[0].sprite = null;
                foreach(SpriteRenderer i in Foreground){
                    i.sprite = InsideSprite[0];}}
            else if(Vertical == -1){
                Build[0].sprite = BuildSprite[0];
                foreach(SpriteRenderer i in Foreground){
                    i.sprite = ForegroundSprite[Area];}}}}}