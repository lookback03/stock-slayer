# 제작과 운영 기록

캔들 베기와 투자 정산을 중심으로 게임을 만들고, QA 과정에서 기능을 추가하며 Android와 WebGL 출시를 준비했다. 출시 후에는 사용자 반응과 기기별 문제를 보며 패치했다.

## 개발에서 운영까지

| 시기·단계 | 작업 |
|---|---|
| 2026년 1월 | 개발 시작 |
| 2026년 5월 | MVP와 QA. 테스트하면서 버그 수정과 기능 추가를 병행 |
| 플랫폼 통합 | Android의 Firebase·AdMob, WebGL의 Appintoss SDK·JavaScript bridge 연결 |
| 출시 | Google Play Android(2026.05.16) 와 Appintoss WebGL(2026.05.28) 로 공개 |
| 출시 후 이용 | Appintoss에서 초기 이용이 늘었고 Peak DAU 약 181명 기록 (2026.06.01 ~ 2026.07.31) |
| 플랫폼 대응 | SDK 호출·Safe Area 문제 수정, 이미지·음원 용량 조정 |
| 업데이트 | 사용자 반응을 보고 패치. 시작·로딩 문제 대응 중 Addressables 시도 후 롤백 |
| 이후 운영 | 초기 이용 구간 이후 이용 감소, 유지보수 진행. Google Play에는 최신 기능 일부 미반영 |

## 플랫폼을 연결하며 했던 작업

| 문제 | 대응 | 결과 |
|---|---|---|
| Appintoss 함수명 불일치·Firebase 연동 실패 | 문서를 다시 확인하고 호출 이름·연동 로직 수정 | 정상 연동 확인 |
| 플랫폼 빌드 용량 초과 | sprite·음원 압축 조정과 atlas 적용 | 허용 용량 안에서 빌드 성공 |
| 모바일 UI 잘림 | 테스트기로 Safe Area를 확인하며 anchor 조정 | 테스트 화면의 잘림 수정 |

Appintoss 이식은 SDK를 연결하는 것 외에도 호출 이름, Firebase 연동, UI 배치와 빌드 용량을 맞추는 작업이 필요했다. QA와 수정은 출시 전 한 번으로 끝내지 않고 패치 과정에서도 반복했다.

## 운영 중의 시작·로딩 문제

일부 Android/iOS 기기에서는 흰 화면과 재시작이 발생했다. Memory Profiler로 관찰하고 Addressables를 시도했지만 로딩이 느려져 롤백했다. 빌드 용량 조정과 실행 중 메모리 문제는 별도로 다뤘으며, 종료 원인은 규명하지 못했다. [메모리 조사 기록](postmortems/IOS_WEBGL_OOM.md)

[운영 지표](METRICS.md) · [수동 출시 과정](RELEASE_PIPELINE.md)

