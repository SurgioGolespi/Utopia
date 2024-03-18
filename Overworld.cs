using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Overworld : MonoBehaviour{
    [Header("Script Objects")]
    public Menu MenuObj;
    public Battle BattleObj;
    [Header("Game Objects")]
    public SpriteRenderer[] Party;
    public SpriteRenderer[] Foreground;
    public SpriteRenderer[] Build;
    public SpriteRenderer[] NPC;
    public SpriteRenderer[] Enemy;
    public Sprite[] PartySprite;
    public Sprite[] EnemySprite;
    public Sprite[] ForegroundSprite;
    public Sprite[] BuildSprite;
    public Sprite[] NPCSprite;
    public Sprite[] InsideSprite;
    public Sprite ExitIcon;
    public Camera MainCam;
    public GameObject[] AreaSprite;
    [Header("Move Variables")]
    public float Horizontal;
    public float Vertical;
    public bool MoveOn;
    public bool InsideOn;
    public int Area;
    public Vector3 Position;
    public float Steps;
    void Start(){
        Position = Party[0].transform.position;
        for(int i = 0; i < 3; i ++){
            Party[i].sprite = PartySprite[MenuObj.PartyOrder[i]];}}
    void Update(){
        TouchScreen();
        CountSteps();
        Party[0].transform.Translate(Horizontal * Time.deltaTime * 10, 0, 0);
        if(InsideOn){
            Party[0].transform.position = new Vector3(Mathf.Clamp(Party[0].transform.position.x, 0, 500), 0, 0);}
        Party[1].transform.position = Party[0].transform.position + new Vector3(-3 * Party[0].transform.localScale.x, 0, 0);
        Party[2].transform.position = Party[0].transform.position + new Vector3(-6 * Party[0].transform.localScale.x, 0, 0);
        Party[1].transform.localScale = new Vector3(Party[0].transform.localScale.x, 1, 1);
        Party[2].transform.localScale = new Vector3(Party[0].transform.localScale.x, 1, 1);
        MainCam.transform.position = Party[0].transform.position + new Vector3(3, 2.2f, -10);
        foreach(SpriteRenderer i in Foreground){
            i.transform.position = ((Party[0].transform.position.x - i.transform.position.x) * Horizontal >= 44) 
            ? new Vector3(i.transform.position.x + 69 * Horizontal, 0, 0): i.transform.position;
        if(Party[0].transform.position.x > 500){
            AreaPlus();}
        if(Party[0].transform.position.x < 0){
            AreaMinus();}}}
    public void TouchScreen(){
        if(Input.touchCount > 0 && MoveOn){
            if(Input.GetTouch(0).phase == TouchPhase.Moved){
                Horizontal = (Mathf.Abs(Input.GetTouch(0).deltaPosition.x) > 7.5f) ? Mathf.Sign(Input.GetTouch(0).deltaPosition.x) : Horizontal;
                Vertical = (Mathf.Abs(Input.GetTouch(0).deltaPosition.y) > 7.5f) ? Mathf.Sign(Input.GetTouch(0).deltaPosition.y) : 0;}
            Party[0].transform.localScale = (Horizontal != 0) ? new Vector3(Horizontal, 1, 1) : new Vector3(Party[0].transform.localScale.x, 1, 1);
            if(Input.GetTouch(0).phase == TouchPhase.Began && NPC[0].sprite != null){
                if(Vector3.Distance(Camera.main.ScreenToWorldPoint(new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, 10)), 
                NPC[0].transform.position) < 1){
                    MenuObj.DialogueRun();}}}
        else{
            Horizontal = 0;
            Vertical = 0;}
        if(Area == 1 && Mathf.Abs(Party[0].transform.position.x - Build[0].transform.position.x) <= 2){
            GoInside();}}
    public void AreaPlus(){
         if(Area == 1){
            AreaSprite[0].SetActive(false);}
        Area = (Area + 1) % 3;
        Party[0].transform.position = new Vector3(0,0,0);
        foreach(SpriteRenderer i in Foreground){
            i.transform.position += new Vector3(-500,0,0);
            i.sprite = ForegroundSprite[Area];}
        if(Area == 1){
            AreaSprite[0].SetActive(true);
            Build[0].sprite = BuildSprite[0];
            NPC[0].sprite = NPCSprite[0];}}
    public void AreaMinus(){
        if(Area == 1){
            AreaSprite[0].SetActive(false);}
        Area = (Area + 2) % 3;
        Party[0].transform.position = new Vector3(500,0,0);
        foreach(SpriteRenderer i in Foreground){
            i.transform.position += new Vector3(500,0,0);
            i.sprite = ForegroundSprite[Area];}
        if(Area == 1){
            AreaSprite[0].SetActive(true);
            Build[0].sprite = BuildSprite[0];
            NPC[0].sprite = NPCSprite[0];}}
    public void GoInside(){
        if(Vertical == 1){
            InsideOn = true;
            NPC[0].sprite = null;
            Build[0].sprite = ExitIcon;
            Build[0].transform.position = new Vector3(Build[0].transform.position.x, 2, 0);
            foreach(SpriteRenderer i in Foreground){
                i.sprite = InsideSprite[0];}}
        else if(Vertical == -1){
            InsideOn = false;
            NPC[0].sprite = PartySprite[1];
            Build[0].sprite = BuildSprite[0];
            Build[0].transform.position = new Vector3(Build[0].transform.position.x, 0, 0);
           foreach(SpriteRenderer i in Foreground){
                i.sprite = ForegroundSprite[Area];}}}
    public void CountSteps(){
        if(Area != 1){
            Steps += Vector3.Distance(Position, Party[0].transform.position);
            Position = Party[0].transform.position;}
        if((int)Steps % 250 == 0){
            Steps++;
            MoveOn = false;
            Party[0].transform.localScale = new Vector3(1,1,1);
            for(int i = 0; i < 3; i++){
                Enemy[i].gameObject.SetActive(true);
                Enemy[i].transform.position = Party[0].transform.position + new Vector3(6 + 3 * i, 0, 0);
                Enemy[i].sprite = EnemySprite[BattleObj.EnemyOrder[i]];}
            BattleObj.BattleOn();}}}