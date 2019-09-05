using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine;

public class PauseManager : MonoBehaviour {

    Canvas canvas;

    void Start()
    {
        canvas = GetComponent<Canvas>();
        canvas.enabled = false;
        Cursor.visible = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            Pause();
        }
    }

    public void Pause()
    {
        canvas.enabled = !canvas.enabled;
        Cursor.visible = !Cursor.visible;
        Time.timeScale = Time.timeScale == 0 ? 1 : 0;
        Cursor.lockState = Time.timeScale == 0 ? CursorLockMode.Locked : CursorLockMode.None;
        PlayerCharacter.pc.GetComponent<FirstPersonController>().enabled = !PlayerCharacter.pc.GetComponent<FirstPersonController>().enabled;

        //GameUISystem.uiSystem.ChangeState(Time.timeScale == 0 ? GameUISystem.UIState.PAUSE : GameUISystem.UIState.HUD);
    }
}
