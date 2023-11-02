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

            MainManager.Instance.netGameManager.UserLogin(userID, 2); //�ڿ� ���� ����ȣ(���� �� ���� )
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

        
        //��Ʈ��ũ ȣ�� �ڵ�
        public void ObjectInteraction(int a , string b, int c) //c�� 0 default 1 active 2 off
        {
            var data = new Object_Interaction
            {
                USER = a,
                DATA = 2,
                WHERE = b, //� ������Ʈ Ȱ��ȭ �Ұ���
                STATE = c //WHERE�� ������Ʈ ���� Ȱ��ȭ �Ҳ��� ���ٲ��� ��.(�߰��� �ϳ� ��Ʈ�� �� �� �ִ°�)
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
                // 0.5�� �ڿ� RoomOneUserAdd �޼��� ����
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
                Debug.Log("�÷��̾� ������ ����" + user.m_szUserID);
                GameManager.gm.CreatePlayer(false, user.m_szUserID);
            }
        }


        public void RoomBroadcast(string szData)
        {
            LitJson.JsonData jData = LitJson.JsonMapper.ToObject(szData);
            string userID = jData["USER"].ToString();
            int dataID = Convert.ToInt32(jData["DATA"].ToString());

            Debug.Log("RoomBroadcast : " + userID + " , " + dataID.ToString());
            if (dataID == 1) //���ӽ���
            {
                InvokeRepeating("UserMove", 0, 0.01f);
            }
            else if (dataID == 2) //������Ʈ Ȱ��ȭ
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
                            case 0://���� �� ȸ��
                                GameManager.gm.objController.Puzzle_Cube_RotateHor();
                                break;
                            case 1://���� �� ȸ��
                                GameManager.gm.objController.Puzzle_Cube_RotateVer();
                                break;
                            case 2: //���� üũ
                                GameManager.gm.objController.Puzzle_Cube_CheckAnswer();
                                break;
                            case 3: // ���� ����
                                GameManager.gm.objController.Puzzle_Cube_ChooseHor();
                                break;
                            case 4: // ���� ����
                                GameManager.gm.objController.Puzzle_Cube_ChooseVer();
                                break;
                            case 5: //ť�� ���� �� �ֱ�
                                GameManager.gm.objController.Puzzle_Cube_Random();
                                break;
                        }
                        break;
                    #endregion
                    #region Maze
                    case "Maze":
                        //�ߺ� ���� �ڵ�� ����

                        switch (state) //������ �����ڵ� 0:B 1:G 2:R
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
                        //�ߺ� ���� ���� �ڵ� ����

                        break;
                    #endregion
                    #region Room
                    case "Room":
                        //�ߺ� ���� �ڵ�� ����

                        switch (state) //������ �����ڵ� 0:B 1:G 2:R
                        {
                            case 0:
                                GameManager.gm.objController.Puzzle_Room_aa();
                                break;
                        }
                        //�ߺ� ���� ���� �ڵ� ����
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

