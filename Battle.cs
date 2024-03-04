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
    public Image[] PHPFill;
    public Image[] EHPFill;
    [Header("Battle Variables")]
    public int TurnCount;
    public int[] PartyHP;
    public int[] EnemyHP;
    public int Damage;
    public bool SkillOn;
    public bool TargetOn;
    public class Stats{
        public int HP;
        public int AP;
        public string[] SN;
        public int EP;
        public int LV;
        public int EN;
        public Stats(int HealthPoints, int AttackPoints, string[] SkillNames, int ExperiencePoints, int Level, int ExperienceNeeded){
            HP = HealthPoints;
            AP = AttackPoints;
            SN = SkillNames;
            EP = ExperiencePoints;
            LV = Level;
            EN = ExperienceNeeded;}}
    public Stats[] PartyStats;
    public Stats[] EnemyStats;
    void Start(){
        PartyStats = new Stats[4];
        PartyStats[0] = new Stats(100, 10, new string[]{"Dust to Dust","Godhand","Uncommited Sin"}, 0, 1, 100);
        PartyStats[1] = new Stats(100, 10, new string[]{"Party1","Party1","Party1"}, 0, 1, 100);
        PartyStats[2] = new Stats(100, 10, new string[]{"Party2","Party2","Party2"}, 0, 1, 100);
        PartyStats[3] = new Stats(100, 10, new string[]{"","",""}, 0, 1, 100);
        EnemyStats = new Stats[3];
        EnemyStats[0] = new Stats(100, 10, new string[]{"","",""}, 0, 1, 100);
        EnemyStats[1] = new Stats(100, 10, new string[]{"","",""}, 0, 1, 100);
        EnemyStats[2] = new Stats(100, 10, new string[]{"","",""}, 0, 1, 100);}
    IEnumerator EPGain(){
        for(int i = 0; i < 3; i++){
            PartyStats[MenuObj.PartyOrder[i]].EP += 100;
            if(PartyStats[MenuObj.PartyOrder[i]].EP == PartyStats[MenuObj.PartyOrder[i]].EN){
                PartyStats[MenuObj.PartyOrder[i]].LV += 1;
                PartyStats[MenuObj.PartyOrder[i]].EN *= PartyStats[MenuObj.PartyOrder[i]].LV;
                PartyStats[MenuObj.PartyOrder[i]].AP += 10;
                PartyStats[MenuObj.PartyOrder[i]].HP += 100;
                PDamageText[i].text = "Level Up";
                yield return new WaitForSeconds(0.5f);
                PDamageText[i].text = "";}}}
    public void BattleOn(){
        BattleScreen.SetActive(true);
        MenuObj.MenuButton.gameObject.SetActive(false);
        for(int i = 0; i < 3; i++){
            PartyHP[i] = PartyStats[MenuObj.PartyOrder[i]].HP;
            EnemyHP[i] = EnemyStats[i].HP;
            EHPFill[i].fillAmount = 1;}
        Action(0);}
    public void Action(int ActionIndex){
        if(SkillOn){
            SkillOn = false;
            Damage = PartyStats[MenuObj.PartyOrder[TurnCount % 3]].AP;
            for(int i = 0; i < 3; i++){
                SkillText[i].text = OverworldObj.Enemy[i].name;}
            TargetOn = true;}
        else if(TargetOn && EnemyHP[ActionIndex] > 0){
            TargetOn = false;
            TurnCount += 1;
            for(int i = 0; i < 3; i++){
                    SkillText[i].text = PartyStats[MenuObj.PartyOrder[TurnCount % 3]].SN[i];}
            if(TurnCount > 0){
                EnemyHP[ActionIndex] -= Damage;
                if(EnemyHP[ActionIndex] <= 0){
                    OverworldObj.Enemy[ActionIndex].sprite = null;}
                EHPFill[ActionIndex].fillAmount = (float)EnemyHP[ActionIndex]/(float)EnemyStats[ActionIndex].HP;
                StartCoroutine(PartyAttack(ActionIndex));}
            else{
                SkillOn = true;}}}
    IEnumerator PartyAttack(int TargetIndex){
        EDamageText[TargetIndex].text = Damage.ToString();
        yield return new WaitForSeconds(0.5f);
        EDamageText[TargetIndex].text = "";
        if(EnemyHP.All(num => num <= 0)){
                BattleOff();}
        else if(TurnCount % 3 == 0){
            StartCoroutine(EnemyAttack());}
        else{
             SkillOn = true;}}
    IEnumerator EnemyAttack(){
        for(int i = 0; i < 3; i ++){
            if(EnemyHP[i] > 0){
                int Target = Random.Range(0,3);
                Damage = EnemyStats[i].AP;
                PartyHP[Target] -= Damage;
                PHPFill[Target].fillAmount = (float)PartyHP[Target]/(float)PartyStats[MenuObj.PartyOrder[Target]].HP;
                PDamageText[Target].text = Damage.ToString();
                yield return new WaitForSeconds(0.5f);
                PDamageText[Target].text = "";}}
        SkillOn = true;}
    public void BattleOff(){
        BattleScreen.SetActive(false);
        TurnCount = -1;
        Damage = 0;
        TargetOn = true;
        OverworldObj.MoveOn = true;
        MenuObj.MenuButton.gameObject.SetActive(true);
        StartCoroutine(EPGain());}}