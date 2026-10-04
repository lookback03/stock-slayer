// Sanitized production-derived sample from Stock Slayer.
// Identifiers, credentials, vendor-specific details, and unrelated
// product logic were removed or simplified for public technical review.

#if UNITY_WEBGL && !UNITY_EDITOR
using System;
using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine;

public class FirestoreWebGLSample : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void Portfolio_CheckCoupon(string uid, string couponCode);

    private Action<bool, string, double> activeCallback;

    public IEnumerator CheckCouponRoutine(
        string uid, string couponCode, Action<bool, string, double> onResult)
    {
        if (string.IsNullOrEmpty(uid))
        {
            onResult?.Invoke(false, "Identity unavailable", 0);
            yield break;
        }

        activeCallback = onResult;
        Portfolio_CheckCoupon(uid, couponCode);
        yield break;
    }

    public void OnCouponResult(string json)
    {
        if (activeCallback == null) return;
        try
        {
            CouponResult result = JsonUtility.FromJson<CouponResult>(json);
            activeCallback.Invoke(result.success, result.message, result.rewardAmount);
        }
        catch (Exception)
        {
            activeCallback.Invoke(false, "Coupon processing failed", 0);
        }
        finally
        {
            activeCallback = null;
        }
    }

    [Serializable]
    private class CouponResult
    {
        public bool success;
        public string message;
        public double rewardAmount;
    }
}
#endif
