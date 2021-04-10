using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialTextScroller : MonoBehaviour
{
    public GameObject[] texts;
    public PlayerControls playerControls;
    private int currentText = 0;
    private void Awake() {
        playerControls = new PlayerControls();
    }

    void OnEnable() {
        playerControls.Enable();
    }

    void OnDisable() {
        playerControls.Disable();
    }

    void Start()
    {
        texts[currentText].SetActive(true);
    }
    void Update()
    {
        if (currentText == texts.Length - 1 && playerControls.InGame.Shoot.WasPressedThisFrame()) {SceneManager.LoadScene("Level1");}
        else if ((playerControls.InGame.Move.triggered || playerControls.InGame.Jump.WasPressedThisFrame() || playerControls.InGame.Dash.WasPressedThisFrame()) && currentText < texts.Length - 1)
        {
            Debug.Log("Movement");
            texts[currentText].SetActive(!texts[currentText].activeSelf);
            ++currentText;
            texts[currentText].SetActive(!texts[currentText].activeSelf);
        }
    }
}
