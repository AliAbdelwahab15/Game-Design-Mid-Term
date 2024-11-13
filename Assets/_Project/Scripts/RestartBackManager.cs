using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RestartBackManager : MonoBehaviour
{
        public Button backButton;
        public Button restartButton;

        void Start()
        {
            backButton.onClick.AddListener(GoBack);
            restartButton.onClick.AddListener(RestartGame);
            backButton.gameObject.SetActive(false);
            restartButton.gameObject.SetActive(false);
        }

        public void ShowBackButton()
        {
            backButton.gameObject.SetActive(true);
        }

        public void ShowRestartButton()
        {
            restartButton.gameObject.SetActive(true);
        }

        void GoBack()
        {
            SceneManager.LoadScene(0);
        }

        void RestartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }


}
