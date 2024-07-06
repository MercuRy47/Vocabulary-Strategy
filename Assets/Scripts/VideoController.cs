using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class VideoController : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public RawImage rawImage;
    public GameObject objImageToShow;
    public GameObject objImageToShow2;
    public float fadeDuration = 1f;
    public float showDuration = 4f;

    void Start()
    {
        videoPlayer.Play();
        StartCoroutine(PlayVideoAndShowImage());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            BackToHomePage();
        }
    }

    public void BackToHomePage()
    {
        SceneManager.LoadScene("HomePage");
    }

    IEnumerator PlayVideoAndShowImage()
    {

        yield return new WaitForSeconds(showDuration);

        objImageToShow.SetActive(true);

        Image image = objImageToShow.GetComponent<Image>();
        Color startColor = image.color;
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 1f);

        float timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            image.color = Color.Lerp(Color.clear, endColor, timer / fadeDuration);
            yield return null;
        }

        image.color = endColor;

        yield return new WaitForSeconds(3f);

        timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            image.color = Color.Lerp(endColor, Color.clear, timer / fadeDuration);
            yield return null;
        }

        image.color = Color.clear;

        objImageToShow2.SetActive(true);

        Image image2 = objImageToShow2.GetComponent<Image>();
        Color startColor2 = image2.color;
        Color endColor2 = new Color(startColor2.r, startColor2.g, startColor2.b, 1f);

        timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            image2.color = Color.Lerp(Color.clear, endColor2, timer / fadeDuration);
            yield return null;
        }

        image2.color = endColor2;

        yield return new WaitForSeconds(3f);

        timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            image2.color = Color.Lerp(endColor2, Color.clear, timer / fadeDuration);
            yield return null;
        }

        image2.color = Color.clear;

        UnityEngine.SceneManagement.SceneManager.LoadScene("HomePage");
    }
}