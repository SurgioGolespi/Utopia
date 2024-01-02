using UnityEngine;
public class Overworld : MonoBehaviour{
    public Battle Btl;
    public Menu Mnu;
    public Transform[] Party;
    public Transform[] Enemy;
    public Transform[] Foreground;
    public Transform[] EnemyFab;
    public Transform[] PartyFab;
    public Transform[] BuildingFab;
    public Transform[] Building;
    public Sprite[] FGSprites;
    public SpriteRenderer[] FGSpriteRenderer;
    public Transform Camera;
    private Vector3 Position;
    public int Steps;
    public int CurFG;
    public bool MenuOn;
    public bool BuildOn;
    public AudioSource Soundtrack;
    void Start(){
        Soundtrack.Play();
        for(int i = 0; i < 3; i++){
                Party[i] = Instantiate(PartyFab[Mnu.CurrentParty[i]], new Vector3(0 - 2 * i, 0, 0), Quaternion.identity);
                Party[i].name = PartyFab[Mnu.CurrentParty[i]].name;}}
    void Update(){
        if(Enemy[0] == null && MenuOn == false){
            for(int i = 0; i < 3; i++){Party[i].localScale = new Vector3(Mathf.Sign(Input.GetAxis("Horizontal")), 1, 1);}
            Party[0].position = Party[0].position + new Vector3(Input.GetAxis("Horizontal") * 0.2f, 0, 0);}
        Party[1].position = new Vector3(Party[0].position.x - 2 * Party[0].localScale.x, 0, 0);
        Party[2].position = new Vector3(Party[0].position.x - 4 * Party[0].localScale.x, 0, 0);
        foreach(Transform i in Foreground){
            i.position = ((Party[0].position.x - i.position.x) * Party[0].localScale.x > 24) ? new Vector3(i.position.x + 57 * Party[0].localScale.x, 1, 0) : i.position;}
        Camera.position = new Vector3(Party[0].position.x + 4, 1, -1);
        if(Vector3.Distance(Position, Party[0].position) > 1){
            Steps++;
            Position = Party[0].position;}
        if(Steps % 100 == 0 && Steps > 0 && CurFG % 3 != 1){
            Steps++;
            for(int i = 0; i < 3; i++){
                Party[i].localScale = new Vector3(1,1,1);
                Enemy[i] = Instantiate(EnemyFab[i], new Vector3(Party[0].position.x + 8 + 2 * i, 0, 0), Quaternion.identity);
                Enemy[i].name = EnemyFab[i].name;}
            Btl.BattleStart();}
        if(Party[0].position.x > 500 || Party[0].position.x < 0){
            CurFG = (Party[0].position.x > 0) ? (CurFG + 1) % 3 : (CurFG + 2) % 3;
            Party[0].position = (Party[0].position.x > 0) ? new Vector3(0, 0, 0) : new Vector3(500, 0, 0);
            for(int i = 0; i < 3; i++){
                Foreground[i].position = new Vector3(Party[0].position.x - 15 + 19 * i, 1, 0);
                FGSpriteRenderer[i].sprite = FGSprites[CurFG];}}
        if(CurFG == 1 && Party[0].position.x == 0 && !BuildOn){
            BuildOn = true;
            Building[0] = Instantiate(BuildingFab[0], new Vector3(Party[0].position.x + 50, 1, 0), Quaternion.identity);
            Building[0].name = BuildingFab[0].name;}}}