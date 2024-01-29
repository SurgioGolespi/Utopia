using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
public class Menu : MonoBehaviour{
    [Header("GameObjects")]
    public Overworld OverworldObj;
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
    [Header("Pull Variables")]
    public int Prisms;
    public string PullResults;
    public int PullIndex;
    [Header("Dialogue Variables")]
    public string[] Dialogue;
    public int DialogueIndex;
    [Header("Party Variables")]
    public bool StatsOn;
    public bool OrderOn;
    public int[] NewPartyOrder;
    public int OrderIndex;
    public void Login(){
        LoginScreen.gameObject.SetActive(false);
        OverworldObj.MoveOn = true;}
    public void MenuOn(){
        if(MenuScreen.transform.gameObject.activeSelf){
                MenuButtonText.text = "Menu";
                OverworldObj.MoveOn = true;
                MenuScreen.gameObject.SetActive(false);
                PartyScroll.gameObject.SetActive(false);
                if(StatsOn || OrderOn){
                    AppText.text = "";
                    OrderIndex = 0;
                     for(int i = 0; i < 3; i++){
                        NewPartyOrder[i] = -1;}
                    PartyScroll.gameObject.SetActive(false);
                    StatsOn = false;
                    OrderOn = false;}}
        else{
            MenuButtonText.text = "X";
            AppText.text = "";
            foreach(Transform i in App){
                i.gameObject.SetActive(true);}
            MenuScreen.transform.gameObject.SetActive(true);
            OverworldObj.MoveOn = false;}}
    public void Party(){
        PartyScroll.gameObject.SetActive(true);
        for(int i = 0; i < PartyScrollContent.Length; i++){
            PartyScrollContent[i].name = OverworldObj.PartySprite[i].name;
            PartyScrollContent[i].sprite = OverworldObj.PartySprite[i];}
        foreach(Transform i in App){
            i.gameObject.SetActive(false);}}
     public void Pull(){
        foreach(Transform i in App){
            i.gameObject.SetActive(false);}
        if(Prisms >= 1000){
            MenuButton.gameObject.SetActive(false);
            BackButton.gameObject.SetActive(false);
            PullImage.gameObject.SetActive(true);
            Prisms -= 1000;
            MenuScreen.color = Color.black;
            StartCoroutine(PullEffect());}
        else{
            AppText.text = "Not Enough Prisms";}}
    public void PathRun(){
        foreach(Transform i in App){
            i.gameObject.SetActive(false);}
        AppText.text = "Reach the Black Tower";}
    IEnumerator PullEffect(){
        for(int i = 0; i < 10; i++){
            PullIndex = Random.Range(0,3);
            PullResults += OverworldObj.Party[PullIndex].name + " ";
            PullImage.sprite = OverworldObj.Party[PullIndex].sprite;
            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began);}
        AppText.text = PullResults;
        PullImage.sprite = null;
        PullResults = "";
        MenuScreen.color = Color.white;
        PullImage.gameObject.SetActive(false);
        BackButton.gameObject.SetActive(true);
        MenuButton.gameObject.SetActive(true);}
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
        if(StatsOn || OrderOn){
            AppText.text = "";
            OrderIndex = 0;
            PartyScroll.gameObject.SetActive(true);
            StatsOn = false;
            OrderOn = false;
            for(int i = 0; i < 3; i++){
                NewPartyOrder[i] = -1;}}
        else if(!App[0].gameObject.activeSelf){
            PartyScroll.gameObject.SetActive(false);
            AppText.text = "";
            foreach(Transform i in App){
                i.gameObject.SetActive(true);}}
        else{
            PartyScroll.gameObject.SetActive(false);
            MenuOn();}}
    public void TestFunc(int PSCIndex){
        if(StatsOn){
            PartyScroll.gameObject.SetActive(false);
            AppText.text = OverworldObj.PartySprite[PSCIndex].name;}
        if(OrderOn && NewPartyOrder[0] != PSCIndex && NewPartyOrder[1] != PSCIndex){
            NewPartyOrder[OrderIndex] = PSCIndex;
            OrderIndex++;
            if(OrderIndex == 3){
                for(int i = 0; i < 3; i++){
                    OverworldObj.Party[i].sprite = OverworldObj.PartySprite[NewPartyOrder[i]];
                    OverworldObj.Party[i].name = OverworldObj.PartySprite[NewPartyOrder[i]].name;}
                OrderIndex = 0;
                MenuBack();}}}
    public void StatsRun(){
        if(!OrderOn){
            StatsOn = true;}}
     public void OrderRun(){
        if(!StatsOn){
            OrderOn = true;}}};