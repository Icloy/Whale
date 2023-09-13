using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Net;

using KWNET;

public class NetworkSample : KWSingleton<NetworkSample>
{
	public UserHandle m_userHandle = new UserHandle();    // 유저개인정보
	public RoomSession m_roomSession = new RoomSession(); // 방정보

	override public void Awake()
	{
		base.Awake();
	}

	public void ConnectServer(string szIP, int nPort, bool isIntranet = false)
	{
		NetManager.instance.Init(szIP, nPort, isIntranet);	
	}

	//방에서 특정유저정보 가져오기
	public UserSession GetRoomUserSession(string szUserID)
	{
		for(int i = 0; i < m_roomSession.m_userList.Count; i++)
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

		Debug.Log("Recv_ROOM_ENTER : " + m_roomSession.m_RoomNo.ToString() );

		//SceneSample.s.RoomEnter();
	}

	//다른 유저 방입장
	public void Recv_ROOM_MAN_IN(BinaryReader br)
	{
		UserSession userSession = new UserSession();
		userSession.ReadBin(br);

		m_roomSession.m_userList.Add(userSession);

		Debug.Log("Recv_ROOM_MAN_IN : " + userSession.m_szUserID );

		//SceneSample.s.RoomUserAdd(userSession);
	}

	//다른유저 방 퇴장
	public void Recv_ROOM_MAN_OUT(BinaryReader br)
	{
		UserSession userSession = new UserSession();
		userSession.ReadBin(br);

		for(int i = 0; i < m_roomSession.m_userList.Count; i++)
		{
			if (m_roomSession.m_userList[i].m_szUserID == userSession.m_szUserID)
			{
				m_roomSession.m_userList.RemoveAt(i);
				break;
			}
		}

		//SceneSample.s.RoomUserDel(userSession);

		Debug.Log("Recv_ROOM_MAN_OUT : " + userSession.m_szUserID );
	}

	//방에서 패킷 주고 받기
	public void Recv_ROOM_DATA(BinaryReader br)
	{
		string szData = "";
		NetString.ReadString(br, ref szData);

		Debug.Log("Recv_ROOM_DATA" + szData);

		//SceneSample.s.RoomData(szData);
	}

	//방에서 본인정보 업데이트
	public void Recv_ROOM_USER_UPDATE(BinaryReader br)
	{
		UserSession userSession = new UserSession();
		userSession.ReadBin(br);

		for(int i = 0; i < m_roomSession.m_userList.Count; i++)
		{
			if (m_roomSession.m_userList[i].m_szUserID == userSession.m_szUserID)
			{
				m_roomSession.m_userList[i].m_ucUserData1 = userSession.m_ucUserData1;
				m_roomSession.m_userList[i].m_ucUserData2 = userSession.m_ucUserData2;
				m_roomSession.m_userList[i].m_ucUserData3 = userSession.m_ucUserData3;
				m_roomSession.m_userList[i].m_ucUserData4 = userSession.m_ucUserData4;
				break;
			}
		}		

		Debug.Log("Recv_ROOM_USER_UPDATE" + userSession.m_szUserID);

		//SceneSample.s.RoomUserUpdate(userSession);
	}
	public void Recv_ROOM_UPDATE(BinaryReader br)
	{
		m_roomSession.ReadBin(br);
		Debug.Log("Recv_ROOM_UPDATE : " + m_roomSession.m_RoomNo.ToString() );

		//SceneSample.s.RoomUpdate();
	}

	public void UserLogin(string szID, byte byGroup)
	{
		NetManager.instance.Send_WAIT_LOGIN(szID, byGroup, this.gameObject);	
	}

	public void OnRecvWaitLogin(BinaryReader br)
	{
		ushort usResult = br.ReadUInt16();
		m_userHandle.ReadBin(br);

		//SceneSample.s.UserLoginResult(usResult);
	}

	public void RoomData(string szData)
	{
		NetManager.instance.Send_ROOM_DATA( szData );	
	}
	public void RoomUserUpdate(UserSession userSession)
	{
		NetManager.instance.Send_ROOM_USER_UPDATE( userSession );	
	}
	public void RoomUserMove(UserSession userSession)
	{
		NetManager.instance.Send_ROOM_USER_MOVE( userSession );	
	}
}
