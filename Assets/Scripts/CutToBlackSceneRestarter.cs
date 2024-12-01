using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutToBlackSceneRestarter : MonoBehaviour
{
    public AudioSource deathAudioSource;
    public GameObject blackScreenPanel;

    public void CutToBlackAndRestartScene()
    {
        blackScreenPanel.SetActive(true);
        deathAudioSource.Play();

        var waitForSecondsJob = WaitForSeconds(1f).Then(RestartScene);
        StartCoroutine(waitForSecondsJob);
    }

    private IEnumerator WaitForSeconds(float waitSeconds)
    {
        var timeElapsed = 0f;

        while (timeElapsed < waitSeconds)
        {
            timeElapsed += Time.deltaTime;
            yield return null;
        }
    }

    private void RestartScene()
    {
        var activeSceneBuildIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(activeSceneBuildIndex);
    }
}

static class EnumeratorExtension
{
    public static IEnumerator Then(this IEnumerator currentEnumerator, Action followupAction)
    {
        while (currentEnumerator.MoveNext())
        {
            yield return currentEnumerator.Current;
        }

        followupAction();
    }
}