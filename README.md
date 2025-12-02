# UnityRehab
### 친치로 게임 만들기
**25.11.24.**
1. 주사위 굴리기 및 바닥면 감지를 통한 윗면 출력 기능 구현
2. 이전에 C++로 구현한 친치로 코드를 개선시키고 씬에 주사위를 3개로 늘려서 실행 예쩡
3. 주사위 낙하 속도에 관해 조정 필요

**25.11.25.**
1. 주사위 2개 추가
2. 친치로(영문 'Cee-lo') 족보에 따라 주사위 눈 패턴 감지
   및 이전 결과를 출력하는 UI가 초기화 되도록 하는 스크립트 'DiceManager.cs' 추가
3. 주사위를 던진 직후 주사위가 바닥에서 떨어지는 순간에 주사위 눈을 인식하는 문제 수정

TODO: 점수판 추가, 리롤 횟수 감지(+UI)
<img width="1580" height="886" alt="image" src="https://github.com/user-attachments/assets/8d4ef23c-56d9-4636-8010-aeb190e20fc5" />

**25.11.28.**
1. 스크립트 'ScoreManager.cs', 'GameManager.cs' 추가, 점수판 UI를 만들고 GameManager를 통해 버튼의 온 클릭 이벤트 리스트 정리
2. 주사위 눈 패턴에 맞는 역이 없을 경우에 리롤하는 횟수를 감지해서 3번 이상일 경우 패배했다는 이미지를 화면 가운데 출력

TODO: 코드 정리 필요, 재시작, 종료 등 행동에 대한 화면 출력 구상

<img width="561" height="314" alt="image" src="https://github.com/user-attachments/assets/bf1b4a4a-ea95-4f9f-b926-2b0f4eed0996" />
주사위 눈에 맞는 역이 있을 경우

<img width="565" height="316" alt="image" src="https://github.com/user-attachments/assets/21a4e2c2-6861-4283-96c4-3c584bcbff43" />
주사위 눈에 맞는 역이 없을 경우

<img width="566" height="314" alt="image" src="https://github.com/user-attachments/assets/b307840d-a004-4b16-b84a-c7698dfdce52" />
리롤 3회 이상이 됐을 경우

**25.12.02.**
1. DiceManager에서 점수에 직접적으로 영향을 주지 않고 GameManager를 경유하도록 코드 수정
2. 역 없음이 3번 나올 경우 화면 중앙에 최종 점수와 재시작, 종료 버튼을 출력

TODO: 주사위 회전토크 또는 투명 벽 범위 수정(xz평면에서 너무 튀어나감), 종료화면 출력 시 주사위 굴리는 버튼 상호작용 막기

에디터 초기화면 - 플레이 시 중앙 화면 비활성화
<img width="1579" height="882" alt="image" src="https://github.com/user-attachments/assets/327498e1-6656-4775-bf1e-4fb935d00736" />

현재 주사위 눈 패턴과 맞는 역이 있으면 그에 해당하는 점수 추가 후 패널 출력, 없으면 역 없음 표시 후 내부 카운팅 변수에 +1
![diceRolling](https://github.com/user-attachments/assets/04f31273-99ba-412e-adea-72df76eb24bb)

역 없음 3회 시 종료 화면 출력 아래 gif는 재시작 클릭(재시작 시 내부 카운팅 변수 0으로 초기화)
![Retry](https://github.com/user-attachments/assets/e64c844e-9e38-4977-ba6b-460fc6340970)

위와 동일하나 종료 버튼 클릭, 에디터 종료(#if #endif로 에디터일 경우와 아닐 경우로 분리)
![exit](https://github.com/user-attachments/assets/4e0bb58e-bff5-4007-bf50-61869dd5ef60)
