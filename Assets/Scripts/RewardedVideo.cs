using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class RewardedVideo : MonoBehaviour, IUnityAdsListener
{
    public static RewardedVideo instance;
    public string gameID = "123123";
    public string placementID = "videoId";
    public bool testMode;

    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        Advertisement.Initialize(gameID, testMode);
    }

    IEnumerator ShowVideo()
    {
        while (!Advertisement.IsReady(placementID))
        {
            yield return new WaitForSeconds(0.5f);
            Debug.Log("Video Waiting");
        }
        Debug.Log("Video Ready");
        Advertisement.Show(placementID);
    }

    public void IUnityAdsListener(string placementID, ShowResult showResult)
    {
        if (showResult == ShowResult.Finished)
        {

        }
        else if (showResult == ShowResult.Skipped)
        {

        }
        else if (showResult == ShowResult.Failed)
        {

        }
    }

    public void OnUnityAdsReady(string placementId)
    {
        if (placementID == placementId)
            Advertisement.Show(placementID);
    }

    public void OnUnityAdsDidError(string message)
    {
        Debug.LogError(message);
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        // Evento que detecta ek inicio del video
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult result)
    {
        if (placementID == placementId)
        {
            if (result == ShowResult.Finished)
            {
                DataLoader.instance.currentPlayer.lives++;
            }
        }
    }

    public void ShowVideoForLifes()
    {
        StartCoroutine(ShowVideo());
    }
}
