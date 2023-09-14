using MNF;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace whale
{
    public class NetGameManager : MonoBehaviour
    {
        public UserHandle m_userHandle = new UserHandle();    // ������������
        public RoomSession m_roomSession = new RoomSession(); // ������

        public void ConnectServer(string szIP, int nPort, bool isIntranet = false)
        {
            MainManager.Instance.networkManager.Init(szIP, nPort, isIntranet);
        }

        //�濡�� Ư���������� ��������
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

        //���� ������
        public void Recv_ROOM_ENTER(BinaryReader br)
        {
            m_roomSession.ReadBin(br);

            Debug.Log("Recv_ROOM_ENTER : " + m_roomSession.m_RoomNo.ToString());

            TitleManager.s.RoomEnter();
        }

        //�ٸ� ���� ������
        public void Recv_ROOM_MAN_IN(BinaryReader br)
        {
            UserSession userSession = new UserSession();
            userSession.ReadBin(br);

            m_roomSession.m_userList.Add(userSession);

            Debug.Log("Recv_ROOM_MAN_IN : " + userSession.m_szUserID);

            TitleManager.s.RoomUserAdd(userSession);
        }

        //�ٸ����� �� ����
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

            TitleManager.s.RoomUserDel(userSession);

            Debug.Log("Recv_ROOM_MAN_OUT : " + userSession.m_szUserID);
        }

        //�濡�� ��Ŷ �ְ� �ޱ�
        public void Recv_ROOM_BROADCAST(BinaryReader br)
        {
            string szData = "";
            NetString.ReadString(br, ref szData);

            Debug.Log("Recv_ROOM_BROADCAST" + szData);

            TitleManager.s.RoomBroadcast(szData);
        }

        //�濡�� �������� ������Ʈ
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

            TitleManager.s.RoomUserDataUpdate(userSession);
        }
        public void Recv_ROOM_USER_MOVE_DIRECT(BinaryReader br)
        {
            UserSession userSession = new UserSession();
            userSession.ReadBin(br);

            for (int i = 0; i < m_roomSession.m_userList.Count; i++)
            {
                if (m_roomSession.m_userList[i].m_szUserID == userSession.m_szUserID)
                {
                    m_roomSession.m_userList[i].UserMoveDirect(userSession);
                    break;
                }
            }

            Debug.Log("Recv_ROOM_USER_MOVE_DIRECT" + userSession.m_szUserID);

            TitleManager.s.RoomUserMoveDirect(userSession);
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

            TitleManager.s.RoomUserItemUpdate(userSession);
        }
        public void Recv_ROOM_DATA_UPDATE(BinaryReader br)
        {
            m_roomSession.ReadRoomDataUpdate(br);
            Debug.Log("Recv_ROOM_DATA_UPDATE : " + m_roomSession.m_RoomNo.ToString());
        }
        public void Recv_ROOM_UPDATE(BinaryReader br)
        {
            m_roomSession.ReadBin(br);
            Debug.Log("Recv_ROOM_UPDATE : " + m_roomSession.m_RoomNo.ToString());

            TitleManager.s.RoomUpdate();
        }

        public void UserLogin(string szID, byte byGroup)
        {
            MainManager.Instance.networkManager.Send_WAIT_LOGIN(szID, byGroup, this.gameObject);
        }

        public void OnRecvWaitLogin(BinaryReader br)
        {
            ushort usResult = br.ReadUInt16();
            m_userHandle.ReadBin(br);

            TitleManager.s.UserLoginResult(usResult);

            Debug.Log("OnRecvWaitLogin");
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