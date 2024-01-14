using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Overworld : MonoBehaviour{
    public Transform[] Party;
    public Transform[] Foreground;
    public Camera MainCam;
    public float Horizontal;
    void Update(){
        TouchScreen();
        Party[0].transform.Translate(Horizontal * Time.deltaTime * 10, 0, 0);
        Party[0].localScale = (Horizontal != 0) ? new Vector3(Horizontal, 1, 1) : new Vector3(Party[0].localScale.x, 1, 1);
        MainCam.transform.position = Party[0].position + new Vector3(4, 2.2f, -10);
        foreach(Transform i in Foreground){
            i.position = ((Party[0].position.x - i.position.x) * Horizontal >= 40) ? new Vector3(i.position.x + 66 * Horizontal, 0, 0): i.position;}}
    public void TouchScreen(){
        if(Input.touchCount > 0){
            if(Input.GetTouch(0).phase == TouchPhase.Moved){
                Horizontal = (Mathf.Abs(Input.GetTouch(0).deltaPosition.x) > 5) ? Mathf.Sign(Input.GetTouch(0).deltaPosition.x) : Horizontal;}}
        else{
            Horizontal = 0;}}}
