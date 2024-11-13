using UnityEngine;

public class ExitGame : MonoBehaviour
{
    public void ExitGameMethod()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

}
