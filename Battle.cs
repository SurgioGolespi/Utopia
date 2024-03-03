using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
public class Battle : MonoBehaviour{
    [Header("Script Objects")]
    public Overworld OverworldObj;
    public Menu MenuObj;
    [Header("Game Objects")]
    public GameObject BattleScreen;
    public TextMeshProUGUI[] SkillText;
    public TextMeshProUGUI[] PDamageText;
    public TextMeshProUGUI[] EDamageText;
    [Header("Battle Variables")]
    public int ActiveParty;
    public int[] PartyHP;
    public int[] EnemyHP;
    public int Damage;
    public bool SkillOn;
    public bool TargetOn;
    public class Stats{
        public int HP;
        public int AP;
        public string[] SN;
        public Stats(int HealthPoints, int AttackPoints, string[] SkillNames){
            HP = HealthPoints;
            AP = AttackPoints;
            SN = SkillNames;}}
    public Stats[] PartyStats;
    public Stats[] EnemyStats;
    void Start(){
        PartyStats = new Stats[4];
        PartyStats[0] = new Stats(100, 10, new string[]{"Dust to Dust","Godhand","Uncommited Sin"});
        PartyStats[1] = new Stats(100, 10, new string[]{"","",""});
        PartyStats[2] = new Stats(100, 10, new string[]{"","",""});
        PartyStats[3] = new Stats(100, 10, new string[]{"","",""});
        EnemyStats = new Stats[3];
        EnemyStats[0] = new Stats(100, 10, new string[]{"","",""});
        EnemyStats[1] = new Stats(100, 10, new string[]{"","",""});
        EnemyStats[2] = new Stats(100, 10, new string[]{"","",""});}
    public void BattleOn(){
        BattleScreen.SetActive(true);
        MenuObj.MenuButton.gameObject.SetActive(false);
        for(int i = 0; i < 3; i++){
            PartyHP[i] = PartyStats[MenuObj.PartyOrder[i]].HP;
            EnemyHP[i] = EnemyStats[i].HP;}
        Action(0);}
    public void Action(int ActionIndex){
        if(SkillOn){
            SkillOn = false;
            Damage = PartyStats[MenuObj.PartyOrder[ActiveParty]].AP;
            for(int i = 0; i<3; i++){
                SkillText[i].text = OverworldObj.Enemy[i].name;}
            TargetOn = true;}
        else if(TargetOn){
            EnemyHP[ActionIndex] -= Damage;
            TargetOn = false;
            for(int i = 0; i<3; i++){
                SkillText[i].text = PartyStats[MenuObj.PartyOrder[ActiveParty]].SN[i];}
            if(EnemyHP.All(num => num < 0)){
                BattleOff();}
            SkillOn = true;}}
    public void BattleOff(){
        BattleScreen.SetActive(false);
        OverworldObj.MoveOn = true;
        MenuObj.MenuButton.gameObject.SetActive(true);}}