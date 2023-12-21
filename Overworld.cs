using UnityEngine;
public class Overworld : MonoBehaviour{
    public Battle Btl;
    public Transform[] Party;
    public Transform[] Enemy;
    public Transform[] Foreground;
    public Transform[] EnemyFab;
    public Transform[] PartyFab;
    public Sprite[] FGSprites;
    public SpriteRenderer[] FGSpriteRenderer;
    public Transform Camera;
    private Vector3 Position;
    private int Steps;
    private int Distance;
    public bool MenuOn;
    void Start(){
        for(int i = 0; i < 3; i++){
                Party[i] = Instantiate(PartyFab[0], new Vector3(0 - 2 * i, 0, 0), Quaternion.identity);
                Party[i].name = PartyFab[0].name;}}
    void Update(){
        if(Enemy[0] == null && MenuOn == false){
            for(int i = 0; i < 3; i++){Party[i].localScale = new Vector3(Mathf.Sign(Input.GetAxis("Horizontal")), 1, 1);}
            Party[0].position = Party[0].position + new Vector3(Input.GetAxis("Horizontal") * 0.2f, 0, 0);
            Party[1].position = new Vector3(Party[0].position.x - 2 * Party[0].localScale.x, 0, 0);
            Party[2].position = new Vector3(Party[0].position.x - 4 * Party[0].localScale.x, 0, 0);}
        foreach(Transform i in Foreground){
            i.position = ((Party[0].position.x - i.position.x) * Party[0].localScale.x > 23) ? new Vector3(i.position.x + 57 * Party[0].localScale.x, 2, 0) : i.position;}
        Camera.position = new Vector3(Party[0].position.x + 4, 2, -1);
        if(Vector3.Distance(Position, Party[0].position) > 1){
            Steps++;
            Position = Party[0].position;}
        if(Steps % 100 == 0 && Steps > 0){
            Steps++;
            for(int i = 0; i < 3; i++){
                Party[i].localScale = new Vector3(1,1,1);
                Enemy[i] = Instantiate(EnemyFab[0], new Vector3(Party[0].position.x + 8 + 2 * i, 0, 0), Quaternion.identity);
                Enemy[i].name = EnemyFab[0].name;}
            Btl.BattleStart();}
         if(Party[0].position.x > Distance){
            Distance = (int)Party[0].position.x;
            if(Distance == 570){for(int i = 0; i < 3; i++){FGSpriteRenderer[i].sprite = FGSprites[1];}}}}}