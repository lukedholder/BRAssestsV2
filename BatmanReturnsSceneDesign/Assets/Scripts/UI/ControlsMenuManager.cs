using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ControlsMenuManager : MonoBehaviour
{
    private static GameObject controlsMenu;
    public static ControlsMenuManager instance; // global instance
    public Button returnButton;
    [SerializeField] private GameObject adminCtrls;
    public GameObject menuToReturnTo;
    //public static bool menuToReturnToBool = false;

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        InitializeControlsMenu();
    }

    void Start()
    {
        instance = this;
    }

    void InitializeControlsMenu()
    {
        controlsMenu = GameObject.Find("ViewControlsMenu");
        // TODO check if user is an admin to determine if admin ctrls can be shown
        returnButton = GameObject.Find("ViewControlsMenu/ReturnButton").GetComponent<Button>();
        returnButton.onClick.AddListener(delegate {ReturnFromCtrlMenu(menuToReturnTo);});

        adminCtrls = GameObject.Find("ViewControlsMenu/AdminControls");
        
        // if a user is logged in
        if (SceneManager.GetActiveScene().buildIndex > 0)
        {
            // user is playing game, in pause menu
            // controls menu should go back to pause menu
            menuToReturnTo = GameObject.Find("PauseMenu");
            PauseMenuManager.instance.Resume();
            //menuToReturnToBool = true;
        } else
        {
            // user is not playing game, in main menu
            // controls menu should go back to main menu
            menuToReturnTo = GameObject.Find("Main Menu");
            //menuToReturnToBool = true;
        }

        controlsMenu.SetActive(false);
    }

    public void DisplayControlsMenu()
    {
        controlsMenu.SetActive(true);
        // if the user is an admin, make the admin controls accessible
        adminCtrls.SetActive(UserDB.db.UserIsAdmin(GameManager.currentUID));
    }

    void ReturnFromCtrlMenu(GameObject menuToReturnTo)
    {
        menuToReturnTo.SetActive(true);
        controlsMenu.SetActive(false);
    }
}
