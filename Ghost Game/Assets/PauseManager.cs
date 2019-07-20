using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour {

    public bool paused;
    public Text curhp, speed, moveUsed, R,G,B;
    public GameObject player;
    public GameObject canvas;

    public float rValue;
    public float gValue;
    public float bValue;
    public int ColorChoice;

    public float timeElapsed;
    public float waitTime;
    public float amount;

    public Vector4 co;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        curhp.text = "HP: " + player.GetComponent<HealthManager>().tempHP + " / " + player.GetComponent<HealthManager>().tempMax;
        //speed.text = "Speed: " + player.GetComponent<Moving>().moveSpeed;
        //moveUsed.text = "Move Used: " + player.GetComponent<PlayerController>().move;
        EnablePause();
        if(Input.GetKeyDown(KeyCode.T))
        {
            paused = !paused;
        }

        if(paused)
        {
            Counter();
            R.text = "R: " + rValue.ToString();
            G.text = "G: " + gValue.ToString();
            B.text = "B: " + bValue.ToString();

            ChangeColor();

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

    public void ChangeColor()
    {
        player.GetComponent<SpriteRenderer>().color = new Color(rValue / 255f, gValue / 255f, bValue / 255f, 1f);
    }


    public void Counter()
    {
        //colorchoice
        if(ColorChoice <= 0)
        {
            ColorChoice = 0;
        }
        if (ColorChoice >= 2)
        {
            ColorChoice = 2;
        }
        //rvalue
        if (rValue <= 0)
        {
            rValue = 0;
        }
        if (rValue >= 255)
        {
            rValue = 255;
        }
        //gvalue
        if (gValue <= 0)
        {
            gValue = 0;
        }
        if (gValue >= 255)
        {
            gValue = 255;
        }
        //bvalue
        if (bValue <= 0)
        {
            bValue = 0;
        }
        if (bValue >= 255)
        {
            bValue = 255;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && ColorChoice >= 0)
        {
            ColorChoice--;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) && ColorChoice <= 2)
        {
            ColorChoice++;
        }

        if(ColorChoice == 0)
        {
            if(Input.GetKey(KeyCode.LeftArrow) && rValue>= 0)
            {
                DecreaseRed();
            }
            if (Input.GetKey(KeyCode.RightArrow) && rValue <= 255)
            {
                IncreaseRed();
            }

        }
        if (ColorChoice == 1)
        {
            if (Input.GetKey(KeyCode.LeftArrow) && gValue >= 0)
            {
                DecreaseGreen();
            }
            if (Input.GetKey(KeyCode.RightArrow) && gValue <= 255)
            {
                IncreaseGreen();
            }
        }
        if (ColorChoice == 2)
        {
            if (Input.GetKey(KeyCode.LeftArrow) && bValue >= 0)
            {
                DecreaseBlue();
            }
            if (Input.GetKey(KeyCode.RightArrow) && bValue <= 255)
            {
                IncreaseBlue();
            }
        }

    }

    public void IncreaseRed()
    {
        if(timeElapsed >= amount)
        {
            rValue++;
            timeElapsed = 0f;

        }else
        {
            timeElapsed += Time.deltaTime;
        }
    }

    public void DecreaseRed()
    {
        if (timeElapsed >= amount)
        {
            rValue--;
            timeElapsed = 0f;

        }
        else
        {
            timeElapsed += Time.deltaTime;
        }
    }

    public void IncreaseGreen()
    {
        if (timeElapsed >= amount)
        {
            gValue++;
            timeElapsed = 0f;

        }
        else
        {
            timeElapsed += Time.deltaTime;
        }
    }

    public void DecreaseGreen()
    {
        if (timeElapsed >= amount)
        {
            gValue--;
            timeElapsed = 0f;

        }
        else
        {
            timeElapsed += Time.deltaTime;
        }
    }

    public void IncreaseBlue()
    {
        if (timeElapsed >= amount)
        {
            bValue++;
            timeElapsed = 0f;

        }
        else
        {
            timeElapsed += Time.deltaTime;
        }
    }

    public void DecreaseBlue()
    {
        if (timeElapsed >= amount)
        {
            bValue--;
            timeElapsed = 0f;

        }
        else
        {
            timeElapsed += Time.deltaTime;
        }
    }

    public void AdjustRed(float newRed)
    {
        rValue = newRed;
    }

    public void AdjustGreen(float newGreen)
    {
        gValue = newGreen;
    }

    public void AdjustBlue(float newBlue)
    {
        bValue = newBlue;
    }


}
