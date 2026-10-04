# 코드 샘플

게임에서 사용한 객체 풀, WebGL 연동, 세션 저장, 보상 광고 흐름을 좁게 발췌했다. 각 README는 해당 코드가 플레이와 플랫폼 연동에서 맡는 역할을 설명한다.

| 주제 | 코드 | 살펴볼 흐름 |
|---|---|---|
| [객체 수명](performance/README.md) | [ObjectPoolSample.cs](performance/ObjectPoolSample.cs) | prefab별 대여·반환과 파괴된 항목 처리 |
| [WebGL 왕복](platform/README.md) | [C#](platform/FirestoreWebGLSample.cs) · [jslib](platform/FirebaseBridge.sample.jslib) | 쿠폰 callback과 호스트 준비 polling |
| [세션 저장](persistence/README.md) | [SessionPersistenceSample.cs](persistence/SessionPersistenceSample.cs) | 투자금 backup과 다음 초기화의 회수 |
| [보상 광고](monetization/README.md) | [RewardAdLifecycleSample.cs](monetization/RewardAdLifecycleSample.cs) | 보상 획득·닫힘·재로드와 구독 해제 |

[게임 구조](../docs/ARCHITECTURE.md) · [공개 범위](../docs/SECURITY_AND_REDACTION.md)
