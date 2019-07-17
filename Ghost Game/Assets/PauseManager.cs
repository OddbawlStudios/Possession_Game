using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour {

    public bool paused;
    public Text curhp, speed, moveUsed;
    public GameObject player;
    public GameObject canvas;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {

        curhp.text = "HP: " + player.GetComponent<PlayerController>().tempHp + " / " + player.GetComponent<PlayerController>().tempMax;
        speed.text = "Speed: " + player.GetComponent<PlayerController>().moveSpeed;
        moveUsed.text = "Move Used: " + player.GetComponent<PlayerController>().move;
        EnablePause();
        if(Input.GetKeyDown(KeyCode.T))
        {
            paused = !paused;
        }



    }

    public void EnablePause()
    {
        if (paused)
        {
            canvas.SetActive(true);
        }
        else
        {
            canvas.SetActive(false);
        }
    }
}
