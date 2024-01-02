using UnityEngine;
public class Login : MonoBehaviour{
    public Transform LoginScreen;
    public Overworld Over;
    public void StartLogin(){
            Over.MenuOn = false;
            LoginScreen.gameObject.SetActive(false);}}