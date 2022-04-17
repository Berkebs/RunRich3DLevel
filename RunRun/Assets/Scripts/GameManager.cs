using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Player Playercs;

    public GameObject FinishPanel;
    public TextMeshProUGUI FinishText,FinishMoneyText;
    public void GoScene(int SceneNumber) 
    {
        SceneManager.LoadScene(SceneNumber);
    }

    public void Defeat(int Money) 
    {
        Playercs.enabled = false;
        FinishText.text = "Defeat";
        FinishMoneyText.text = Money.ToString();
        FinishPanel.SetActive(true);
    }
    public void Finish(int Money) 
    {
        Playercs.enabled = false;
        FinishText.text = "Victory";
        FinishMoneyText.text=Money.ToString();
        FinishPanel.SetActive(true);
    }

}
