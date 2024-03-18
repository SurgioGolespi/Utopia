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
    public Sprite[] StatusIcon;
    public Image[] P0Status;
    public Image[] P1Status;
    public Image[] P2Status;
    public Image[] E0Status;
    public Image[] E1Status;
    public Image[] E2Status;
    public Image[][] PStatus;
    public Image[][] EStatus;
    [Header("Battle Animations")]
    public Sprite[] PAttackSprite;
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
        public string ID;
        public int PC;
        public Stats(string Identifier, int HealthPoints, int AttackPoints, string[] SkillNames, int ExperiencePoints, 
        int Level, int ExperienceNeeded, int[] AttackCount, int[] StatusTypes, int PrizeCount){
            ID = Identifier;
            HP = HealthPoints;
            AP = AttackPoints;
            SN = SkillNames;
            EP = ExperiencePoints;
            LV = Level;
            EN = ExperienceNeeded;
            AC = AttackCount;
            ST = StatusTypes;
            PC = PrizeCount;}}
    public Stats[] PartyStats;
    public Stats[] EnemyStats;
    void Start(){
        PartyStats = new Stats[4];
        PartyStats[0] = new Stats("Party0", 100, 10, new string[]{"Dust to Dust","Seventh Day","Uncommited Sin"}, 0, 1, 100, new int[]{1,2,3}, new int[]{1,0,0}, 0);
        PartyStats[1] = new Stats("Party1", 100, 10, new string[]{"Party1","Party1","Party1"}, 0, 1, 100, new int[]{1,1,1}, new int[]{0,0,0}, 0);
        PartyStats[2] = new Stats("Party2", 100, 10, new string[]{"Party2","Party2","Party2"}, 0, 1, 100, new int[]{1,1,1}, new int[]{0,0,0}, 0);
        PartyStats[3] = new Stats("Party3", 100, 10, new string[]{"","",""}, 0, 1, 100, new int[]{1,1,1}, new int[]{0,0,0}, 0);
        EnemyStats = new Stats[3];
        EnemyStats[0] = new Stats("Enemy0", 100, 10, new string[]{"","",""}, 0, 1, 100, new int[]{1,1,1}, new int[]{0,0,0}, 0);
        EnemyStats[1] = new Stats("Enemy1", 100, 10, new string[]{"","",""}, 0, 1, 100, new int[]{1,1,1}, new int[]{0,0,0}, 0);
        EnemyStats[2] = new Stats("Enemy2", 100, 10, new string[]{"","",""}, 0, 1, 100, new int[]{1,1,1}, new int[]{0,0,0}, 0);
        PStatus = new Image[][] {
            P0Status, P1Status, P2Status};
        EStatus = new Image[][] {
            E0Status, E1Status, E2Status};}
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
            PHPFill[i].fillAmount = 1;
            EHPFill[i].fillAmount = 1;
            for(int j = 0; j < 1; j++){
                PStatus[i][j].gameObject.SetActive(false);
                EStatus[i][j].gameObject.SetActive(false);}}
        Action(0);}
    public void Action(int ActionIndex){
        if(SkillOn){
            SkillOn = false;
            Skill = ActionIndex;
            for(int i = 0; i < 3; i++){
                SkillText[i].text = EnemyStats[EnemyOrder[i]].ID;}
            TargetOn = true;}
        else if(TargetOn && EnemyHP[ActionIndex] > 0){
            TargetOn = false;
            TurnCount++;
            for(int i = 0; i < 3; i++){
                    SkillText[i].text = PartyStats[MenuObj.PartyOrder[TurnCount % 3]].SN[i];}
            if(TurnCount > 0){
                StartCoroutine(PartyAttack(ActionIndex));}
            else{
                SkillOn = true;}}}
    IEnumerator PartyAttack(int PartyTarget){
        OverworldObj.Party[(TurnCount + 2) % 3].sprite = PAttackSprite[MenuObj.PartyOrder[(TurnCount + 2) % 3]];
        for(int i = 0; i < PartyStats[MenuObj.PartyOrder[(TurnCount + 2) % 3]].AC[Skill]; i++){
            Damage = PApplyStatus(PartyStats[MenuObj.PartyOrder[(TurnCount + 2)% 3]].AP, PartyTarget);
            EnemyHP[PartyTarget] -= Damage;
            EHPFill[PartyTarget].fillAmount = (float)EnemyHP[PartyTarget]/(float)EnemyStats[PartyTarget].HP;
            EDamageText[PartyTarget].text = Damage.ToString();
            yield return new WaitForSeconds(0.5f);
            OverworldObj.Party[(TurnCount + 2) % 3].sprite = OverworldObj.PartySprite[MenuObj.PartyOrder[(TurnCount + 2) % 3]];
            EDamageText[PartyTarget].text = "";}
        if(EnemyHP[PartyTarget] <= 0){
                OverworldObj.Enemy[PartyTarget].sprite = null;
                foreach(Image Status in EStatus[PartyTarget]){
                    Status.gameObject.SetActive(false);}}
        if(EnemyHP.All(num => num <= 0)){
                BattleOff();}
        else if(TurnCount % 3 == 0){
            StartCoroutine(EnemyAttack());}
        else{
             SkillOn = true;}}
    public int PApplyStatus(int Damage, int PartyTarget){
        if(EnemyStatus[PartyTarget, 0] > 0){
            Damage *= 2;
            EStatus[PartyTarget][0].gameObject.SetActive(false);
            EnemyStatus[PartyTarget, 0] -= 1;}
        if(PartyStats[MenuObj.PartyOrder[(TurnCount + 2) % 3]].ST[Skill] == 1){
            EStatus[PartyTarget][0].gameObject.SetActive(true);
            EStatus[PartyTarget][0].sprite = StatusIcon[0];
            EnemyStatus[PartyTarget, 0] += 1;}
        return Damage;}
    public int EApplyStatus(int Damage, int EnemyTarget, int EnemyIndex){
        if(PartyStatus[EnemyTarget, 0] > 0){
            Damage *= 2;
            PStatus[EnemyTarget][0].gameObject.SetActive(false);
            PartyStatus[EnemyTarget, 0] -= 1;}
        if(EnemyStats[EnemyOrder[EnemyIndex]].ST[Skill] == 1){
            PStatus[EnemyTarget][0].gameObject.SetActive(true);
            PStatus[EnemyTarget][0].sprite = StatusIcon[0];
            PartyStatus[EnemyTarget, 0] += 1;}
        return Damage;}
    IEnumerator EnemyAttack(){
        for(int i = 0; i < 3; i++){
            if(EnemyHP[i] > 0){
                int EnemyTarget = Random.Range(0,3);
                Damage = EnemyStats[i].AP;
                PartyHP[EnemyTarget] -= Damage;
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