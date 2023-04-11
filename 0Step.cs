/*
목표 : 게임 준비하기


    1. 스프라이트를 담을 폴더 준비하기

    2. 다운 받은 스프라이트 설정
        PixelPerUnit, FilterMode, Compression 설정
        스프라이트 에디터에서 스프라이트 자르기

    3. 플레이어 스프라이트 씬에 배치

    4. 스크립트 폴더 준비하기
*/

/*
목표 : 플레이어 이동


    1. 플레이어 스크립트 만들기

    2. 오브젝트에 스크립트 부착

    3. 이동 로직을 위한 필요 속성 : 수평/수직 방향값을 받아서 저장할 변수, 플레이어의 현재 위치, 플레이어의 다음 위치, 속도, 

    4. transform을 활용한 이동 로직 만들기
        미래의 위치는 현재의 위치 + 속도 * 시간
*/

/*
목표 : 해상도 조절


    1. 유니티 레이아웃 조정

    2. 9 : 16 비율로 화면 조정
        9 : 19 비율도 가능

    3. 플레이어 위치 재 조정
*/

/*
목표 : 경계 설정


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
목표 : 경계 로직


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
목표 : 플레이어의 애니매이션


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
목표 : 발사체 구성


    1. 씬에 총알 스프라이트 배치

    2. 총알 오브젝트에 콜라이더와 리지드바디 부착
        총알은 AddForce를 활용하여 발사할 예정이기 때문에 BodyType은 Dynamic으로 지정한다.
        총알은 중력의 영향을 받지 않을 것 이기 때문에 중력값 또한 0으로 지정한다.

    3. 총알 오브젝트를 에셋화(프리팹)한다.
        프리팹 폴더를 준비한다.
*/

/*
목표 : 오브젝트 삭제


    1. 총알이 쌓이지 않도록 오브젝트 경계선을 만든다.

    2. 총알을 관리할 총알 스크립트를 만든다.

    3. 총알 스크립트에서 경계선과 충돌했을 때 스스로 제거하는 함수(OnTrigger)를 만든다.

    4. 총알 경계선 전용 태그를 만든다.

    5. 씬에 배치해 둔 총알 오브젝트는 지워준다.
*/

/*
목표 : 총알 만들기


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
목표 : 발사체 다듬기


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
목표 : 더 강한 총알 발사


    1. 필요 속성 : 파워를 수치화 한 변수

    2. Fire() 함수로 가서 파워 변수에 따른 각기 다른 총알 발사를 작성한다.

    3. switch 문으로 파워가 1일 때, 2일 때, 3일 때 각기 다른 총알을 발사한다.
        1일 때는 기존 발사체 그대로
        2일 때는 기존 발사체를 두 발 발싸
            객체화 할 때 위치를 수정해 준다.
        3일 때는 더 강한 총알을 추가로 발싸
*/

/*
목표 : 적 기체 준비하기


    1. 적 기체들을 씬에 옮긴다.

    2. 각 기체에 콜라이더를 부착한다.
        스프라이트 에디터에서 폴리곤 콜라이더를 위해 PhysicsShape를 수정해 준다.

    3. 각 기체도 움직이는 속력 값이 있기 때문에 리지드바디를 부착한다.
        중력은 받으면 않된다.

    4. 태그를 추가해 준다.
*/

/*
목표 : 적 기초 로직 만들기


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
목표 : 적 기체 생성


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