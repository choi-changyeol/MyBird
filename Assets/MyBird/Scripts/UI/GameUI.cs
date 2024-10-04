using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MyBird
{
    public class GameUI : MonoBehaviour
    {
        #region Variables
        public TextMeshProUGUI scoreText;
        public TextMeshProUGUI bestscoreText;
        public TextMeshProUGUI newText;
        private void OnEnable()
        {

            //게임 데이터 저장
            int bastScore = PlayerPrefs.GetInt("BastScore", 0); //저장된 데이터 가져오기
            if (GameManager.Score > bastScore) //저장된 데이터와 비교하기
            {
                GameManager.BastScore = GameManager.Score;
                PlayerPrefs.SetInt("BastScore", GameManager.Score);
                newText.text = "NEW";
            }
            else if(GameManager.Score == GameManager.BastScore)
            {
                newText.text = "SAME";
            }
            else
            {
                newText.text = "";
            }
            //UI저장
            bestscoreText.text = GameManager.BastScore.ToString();
            scoreText.text = GameManager.Score.ToString();
        }
        #endregion
        public void Retry()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        public void Menu()
        {
            Debug.Log("GotoTitle");
        }
    }
}