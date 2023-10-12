using whale;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using static UnityEditor.Progress;
using MNF;

namespace whale
{
    public class NetGameManager : MonoBehaviour
    {
        public UserHandle m_userHandle = new UserHandle();    // 유저개인정보
        public RoomSession m_roomSession = new RoomSession(); // 방정보

        private float m_fFlowTime = 0.0f;
        private float m_fSendTime = 0.0f;

        public void Update()
        {
            m_fFlowTime += Time.deltaTime;
        }

        private bool IsSendEnable()
        {
            float fGap = m_fFlowTime - m_fSendTime;
            if (fGap < 0.1)
            {
                Debug.Log("SendData Time = " + fGap.ToString());
                return false;
            }

            m_fSendTime = m_fFlowTime;
            return true;
        }

        public void ConnectServer(string szIP, int nPort, bool isIntranet = false)
        {
            MainManager.Instance.networkManager.Init(szIP, nPort, isIntranet);
        }

        //방에서 특정유저정보 가져오기
        public UserSession GetRoomUserSession(string szUserID)
        {
            for (int i = 0; i < m_roomSession.m_userList.Count; i++)
            {
                if (m_roomSession.m_userList[i].m_szUserID == szUserID)
                {
                    return m_roomSession.m_userList[i];
                }
            }

            return null;
        }

        //본인 방입장
        public void Recv_ROOM_ENTER(BinaryReader br)
        {
            m_roomSession.ReadBin(br);

            Debug.Log("Recv_ROOM_ENTER : " + m_roomSession.m_RoomNo.ToString());
        }

        //다른 유저 방입장
        public void Recv_ROOM_MAN_IN(BinaryReader br)
        {
            UserSession userSession = new UserSession();
            userSession.ReadBin(br);

            m_roomSession.m_userList.Add(userSession);

            Debug.Log("Recv_ROOM_MAN_IN : " + userSession.m_szUserID);

            MainManager.Instance.titleManager.RoomUserAdd(userSession);
        }

        //다른유저 방 퇴장
        public void Recv_ROOM_MAN_OUT(BinaryReader br)
        {
            UserSession userSession = new UserSession();
            userSession.ReadBin(br);

            for (int i = 0; i < m_roomSession.m_userList.Count; i++)
            {
                if (m_roomSession.m_userList[i].m_szUserID == userSession.m_szUserID)
                {
                    m_roomSession.m_userList.RemoveAt(i);
                    break;
                }
            }

            MainManager.Instance.titleManager.RoomUserDel(userSession);

            Debug.Log("Recv_ROOM_MAN_OUT : " + userSession.m_szUserID);
        }

        //방에서 패킷 주고 받기
        public void Recv_ROOM_BROADCAST(BinaryReader br)
        {
            string szData = "";
            NetString.ReadString(br, ref szData);

            Debug.Log("Recv_ROOM_BROADCAST" + szData);

            MainManager.Instance.titleManager.RoomBroadcast(szData);
        }

        //방에서 본인정보 업데이트
        public void Recv_ROOM_USER_DATA_UPDATE(BinaryReader br)
        {
            UserSession userSession = new UserSession();
            userSession.ReadBin(br);

            for (int i = 0; i < m_roomSession.m_userList.Count; i++)
            {
                if (m_roomSession.m_userList[i].m_szUserID == userSession.m_szUserID)
                {
                    m_roomSession.m_userList[i].UserDataUpdate(userSession);
                    break;
                }
            }

            Debug.Log("Recv_ROOM_USER_DATA_UPDATE" + userSession.m_szUserID);

            MainManager.Instance.titleManager.RoomUserDataUpdate(userSession);
        }
        public void Recv_ROOM_USER_MOVE_DIRECT(BinaryReader br)
        {
            UserSession userSession = new UserSession();
            userSession.ReadBin(br);

            if (m_userHandle.m_szUserID == userSession.m_szUserID)
                return;

            for (int i = 0; i < m_roomSession.m_userList.Count; i++)
            {
                if (m_roomSession.m_userList[i].m_szUserID == userSession.m_szUserID)
                {
                    m_roomSession.m_userList[i].UserMoveDirect(userSession);
                    break;
                }
            }

            Debug.Log("Recv_ROOM_USER_MOVE_DIRECT" + userSession.m_szUserID);

            MainManager.Instance.titleManager.RoomUserMoveDirect(userSession);
        }
        public void Recv_ROOM_USER_ITEM_UPDATE(BinaryReader br)
        {
            UserSession userSession = new UserSession();
            userSession.ReadBin(br);

            for (int i = 0; i < m_roomSession.m_userList.Count; i++)
            {
                if (m_roomSession.m_userList[i].m_szUserID == userSession.m_szUserID)
                {
                    m_roomSession.m_userList[i].UserDataUpdate(userSession);
                    break;
                }
            }

            Debug.Log("Recv_ROOM_USER_ITEM_UPDATE" + userSession.m_szUserID);

            MainManager.Instance.titleManager.RoomUserItemUpdate(userSession);
        }
        public void Recv_ROOM_DATA_UPDATE(BinaryReader br)
        {
            m_roomSession.ReadRoomDataUpdate(br);
            Debug.Log("Recv_ROOM_DATA_UPDATE : " + m_roomSession.m_RoomNo.ToString());
        }
        public void Recv_ROOM_UPDATE(BinaryReader br)
        {
            m_roomSession.ReadBin(br);
            //Debug.Log("Recv_ROOM_UPDATE : " + m_roomSession.m_RoomNo.ToString() );


            //MainManager.Instance.titleManager.RoomUpdate();
        }

        public void UserLogin(string szID, byte byGroup)
        {
            MainManager.Instance.networkManager.Send_WAIT_LOGIN(szID, byGroup, this.gameObject);
        }

        public void OnRecvWaitLogin(BinaryReader br)
        {
            ushort usResult = br.ReadUInt16();
            m_userHandle.ReadBin(br);

            MainManager.Instance.titleManager.UserLoginResult(usResult);

            Debug.Log("OnRecvWaitLogin" + m_userHandle.m_szUserID);

            }

        public void RoomBroadcast(string szData)
        {
            MainManager.Instance.networkManager.Send_ROOM_BROADCAST(szData);
        }
        public void RoomUserDataUpdate(UserSession userSession)
        {
            MainManager.Instance.networkManager.Send_ROOM_USER_DATA_UPDATE(userSession);
        }
        public void RoomUserMove(UserSession userSession)
        {
            MainManager.Instance.networkManager.Send_ROOM_USER_MOVE(userSession);
        }
        public void RoomUserMoveDirect(UserSession userSession)
        {
            MainManager.Instance.networkManager.Send_ROOM_USER_MOVE_DIRECT(userSession);
        }
        public void RoomUserItemUpdate(UserSession userSession)
        {
            MainManager.Instance.networkManager.Send_ROOM_USER_ITEM_UPDATE(userSession);
        }
        public void RoomDataUpdate(RoomSession roomSession)
        {
            MainManager.Instance.networkManager.Send_ROOM_DATA_UPDATE(roomSession);
        }
    }
}