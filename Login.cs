using UnityEngine;
public class Login : MonoBehaviour{
    public Overworld Over;
    public Canvas LoginCanvas;
    void Start(){
        Over.MenuOn = true;}
    void Update(){
        if(Input.GetKeyDown(KeyCode.Space)){
            LoginCanvas.enabled = false;
            Over.MenuOn = false;}}}