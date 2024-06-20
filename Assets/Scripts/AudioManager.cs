using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private bool isMuted = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M)) // กดปุ่ม M เพื่อปิดหรือเปิดเสียง
        {
            ToggleMute();
        }
    }

    public void ToggleMute()
    {
        isMuted = !isMuted;
        AudioListener.volume = isMuted ? 0 : 1;
        Debug.Log("Audio " + (isMuted ? "Muted" : "Unmuted"));
    }
}
