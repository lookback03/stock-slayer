// Sanitized production-derived sample from Stock Slayer.
// Identifiers, credentials, vendor-specific details, and unrelated
// product logic were removed or simplified for public technical review.

using System;
using UnityEngine;
#if !UNITY_WEBGL || UNITY_EDITOR
using GoogleMobileAds.Api;
#endif

public class RewardAdLifecycleSample : MonoBehaviour
{
    public static RewardAdLifecycleSample Instance;
    private const string Placement = "REDACTED_REWARD_PLACEMENT";
    private Action pendingReward;
    private Action mainThreadAction;
    private bool rewardEarned;
    private bool isAdPlaying;
#if !UNITY_WEBGL || UNITY_EDITOR
    private RewardedAd rewardedAd;
#else
    private bool loaded;
    private Action loadUnsubscribe;
    private Action showUnsubscribe;
#endif

    private void Awake()
    {
        if (Instance == null) { Instance = this; DontDestroyOnLoad(gameObject); }
        else { Destroy(gameObject); return; }
    }

    private void Start()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        MobileAds.Initialize(_ => { mainThreadAction = LoadRewardAd; });
#else
        LoadRewardAd();
#endif
    }

    private void Update()
    {
        if (mainThreadAction == null) return;
        mainThreadAction.Invoke();
        mainThreadAction = null;
    }

    private void OnApplicationPause(bool paused)
    {
        if (!paused && isAdPlaying) MuteAudio();
    }

    private void MuteAudio() { AudioListener.pause = true; AudioListener.volume = 0f; }
    private void RestoreAudio()
    {
        isAdPlaying = false;
        AudioListener.pause = false;
        AudioListener.volume = 1f;
    }

    public void LoadRewardAd()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        if (rewardedAd != null) { rewardedAd.Destroy(); rewardedAd = null; }
        RewardedAd.Load(Placement, new AdRequest(), (ad, error) =>
        {
            if (error != null || ad == null) return;
            rewardedAd = ad;
            ad.OnAdFullScreenContentClosed += () =>
            {
                mainThreadAction = () =>
                {
                    RestoreAudio();
                    if (rewardEarned)
                    {
                        rewardEarned = false;
                        pendingReward?.Invoke();
                    }
                    LoadRewardAd();
                };
            };
            ad.OnAdFullScreenContentFailed += errorOnShow =>
            {
                mainThreadAction = () => { RestoreAudio(); LoadRewardAd(); };
            };
        });
#else
        loadUnsubscribe?.Invoke();
        loadUnsubscribe = AppsInToss.AIT.LoadFullScreenAd(
            Placement,
            adEvent =>
            {
                if (adEvent.Type == "loaded") loaded = true;
                else if (adEvent.Type == "failedToLoad") loaded = false;
            },
            error => { loaded = false; });
#endif
    }

    public bool ShowRewardAd(Action onReward)
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            Debug.LogWarning("Ad unavailable");
            return false;
        }
        pendingReward = onReward;
        rewardEarned = false;
#if !UNITY_WEBGL || UNITY_EDITOR
        if (rewardedAd != null && rewardedAd.CanShowAd())
        {
            isAdPlaying = true;
            MuteAudio();
            rewardedAd.Show(reward => { rewardEarned = true; });
            return true;
        }
        LoadRewardAd();
        Debug.LogWarning("Ad unavailable");
        return false;
#else
        if (!loaded)
        {
            LoadRewardAd();
            Debug.LogWarning("Ad unavailable");
            return false;
        }
        isAdPlaying = true;
        MuteAudio();
        showUnsubscribe?.Invoke();
        showUnsubscribe = AppsInToss.AIT.ShowFullScreenAd(
            Placement,
            adEvent =>
            {
                switch (adEvent.Type)
                {
                    case "userEarnedReward": rewardEarned = true; break;
                    case "dismissed":
                        RestoreAudio();
                        if (rewardEarned)
                        {
                            rewardEarned = false;
                            pendingReward?.Invoke();
                        }
                        loaded = false;
                        LoadRewardAd();
                        break;
                    case "failedToShow":
                        RestoreAudio();
                        loaded = false;
                        LoadRewardAd();
                        Debug.LogWarning("Ad unavailable");
                        break;
                }
            },
            error =>
            {
                RestoreAudio();
                loaded = false;
                LoadRewardAd();
                Debug.LogWarning("Ad unavailable");
            });
        return true;
#endif
    }

    private void OnDestroy()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        loadUnsubscribe?.Invoke();
        showUnsubscribe?.Invoke();
#endif
    }
}
