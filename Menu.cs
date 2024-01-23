using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
public class Menu : MonoBehaviour{
    public Overworld OverworldObj;
    public Transform MenuScreen;
    public Transform LoginScreen;
    public TextMeshProUGUI AppText;
    public TextMeshProUGUI MenuButtonText;
    public Transform[] App;
    public Transform DialogueBox;
    public TextMeshProUGUI DialogueText;
    public Image PullImage;
    public int Prisms;
    public string PullResults;
    public int PullIndex;
    public string[] Dialogue;
    public int DialogueIndex;
    public Transform MenuButton;
    public void Login(){
        LoginScreen.gameObject.SetActive(false);
        OverworldObj.MoveOn = true;}
    public void MenuOn(){
        if(MenuScreen.gameObject.activeSelf){
                MenuButtonText.text = "Menu";
                OverworldObj.MoveOn = true;
                MenuScreen.gameObject.SetActive(false);}
        else{
            MenuButtonText.text = "X";
            AppText.text = "";
            foreach(Transform i in App){
                i.gameObject.SetActive(true);}
            MenuScreen.gameObject.SetActive(true);
            OverworldObj.MoveOn = false;}}
    public void Party(){
        foreach(Transform i in App){
            i.gameObject.SetActive(false);}
        foreach(SpriteRenderer i in OverworldObj.Party){
                AppText.text += i.sprite.name + " ";}}
     public void Pull(){
        foreach(Transform i in App){
            i.gameObject.SetActive(false);}
        if(Prisms >= 1000){
            MenuButton.gameObject.SetActive(false);
            Prisms -= 1000;
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
            yield return new WaitForSeconds(1);}
        AppText.text = PullResults;
        PullImage.sprite = null;
        PullResults = "";
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
                OverworldObj.MoveOn = true;}}}}