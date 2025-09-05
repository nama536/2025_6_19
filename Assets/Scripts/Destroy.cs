using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class Destroy : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    private int score;


    //��O�ɏo���I�u�W�F�N�g����������������

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Candy")
        {
            Destroy(other.gameObject);
            score++;
            scoreText.text = "score:" + score.ToString();
        }
    }

    void Start()
    {
        scoreText.text = "score:" + score.ToString();
    }

}
