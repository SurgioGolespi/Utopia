using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
public class Battle : MonoBehaviour{
    public Overworld Over;
    public Image[] PartyHP;
    public  Character Char;
    public Menu Mnu;
    public Transform[] PartyHPBar;
    public Image[] EnemyHP;
    public Transform[] EnemyHPBar;
    public Transform[] Skill;
    public TextMeshProUGUI[] SkillTxt;
    public TextMeshProUGUI[] PDamageTxt;
    public TextMeshProUGUI[] EDamageTxt;
    public int BattleState;
    public int AttackCount;
    public int TurnCount;
    public int[] PHP = new int[3];
    public int[] PATK = new int[3];
    public int[] EHP = new int[3];
    public int[] EATK = new int[3];
    public int[] Position = new int[3];
    public bool[] Used = new bool[3];
    void Start(){
        Activate(PartyHPBar, false);
        Activate(EnemyHPBar, false);
        Activate(Skill, false);}
    public void BattleStart(){
        Mnu.MenuButton.gameObject.SetActive(false);
        AttackCount = 0; 
        BattleState = 0; 
        TurnCount = 0;
        Activate(Skill, true);
        Activate(PartyHPBar, true);
        Activate(EnemyHPBar, true);
        for(int i = 0; i < 3; i++){
            Used[i] = false;
            Position[i] = 0;
            Over.EnemySpriteRenderer[i].sprite = Over.EnemySprite[i];
            PHP[i] = Char.CharDatabase[Over.Party[i].name].HP;
            PATK[i] = Char.CharDatabase[Over.Party[i].name].ATK;
            EHP[i] = Char.CharDatabase[Over.Enemy[i].name].HP;
            EATK[i] = Char.CharDatabase[Over.Enemy[i].name].ATK;
            PartyHP[i].fillAmount = PHP[i]/Char.CharDatabase[Over.Party[i].name].HP;
            EnemyHP[i].fillAmount = EHP[i]/Char.CharDatabase[Over.Enemy[i].name].HP;
            SkillTxt[i].text = Over.Party[i].name;}}
    public void BattleUp(){
        if(BattleState == 0){
            for(int i = 0; i < 3; i++){
                SkillTxt[i].text = Char.CharDatabase[Over.Party[Position[0]].name].Skill[i];}
                Used[Position[0]] = true;}
        if(BattleState == 1){
            for(int i = 0; i < 3; i++){SkillTxt[i].text = Over.Enemy[i].name;}}
        if(BattleState == 2){
            for(int i = 0; i < 3; i++){SkillTxt[i].text = Over.Party[i].name;}}
        if(BattleState < 2){
            BattleState++;}
        else{
            AttackCount++;
            PlayerAttack();
            if(EHP[0] <= 0 && EHP[1] <= 0 && EHP[2] <= 0){
                BattleEnd();}
            else if(AttackCount % 3 == 0){
                EnemyAttack();}
            else{BattleState = 0;}}}
    public void SKill0(){
        if((BattleState != 2 || EHP[0] > 0 || Char.CharDatabase[Over.Party[Position[0]].name].Role[Position[1]] != "Attack") 
            && (BattleState != 0 || Used[0] != true) && (Over.Enemy[0] != null)){
            Position[BattleState] = 0;
            BattleUp();}}
     public void Skill1(){
        if((BattleState != 2 || EHP[1] > 0 || Char.CharDatabase[Over.Party[Position[0]].name].Role[Position[1]] != "Attack") 
            && (BattleState != 0 || Used[1] != true) && (Over.Enemy[0] != null)){
            Position[BattleState] = 1;
            BattleUp();}}
     public void Skill2(){
        if((BattleState != 2 || EHP[2] > 0 || Char.CharDatabase[Over.Party[Position[0]].name].Role[Position[1]] != "Attack") 
            && (BattleState != 0 || Used[2] != true) && (Over.Enemy[0] != null)){
            Position[BattleState] = 2;
            BattleUp();}}
    public void PlayerAttack(){
        if(Char.CharDatabase[Over.Party[Position[0]].name].Role[Position[1]] == "Attack"){
            EHP[Position[2]] -= PATK[Position[0]];
            if(EHP[Position[2]] <= 0){
                Over.EnemySpriteRenderer[Position[2]].sprite = Over.EnemySprite[3];}
            StartCoroutine(DamageFade(EDamageTxt[Position[2]], PATK[Position[0]].ToString()));
            EnemyHP[Position[2]].fillAmount = (float)EHP[Position[2]]/Char.CharDatabase[Over.Enemy[Position[2]].name].HP;}
        if(Char.CharDatabase[Over.Party[Position[0]].name].Role[Position[1]] == "Heal"){
            PDamageTxt[Position[2]].color = Color.green;
            StartCoroutine(DamageFade(PDamageTxt[Position[2]], PATK[Position[0]].ToString()));
            if(PHP[Position[2]] + PATK[Position[0]] <= Char.CharDatabase[Over.Party[Position[2]].name].HP){
                PHP[Position[2]] += PATK[Position[0]];
                PartyHP[Position[2]].fillAmount = (float)PHP[Position[2]]/Char.CharDatabase[Over.Party[Position[2]].name].HP;}}
        if(Char.CharDatabase[Over.Party[Position[0]].name].Role[Position[1]] == "Stack"){
            PATK[Position[0]] *= 2;}}
    public void EnemyAttack(){
        for(int i = 0; i < 3; i++){
            Used[i] = false;
            if(EHP[i] > 0){
                int Target = Random.Range(0,3);
                PHP[Target] -= EATK[i];
                StartCoroutine(DamageFade(PDamageTxt[Target], EATK[i].ToString()));
                PartyHP[Target].fillAmount = (float)PHP[Target]/Char.CharDatabase[Over.Party[Target].name].HP;}}
        BattleState = 0;
        TurnCount++;}
    public void BattleEnd(){
        Mnu.MenuButton.gameObject.SetActive(true);
        Activate(PartyHPBar, false);
        Activate(EnemyHPBar, false);
        Activate(Skill, false);
        for(int i = 0; i < 3; i++){
            if(Char.LevelUp(Over.Party[i].name, 100)){StartCoroutine(DamageFade(PDamageTxt[i], "Level Up"));};
            Destroy(Over.Enemy[i].gameObject);}}
    private void Activate(Transform[] Object, bool State){
        foreach(Transform Obj in Object){
            Obj.gameObject.SetActive(State);}}
    IEnumerator DamageFade(TextMeshProUGUI Text, string Txt){
        Text.text = Txt;
        yield return new WaitForSeconds(1f);
        Text.color = Color.white;
        Text.text = "";}} 