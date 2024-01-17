using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
public class Overworld : MonoBehaviour{
    public SpriteRenderer[] Party;
    public SpriteRenderer[] Foreground;
    public Sprite[] FGSprite;
    public Camera MainCam;
    public float Horizontal;
    public float Vertical;
    public bool MoveOn;
    void Update(){
        if(MoveOn){
            TouchScreen();}
        Party[0].transform.Translate(Horizontal * Time.deltaTime * 15, 0, 0);
        Party[1].transform.position = Party[0].transform.position + new Vector3(-2 * Party[0].transform.localScale.x, 0, 0);
        Party[2].transform.position = Party[0].transform.position + new Vector3(-4 * Party[0].transform.localScale.x, 0, 0);
        Party[0].transform.localScale = (Horizontal != 0) ? new Vector3(Horizontal, 1, 1) : new Vector3(Party[0].transform.localScale.x, 1, 1);
        Party[1].transform.localScale = new Vector3(Party[0].transform.localScale.x, 1, 1);
        Party[2].transform.localScale = new Vector3(Party[0].transform.localScale.x, 1, 1);
        MainCam.transform.position = Party[0].transform.position + new Vector3(4, 2.2f, -10);
        foreach(SpriteRenderer i in Foreground){
            i.transform.position = ((Party[0].transform.position.x - i.transform.position.x) * Horizontal >= 40) 
            ? new Vector3(i.transform.position.x + 66 * Horizontal, 0, 0): i.transform.position;
        if(Party[0].transform.position.x > 500){
            AreaPlus();}
        if(Party[0].transform.position.x < 0){
            AreaMinus();}}}
    public void TouchScreen(){
        if(Input.touchCount > 0){
            if(Input.GetTouch(0).phase == TouchPhase.Moved){
                Horizontal = (Mathf.Abs(Input.GetTouch(0).deltaPosition.x) > 5) ? Mathf.Sign(Input.GetTouch(0).deltaPosition.x) : Horizontal;
                Vertical = (Mathf.Abs(Input.GetTouch(0).deltaPosition.y) > 5) ? Mathf.Sign(Input.GetTouch(0).deltaPosition.y) : Vertical;}}
        else{
            Horizontal = 0;
            Vertical = 0;}}
    public void AreaPlus(){
        foreach(SpriteRenderer i in Foreground){
            Party[0].transform.position = new Vector3(0,0,0);
            i.transform.position += new Vector3(-500,0,0);
            i.sprite = FGSprite[(Array.IndexOf(FGSprite, i.sprite) + 1) % 3];}}
    public void AreaMinus(){
        foreach(SpriteRenderer i in Foreground){
            Party[0].transform.position = new Vector3(500,0,0);
            i.transform.position += new Vector3(500,0,0);
            i.sprite = FGSprite[(Array.IndexOf(FGSprite, i.sprite) + 2) % 3];}}}