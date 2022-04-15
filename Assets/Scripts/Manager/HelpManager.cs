using UnityEngine;
using UnityEngine.InputSystem;

public class HelpManager : MonoBehaviour
{
    
    public PlayerInput playerInput;
    public string currentSchema;
    
    public GameObject keyboardHelp;
    public GameObject gamepadHelp;
    public GameObject touchHelp;
    
    void Update()
    {
        onSchemeChange();
    }
    
    public void onSchemeChange()
    {
        if(checkCurrentSchema())
        {
            switch(currentSchema)
            {
                case "gamepad":
                    touchHelp.SetActive(false);
                    gamepadHelp.SetActive(true);
                    keyboardHelp.SetActive(false);
                    break;
                case "touch":
                    touchHelp.SetActive(true);
                    gamepadHelp.SetActive(false);
                    keyboardHelp.SetActive(false);
                    break;
                default:
                    touchHelp.SetActive(false);
                    gamepadHelp.SetActive(false);
                    keyboardHelp.SetActive(true);
                    break;
            }
        }
    }
    
    private bool checkCurrentSchema()
    {
        if(playerInput.currentControlScheme.ToLower() != currentSchema)
        {
            currentSchema = playerInput.currentControlScheme.ToLower();
            return true;
        }
        return false;
    }
}