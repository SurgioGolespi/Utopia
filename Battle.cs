using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
public class Battle : MonoBehaviour{
    public Overworld Over;
    public Image[] PartyHP;
    public  Character Char;
    public Transform[] PartyHPBar;
    public Image[] EnemyHP;
    public Transform[] EnemyHPBar;
    public Transform[] Skill;
    public Transform Arrow;
    public TextMeshProUGUI[] SkillTxt;
    public TextMeshProUGUI[] PDamageTxt;
    public TextMeshProUGUI[] EDamageTxt;
    public CanvasScaler CanScale;
    private int BattleState;
    private int AttackCount;
    private int TurnCount;
    private int Index;
    private int[] PHP = new int[3];
    private int[] EHP = new int[3];
    private int[] Position = new int[3];
    void Start(){
        Activate(PartyHPBar, false);
        Activate(EnemyHPBar, false);
        Activate(Skill, false);
        Arrow.gameObject.SetActive(false);}
    void Update(){
        if(Over.Enemy[0] != null){ArrowMove(BattleState);}}
    public void BattleStart(){
        AttackCount = 0; 
        BattleState = 0; 
        TurnCount = 0;
        Index = 0;
        Activate(Skill, true);
        Activate(PartyHPBar, true);
        Activate(EnemyHPBar, true);
        for(int i = 0; i < 3; i++){
            Position[i] = 0;
            PHP[i] = Char.CharDatabase[Over.Party[i].name].HP;
            EHP[i] = Char.CharDatabase[Over.Enemy[i].name].HP;
            PartyHP[i].fillAmount = PHP[i]/Char.CharDatabase[Over.Party[i].name].HP;
            EnemyHP[i].fillAmount = EHP[i]/Char.CharDatabase[Over.Enemy[i].name].HP;
            SkillTxt[i].text = Over.Party[i].name;}
        Arrow.gameObject.SetActive(true);}
    public void BattleUp(){
        if(BattleState == 0){
            for(int i = 0; i < 3; i++){SkillTxt[i].text = Char.CharDatabase[Over.Party[Position[0]].name].Skill[i];}}
        if(BattleState == 1){
            for(int i = 0; i < 3; i++){SkillTxt[i].text = Over.Enemy[i].name;}}
        if(BattleState == 2){
            for(int i = 0; i < 3; i++){SkillTxt[i].text = Over.Party[i].name;}}
        if(BattleState < 2){BattleState++;}
        else{
            AttackCount++;
            PlayerAttack();
            if(EHP[0] <= 0 && EHP[1] <= 0 && EHP[2] <= 0){BattleEnd();}
            else if(AttackCount % 3 == 0){EnemyAttack();}
            else{BattleState = 0;}}
        Index = Position[BattleState];}
    public void ArrowMove(int State){
        if(Input.GetKeyDown(KeyCode.D)){Index = (Index + 1) % 3;}
        if(Input.GetKeyDown(KeyCode.A)){Index = (Index - 1 + 3) % 3;}
        Arrow.position = new Vector3(Skill[Index].position.x + Mathf.RoundToInt(125 * Screen.width/CanScale.referenceResolution.x), Arrow.position.y, 0);
        if(Input.GetKeyDown(KeyCode.Space) && (BattleState != 2 || EHP[Index] > 0)){
            Position[BattleState] = Index;
            BattleUp();}}
    public void PlayerAttack(){
        EHP[Position[2]] -= Char.DamageMod(Over.Party[Position[0]].name, Position[1]);
        EDamageTxt[Position[2]].text = Char.DamageMod(Over.Party[Position[0]].name, Position[1]).ToString();
        EnemyHP[Position[2]].fillAmount = (float)EHP[Position[2]]/Char.CharDatabase[Over.Enemy[Position[2]].name].HP;}
    public void EnemyAttack(){
        for(int i = 0; i < 3; i++){
            if(EHP[i] > 0){
                int Target = Random.Range(0,2);
                PHP[Target] -= Char.DamageMod(Over.Enemy[i].name, TurnCount % 3);
                PDamageTxt[Target].text = Char.DamageMod(Over.Enemy[i].name, TurnCount % 3).ToString();
                PartyHP[Target].fillAmount = (float)PHP[Target]/Char.CharDatabase[Over.Party[Target].name].HP;}}
        BattleState = 0;
        TurnCount++;}
    public void BattleEnd(){
        Activate(PartyHPBar, false);
        Activate(EnemyHPBar, false);
        Activate(Skill, false);
        Arrow.gameObject.SetActive(false);
        for(int i = 0; i < 3; i++){
            Destroy(Over.Enemy[i].gameObject);
            EDamageTxt[i].text = "";
            PDamageTxt[i].text = "";}}
    private void Activate(Transform[] Object, bool State){
        foreach(Transform Obj in Object){Obj.gameObject.SetActive(State);}}} 