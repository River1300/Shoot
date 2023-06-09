/*
목표 : 게임 준비하기 1


    1. 스프라이트를 담을 폴더 준비하기

    2. 다운 받은 스프라이트 설정
        PixelPerUnit, FilterMode, Compression 설정
        스프라이트 에디터에서 스프라이트 자르기

    3. 플레이어 스프라이트 씬에 배치

    4. 스크립트 폴더 준비하기
*/

/*
목표 : 플레이어 이동 2


    1. 플레이어 스크립트 만들기

    2. 오브젝트에 스크립트 부착

    3. 이동 로직을 위한 필요 속성 : 수평/수직 방향값을 받아서 저장할 변수, 플레이어의 현재 위치, 플레이어의 다음 위치, 속도, 

    4. transform을 활용한 이동 로직 만들기
        미래의 위치는 현재의 위치 + 속도 * 시간
*/

/*
목표 : 해상도 조절 3


    1. 유니티 레이아웃 조정

    2. 9 : 16 비율로 화면 조정
        9 : 19 비율도 가능

    3. 플레이어 위치 재 조정
*/

/*
목표 : 경계 설정 4


    1. 플레이어 오브젝트에 콜라이더 부착
        콜라이더 크기 조절

    2. 빈 오브젝트 만들기
        빈 오브젝트 자식으로 상/하/좌/우 빈 오브젝트 만들기
        자식 오브젝트에게 콜라이더 부착
            콜라이더 크기를 화면 경계선 크기로 조절

    3. 플레이어 오브젝트에 리지드바디 부착
        BodyType Kinematics으로 설정하여 충돌 물리연산을 받지 않도록 한다.
            이러면 스크립트 로직으로만 움직이게 할 수 있다.

    4. 경계선에도 리지드바디 부착
        BodyType Static으로 설정하여 고정한다.
*/

/*
목표 : 경계 로직 5


    1. 플레이어가 경계선에 닿으면 해당 방향으로의 이동 값을 0로 만든다.

    2. 필요 속성 : 상/하/좌/우 경계선에 닿았는지 체크할 bool 변수

    3. 통과 이벤트 함수(OnTrigger)를 만들어서 경계선과의 충돌을 체크한다.
        태그로 경계선과의 충돌을 체크한다.
            스위치 문의 이름으로 어떤 방향 경계선에 닿았는지 체크한다.
    
    4. 경계선에 닿았는데 AxisRaw 값이 1,-1이라면 0으로 만든다.

    5. 경계선에서 벗어낫을 때 통과 이벤트 함수(Exit)만들어서 경계선에서 벗어났는지 체크한다.

    6. 경계선 자식 오브젝트에 태그를 추가해 준다.

    7. 경계선 자식 오브젝트의 콜라이더는 트리거로 전환한다.
*/

/*
목표 : 플레이어의 애니매이션 6


    1. 움직임에 따른 애니매이션 클립을 만든다.

    2. 애니매이터에서 트랜지션을 연결한다.

    3. 정수형 파라미터를 만든다.
        직진은 수평값이 0
        오른쪽은 수평값이 1
        왼쪽은 수평값이 -1

    4. 방향키가 입력됬을 때와 방향키 입력이 끝날 때 파라미터에 값을 전달하면 된다.

    5. 필요 속성 : 애니매이터

    6. 수평값의 키가 눌렸을 때, 때였을 때 파라미터에 수평 값을 전달한다.
*/

/*
목표 : 발사체 구성 7


    1. 씬에 총알 스프라이트 배치

    2. 총알 오브젝트에 콜라이더와 리지드바디 부착
        총알은 AddForce를 활용하여 발사할 예정이기 때문에 BodyType은 Dynamic으로 지정한다.
        총알은 중력의 영향을 받지 않을 것 이기 때문에 중력값 또한 0으로 지정한다.

    3. 총알 오브젝트를 에셋화(프리팹)한다.
        프리팹 폴더를 준비한다.
*/

/*
목표 : 오브젝트 삭제 8


    1. 총알이 쌓이지 않도록 오브젝트 경계선을 만든다.

    2. 총알을 관리할 총알 스크립트를 만든다.

    3. 총알 스크립트에서 경계선과 충돌했을 때 스스로 제거하는 함수(OnTrigger)를 만든다.

    4. 총알 경계선 전용 태그를 만든다.

    5. 씬에 배치해 둔 총알 오브젝트는 지워준다.
*/

/*
목표 : 총알 만들기 9


    1. 총알은 플레이어 스크립트에서 사용하도록 한다.

    2. 필요 속성 : 총알 프리팹을 담을 게임 오브젝트 변수

    3. 총알 발사 로직을 작성하기 전에 이전에 만들어 두었던 이동 로직을 캡슐화 하여 Update()함수를 정리한다.

    4. 발사 함수를 만든다.

    5. Instantiate()를 통해 프리팹에서 총알을 객체화 시킨다.
        그리고 이 객체를 게임 오브젝트 변수로 저장한다.
        플레이어 위치에서 발사하고 플레이어 방향으로 발사한다.
    
    6. 저장된 게임 오브젝트로 부터 Rigidbody2D를 받아온다.
        AddForce로 발사한다.

    7. public 으로 만들었던 속성에 프리팹을 배정한다.

    8. 총알 프리팹에 부착된 콜라이더에 트리거를 체크한다.
*/

/*
목표 : 발사체 다듬기 10


    1. 사용자가 발사 버튼을 눌렀을 때만 총알을 발사하도록 한다.
        발사 버튼을 누르지 않았다면 반환한다.

    2. 총알 발사 딜레이를 준다.

    3. 필요 속성 : 최대 총알 발사 속도와 현재 총알 발사 속도 변수

    4. 장전 함수를 만든다.
        현재 발사 속도에 시간을 매 프레임 마다 더한다.

    5. 다시 발사 함수에서 (현재 발사 속도 < 최대 발사 속도) 이면 총알을 발사하지 않는다.

    6. 총알을 한 발 발사 하였다면 현재 발사 속도를 초기화 한다.
*/

/*
목표 : 더 강한 총알 발사 11


    1. 필요 속성 : 파워를 수치화 한 변수

    2. Fire() 함수로 가서 파워 변수에 따른 각기 다른 총알 발사를 작성한다.

    3. switch 문으로 파워가 1일 때, 2일 때, 3일 때 각기 다른 총알을 발사한다.
        1일 때는 기존 발사체 그대로
        2일 때는 기존 발사체를 두 발 발싸
            객체화 할 때 위치를 수정해 준다.
        3일 때는 더 강한 총알을 추가로 발싸
*/

/*
목표 : 적 기체 준비하기 12


    1. 적 기체들을 씬에 옮긴다.

    2. 각 기체에 콜라이더를 부착한다.
        스프라이트 에디터에서 폴리곤 콜라이더를 위해 PhysicsShape를 수정해 준다.

    3. 각 기체도 움직이는 속력 값이 있기 때문에 리지드바디를 부착한다.
        중력은 받으면 않된다.

    4. 태그를 추가해 준다.
*/

/*
목표 : 적 기초 로직 만들기 13


    1. 스크립트 만들고 부착

    2. 필요 속성 : 속도, 체력, 리지드바디, 피격 상태를 위한 스프라이트 렌더러, 스프라이트를 바꾸기 위한 스프라이트 배열

    3. 리지드바디 + 스프라이트 렌더러를 초기화 한다. 그리고 적 기체가 탄생하면 바로 아래로 내려가도록 속력도 초기화 한다.
        velocity로 속력을 초기화 한다.

    4. 플레이어의 총알을 맞았을 때 호출되는 함수를 만든다.
        매개 변수로 피격 데미지를 받는다.
        체력에서 데미지를 빼준다.
        체력이 0보다 작아진다면 해당 오브젝트를 제거한다.
        스프라이트를 교체한다.

    5. 스프라이트가 원상태로 돌아오는 함수를 만든다.
        스프라이트를 기본으로 교체한다.
        피격 함수에서 Invoke()를 통해 스프라이트 함수를 호출한다.

    6. 충돌 함수(OnTrigger)를 만든다.
        만약 충돌한 콜라이더가 총알 경계선일 경우 오브젝트를 제거한다.
        만약 충돌한 콜라이더가 플레이어 총알이라면 피격 함수를 호출한다.
            피격함수의 매개변수로 데미지를 전달해야 한다.
            총알 스크립트 변수를 만들어서 총알 데미지를 받아온다.
                현재 총알 스크립트에 데미지 속성이 없으므로 추가해 준다.

    7. 적에게 부착된 콜라이더의 트리거를 켜준다.

    8. 플레이어 총알이 적에게 충돌하면 충돌한 총알은 사라져야 한다.
        적이 충돌한 콜라이더가 플레이어 총알이라면 피격 함수를 호출한 뒤 충돌한 오브젝트를 제거한다.

    9. 로직이 모두 완성되었다면 적 오브젝트를 모두 프리팹으로 저장한다.
*/

/*
목표 : 적 기체 생성 14


    1. 게임 매니저를 만든다.

    2. 필요 속성 : 적 공장 배열(3개), 적이 나타날 위치(Transform)배열, 적 생성 시간, 현재 딜레이 시간

    3. 매 프레임마다 현재 딜레이 시간은 계속해서 증가한다.
        만약 딜레이 시간이 적 생성 시간보다 커질 경우 적을 생성한다.
        적을 생성한 뒤에는 딜레이 시간을 초기화 한다.
        적을 한 번 생성하였다면 적 생성 시간을 랜덤 시간으로 초기화 한다.

    4. 적 생성 함수를 만든다.
        3가지 적 중 랜덤한 적을 고른다.
        5가지 소환될 위치도 랜덤한 위치로 고른다.
        Instantiate()를 통해 적을 객체화 한다.

    5. 빈 오브젝트를 만들어서 적이 생성될 위치에 나열한다.
*/

/*
목표 : 적 기체가 사이드에서 생성 15


    1. 씬에 생성 위치를 추가로 만든다.

    2. 랜덤 스폰 포인트의 범위를 수정한다.

    3. 사이드에서 생성된 적은 아래로 내려가면 않된다.
        그러므로 적 스크립트에서 지정 했었던 리지드바디와 속력은 제거한다.
        총알과 마찬가지로 인스턴스화되는 때에 방향과 속도를 지정하여 적을 이동 시킨다.
            적을 인스턴스화 할 때 해당 객체를 저장하고 리지드 바디를 받아온다.
            적 스크립트도 저장하여 속력 속성을 사용하도록 한다.

    4. 사이드 오른쪽일 때 속력 * -1로 x축 방향, 플레이어가 앞으로 전진하는 느낌이 들도록 내려가는 -1 속력을 지정한다.
        반대의 경우도 마찬가지로 지정한다.

    5. 사이드가 아닐 때는 x 축은 0, y축으로 속도를 * 연산하여 적을 이동시킨다.

    6. 사이드에서 날아올 때 객체를 회전 시켜준다.
        Z축으로 Rotate 시킨다. back = -1 이므로 * 90도를 하여 방향을 잡는다.
        반대는 front로 한다.
*/

/*
목표 : 적 기체가 플레이어를 공격 16

    
    1. 플레이어의 총알 프리팹을 복사하여 수정해서 적 총알 프리팹으로 사용한다.

    2. 적 총알 태그를 추가한다.

    3. 필요 속성 : 플레이어가 총알을 발사할 때 필요했던 것, 총알 공장, 총알 딜레이

    4. 플레이어는 버튼을 눌렀을 때만 발사 했지만 적은 자동으로 발사한다.

    5. 플레이어 로직에서 발사 함수와 재장전 함수를 복사하여 수정을 하도록 한다.
        적은 파워 개념이 없으므로 파워에 따른 총알 발사 로직은 모두 지운다.
        적은 버튼을 누를 필요가 없다.
        S타입과 L타입의 공격을 제어문으로 나눈다. 이때 구분은 오브젝트 이름으로 구분하도록 한다.
            속성으로 적 이름을 추가하도록 한다.
        기본 플레이어의 총알 발사 로직에서 어디를 향해 쏠지를 수정해 준다.
            플레이어에게 발사하기 위해서 플레이어 변수가 필요하다.
                그런데 프리팹이기 때문에 public 속성으로 추가해도 프리팹 상태에서는 값을 받을 수 없다.
                때문에 프리팹이 인스턴스화 한 순간에 플레이어 변수를 받아와야 한다.
                    이는 게임 매니저가 해줘야 한다.
                        게임 매니저가 플레이어 오브젝트 변수를 갖는다.
                        적 스크립트에도 똑같이 플레이어 오브젝트 변수를 선언해 둔다.
                        게임 매니저에서 적을 생성하는데 이 때, 생성될 때 우리는 적의 속도를 사용하기 위해 적 스크립트도 받았었다.
                            적 스크립트를 이용해 플레이어 오브젝트 변수를 받아와 게임 매니저와 연동한 플레이어를 넘겨준다.
            목표물 방향 = 목표물 위치 - 나의 위치
                즉, 플레이어의 위치 - 적의 위치가 플레이어를 향하는 방향이다. 이를 벡터로 저장하고 총알 발사 방향으로 지정해 준다.
        S타입은 한발만 발사 하는데 L타입은 두발을 발사하도록 한다.
            플레이어 파워 2 버전을 참고하여 발사한다.
        
    6. 총알이 너무 빠르다.
        좌표값이 포함된 벡터의 크기가 큼으로 일반화가 필요하다.
*/

/*
목표 : 플레이어 피격 17


    1. 플레이어 충돌 함수(OnTriggerEnter2D)에 충돌하는 태그를 제어문에서 추가해준다.

    2. 적 + 적 총알에 충돌하였을 때 플레이어를 비활성화 시킨다.

    3. 플레이어가 비활성화 된 상태에서는 로직이 실행되지 않는다.
        그러므로 게임 매니저에서 플레이어에를 부활시킬 함수를 만든다.
            부활 함수에서 다시 Invke()함수로 부활 실행 함수를 호출한다.
                부활 실행 함수를 만든다.
                플레이어의 위치를 지정해 주고 다시 활성화 시킨다.

    4. 플레이어가 게임 매니저의 활성화 함수를 호출하기 위해서 게임 매니저를 변수로 받는다.
*/

/*
목표 : 목숨과 점수 18


    1. 필요 속성 : 목숨, 점수

    2. 점수를 표시할 Text UI, 목숨을 표시할 Image UI를 만들어 준다.
        Canvas Scaler : UI Scale Mode를 Scale With Screen Size로 변경한다.
            1080 X 1920 Size를 기준으로 잡는다.
    
    3. 게임 오버 Text UI도 만들어 준다.

    4. 재시작 Button UI도 만들어 준다.
        스프라이트 에디터에서 Borader 설정

    5. 게임 오버와 재시작은 빈 오브젝트의 자식으로 묶어서 비활성화 시켜 놓는다.
*/

/*
목표 : UI 로직 19


    1. 적 스크립트에 OnHit()함수를 만들어 두었었다.

    2. 적이 플레이어 총알에 피격될 때 플레이어에게 자신의 점수를 더해 준다.
        적에게 점수 속성을 추가한다.
        플레이어 로직 컴포넌트를 받아온 뒤 점수 속성에 더하기 연산을 한다.

    3. 점수는 게임 매니저에서 출력한다.
        먼저 Text UI를 속성으로 받아온다.
        내친김에 Image UI도 속성으로 받아온다. 이것은 배열이다.
        마지막으로 게임 오버 세트를 GameObject로 받는다.

    4. Update()함수에서 매 프레임 마다 플레이어 로직으로 부터 점수를 받아와 Text로 출력한다.
        string.Format("0:n0", 점수)

    5. 목숨을 잃는 부분은 플레이어 스크립트에 OnTriggerEnter2D에서 적이나 적 총알에 충돌했을 때 이다.
        목숨 속성을 -1 한다.
        게임 매니저에 목숨 이미지를 제거하는 함수를 만들어 준다.
            매개변수로 플레이어 속성인 목숨을 받는다.
            반복문으로 최대 목숨 갯수만큼 알파값을 지워서 투명하게 만들고
            반복문으로 현재 목수 갯수만큼 알파값을 올려서 그려준다.
    
    6. 플레이어의 목숨이 0이된다면?
        게임 매니저에 게임 오버 함수를 만들어 준다.
            만들어 둔 게임 오버 set UI를 활성화 시킨다.

    7. 재시작 버튼을 누르면 씬을 새로 불러오는 함수를 만든다.

    8. 재시작 버튼에 함수를 연결한다.
*/

/*
목표 : 버그( 플레이어가 총알 두개에 동시 피격될 경우 목숨이 한 꺼번에 두 개 깍인다. ) 20

    
    1. 한 발 맞는 순간 해당 로직을 닫는 플래그를 만든다.

    2. 필요 속성 : 피격되었다는 bool 변수

    3. 이미 피격 상태 true라면 로직을 실행하지 않고 바로 반환한다.

    4. 피격 전 상태 false라면 true로 바꾸고 로직을 실행한다.

    5. 게임 매니저에서 플레이어를 다시 활성화 할 때 false로 바꿔준다.
        플레이어 로직을 받아와서 속성을 바꾼다.
*/

/*
목표 : 아이템 배치 21


    1. 아이템 스프라이트 씬에 나열

    2. 아이템에 리지드바디와 콜라이더를 부착한다.
        중력은 0

    3. 아이템 스크립트를 만들고 부착한다.

    4. 필요 속성 : 아이템 이름, 리지드바디

    5. 아이템도 객체 생성과 함께 속도를 초기화 해준다.

    6. 아이템 애니매이션 등록
*/

/*
목표 : 아이템 충돌 로직 22


    1. 아이템 태그 만들기

    2. 플레이어 스크립트에 있는 충돌 로직 OnTriggerEnter2D에서 아이템과 충돌할 경우를 작성한다.

    3. 충돌한 아이템으로 부터 Item 로직을 받아와서 해당 아이템의 이름을 받는다.

    4. switch문으로 아이템 이름에 따라 로직을 작성한다.

    5. 코인을 먹으면 점수를 더한다.

    6. 파워를 먹으면 파워를 더한다.
        파워의 한계가 있기 때문에 최대 파워를 속성으로 추가해 준다.
        만약 파워가 최대 파워와 같다면 파워를 먹으면 점수로 치환된다.

    7. 폭탄을 먹으면 모든 적의 총알이 제거되고 적에게 데미지를 입힌다.
*/

/*
목표 : 폭탄 로직 23


    1. 폭탄 이펙트를 만든다.

    2. 애니매이션 등록

    3. 애니매이션 속도를 높인다.

    4. 알파값을 살짝 조정해 준다.

    5. 비활성화 시켜 둔다.

    6. 필요 속성 : 플레이어가 활성화 할 수 있도록 폭탄 이펙트를 게임 오브젝트로 받는다.

    7. 다시 switch 문으로 돌아가 폭탄 이펙트를 활성화 시킨다.

    8. 게임 오브젝트 배열로 화면 상에 있는 적들을 모두 받아온다.
        GameObject.FindGameObjectsWithTag("Enemy");

    9. 받은 오브젝트 배열을 반복문으로 돌린다.
        각각의 적 스크립트를 받고 OnHit()함수를 호출하여 데미지를 전달한다.

    10. 배열로 적들을 모두 받아왔듯이 적 총알을 받아와서 반복문으로 제거한다.

    11. 폭탄 이펙트를 비활성화 시키는 함수를 만든다.
        Invoke로 비활성화 시킨다.

    12. switch문에서 빠져나와서 충돌한 아이템을 삭제한다.
*/

/*
목표 : 버튼을 눌렀을 때 폭탄 사용 24


    1. 필요 속성 : 최대 폭탄 갯수와 현재 폭탄 갯수, 현재 폭탄을 사용 중인지 확인할 bool

    2. 폭탄 발사 함수를 만든다.

    3. 발사 버튼을 누르지 않았다면 폭탄을 발사하지 않고 반환한다.

    4. 이미 폭탄이 발사 중이라면 마찬가지로 반환한다.

    5. 폭탄의 갯수가 0개이면 마찬가지로 반환한다.

    6. 폭탄을 사용했으므로 현재 폭타의 개수를 한 개 줄인다.

    7. switch문에 적어 두었던 폭탄 기능을 모두 폭탄 발사 함수로 옮긴다.

    8. 기존 switch문에는 power와 동일한 방식으로 폭탄의 갯수를 증가 시킨다.

    9. 폭탄 이펙트를 비활성화 할 때 bool 변수에 false를 준다.

    10. 폭탄 UI를 만든다.

    11. 폭탄 UI 역시 게임 매니저가 그린다.
        Image 배열로 폭탄 이미지를 받는다.

    12. 목숨을 그린 방식과 똑같이 그린다.

    13. 플레이어가 폭탄을 먹었을 때, 사용했을 때 마다 게임 매니저의 폭탄 그리기 함수를 호출한다.

    14. 시작할 떄는 폭탄이 0개 이므로 씬에 그려둔 폭탄 이미지는 알파값을 0으로 둔다.
*/

/*
목표 : 아이템 드랍 25


    1. 아이템 프리펩 화

    2. 적이 죽으면 아이템이 일정 확률로 나온다.

    3. 필요 속성 : 종류별 아이템 프리펩

    4. 적이 죽을 때 랜덤한 숫자를 정수로 받는다.

    5. 확률을 지정하여 아이템을 인스턴스화 한다.

    6. 적이 제거되기 전에 연속해서 여러발을 맞을 경우 아이템을 여러개 생성하는 경우가 생긴다.
        이미 체력이 0보다 낮다면 더이상 OnHit 함수를 실행하지 않고 반환한다.
*/

/*
목표 : 배경 구성하기 26


    1. 배경을 3개의 그룹으로 나누어 관리한다.
        그룹당 같은 이미지를 3개씩 넣는다.

    2. 위 아래로 -10, +10 이미지를 나눈다.

    3. 이미지 스크립트를 만들고 그룹에 부착한다.

    4. 필요 속성 : 속도

    5. 현재 위치와 미래 위치를 이용하여 매 프레임 당 이미지를 아래로 이동 시킨다.

    6. 각각의 속도를 지정해 준다.
*/

/*
목표 : 스크롤링 27


    1. 현재 메인 카메라 사이즈 5, 배경 높이 12

    2. 화면 아래로 내려간 가장 밑의 배경 화면은 맨 위로 붙인다.

    3. 필요 속성 : 배경 스프라이트의 위치 배열, 시작인덱스, 끝 인덱스

    4. 시작 인덱스는 가장 위에 있는 배경, 끝 인덱스는 가장 아래 있는 배경

    5. 가장 아래 있는 배경의 y축 위치를 체크한다.
        위에 있는 배경의 localPosition과 아래 있는 배경의 localPosition을 미리 저장한다.
        가장 아래 있는 배경을 가장 위에 있는 배경의 위에 붙인다.(localPosition + Vector3.up * 10)

    6. 가장 아래 있는 배경의 y축 위치에서 옮길 기준이 될 값을 변수로 만든다.
        카메라의 size를 받아온다. Camera.main.orthographicSize * 2

    7. 위치를 바꾸었다면 시작 인덱스에 담을 값과 끝 인덱스에 담을 값의 교체가 이루어져야 한다.
        이 때 끝 인덱스가 0보다 작아지면 않되지 제어문을 만들어 0보다 작을 경우 2를 담도록 한다.
*/

/*
목표 : 오브젝트 풀링 세팅 28


    1. 오브젝트 매니저를 만든다.

    2. 오브젝트 마다 배열로 담을 수 있는 게임 오브젝트 변수를 만든다.

    3. 배열의 길이를 초기화 한다.

    4. 배열을 채울 초기화 함수를 만든다.
        프리팹을 속성으로 받아준다.
        배열의 길이만큼 반복하며 배열을 해당 오브젝트로 채운다.
            위치와 방향은 스킵
        만들어진 오브젝트는 모두 비활성화 시켜 놓는다.
*/

/*
목표 : 오브젝트 풀 사용하기 29


    1. 오브젝트 풀에 접근할 수 있는 public 함수를 만든다.
        구분을 위해 매개 변수로 문자열을 받는다.

    2. switch문으로 객체를 반환해 준다.
        반복문을 돌면서 비활성화 되어 있는 객체를 활성화하여 반환한다.

    3. 반복되는 구문을 피하기 위해 게임 오브젝트 배열을 임시로 만든다.

    4. 먼저 적을 만들때 오브젝트 풀을 불러온다.
        게임 매니저의 적 생성 함수를 수정한다.
            오브젝트 매니저를 변수로 받는다.
        그동안 적을 생성할 때 외부에서 적 공장을 받아와 인스턴스화를 진행 했었다.
        이제 적 공장을 받기 위해 만들어 두었던 변수를 문자열 배열로 수정한다.
            배열은 초기화를 진행한다.
        인스턴스화 함수를 실행하는 대신에 오브젝트 매니저의 함수를 호출한다.
            스폰 포인트는 만들어진 객체에 따로 지정해 준다.

    5. 이제 만들어진 객체는 삭제되는 것이 아니라 비활성화 되어야 한다.
        각 스크립트에서 만들어 두었던 삭제 코드를 모두 비활성화로 바꿔준다.

    6. 플레이어 총알을 만들때 오브젝트 풀을 불러온다.
        오브젝트 매니저를 변수로 받는다.
        총알을 발사할 때 오브젝트 매니저로 함수를 호출한다.

    7. 적 총알을 만들 때 오브젝트 풀을 불러온다.
        적은 프리팹이므로 게임 매니저로 부터 오브젝트 매니저를 받는다.
            게임 매니저에서 적을 객체로 받을 때
        플레이어 총알과 마찬가지로 객체를 불러온다.

    8. 적이 아이템을 만들 때 오브젝트 풀을 불러온다.

    9. 적이 활성화될 때 방향이 지정되는대 이 방향이 다른 스폰포인트에서 생성될 때에도 계속 유지된다.
        적이 비활성화 될 때 방향을 초기화 한다. Quaternion.identity;

    10. 아이템 로직에서 속도를 지정하여 활성화될 때 멈춰 있는다.
        아이템의 속도는 적이 아이템을 불러올 때 지정하도록 한다.

    11. 체력이 모두 깍인 적은 다시 활성화될 때 체력이 회복되어야 한다.
        적 스크립트에서 활성화 함수를 만든다.
        switch문을 활용하여 적의 이름에 따라 체력을 회복 시킨다.

    12. 오브젝트 풀로 미리 오브젝트를 많이 만들어 두었으므로 FindObjects구문을 수정하도록 한다.
        오브젝트 매니저에서 게임 오브젝트 배열을 받아오는 함수를 만든다.
            매개변수로 전달된 오브젝트 배열을 통째로 반환한다.
        적 스크립트로 돌아가서 적 종류별로 객체의 배열을 다 받아온다.
        반복문을 통해서 활성화된 적 객체에게만 데미지를 준다.
        총알 또한 만찬가지로 배열을 통째로 전달받아서 활성화된 적 총알만 비활성화 한다.
*/

/*
목표 : 스테이지 구성 준비 30


    1. 적 타입과 적 스폰 위치를 속성으로 갖는 구조체 생성

    2. 필요 속성 : 소환 시간, 적 타입, 소환 위치
*/

/*
목표 : 스테이지에 출현하는 적을 구성한다. 31


    1. 메모장

    2. 메모장에 시간, 타입, 위치 순으로 콤마를 구분자로 삼아 입력해 낳간다.

    3. 다 입력한 뒤 저장한다.

    4. 리소스폴더 생성

    5. 메모장 옮기기
*/

/*
목표 : 파일 읽기 32


    1. 게임 매니저에 파일을 읽는 함수를 만든다. 

    2. 필요 속성 : 구조체를 담는 리스트, 인덱스, 모든 소환이 끝났는지 확인할 플래그

    3. 리스트를 초기화 해준다.

    4. 리스트를 일단 비운다. Clear();

    5. 플래그는 false, 인덱스는 0부터 시작
*/

/*
목표 : 진짜로 파일 읽기 33


    1. 파일을 읽기 위해 라이브러리를 추가한다. System.IO;

    2. "텍스트 파일"타입의 변수를 만든다. TextAsset
        폴더를 통해서 파일을 불러온다. Resources.Load("Stage 1") as TextAsset;

    3. 문자열을 읽는 변수를 만든다. StringReader;
        변수를 초기화 할때 텍스트 파일을 불러온다.

    4. 파일을 한 줄씩 읽는다.
        문자열은 Split()을 통해 나눌 수 있다.

    5. 구조체 변수를 만들고 각각의 속성에 문자열의 값을 저장한다.

    6. 리스트에 구조체 변수를 넣는다.
*/

/*
목표 : 파일을 끝까지 읽기 34


    1. 파일 끝까지 읽을 수 있도록 반복문을 만든다.
        한줄 씩 읽는데 받은 값이 null이라면 반복을 멈춘다.

    2. 파일을 다 읽고 파일을 닫는다.

    3. 가장 처음 등장하는 적의 시간은 리스트에 저장된 첫 번째 원소의 시간 속성으로 배정해 준다.
*/

/*
목표 : 데이터 적용 35


    1. 그동안 매 프레임마다 현재 시간을 증가 시키며 소환시간이 되면 랜덤한 적을 랜덤한 위치에 소환했었다.
        이제는 파일에 등록된 대로 소환 시간, 소환 위치에 적 타입을 소환하도록 한다.

    2. switch문으로 리스트에 있는 적의 타입에 따라 변수에 값을 저장한다.
        그 값을 오브젝트 매니저의 객체 소환 함수의 매개변수로 전달한다.

    3. 랜덤한 소환 위치는 파일에 저장된 위치를 배정한다.

    4. 한 기의 적을 생성 하였다면 spawnIndex를 증가 시킨다.

    5. 모든 파일을 다 받아왔다면 spawnIndex == spawnList.Count 이전에 만들어 둔 플래그를 true로 배정한다.

    6. 다음 소환 시간을 다시 지정해 준다.

    7. 업데이트 함수에서 진행하던 소환 조건에 플래그를 추가한다.
*/

/*
목표 : 보조 무기 준비 36


    1. 보조 무기 스프라이트 배치

    2. 보조 무기가 발사할 총알 공장 만들기

    3. 보조 무기 스크립트 만들고 부착
*/

/*
목표 : 보조 무기 기본 작동 구현 37


    1. 필요 속성 : 총알 발사 시간, 오브젝트 매니저

    2. 이동, 발사, 재장전 함수가 필요하다.

    3. 플레이어와 다르게 오직 한 발씩 만 총알을 발사 한다.

    4. 오브젝트 매니저에 보조 무기 총알을 등록한다.
*/

/*
목표 : 보조 무기 이동 38


    1. 필요 속성 : 따라다닐 위치, 플레이어의 위치, 위치를 저장할 큐 저장소, 플레이어를 따라다닐 때의 시간 차

    2. 매 프레임 마다 보조 무기의 위치에 따라다닐 위치를 배정한다.

    3. 감시하는 함수를 만든다. 이 곳에서 따라다닐 위치를 매번 업데이트 한다.

    4. 따라다닐 위치에 플레이어의 위치를 배정해 본다.

    5. 큐를 초기화 한다.

    6. 큐에 플레이어의 위치를 넣는다.

    7. 따라다닐 위치에는 큐에 저장된 위치를 받아온다.
        이때 살짝 딜레이를 준다.
            큐에 저장된 위치의 갯수가 시간 차보다 값이 크다면 그 때부터 Dequeue를 한다.

    8. 보조 무기가 플레이어에 완전히 달라 붙지 않고, 플레이어가 멈추면 보조 무기도 멈추는게 일반적이다.
        플레이어가 멈춰 있을 때도 매 프레임 마다 위치 값을 큐에 집어 넣기 때문이다.

    9. 이미 있는 위치 값을 또 넣으려고 한다면 그 값을 받지 않도록 한다. Contains()

    10. 이러면 보조 무기의 초기 위치는 씬에서 지정한 위치값( 0, 0, 0 )이 된다.
        큐에 저장된 위치의 갯수가 시간 차 보다 값이 작다면 일단은 플레이어의 위치를 배정한다.
*/

/*
목표 : 파워에 따라 보조 무기 추가. 39


    1. 기존 Max 파워에서 추가로 Power 아이템을 먹을 시 보조 무기를 한 개씩 추가한다.

    2. 플레이어가 Power 아이템을 먹을 때 보조 무기를 소환하는 함수를 호출하도록 한다.

    3. 필요 속성 : 보조 무기 오브젝트를 담을 게임 오브젝트 배열, 

    4. 보조 무기 소환 함수를 만든다.
        파워가 일정 값일 경우 보조 무기를 활성화

    5. 보조 무기는 플레이어를 따라가게 되어 있다.
        이를 이용하여 따라갈 오브젝트를 이전 보조 무기로 지정하여 줄줄이 따라다니도록 한다.

    6. 기존 MaxPower를 수정해 준다.

    7. 파워에 따라 각기 다른 총알을 발사하던 switch문에 case 3을 default로 수정한다.
*/

/*
목표 : 보스 준비하기 40


    1. 스프라이트 배치

    2. 애니메이션 클립 적용
        기본, 피격
        트랜지션을 연결하고 파라미터로 트리거를 만든다.

    3. 콜라이더와 리지드 바디를 부착한다.
*/

/*
목표 : 보스 총알과 프리팹 41

    
    1. 총알 프리팹 만들기, 2개

    2. 오브젝트 매니저에 보스 총알 추가

    3. 오브젝트 매니저에 프리팹 등록

    4. 총알 스크립트로 가서 스스로 회전하는 총알을 만들 예정이다.

    5. 필요 속성 : public bool 변수

    6. Update() 함수에서 이 총알이 회전하는 총알인지 확인한다.
        그렇다면 z축으로 회전 시킨다.

    7. 씬에서 회전 하는 보스 총알은 bool을 체크한다.
*/

/*
목표 : 보스 기본 로직 42


    1. 보스에 적 스크립트를 부착
        이름, 점수, 속도, 체력 등록
    
    2. 보스는 일반적인 적 처럼 발사와 재장전을 하지 않기 때문에 Update()에서 보스인지 확인하고 보스일 경우 반환하도록 한다.

    3. 일반적인 적은 피격될 때 스프라이트 변경이 이러난다. 그러나 보스는 피격 애니메이션을 출력할 것이다.
        피격될 때 보스인지 확인하고, 보스라면 트리거 파라미터를 전달한다.
            애니메이터 컴포넌트를 속성으로 받아와야 한다.
                일반적인 적은 애니메이터가 부착되어 있지 않기 때문에 조건부로 애니메이터를 초기화 한다.
        피격되서 죽을 때 보스는 아이템을 떨어뜨리지 않는다.
            보스일 경우 랜덤 값이 무조건 0이 나오도록 한다.

    4. 보스는 경계선에 닿아도 제거되지 않는다.

    5. 보스를 프리팹화 한다.
        위치 초기화

    6. 오브젝트 매니저에서 보스를 등록

    7. 스테이지 메모장에 보스 등록

    8. 게임 매니저에서 적의 이름을 담는 문자열 배열에 보스를 추가한다.

    9. SpawnEnemy함수에 작성한 switch문에 보스를 추가한다.
*/

/*
목표 : 보스의 패턴 흐름을 작성 43


    1. 보스가 끝까지 내려가지 않도록 멈추는 함수를 만든다.

    2. 적이 활성화될 때 (OnEnable) 보스의 체력을 추가하고 멈추는 함수를 호출한다.

    3. 보스의 리지드바디를 받아와서 속도를 0으로 만든다.

    4. 이때 멈추는 함수를 바로 호출하면 보스는 맵 밖에서 멈춰 있으므로 Invoke로 호출하도록 한다.
        그런데 문제가 있다. 오브젝트매니저에서 만들어지는 순간 OnEnable이 한 번 활성화 되고 게임 매니저가 객체를 받을 떄 한 번 더 활성화 된다.
        그러면 Invoke는 두 번 중첩되게 된다.
            멈추는 함수에서 현재 게임오브젝트가 비활성화 상태일 떄는 반환하도록 한다.
    
    5. 보스가 멈추었다면 생각하는 함수를 호출한다.

    6. 생각하는 함수를 만든다.

    7. 필요 속성 : 보스의 패턴 번호, 패턴 반복 횟수, 최대 패턴 반복 횟수 배열

    8. 보스는 4가지 패턴을 구현할 예정이다. 그리고 이 패턴을 보스가 살아있는 동안 반복될 예정이다.
        그러므로 생각을 하기 전에 패턴 번호가 반복되는 초기화 문장을 작성한다. 패턴 번호가 3이면 0으로 아니라면 +1

    9. 패턴 번호를 통한 switch문을 만든다.
    
    10. 4가지 패턴 함수를 각각 만든다.
        각각의 함수는 자신의 패턴을 실행한 뒤 패턴 반복 횟수를 증가 시킨다.
        만약 패턴 반복 횟수가 최대 패턴 반복 횟수보다 작다면 재귀한다.
        만약 패턴 반복 횟수가 최대 패턴 반복 횟수보다 크다면 생각한다.
        패턴 반복 횟수는 생각할 때 마다 초기화 한다.
*/

/*
목포 : 앞으로 4발 발사 구현 44


    1. 오브젝트 매니저로 부터 4 발의 총알을 각기 다른 게임 오브젝트로 받는다.

    2. 각각의 총알 위치를 잡아주고 리지드바디 컴포넌트를 받는다.

    3. 아래 방향으로 각각의 총알을 발사한다.
*/

/*
목표 : 샷건 형태로 발사 구현 45


    1. 반복문을 통해 5발의 총알을 발사 한다.

    2. 5발의 총알이 곂쳐서 발사되지 않도록 랜덤한 벡터값을 구한다.
        x축과 y축 각각 랜덤한 값을 받는다.

    3. 기존 플레이어의 방향에 랜덤한 방향을 더하여 방향을 지정한다.
*/

/*
목표 : 부채꼴로 발사 구현 46


    1. 회전하는 총알을 받아오도록 한다.

    2. 회전하기 전에 회전 값을 초기화 한다. Quaternion.identity;

    3. 방향을 지정하는데, Sin함수를 이용하도록 한다. 매개 변수는 1씩 증가하는 반복 횟수에 최대 반복 횟수를 나눈 값을 준다.
        이 함수는 매개 변수로 각도 값을 받아 해당 각도의 사인 값을 반환한다.
        한 변의 길이가 1인 직각삼각형의 각도에 대한 사인 값을 구하는 것과 관련이 있습니다. 즉, 각도를 입력하면 그에 해당하는 사인 값이 출력된다.
        Mathf.Sin() 함수는 다음과 같은 형태로 사용된다.
        반환 값은 -1과 1 사이의 사인 값이다. 예를 들어, Mathf.Sin(Mathf.PI / 2)는 1을 반환한다.
        이 함수는 주로 애니메이션 및 게임 개발에서 사용된다. 예를 들어, 주기적으로 반복되는 움직임을 만들 때 쓰일 수 있다.
    
    4. 부채꼴 모양으로 펼치기 위해서 전달할 값에 180도를 곱해서 전달한다.
        부채꼴 모양의 총알의 간격을 더 퍼트리고자 한다면 추가로 정수 값을 곱해 준다.

    5. 반복 횟수를 홀수로 지정하여 같은 좌표로만 총알이 퍼지지 않도록 한다.

    6. Sin을 Cos으로 변경하여 발사 시작하는 좌표를 바꿔준다.
*/

/*
목표 : 원형 형태로 발사 구현 47


    1. 반복문을 통해 수십발의 총알을 발사할 예정이다.
        발사할 총알을 변수로 저장해 준다.

    2. 발사 방향은 부채꼴에서 약간의 수정이 필요하다.
        기존 PI에 2를 곱해준다.
        전체 총알 갯수에서 현재 총알의 번호를 나눈 값을 곱해 준다.

    3. x축과 y축 동일하게 작성한다.
        단 하나는 Sin, 하나는 Cos

    4. 총알의 발사 방향에 따라 이미지 방향도 조정한다.
        방향 벡터로 z축의 회전을 준다.
            원 형태 이므로 360도를 곱해주고
            발사 방향에 따라 약간씩 수정되어야 하므로 전체 총알 갯수에서 현재 총알 번호를 나눈 값을 곱해준다.
            보조 값으로 같은 축의 90도를 곱한 값을 더해 준다.

    5. 같은 위치로 개속 발사되지 않도록 발사할 총알 갯수를 두가지 타입으로 나누어 저장한다.

    6. 패턴 반복 횟수의 나눈 몫을 이용하여 두가지 타입을 번갈아 발사한다.
*/

/*
목표 : 플레이어 무적 타임 48


    1. 필요 속성 : 현재 부활 상태인지 확인할 플래그, 스프라이트 랜더러

    2. 플레이어가 활성화 될 때 OnEnable 플래그에 true를 배정
        플래그를 함수로 만든다.

    3. 현재 무적 상태라면 알파값을 내린다.

    4. 무적 상태가 끝나면 알파값을 올린다.

    5. 플래그 함수는 호출될 때마다 플래그 값이 반대로 되는 함수이다.

    6. 플레이어가 활성화될 때 함수를 바로 부르고 Invoke로 한 번 더 부른다.

    7. 적 총알에 맞았을 때 피격 로직이 실행되기 전에 플래그를 확인하고, 무적 상태라면 반환한다.

    8. 보조 무기도 같은 효과를 주기 위해 보조 무지를 플레이어의 자식으로 둔다.

    9. 플레이어의 알파값을 조정할 때 반복문을 통해 보조 무기도 알파값을 조정한다.
*/

/*
목표 : 폭발 효과 49


    1. 스프라이트 배치

    2. 애니매이션 배치

    3. 빈 애니매이션 클립을 기본 값으로 지정하고 폭발 애니매이션을 any로 연결

    4. 폭발 스크립트를 만들고 부착

    5. 애니매이터로 파라미터를 전달하는 함수를 만든다.

    6. 매개 변수로 문자열을 받아서 비행기 크기에 따라 각기 다른 크기의 애니매이션이 출력되도록 한다.
        transform.localScale = Vector3.one * 값

    7. 프리팹화 한다.

    8. 오브젝트 매니저에 등록한다.

    9. 게임 매니저에서 폭발 함수를 호출하는 함수를 만든다.
        매개 변수로 위치와 문자열 타입을 받는다.
        객체를 받아 오고 로직을 받는다.

    10. 객체의 위치를 지정해 준다.

    11. 폭발 함수에 타입을 전달한다.

    12. 플레이어가 터질 때 게임 매니저의 함수를 호출한다.

    13. 적이 터질 때 게임 매니저의 함수를 호출한다.
        적 스크립트에 게임 매니저 속성을 추가한다.
        게임 매니저에서 적을 생성할 때 적 스크립트의 게임 매니저에 자기 자신을 전달한다.

    14. 게임 매니저에서 폭발 객체를 활성화 하여 받았다면 애니매이션이 끝난 뒤에 비활성화 되어야 한다.

    15. 폭발 스크립트에서 비활성화 함수를 만들고 활성화될 때 Invoke로 비활성화 함수를 호출한다.

    16. 폭발 프리팹에서 스프라이트 초기 값을 없앤다.
*/

/*
목표 : 컨트롤 UI 준비 50


    1. Canvas -> Render Mode -> Screen Space - Camera
        Pixel Perfect, Order Layer
    
    2. 이미지 UI를 만들고 방향키 이미지를 등록

    3. 이미지 UI의 자식으로 버튼을 만든다.
        이미지는 알파값을 낮추고, 텍스트는 지우고 Color Tint를 None으로 설정

    4. 100 * 100 사이즈로 9개를 만들어 배치한다.

    5. 버튼에 이벤트 트리거 부착
        Pointer Donw, Up, Enter
*/

/*
목표 : 컨트롤러 구현 51


    1. 플레이어 스크립트에서 조이패드로 방향값을 주는 함수를 만든다.
        bool 타입의 배열 변수와 그냥 bool 변수가 필요하다.

    2. 반복문을 활용하여 bool 배열을 0 ~ 8까지 순회하며 버튼이 눌린 번호에만 true를 주도록 한다.

    3. 버튼이 눌렸을 때의 함수와 때였을 때의 함수를 만들어서 현재 버튼이 눌린 상태인지 true, false를 주도록 한다.

    4. Move함수에서 bool 배열에서 true인 값에 따라 방향 값을 수직, 수평으로 지정해 준다.

    5. 경계선에 다았을 때 해당 방향으로의 값을 0으로 만드는 제어문을 만들었는데, or로 현재 버튼이 눌렸는지도 체크해서 버튼이 눌리지 않았다면 0이 되게 한다.

    6. 공격 버튼과 폭탄 버튼을 준비한다.

    7. 공격 함수와 폭탄 함수를 만든다.
        공격은 버튼 눌렀을 때, 때었을 때 각각 만들고 폭탄은 눌렀을 때를 만든다.
    
    8. 필요 속성: 총알 버튼이 눌렸는지, 폭탄 버튼이 눌렸는지 체크할 플래그

    9. 버튼의 눌림 여부에 따라 플래그에 true, false를 배정

    10. 발사 함수에서 새로운 제어문을 만든다.
        발사 버튼이 false라면 반환

    11. 폭탄 함수에서 새로운 제어문을 만든다.
*/

/*
목표 : 스테이지 등록 52


    1. 메모장 스테이지를 추가한다.

    2. 게임 매니저에서 스테이지를 관리하도록 한다.
        스테이지 번호를 배정한 변수를 만든다.

    3. 리소스에서 스테이지 메모장을 받을 때 변수를 값으로 활용한다.

    4. 스테이지를 처음 읽을 때, 스테이지를 모두 읽었을 때의 함수를 각각 만든다.

    5. 스테이지를 모두 읽었다면 변수 값을 증가시킨다.

    6. 스테이지를 처음 불러올 때는 ReadSpawnFile()함수를 호출한다.

    7. 게임이 시작될 때 스테이지 시작 함수를 호출한다.

    8. 스테이지를 시작할 때
        스테이지 UI를 표시
        검은 화면에서 밝아짐 Fade In

    9. 스테이지를 끝낼 때
        클리어 UI를 표시
        검은 화면으로 어두워짐 Fade Out
        플레이어 위치 초기화

    10. 스테이지 시작, 끝 Text UI를 만든다. 애니매이터와 애니매이션 클립을 만든다.
        애니매이터에 빈 애니매이션 클립을 만들고 연결
        Fade In, Out할 애니매이션 클랩 연결
        트리거 파라미터 생성

    11. 게임 매니저가 애니매이터를 받아서 트리거를 쏜다.
        시작 애니매이터와 끝 애니매이터 각각 받는다.

    12. 애니매이션은 스케일 값으로 만든다.
        트랜지션이 겹치지 않도록 조정해 준다.

    13. 애니매이터로 Text UI컴포넌트를 받아와서 스테이지 번호에 맞게 텍스트를 출력한다.
*/

/*
목표 : Fade In/Out


    1. 스프라이트 배치

    2. 애니매이터, 애니매이션 클립 2개 생성
        트리거 In/Out

    3. 애니매이터 받기

    4. 스프라이트 랜더러의 칼라값으로 애니매이션 조정

    5. 플레이어의 위치를 저장할 Transform변수가 필요하다.

    6. 플레이어의 위치를 저장할 빈 오브젝트 생성

    7. 보스를 잡으면 StageEnd함수를 출력한다.
*/