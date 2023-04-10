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