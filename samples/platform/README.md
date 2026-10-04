# Unity ↔ JavaScript WebGL Bridge

## 역할

Unity에서 보낸 쿠폰 요청을 호스트 JavaScript에서 처리하고 JSON 결과를 돌려받는다. Appintoss 이식 과정에서 호출 이름과 Firebase 연동 로직을 맞춰야 했던 부분이다.

## 구현

C#은 빈 UID를 검사하고 callback을 보관한다. JSON 응답을 파싱해 callback을 호출한 뒤 `finally`에서 해제한다. `.jslib`는 500 ms 간격으로 호스트 준비를 확인하고, 20회 지연 재확인 후에도 준비되지 않으면 쿠폰 요청에 실패 응답을 보낸다. 닉네임 변경은 같은 상황에서 조용히 종료한다.

| 방향 | 샘플의 호출 계약 |
|---|---|
| C# → jslib | `Portfolio_CheckCoupon(uid, couponCode)` |
| jslib → 호스트 | `window.PortfolioBackend.checkAndUseCoupon(uid, couponCode)` |
| 호스트 → Unity | GameObject `PortfolioFirestoreBridge`, method `OnCouponResult`, JSON `success/message/rewardAmount` |
| 닉네임 변경 | `Portfolio_UpdateNickname` → `updateNickname`. C# 호출부 생략 |

C# 외부 함수·JS export와 응답을 받는 GameObject·method 이름이 일치해야 한다. 호스트가 성공 응답을 전달하고, C# component는 위 이름의 GameObject에 붙는다.

호스트·export·receiver 이름을 바꾸고 UID를 인자로 전달하도록 단순화했다. `PortfolioBackend`는 바꾼 호스트 이름이며 실제 CRUD·Firebase 구성·인증 구현은 포함하지 않는다.

## 코드

[FirestoreWebGLSample.cs](FirestoreWebGLSample.cs) · [FirebaseBridge.sample.jslib](FirebaseBridge.sample.jslib)