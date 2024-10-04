using UnityEngine;
using UnityEngine.SceneManagement;

namespace MyBird
{
    public class PlayUI : MonoBehaviour
    {
        private void Update()
        {
            if(Input.GetKeyUp(KeyCode.P))
            {
                PlayerPrefs.DeleteAll();
            }
        }
        public void Play()
        {
            SceneManager.LoadScene("PlayScene");
        }
    }
}