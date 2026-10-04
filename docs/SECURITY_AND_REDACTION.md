# 소스 공개 범위

이 저장소는 게임 화면, 기술 문서와 정제한 코드 발췌로 Stock Slayer의 제작 과정을 소개한다. 전체 게임을 재빌드할 수 있는 Unity 프로젝트는 제공하지 않는다.

## 제외한 자료

- 전체 게임 소스, 씬·prefab·전체 자산과 빌드 산출물
- Firebase 구성, 실제 광고 ID, 운영·배포 설정, 서명 자료
- 계정·사용자 ID·기록·원시 분석 데이터
- Appintoss SDK, Firebase SDK, Google Mobile Ads SDK, DOTween 등 공급자 구현
- Unity Asset Store 패키지, 외부 SFX·음악·VFX·폰트 원본과 편집 파일

## 코드 샘플

class·호스트 객체·export·receiver·저장 key·광고 식별자를 바꾸고 제품 밸런스·기록·보너스·대형 UI 흐름을 생략했다. 게임에서 사용한 제어 흐름은 유지했다.

샘플은 출시 파일과 동일하지 않으며 일부 호출 대상과 SDK가 없어 그대로 실행할 수 없다. 로컬 저장 구현과 호스트의 Firebase 구성·CRUD·인증은 포함하지 않는다. [샘플별 범위](../samples/README.md)

게임 화면과 DAU 집계 캡처는 제품과 운영 경험을 설명하기 위한 자료다. 공개 스토어 링크 외에 운영 설정이나 사용자별 데이터는 제공하지 않는다. [이용 안내](../LICENSE.md)
