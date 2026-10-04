// Sanitized production-derived sample from Stock Slayer.
// Identifiers, credentials, vendor-specific details, and unrelated
// product logic were removed or simplified for public technical review.

using System;
using UnityEngine;

public class SessionPersistenceSample : MonoBehaviour
{
    public static SessionPersistenceSample Instance;
    public static Action OnUIUpdated;
    private const string BalanceKey = "portfolio_balance";
    private const string BackupKey = "portfolio_session_backup";
    public double startingBalance; // Product balance value omitted.
    public double totalAsset;
    public double currentCash;
    public double principal;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
        totalAsset = SampleLocalStore.Read(BalanceKey, startingBalance);
    }

    private async void Start()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        try
        {
            string stored = await AppsInToss.AIT.StorageGetItem(BalanceKey);
            if (!string.IsNullOrEmpty(stored) && double.TryParse(stored, out double balance))
            {
                totalAsset = balance;
                SampleLocalStore.Write(BalanceKey, totalAsset);
            }
            else
            {
                totalAsset = SampleLocalStore.Read(BalanceKey, startingBalance);
                await AppsInToss.AIT.StorageSetItem(BalanceKey, totalAsset.ToString());
            }

            string backup = await AppsInToss.AIT.StorageGetItem(BackupKey);
            if (!string.IsNullOrEmpty(backup))
            {
                if (double.TryParse(backup, out double cash))
                {
                    totalAsset += cash;
                    SampleLocalStore.Write(BalanceKey, totalAsset);
                    await AppsInToss.AIT.StorageSetItem(BalanceKey, totalAsset.ToString());
                }
                await AppsInToss.AIT.StorageRemoveItem(BackupKey);
            }
            OnUIUpdated?.Invoke();
        }
        catch (Exception error) { Debug.LogWarning(error.Message); }
#else
        OnUIUpdated?.Invoke();
#endif
    }

    public async void SaveAsset()
    {
        SampleLocalStore.Write(BalanceKey, totalAsset);
        PlayerPrefs.Save();
#if UNITY_WEBGL && !UNITY_EDITOR
        try
        {
            await AppsInToss.AIT.StorageSetItem(BalanceKey, totalAsset.ToString());
        }
        catch { }
#endif
    }

    public async void StartInvest(double amount)
    {
        if (amount > totalAsset) amount = totalAsset;
        if (amount < 0) amount = 0;
        principal = amount;
        currentCash = principal;
        totalAsset -= principal;
        SaveAsset();
#if UNITY_WEBGL && !UNITY_EDITOR
        try
        {
            await AppsInToss.AIT.StorageSetItem(BackupKey, currentCash.ToString());
        }
        catch { }
#endif
        OnUIUpdated?.Invoke();
    }

    public async void FinishInvest()
    {
        double rawProfit = currentCash - principal;
        double finalProfit = rawProfit; // Product shop bonus calculation omitted.
        currentCash = principal + finalProfit;
        totalAsset += currentCash;
        SaveAsset();
#if UNITY_WEBGL && !UNITY_EDITOR
        try { await AppsInToss.AIT.StorageRemoveItem(BackupKey); }
        catch { }
#endif
        OnUIUpdated?.Invoke();
    }
}
