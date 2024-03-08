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
    public int Skill;
    public bool SkillOn;
    public bool TargetOn;
    public int[] EnemyOrder;
    public int[,] PartyStatus = new int[3, 1];
    public int[,] EnemyStatus = new int[3, 1];
    public class Stats{
        public int HP;
        public int AP;
        public string[] SN;
        public int EP;
        public int LV;
        public int EN;
        public int[] AC;
        public int[] ST;
        public Stats(int HealthPoints, int AttackPoints, string[] SkillNames, int ExperiencePoints, int Level, int ExperienceNeeded, int[] AttackCount, int[] StatusTypes){
            HP = HealthPoints;
            AP = AttackPoints;
            SN = SkillNames;
            EP = ExperiencePoints;
            LV = Level;
            EN = ExperienceNeeded;
            AC = AttackCount;
            ST = StatusTypes;}}
    public Stats[] PartyStats;
    public Stats[] EnemyStats;
    void Start(){
        PartyStats = new Stats[4];
        PartyStats[0] = new Stats(100, 10, new string[]{"Dust to Dust","Seventh Day","Uncommited Sin"}, 0, 1, 100, new int[]{1,2,3}, new int[]{1,0,0});
        PartyStats[1] = new Stats(100, 10, new string[]{"Party1","Party1","Party1"}, 0, 1, 100, new int[]{1,1,1}, new int[]{0,0,0});
        PartyStats[2] = new Stats(100, 10, new string[]{"Party2","Party2","Party2"}, 0, 1, 100, new int[]{1,1,1}, new int[]{0,0,0});
        PartyStats[3] = new Stats(100, 10, new string[]{"","",""}, 0, 1, 100, new int[]{1,1,1}, new int[]{0,0,0});
        EnemyStats = new Stats[3];
        EnemyStats[0] = new Stats(100, 10, new string[]{"","",""}, 0, 1, 100, new int[]{1,1,1}, new int[]{0,0,0});
        EnemyStats[1] = new Stats(100, 10, new string[]{"","",""}, 0, 1, 100, new int[]{1,1,1}, new int[]{0,0,0});
        EnemyStats[2] = new Stats(100, 10, new string[]{"","",""}, 0, 1, 100, new int[]{1,1,1}, new int[]{0,0,0});}
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
            Skill = ActionIndex;
            for(int i = 0; i < 3; i++){
                SkillText[i].text = OverworldObj.Enemy[i].name;}
            TargetOn = true;}
        else if(TargetOn && EnemyHP[ActionIndex] > 0){
            TargetOn = false;
            TurnCount += 1;
            for(int i = 0; i < 3; i++){
                    SkillText[i].text = PartyStats[MenuObj.PartyOrder[TurnCount % 3]].SN[i];}
            if(TurnCount > 0){
                StartCoroutine(PartyAttack(ActionIndex));}
            else{
                SkillOn = true;}}}
    IEnumerator PartyAttack(int PartyTarget){
        for(int i = 0; i < PartyStats[MenuObj.PartyOrder[(TurnCount + 2) % 3]].AC[Skill]; i++){
            Damage = ApplyStatus(PartyStats[MenuObj.PartyOrder[(TurnCount + 2)% 3]].AP, PartyTarget);
            EnemyHP[PartyTarget] -= Damage;
            EHPFill[PartyTarget].fillAmount = (float)EnemyHP[PartyTarget]/(float)EnemyStats[PartyTarget].HP;
            EDamageText[PartyTarget].text = Damage.ToString();
            yield return new WaitForSeconds(0.5f);
            EDamageText[PartyTarget].text = "";}
        if(EnemyHP[PartyTarget] <= 0){
                OverworldObj.Enemy[PartyTarget].sprite = null;}
        if(EnemyHP.All(num => num <= 0)){
                    BattleOff();}
        else if(TurnCount % 3 == 0){
            StartCoroutine(EnemyAttack());}
        else{
             SkillOn = true;}}
    public int ApplyStatus(int Damage, int PartyTarget){
        if(EnemyStatus[PartyTarget, 0] > 0){
            Damage *= 2;
            Debug.Log("Break Exploited");
            EnemyStatus[PartyTarget, 0] -= 1;}
        if(PartyStats[MenuObj.PartyOrder[(TurnCount + 2) % 3]].ST[Skill] == 1){
            EnemyStatus[PartyTarget, 0] += 1;
            Debug.Log("Break Inflicted");}
        return Damage;}
    IEnumerator EnemyAttack(){
        for(int i = 0; i < 3; i++){
            if(EnemyHP[i] > 0){
                int EnemyTarget = Random.Range(0,3);
                Damage = EnemyStats[i].AP;
                PartyHP[EnemyTarget] -= Damage;
                PHPFill[EnemyTarget].rectTransform.sizeDelta = new Vector2(400 * (float)PartyHP[EnemyTarget]/(float)PartyStats[MenuObj.PartyOrder[EnemyTarget]].HP, 25);
                PDamageText[EnemyTarget].text = Damage.ToString();
                yield return new WaitForSeconds(0.5f);
                PDamageText[EnemyTarget].text = "";}}
        SkillOn = true;}
    public void BattleOff(){
        BattleScreen.SetActive(false);
        TurnCount = -1;
        Damage = 0;
        TargetOn = true;
        OverworldObj.MoveOn = true;
        MenuObj.MenuButton.gameObject.SetActive(true);
        StartCoroutine(EPGain());}}