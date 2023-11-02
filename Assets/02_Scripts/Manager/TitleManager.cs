using System;
using UnityEngine;
using UnityEngine.UI;
using MNF;
using System.Collections;

namespace whale
{
    public class TitleManager : MonoBehaviour
    {
        /*public GameObject loginPanel;
        public GameObject gameStartBtn;
        public GameObject playerPrefab;
        public InputField inputUserID;
        public GameManager gameManager;
        */
        void Start()
        {
            MainManager.Instance.netGameManager.ConnectServer("3.34.116.91", 3650, true); 
             //MainManager.Instance.netGameManager.ConnectServer("192.168.246.193", 3650, true);
            //MainManager.Instance.netGameManager.ConnectServer("172.16.115.87", 3650, true);
            //MainManager.Instance.netGameManager.ConnectServer("127.0.0.1", 3650, true);
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                RoomSession roomSession = MainManager.Instance.netGameManager.m_roomSession;
                roomSession.m_nRoomData[0] = 11;
                MainManager.Instance.netGameManager.RoomDataUpdate(roomSession);

            }
        }

        public void OnClick_Login(string userID)
        {
            if (userID.Length < 1)
                return;

            MainManager.Instance.netGameManager.UserLogin(userID, 2); //뒤에 숫자 조번호(서버 방 입장 )
        }

        public void OnClick_Start()
        {
            var data = new GAME_START
            {
                USER = MainManager.Instance.netGameManager.m_userHandle.m_szUserID,
                DATA = 1
            };

            string sendData = LitJson.JsonMapper.ToJson(data);
            MainManager.Instance.netGameManager.RoomBroadcast(sendData);
        }

        public void OnClick_TankChange()
        {
            UserSession userSession = MainManager.Instance.netGameManager.GetRoomUserSession(
                MainManager.Instance.netGameManager.m_userHandle.m_szUserID);

            userSession.m_nUserData[0] = (byte)UnityEngine.Random.Range(0, 4);
            MainManager.Instance.netGameManager.RoomUserDataUpdate(userSession);
        }

        void UserMove()
        {
            UserSession userSession = MainManager.Instance.netGameManager.GetRoomUserSession(
                MainManager.Instance.netGameManager.m_userHandle.m_szUserID);

            MainManager.Instance.netGameManager.RoomUserMove(userSession);
        }

        
        //네트워크 호출 코드
        public void ObjectInteraction(int a , string b, int c) //c는 0 default 1 active 2 off
        {
            var data = new Object_Interaction
            {
                USER = a,
                DATA = 2,
                WHERE = b, //어떤 오브젝트 활성화 할건지
                STATE = c //WHERE의 오브젝트 상태 활성화 할껀지 없앨껀지 등.(추가로 하나 컨트롤 할 수 있는값)
            };

            string sendData = LitJson.JsonMapper.ToJson(data);
            MainManager.Instance.netGameManager.RoomBroadcast(sendData);
        }


        public void UserLoginResult(ushort usResult)
        {
            if (usResult == 0)
            {
                MainManager.Instance.loadingManager.LoadScene("03_GameScene");
            }

            Debug.Log("UserLoginResult : " + usResult.ToString());
            MainManager.Instance.statusContainer.userNum = usResult + 1;
        }

        public void RoomEnter()
        {
            RoomSession roomSession = MainManager.Instance.netGameManager.m_roomSession;
            Debug.Log("RoomEnter List Cnt" + roomSession.m_userList.Count);
            for (int i = 0; i < roomSession.m_userList.Count; i++)
            {
                // 0.5초 뒤에 RoomOneUserAdd 메서드 실행
                float delay = 0.5f;
                UserSession user = roomSession.m_userList[i];
                StartCoroutine(DelayedRoomOneUserAdd(user, delay));
            }
        }

        private IEnumerator DelayedRoomOneUserAdd(UserSession user, float delay)
        {
            yield return new WaitForSeconds(delay);
            RoomOneUserAdd(user);
        }

        public void RoomUserAdd(UserSession user)
        {
            RoomOneUserAdd(user);
        }

        public void RoomUserDel(UserSession user)
        {
            GameObject playerObj = GameObject.Find(user.m_szUserID);
            Destroy(playerObj, 0);
        }

        void RoomOneUserAdd(UserSession user)
        {
            Vector3 pos = user.m_userTransform[0].GetVector3();
            if (user.m_szUserID != MainManager.Instance.netGameManager.m_userHandle.m_szUserID)
            {
                Debug.Log("플레이어 프리팹 제작" + user.m_szUserID);
                GameManager.gm.CreatePlayer(false, user.m_szUserID);
            }
        }


        public void RoomBroadcast(string szData)
        {
            LitJson.JsonData jData = LitJson.JsonMapper.ToObject(szData);
            string userID = jData["USER"].ToString();
            int dataID = Convert.ToInt32(jData["DATA"].ToString());

            Debug.Log("RoomBroadcast : " + userID + " , " + dataID.ToString());
            if (dataID == 1) //게임시작
            {
                InvokeRepeating("UserMove", 0, 0.01f);
            }
            else if (dataID == 2) //오브젝트 활성화
            {
                string where = jData["WHERE"].ToString();
                if (where == null) return;
                int state = Convert.ToInt32(jData["STATE"].ToString());
                switch (where)
                {
                    #region Cube
                    case "Cube" :
                        if (GameManager.gm.objController.boolCubeRotate) return;
                        GameManager.gm.objController.boolCubeRotate = true;
                        switch (state)
                        {
                            case 0://가로 축 회전
                                GameManager.gm.objController.Puzzle_Cube_RotateHor();
                                break;
                            case 1://세로 축 회전
                                GameManager.gm.objController.Puzzle_Cube_RotateVer();
                                break;
                            case 2: //정답 체크
                                GameManager.gm.objController.Puzzle_Cube_CheckAnswer();
                                break;
                            case 3: // 가로 선택
                                GameManager.gm.objController.Puzzle_Cube_ChooseHor();
                                break;
                            case 4: // 세로 선택
                                GameManager.gm.objController.Puzzle_Cube_ChooseVer();
                                break;
                            case 5: //큐브 랜덤 값 주기
                                GameManager.gm.objController.Puzzle_Cube_Random();
                                break;
                        }
                        break;
                    #endregion
                    #region Maze
                    case "Maze":
                        //중복 감지 코드는 여기

                        switch (state) //각각의 실행코드 0:B 1:G 2:R
                        {
                            case 0:
                                GameManager.gm.objController.Puzzle_Maze_Blue();
                                break;
                            case 1:
                                GameManager.gm.objController.Puzzle_Maze_Green();
                                break;
                            case 2:
                                GameManager.gm.objController.Puzzle_Maze_Red();
                                break;
                        }
                        //중복 감지 해제 코드 여기

                        break;
                    #endregion
                    #region Room
                    case "Room":
                        //중복 감지 코드는 여기

                        switch (state) //각각의 실행코드 0:B 1:G 2:R
                        {
                            case 0:
                                GameManager.gm.objController.Puzzle_Room_aa();
                                break;
                        }
                        //중복 감지 해제 코드 여기
                        break;
                    #endregion

                }
            }

        }
        public void RoomUserDataUpdate(UserSession user)
        {
            RoomSession roomSession = MainManager.Instance.netGameManager.m_roomSession;
            for (int i = 0; i < roomSession.m_userList.Count; i++)
            {
                if (roomSession.m_userList[i].m_szUserID == user.m_szUserID)
                {
                    GameObject playerObj = GameObject.Find(roomSession.m_userList[i].m_szUserID);
                    if (playerObj)
                    {
                        Destroy(playerObj, 0);
                    }

                    //RoomOneUserAdd(user);
                    return;
                }
            }
        }
        public void RoomUserMoveDirect(UserSession user)
        {
            RoomSession roomSession = MainManager.Instance.netGameManager.m_roomSession;
            for (int i = 0; i < roomSession.m_userList.Count; i++)
            {
                if (roomSession.m_userList[i].m_szUserID == user.m_szUserID)
                {
                    GameObject playerObj = GameObject.Find(roomSession.m_userList[i].m_szUserID);
                    if (playerObj)
                    {
                        //playerObj.transform.position = roomSession.m_userList[i].m_userTransform[0].GetVector3();
                        playerObj.transform.rotation = Quaternion.Euler(roomSession.m_userList[i].m_userTransform[1].GetVector3());
                        playerObj.transform.position =
                                    roomSession.m_userList[i].m_userTransform[0].GetVector3();
                    }


                    return;
                }
            }
        }
        public void RoomUserItemUpdate(UserSession user)
        {
            RoomSession roomSession = MainManager.Instance.netGameManager.m_roomSession;
            for (int i = 0; i < roomSession.m_userList.Count; i++)
            {
                if (roomSession.m_userList[i].m_szUserID == user.m_szUserID)
                {
                    GameObject playerObj = GameObject.Find(roomSession.m_userList[i].m_szUserID);
                    if (playerObj)
                    {
                        Destroy(playerObj, 0);
                    }


                    return;
                }
            }
        }
        public void RoomUpdate()
        {
            RoomSession roomSession = MainManager.Instance.netGameManager.m_roomSession;
            for (int i = 0; i < roomSession.m_userList.Count; i++)
            {
                if (roomSession.m_userList[i].m_szUserID !=
                    MainManager.Instance.netGameManager.m_userHandle.m_szUserID)
                {
                    GameObject playerObj = GameObject.Find(roomSession.m_userList[i].m_szUserID);
                    if (playerObj)
                    {
                        playerObj.transform.position = roomSession.m_userList[i].m_userTransform[0].GetVector3();
                    }
                }
            }
        }
    }
}

