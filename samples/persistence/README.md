# Session Save / Recovery Boundary

## 역할

투자 시작에 잔액에서 원금을 빼고 정산 때 최종 현금을 더한다. WebGL에서는 진행 중 투자금 backup을 남겨 세션이 중단된 뒤 다음 초기화 때 회수하는 흐름을 사용한다.

## 구현

호스트 잔액을 우선 읽고 로컬 값을 fallback으로 사용한다. backup이 있으면 잔액에 더한 뒤 삭제한다. 투자 시작에 backup을 쓰고 정산 뒤 제거하며, `SaveAsset()`를 await하지 않는 호출도 유지했다.

저장 key를 바꾸고 제품 보너스·기록·UI·pause/quit 저장은 생략했다. `SampleLocalStore.Read/Write`는 생략한 로컬 호출의 이름이며 구현은 포함하지 않는다. 전체 경제 계산이나 모든 종료 시 저장 경로를 담은 샘플은 아니다.

## 코드

[SessionPersistenceSample.cs](SessionPersistenceSample.cs)