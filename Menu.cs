using UnityEngine;
using TMPro;
public class Menu : MonoBehaviour{
    public int[] CurrentParty;
    public Overworld Over;
    public Character Char;
    public Transform MenuScreen;
    public Transform MenuButton;
    public Transform PartyButton;
    public TextMeshProUGUI PartyTxt;
    public Transform PullButton;
    public TextMeshProUGUI PullTxt;
    public Transform ProgressButton;
    public TextMeshProUGUI ProgressTxt;
    public int Prisms;
    public int[] PartyIndex;
    public int[] EnemyIndex;
    void Start(){
        PartyIndex = new int[]{0,1,2};
        EnemyIndex = new int[]{0,1,2};
        MenuScreen.gameObject.SetActive(false);
        Over.BuildSpriteRenderer[0].sprite = null;
        for(int i = 0; i < 3; i++){
            Over.EnemySpriteRenderer[i].sprite = null;
            Over.Party[i].name = Over.PartySprite[PartyIndex[i]].name;}}
    public void MenuOpen(){
        MenuButton.gameObject.SetActive(false);
        MenuScreen.gameObject.SetActive(true);
        PartyButton.gameObject.SetActive(true);
        PullButton.gameObject.SetActive(true);
        ProgressButton.gameObject.SetActive(true);
        Over.MenuOn = true;}
    public void MenuClose(){
        PartyTxt.text = "";
        PullTxt.text = "";
        ProgressTxt.text = "";
        MenuButton.gameObject.SetActive(true);
        MenuScreen.gameObject.SetActive(false);
        Over.MenuOn = false;}
    public void ButtonsOff(){
        PartyButton.gameObject.SetActive(false);
        PullButton.gameObject.SetActive(false);
        ProgressButton.gameObject.SetActive(false);}
    public void PartyMenu(){
        ButtonsOff();
        for(int i = 0; i < 3; i++){
            PartyTxt.text += "HP: " + Char.CharDatabase[Over.Party[i].name].HP + "\n" + 
                        "Attack: " + Char.CharDatabase[Over.Party[i].name].ATK + "\n" +
                        "Level: " + Char.CharDatabase[Over.Party[i].name].LVL + "\n" +
                        "Exp Needed: " + (Char.CharDatabase[Over.Party[i].name].XPN - Char.CharDatabase[Over.Party[i].name].EXP) + "\n";}}
    public void PullMenu(){
        ButtonsOff();
        if(Prisms >= 1000){
            Prisms -= 1000;
            for(int i = 0; i < 10; i++){
                PullTxt.text += Random.Range(3,6) + " Star" + "\n";}}
        else{
            PullTxt.text = "Not Enough Prisms";}}
    public void ProgressMenu(){
        ButtonsOff();
        ProgressTxt.text = "Climb the Black Tower";}}