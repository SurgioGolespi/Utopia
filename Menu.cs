using UnityEngine;
using TMPro;
public class Menu : MonoBehaviour{
    public int[] CurrentParty;
    public Overworld Over;
    public Character Char;
    public Transform MenuScreen;
    public Transform MenuButton;
    public TextMeshProUGUI PartyTxt;
    public Transform PartyButtonTxt;
    void Start(){MenuScreen.gameObject.SetActive(false);}
    public void MenuOff(){MenuButton.gameObject.SetActive(false);}
    public void MenuOn(){MenuButton.gameObject.SetActive(true);}
    public void MenuOpen(){
        MenuButton.gameObject.SetActive(false);
        MenuScreen.gameObject.SetActive(true);
        PartyButtonTxt.gameObject.SetActive(true);
        Over.MenuOn = true;}
    public void MenuClose(){
        PartyTxt.text = "";
        MenuButton.gameObject.SetActive(true);
        MenuScreen.gameObject.SetActive(false);
        Over.MenuOn = false;}
    public void PartyMenu(){
        PartyButtonTxt.gameObject.SetActive(false);
        PartyTxt.text = "HP: " + Char.CharDatabase[Over.Party[0].name].HP + "\n" + 
                        "Level: " + Char.CharDatabase[Over.Party[0].name].LVL + "\n" +
                        "Exp Needed: " + (Char.CharDatabase[Over.Party[0].name].XPN - Char.CharDatabase[Over.Party[0].name].EXP) + "\n" +
                        "HP: " + Char.CharDatabase[Over.Party[1].name].HP + "\n" + 
                        "Level: " + Char.CharDatabase[Over.Party[1].name].LVL + "\n" +
                        "Exp Needed: " + (Char.CharDatabase[Over.Party[1].name].XPN - Char.CharDatabase[Over.Party[0].name].EXP) + "\n" +
                        "HP: " + Char.CharDatabase[Over.Party[2].name].HP + "\n" + 
                        "Level: " + Char.CharDatabase[Over.Party[2].name].LVL + "\n" +
                        "Exp Needed: " + (Char.CharDatabase[Over.Party[2].name].XPN - Char.CharDatabase[Over.Party[0].name].EXP) + "\n";}}