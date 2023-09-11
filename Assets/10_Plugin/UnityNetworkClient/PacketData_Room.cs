using System;
using System.Collections.Generic;
using System.IO;

namespace KWNET
{
	[System.Serializable]
	public class RoomBase
	{
		public int m_RoomNo = 0;
		public Int64 m_nMakeUserNo = 0;
		public string m_szRoomCode  = "";  

		virtual public void ReadBin(BinaryReader br)
		{
			m_RoomNo = br.ReadInt32();
			m_nMakeUserNo = br.ReadInt64();
			NetString.ReadString(br, ref m_szRoomCode);
		}

		virtual public void WriteBin(BinaryWriter bw)
		{

		}
	}
	public class RoomSession : RoomBase
	{
		public List<UserSession> m_userList = new List<UserSession>();

		override public void ReadBin(BinaryReader br)
		{
			base.ReadBin(br);

			m_userList.Clear();
			int size = br.ReadInt32();
			for (int i = 0; i < size; i++)
			{
				UserSession data = new UserSession();
				data.ReadBin(br);
				m_userList.Add(data);
			}
		}

		override public void WriteBin(BinaryWriter bw)
		{

		}
	}
}
