# 기술 스택

Unity의 2D 물리·UI·coroutine으로 게임을 구성하고, Android와 WebGL의 인증·DB·광고 API를 플랫폼별로 연결했다.

## 게임과 플랫폼

| 기술 · 개발 환경 버전 | 사용한 부분 |
|---|---|
| Unity 2022.3.62f3 / C# | 씬·2D 물리·UI·coroutine·오디오 |
| JavaScript / WebGL / `.jslib` | Unity와 호스트 간 호출, Cloud Firestore 연동 |
| Appintoss Unity SDK 2.4.7 | 게임 사용자 키·저장·광고·Safe Area·햅틱·공유·랭킹 |
| Firebase Unity SDK 13.9.0 | Android Firebase Auth·Cloud Firestore, Firebase Analytics  |
| Firebase JavaScript SDK | WebGL 호스트의 Cloud Firestore 호출 |
| Google Mobile Ads Unity 11.0.0 / Android ads 25.0.0 | AdMob 보상·전면 광고 |
| UGUI 1.0.0 / TextMesh Pro 3.0.7 | 메뉴·HUD·결과와 텍스트 UI |
| Post Processing 3.4.0 / DOTween | blur·tween·타깃 상태 복원 |
| Memory Profiler 1.1.12 | 시작 시 메모리 관찰 |
| Gradle / Vite / TypeScript | Android 의존성과 WebGL 호스트 빌드 |
| Rider | Unity/C# 개발 |

## 연동 방식

Android는 Firebase Unity SDK를 사용한다. WebGL에서는 REST로 조회하고 JavaScript bridge를 통해 쓰기를 요청한다. Appintoss SDK는 플랫폼 기능을 제공하고, 게임 코드가 callback·저장·UI를 연결한다.

Firebase Analytics는 사용자 데이터 분석에 사용했다. 별도의 custom event 로깅 wrapper는 작성하지 않았다.

## 제작 도구

아트 초안은 AI로 초안을 만들고 Affinity Designer에서 재작화했다. UI 제작 및 배치와 export에는 Figma, 효과음 편집에는 Audacity를 사용했다. [아트·음원·VFX 제작](ART_AND_ASSETS.md)
