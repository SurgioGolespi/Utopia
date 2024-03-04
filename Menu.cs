using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using System;
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
    [Header("Pull Variables")]
    public int Prisms;
    public string PullResult;
    public int PullIndex;
    public int PullAmount;
    public bool PullOn;
    public Sprite[] BannerSprite;
    [Header("Dialogue Variables")]
    public string[] Dialogue;
    public int DialogueIndex;
    [Header("Party Variables")]
    public bool StatsOn;
    public bool OrderOn;
    public int[] PartyOrder;
    public int[] NewPartyOrder;
    public int OrderIndex;
    public int[] PartyOwned;
    public bool InParty;
    public void Login(){
        LoginScreen.gameObject.SetActive(false);
        OverworldObj.MoveOn = true;}
    public void MenuOn(){
        OverworldObj.MoveOn = !OverworldObj.MoveOn;
        if(MenuScreen.transform.gameObject.activeSelf){
                MenuButtonText.text = "Menu";
                MenuScreen.gameObject.SetActive(false);
                PartyScroll.gameObject.SetActive(false);
                PullScroll.gameObject.SetActive(false);
                AppText.text = "";
                OrderIndex = 0;
                for(int i = 0; i < 3; i++){
                    NewPartyOrder[i] = -1;}
                StatsOn = false;
                OrderOn = false;
                PullOn = false;}
        else{
            MenuButtonText.text = "X";
            foreach(Transform i in App){
                i.gameObject.SetActive(true);}
            MenuScreen.transform.gameObject.SetActive(true);}}
    public void Party(){
        PartyScroll.gameObject.SetActive(true);
        foreach(Image i in PartyScrollContent){
            i.gameObject.SetActive(false);}
        for(int i = 0; i < PartyOwned.Length; i++){
            PartyScrollContent[PartyOwned[i]].gameObject.SetActive(true);
            PartyScrollContent[PartyOwned[i]].name = OverworldObj.PartySprite[PartyOwned[i]].name;
            PartyScrollContent[PartyOwned[i]].sprite = OverworldObj.PartySprite[PartyOwned[i]];}
        foreach(Transform i in App){
            i.gameObject.SetActive(false);}}
     public void Pull(){
        foreach(Transform i in App){
            i.gameObject.SetActive(false);}
        PullScroll.gameObject.SetActive(true);
        foreach(Image i in PullScrollContent){
            i.gameObject.SetActive(false);}
        for(int i = 0; i < BannerSprite.Length; i++){
            PullScrollContent[i].gameObject.SetActive(true);
            PullScrollContent[i].sprite = BannerSprite[i];}}
    public void PathRun(){
        foreach(Transform i in App){
            i.gameObject.SetActive(false);}
        AppText.text = "Reach the Black Tower";}
    IEnumerator PullRun(){
        if(PullOn){
            PullScroll.gameObject.SetActive(false);
            MenuButton.gameObject.SetActive(false);
            BackButton.gameObject.SetActive(false);
            if(Prisms - PullAmount * 100 >= 0){
                Prisms -= PullAmount * 100;
                PullImage.gameObject.SetActive(true);
                MenuScreen.color = Color.black;
                for(int i = 0; i < PullAmount; i++){
                    PullIndex = UnityEngine.Random.Range(0,4);
                    AddParty(PullIndex);
                    PullResult += OverworldObj.PartySprite[PullIndex].name + " ";
                    PullImage.sprite = OverworldObj.PartySprite[PullIndex];
                    yield return new WaitForSeconds(0.5f);
                    yield return new WaitUntil(() => Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began);}
                MenuScreen.color = Color.white;
                PullImage.sprite = null;
                PullImage.gameObject.SetActive(false);
                AppText.text = PullResult;
                PullResult = "";}
            else{
                AppText.text = "Not Enough Prisms";}
            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began);
            AppText.text = "";
            PullOn = false;
            BackButton.gameObject.SetActive(true);
            MenuButton.gameObject.SetActive(true);
            PullScroll.gameObject.SetActive(true);}}
    public void DialogueRun(){
        if(DialogueText.text == ""){
            DialogueIndex = 0;
            DialogueBox.gameObject.SetActive(true);
            OverworldObj.MoveOn = false;
            Dialogue = File.ReadAllLines(Path.Combine(Application.streamingAssetsPath, "Dialogue"));
            DialogueText.text = Dialogue[DialogueIndex];}
        else{
            DialogueIndex++;
            if(DialogueIndex < Dialogue.Length){
                DialogueText.text = Dialogue[DialogueIndex];}
            else{
                DialogueText.text = "";
                DialogueBox.gameObject.SetActive(false);
                OverworldObj.MoveOn = true;}}}
    public void MenuBack(){
        AppText.text = "";
        if(StatsOn || OrderOn){
            OrderIndex = 0;
            PartyScroll.gameObject.SetActive(true);
            StatsOn = false;
            OrderOn = false;
            for(int i = 0; i < 3; i++){
                NewPartyOrder[i] = -1;}}
        else if(PullOn){
            PullOn = false;}
        else if(!App[0].gameObject.activeSelf){
            PartyScroll.gameObject.SetActive(false);
            PullScroll.gameObject.SetActive(false);
            foreach(Transform i in App){
                i.gameObject.SetActive(true);}}
        else{
            MenuOn();}}
    public void PartyRun(int PSCIndex){
        if(StatsOn){
            PartyScroll.gameObject.SetActive(false);
            AppText.text = OverworldObj.PartySprite[PartyOwned[PSCIndex]].name + "\n" +
            "LV: " + BattleObj.PartyStats[PartyOwned[PSCIndex]].LV + "\n" +
            "EP Needed until next LV: " + BattleObj.PartyStats[PartyOwned[PSCIndex]].EP + "/" +  BattleObj.PartyStats[PartyOwned[PSCIndex]].EN + "\n" +
            "HP: " + BattleObj.PartyStats[PartyOwned[PSCIndex]].HP + "\n" +
            "AP: " + BattleObj.PartyStats[PartyOwned[PSCIndex]].AP;}
        if(OrderOn && NewPartyOrder[0] != PSCIndex && NewPartyOrder[1] != PSCIndex){
            NewPartyOrder[OrderIndex] = PartyOwned[PSCIndex];
            OrderIndex++;
            if(OrderIndex == 3){
                for(int i = 0; i < 3; i++){
                    PartyOrder[i] = NewPartyOrder[i];
                    OverworldObj.Party[i].sprite = OverworldObj.PartySprite[NewPartyOrder[i]];
                    OverworldObj.Party[i].name = OverworldObj.PartySprite[NewPartyOrder[i]].name;}
                OrderIndex = 0;
                MenuBack();}}}
    public void StatsRun(){
        if(!OrderOn && !StatsOn){
            StatsOn = true;}}
    public void OrderRun(){
        if(!StatsOn && !OrderOn){
            OrderOn = true;}}
    public void SetPull(int PAIndex){
            PullAmount = PAIndex;
            PullOn = true;}
    public void StartPull(){
        StartCoroutine(PullRun());}
    public void AddParty(int APIndex){
        foreach(int i in PartyOwned){
            if(i == APIndex){
                InParty = true;}}
        if(!InParty){
            Array.Resize(ref PartyOwned, PartyOwned.Length + 1);
            PartyOwned[PartyOwned.Length - 1] = APIndex;}
        InParty = false;}};