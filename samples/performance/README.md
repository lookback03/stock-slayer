# Object Lifetime / Pooling

## 역할

반복해서 등장하는 타깃·조각·일부 효과를 prefab별 풀에서 재사용한다. 일부 일회성 효과는 기존 생성·파괴 흐름을 유지했다.

## 구현

prefab ID별 Queue에서 객체를 꺼내고, 풀이 비면 새로 생성한다. 대기 중 외부에서 파괴된 항목은 건너뛴다. 비활성 객체의 중복 반환은 무시하고 미등록 객체는 파괴한다.

상태·물리·tween reset은 Target이 처리한다. 발췌에서는 클래스·필드 이름을 바꾸고 Inspector prewarm과 지연 반환을 생략했다.

## 코드

[ObjectPoolSample.cs](ObjectPoolSample.cs)