// Sanitized production-derived sample from Stock Slayer.
// Identifiers, credentials, vendor-specific details, and unrelated
// product logic were removed or simplified for public technical review.

mergeInto(LibraryManager.library, {
    Portfolio_UpdateNickname: function(uidPtr, nicknamePtr) {
        var uid = UTF8ToString(uidPtr);
        var nickname = UTF8ToString(nicknamePtr);
        var retryCount = 0;

        var tryUpdate = function() {
            if (window.PortfolioBackend) {
                window.PortfolioBackend.updateNickname(uid, nickname);
            } else {
                retryCount++;
                if (retryCount > 20) return;
                setTimeout(tryUpdate, 500);
            }
        };

        tryUpdate();
    },

    Portfolio_CheckCoupon: function(uidPtr, couponCodePtr) {
        var uid = UTF8ToString(uidPtr);
        var couponCode = UTF8ToString(couponCodePtr);
        var retryCount = 0;

        var tryCheck = function() {
            if (window.PortfolioBackend && window.PortfolioBackend.checkAndUseCoupon) {
                window.PortfolioBackend.checkAndUseCoupon(uid, couponCode);
            } else {
                retryCount++;
                if (retryCount > 20) {
                    if (window.unityInstance) {
                        window.unityInstance.SendMessage(
                            "PortfolioFirestoreBridge",
                            "OnCouponResult",
                            JSON.stringify({
                                success: false,
                                message: "Host bridge unavailable",
                                rewardAmount: 0
                            })
                        );
                    }
                    return;
                }
                setTimeout(tryCheck, 500);
            }
        };

        tryCheck();
    }
});
