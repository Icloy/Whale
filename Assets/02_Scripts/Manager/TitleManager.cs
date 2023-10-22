using System;
using UnityEngine;
using UnityEngine.UI;
using MNF;
using System.Collections;

namespace whale
{
    public class TitleManager : MonoBehaviour
    {
        public GameObject loginPanel;
        public GameObject gameStartBtn;
        public GameObject playerPrefab;
        public InputField inputUserID;
        public GameManager gameManager;
        
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

        //발사했을때의 샘플코드
        private void TankFire()
        {
            var data = new TANK_FIRE
            {
                USER = "User10",
                DATA = 2,
                Power = "10",
                Position = "1,1,1"
            };

            string sendData = LitJson.JsonMapper.ToJson(data);
            MainManager.Instance.netGameManager.RoomBroadcast(sendData);
        }

        //제작중인 샘플코드
        public void ObjectInteraction(int a , int b, string c)
        {
            var data = new Object_Interaction
            {
                USER = a,
                STATE = b,
                WHERE = c,
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
                Debug.Log("icloy 플레이어 프리팹 제작" + user.m_szUserID);
                GameManager.gm.CreatePlayer(false, user.m_szUserID);
            }
        }


        public void RoomBroadcast(string szData)
        {
            LitJson.JsonData jData = LitJson.JsonMapper.ToObject(szData);
            string userID = jData["USER"].ToString();
            int dataID = Convert.ToInt32(jData["DATA"].ToString());

            Debug.Log("RoomBroadcast : " + userID + " , " + dataID.ToString());
            if (dataID == 1)//게임시작
            {
                InvokeRepeating("UserMove", 0, 0.01f);
            }
            else if (dataID == 2)//총알발사
            {
                string power = jData["Power"].ToString();
                string pos = jData["Position"].ToString();
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

