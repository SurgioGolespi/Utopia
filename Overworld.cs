using UnityEngine;
public class Overworld : MonoBehaviour{
    public Battle Btl;
    public Menu Mnu;
    public Transform[] Party;
    public Transform[] Enemy;
    public Transform[] Foreground;
    public Transform[] Building;
    public Sprite[] FGSprite;
    public SpriteRenderer[] FGSpriteRenderer;
    public Sprite[] EnemySprite;
    public SpriteRenderer[] EnemySpriteRenderer;
    public Sprite[] PartySprite;
    public SpriteRenderer[] PartySpriteRenderer;
    public Sprite[] BuildSprite;
    public SpriteRenderer[] BuildSpriteRenderer;
    public Transform Camera;
    private Vector3 Position;
    public int Steps;
    public int CurFG;
    public bool MenuOn;
    public bool BuildOn;
    public bool BattleOn;
    public AudioSource Soundtrack;
    public float HorizontalValue;
    public float VerticalValue;
    void Update(){
        if(!BattleOn && MenuOn == false){
            if(Input.touchCount > 0){
                if (Input.GetTouch(0).phase == TouchPhase.Moved){
                    if (Input.GetTouch(0).deltaPosition.x >= 0){
                        HorizontalValue = 1;}
                    else if (Input.GetTouch(0).deltaPosition.x < 0){
                        HorizontalValue = -1;}
                    if (Input.GetTouch(0).deltaPosition.y >= 0){
                        VerticalValue = 1;}
                    else if (Input.GetTouch(0).deltaPosition.y < 0){
                        VerticalValue = -1;}}
                for(int i = 0; i < 3; i++){
                    Party[i].localScale = new Vector3(Mathf.Sign(HorizontalValue), 1, 1);}}
            else{
                HorizontalValue = 0;
                VerticalValue = 0;}
            Party[0].position = Party[0].position + new Vector3(HorizontalValue * 0.2f, 0, 0);}
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
                    EnemySpriteRenderer[i].sprite = EnemySprite[i];
                    Enemy[i].name = EnemySprite[Mnu.EnemyIndex[i]].name;
                    Enemy[i].position = Party[0].position + new Vector3(8 + 2 * i, 0, 0);}
                Btl.BattleStart();}
            if(Party[0].position.x > 500 || Party[0].position.x < 0){
                CurFG = (Party[0].position.x > 0) ? (CurFG + 1) % 3 : (CurFG + 2) % 3;
                Party[0].position = (Party[0].position.x > 0) ? new Vector3(0, 0, 0) : new Vector3(500, 0, 0);
                for(int i = 0; i < 3; i++){
                    Foreground[i].position = new Vector3(Party[0].position.x - 15 + 19 * i, 1, 0);
                    FGSpriteRenderer[i].sprite = FGSprite[CurFG];}}
            if(CurFG == 1 && Party[0].position.x == 0 && !BuildOn){
                BuildOn = true;
                BuildSpriteRenderer[0].sprite = BuildSprite[0];}}}