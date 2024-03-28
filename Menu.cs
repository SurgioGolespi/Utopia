using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using System;
using System.Linq;
public class Menu : MonoBehaviour{
    [Header("Script Objects")]
    public Overworld OverworldObj;
    public Battle BattleObj;
    [Header("Game Objects")]
    public Transform LoginScreen;
    public TextMeshProUGUI AppText;
    public TextMeshProUGUI MenuButtonText;
    public Transform[] App;
    public Transform DialogueBox;
    public TextMeshProUGUI DialogueText;
    public Image PullImage;
    public Image MenuScreen;
    public Transform MenuButton;
    public Transform BackButton;
    public Transform PartyScroll;
    public Image[] PartyScrollContent;
    public Transform PullScroll;
    public Image[] PullScrollContent;
    public TextMeshProUGUI AppleCountText;
    [Header("Pull Variables")]
    public int Apples;
    public string PullResult;
    public int PullIndex;
    public int PullAmount;
    public bool PullOn;
    public Sprite[] BannerSprite;
    [Header("Dialogue Variables")]
    public string[] Dialogue;
    [Header("Party Variables")]
    public bool StatsOn;
    public bool OrderOn;
    public int[] PartyOrder;
    public int[] NewPartyOrder;
    public int OrderIndex;
    public int[] PartyOwned;
    [Header("Portal Variables")]
    public GameObject Map;
    public void Login(){
        LoginScreen.gameObject.SetActive(false);
        OverworldObj.MoveOn = true;}
    public void MenuOn(){
        OverworldObj.MoveOn = !OverworldObj.MoveOn;
        if(MenuScreen.transform.gameObject.activeSelf){
                MenuButtonText.text = "Menu";
                MenuScreen.gameObject.SetActive(false);
                ResetMenu();}
        else{
            MenuButtonText.text = "X";
            foreach(Transform i in App){
                i.gameObject.SetActive(true);}
            MenuScreen.transform.gameObject.SetActive(true);}}
    public void Party(){
        PartyScroll.gameObject.SetActive(true);
        for(int i = 0; i < PartyOwned.Length; i++){
            PartyScrollContent[PartyOwned[i]].gameObject.SetActive(true);
            PartyScrollContent[PartyOwned[i]].sprite = OverworldObj.PartySprite[PartyOwned[i]];}
        foreach(Transform i in App){
            i.gameObject.SetActive(false);}}
    public void Pull(){
        foreach(Transform i in App){
            i.gameObject.SetActive(false);}
        PullScroll.gameObject.SetActive(true);
        AppleCountText.text = "Apples: " + Apples;
        for(int i = 0; i < BannerSprite.Length; i++){
            PullScrollContent[i].gameObject.SetActive(true);
            PullScrollContent[i].sprite = BannerSprite[i];}}
    public void Story(){
        foreach(Transform i in App){
            i.gameObject.SetActive(false);}
        AppText.text = "Reach the Black Tower";}
    public void Portal(){
        foreach(Transform i in App){
            i.gameObject.SetActive(false);}
        Map.SetActive(true);}
    public void Prism(){}
    public void Prizes(){}
    public IEnumerator PullRun(){
        if(PullOn){
            PullScroll.gameObject.SetActive(false);
            MenuButton.gameObject.SetActive(false);
            BackButton.gameObject.SetActive(false);
            if(Apples - PullAmount * 100 >= 0){
                Apples -= PullAmount * 100;
                PullImage.gameObject.SetActive(true);
                MenuScreen.color = Color.black;
                for(int i = 0; i < PullAmount; i++){
                    PullIndex = UnityEngine.Random.Range(0, OverworldObj.PartySprite.Length);
                    AddParty(PullIndex);
                    PullResult += BattleObj.PartyStats[PullIndex].ID + " ";
                    PullImage.sprite = OverworldObj.PartySprite[PullIndex];
                    yield return new WaitForSeconds(0.5f);
                    yield return new WaitUntil(() => Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began);}
                MenuScreen.color = Color.white;
                PullImage.sprite = null;
                PullImage.gameObject.SetActive(false);
                AppText.text = PullResult;
                PullResult = "";}
            else{
                AppText.text = "Not Enough Apples";}
            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began);
            AppText.text = "";
            PullOn = false;
            BackButton.gameObject.SetActive(true);
            MenuButton.gameObject.SetActive(true);
            Pull();}}
    public IEnumerator DialogueRun(){
        DialogueBox.gameObject.SetActive(true);
        OverworldObj.MoveOn = false;
        Dialogue = File.ReadAllLines(Path.Combine(Application.streamingAssetsPath, "Dialogue"));
        for(int i = 0; i < Dialogue.Length; i++){
            DialogueText.text = Dialogue[i];
            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began);}
        DialogueText.text = "";
        DialogueBox.gameObject.SetActive(false);
        OverworldObj.MoveOn = true;}
    public void MenuBack(){
        if(StatsOn){
            ResetMenu();
            Party();}
        else if(!App[0].gameObject.activeSelf){
            ResetMenu();
            foreach(Transform i in App){
                i.gameObject.SetActive(true);}}
        else{
            MenuOn();}}
    public void PartyRun(int PSCIndex){
        if(StatsOn){
            PartyScroll.gameObject.SetActive(false);
            AppText.text = string.Format("{0}\nLV: {1}\nEP Needed until next LV: {2}/{3}\nHP: {4}\nAP: {5}",
            BattleObj.PartyStats[PartyOwned[PSCIndex]].ID, BattleObj.PartyStats[PartyOwned[PSCIndex]].LV, BattleObj.PartyStats[PartyOwned[PSCIndex]].EP, 
            BattleObj.PartyStats[PartyOwned[PSCIndex]].EN, BattleObj.PartyStats[PartyOwned[PSCIndex]].HP, BattleObj.PartyStats[PartyOwned[PSCIndex]].AP);}
        if(OrderOn && !NewPartyOrder.Any(n => n == PSCIndex)){
            NewPartyOrder[OrderIndex] = PartyOwned[PSCIndex];
            OrderIndex++;
            if(OrderIndex == 3){
                PartyOrder = NewPartyOrder;
                for(int i = 0; i < 3; i++){
                    OverworldObj.Party[i].sprite = OverworldObj.PartySprite[NewPartyOrder[i]];}
                OrderIndex = 0;
                OrderOn = false;}}}
    public void SetParty(int PMIndex){
        if(!OrderOn && PMIndex == 0){
            StatsOn = !StatsOn;}
        if(!StatsOn && PMIndex == 1){
            OrderOn = !OrderOn;}}
    public void SetPull(int PAIndex){
            PullAmount = PAIndex;
            PullOn = !PullOn;}
    public void StartPull(){
        StartCoroutine(PullRun());}
    public void AddParty(int APIndex){
        if(!PartyOwned.Any(n => n == APIndex)){
            Array.Resize(ref PartyOwned, PartyOwned.Length + 1);
            PartyOwned[PartyOwned.Length - 1] = APIndex;}
        else{
            Apples += 10;}}
    public void PortalRun(int NewArea){
        OverworldObj.Party[0].transform.position =  new Vector3(0,0,0);
        if(OverworldObj.Area == (NewArea + 2) % 3){
            OverworldObj.AreaChange(1);}
        else if(OverworldObj.Area == (NewArea + 1) % 3){
            OverworldObj.AreaChange(-1);}
        MenuOn();}
    public void ResetMenu(){
        OrderIndex = 0;
        for(int i = 0; i < 3; i++){
            NewPartyOrder[i] = -1;}
        StatsOn = false;
        OrderOn = false;
        PullOn = false;
        AppText.text = "";
        Map.SetActive(false);
        PartyScroll.gameObject.SetActive(false);
        PullScroll.gameObject.SetActive(false);}};