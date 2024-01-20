using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Menu : MonoBehaviour{
    public Overworld OverworldObj;
    public Transform MenuScreen;
    public Transform LoginScreen;
    public TextMeshProUGUI AppText;
    public Transform[] MenuButtons;
    public Transform DialogueBox;
    public TextMeshProUGUI DialogueText;
    public Image PullImage;
    public int Prisms;
    public string PullResults;
    public int PullIndex;
    public void Login(){
        LoginScreen.gameObject.SetActive(false);
        OverworldObj.MoveOn = true;}
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
            StartCoroutine(PullEffect());}
        else{
            AppText.text = "Not Enough Prisms";}}
    public void Path(){
        foreach(Transform i in MenuButtons){
            i.gameObject.SetActive(false);}
        AppText.text = "Reach the Black Tower";}
    IEnumerator PullEffect(){
        for(int i = 0; i < 10; i++){
            PullIndex = Random.Range(0,3);
            AppText.text = OverworldObj.Party[PullIndex].name;
            PullResults += AppText.text + " ";
            PullImage.sprite = OverworldObj.Party[PullIndex].sprite;
            yield return new WaitForSeconds(1);}
        AppText.text = PullResults;
        PullImage.sprite = null;
        PullResults = "";}
    public void DialogueStart(){
        DialogueBox.gameObject.SetActive(true);
        OverworldObj.MoveOn = false;}
    public void DialogueContinue(){
        
    }}