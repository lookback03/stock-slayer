# Reward Advertisement Lifecycle

## 역할

보상 광고를 부활과 재화 지급에 연결한다. Google Play는 AdMob, Appintoss는 플랫폼 광고를 사용하므로 공급자별 이벤트와 구독 수명을 다룬다.

## 구현

보상 획득에서 flag를 세우고 화면이 닫힐 때 게임 callback을 호출한다. 보상 획득과 닫힘이 서로 다른 이벤트이기 때문이다.

native는 이전 광고를 파괴하고 다시 로드하며 닫힘·실패 처리를 `Update`의 Action으로 전달한다. WebGL은 load/show 구독 handle을 교체하고 `OnDestroy`에서 해제한다. 광고 이후에는 오디오와 로드 상태를 복원한다.

실제 placement를 대체하고 WebGL의 유형별 dictionary를 한 보상 유형으로 줄였다. 전면 광고·mock·focus/visibility 처리는 생략했고 제품 popup은 warning으로 바꿨다.

## 코드

[RewardAdLifecycleSample.cs](RewardAdLifecycleSample.cs)