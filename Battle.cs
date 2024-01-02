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
    public Transform Arrow;
    public TextMeshProUGUI[] SkillTxt;
    public TextMeshProUGUI[] PDamageTxt;
    public TextMeshProUGUI[] EDamageTxt;
    public CanvasScaler CanScale;
    public int BattleState;
    public int AttackCount;
    public int TurnCount;
    public int Index;
    public int[] PHP = new int[3];
    public int[] EHP = new int[3];
    public int[] Position = new int[3];
    public bool[] Used = new bool[3];
    void Start(){
        Activate(PartyHPBar, false);
        Activate(EnemyHPBar, false);
        Activate(Skill, false);
        Arrow.gameObject.SetActive(false);}
    void Update(){
        if(Over.Enemy[0] != null){ArrowMove(BattleState);}}
    public void BattleStart(){
        Mnu.MenuOff();
        AttackCount = 0; 
        BattleState = 0; 
        TurnCount = 0;
        Index = 0;
        Activate(Skill, true);
        Activate(PartyHPBar, true);
        Activate(EnemyHPBar, true);
        for(int i = 0; i < 3; i++){
            Used[i] = false;
            Position[i] = 0;
            PHP[i] = Char.CharDatabase[Over.Party[i].name].HP;
            EHP[i] = Char.CharDatabase[Over.Enemy[i].name].HP;
            PartyHP[i].fillAmount = PHP[i]/Char.CharDatabase[Over.Party[i].name].HP;
            EnemyHP[i].fillAmount = EHP[i]/Char.CharDatabase[Over.Enemy[i].name].HP;
            SkillTxt[i].text = Over.Party[i].name;}
        Arrow.gameObject.SetActive(true);}
    public void BattleUp(){
        if(BattleState == 0){
            for(int i = 0; i < 3; i++){SkillTxt[i].text = Char.CharDatabase[Over.Party[Position[0]].name].Skill[i];}
            Used[Index] = true;}
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
        if(Input.GetKeyDown(KeyCode.A)){Index = (Index + 2) % 3;}
        Arrow.position = new Vector3(Skill[Index].position.x + Mathf.RoundToInt(125 * Screen.width/CanScale.referenceResolution.x), Arrow.position.y, 0);
        if(Input.GetKeyDown(KeyCode.Space) && (BattleState != 2 || EHP[Index] > 0) && (BattleState != 0 || Used[Index] != true)){
            Position[BattleState] = Index;
            BattleUp();}}
    public void PlayerAttack(){
        EHP[Position[2]] -= Char.DamageMod(Over.Party[Position[0]].name, Position[1]);
        StartCoroutine(DamageFade(EDamageTxt[Position[2]], Char.DamageMod(Over.Party[Position[0]].name, Position[1]).ToString()));
        EnemyHP[Position[2]].fillAmount = (float)EHP[Position[2]]/Char.CharDatabase[Over.Enemy[Position[2]].name].HP;}
    public void EnemyAttack(){
        for(int i = 0; i < 3; i++){
            Used[i] = false;
            if(EHP[i] > 0){
                int Target = Random.Range(0,3);
                PHP[Target] -= Char.DamageMod(Over.Enemy[i].name, TurnCount % 3);
                StartCoroutine(DamageFade(PDamageTxt[Target], Char.DamageMod(Over.Enemy[i].name, TurnCount % 3).ToString()));
                PartyHP[Target].fillAmount = (float)PHP[Target]/Char.CharDatabase[Over.Party[Target].name].HP;}}
        BattleState = 0;
        TurnCount++;}
    public void BattleEnd(){
        Mnu.MenuOn();
        Activate(PartyHPBar, false);
        Activate(EnemyHPBar, false);
        Activate(Skill, false);
        Arrow.gameObject.SetActive(false);
        for(int i = 0; i < 3; i++){
            if(Char.LevelUp(Over.Party[i].name, 100)){StartCoroutine(DamageFade(PDamageTxt[i], "Level Up"));};
            Destroy(Over.Enemy[i].gameObject);}}
    private void Activate(Transform[] Object, bool State){
        foreach(Transform Obj in Object){Obj.gameObject.SetActive(State);}}
    IEnumerator DamageFade(TextMeshProUGUI Text, string Txt){
        Text.text = Txt;
        yield return new WaitForSeconds(1f);
        Text.text = "";}} 