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
        if (currentText == texts.Length - 1 && playerControls.InGame.Shoot.WasPressedThisFrame())
        {
            PlayerData.Instance.SetPlayerPosition(new Vector3(-19f, .5f, 0f));
            SceneManager.LoadScene("Garden");
        }
        if (playerControls.InGame.Move.triggered && currentText == 0)
        {
            
            texts[currentText].SetActive(!texts[currentText].activeSelf);
            currentText = 1;
            texts[currentText].SetActive(!texts[currentText].activeSelf);
        } else if (playerControls.InGame.Jump.WasPerformedThisFrame() && currentText == 1)
        {
            
            texts[currentText].SetActive(!texts[currentText].activeSelf);
            currentText = 2;
            texts[currentText].SetActive(!texts[currentText].activeSelf);
        } else if (playerControls.InGame.Dash.WasPerformedThisFrame() && currentText == 2)
        {
            
            texts[currentText].SetActive(!texts[currentText].activeSelf);
            currentText = 3;
            texts[currentText].SetActive(!texts[currentText].activeSelf);
        }
    }
}
