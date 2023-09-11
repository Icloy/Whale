using System;
using System.Collections.Generic;
using System.IO;

namespace KWNET
{
	[System.Serializable]
	public class UserBase
	{
		public byte m_byMainNo	= 0;	// 메인서버 번호
		public Int64 m_nUserNo = 0;		// 유저번호
		public string m_szUserID = "";   // 유저아이디

		virtual public void ReadBin(BinaryReader br)
		{
			m_byMainNo = br.ReadByte();
			m_nUserNo = br.ReadInt64();
			NetString.ReadString(br, ref m_szUserID);
		}

		virtual public void WriteBin(BinaryWriter bw)
		{
			bw.Write((byte)m_byMainNo);
			bw.Write((Int64)m_nUserNo);
			NetString.WriteString(bw, m_szUserID);
		}
	}
	public class UserSession : UserBase
	{
		public NetVector3[] m_userTransform = new NetVector3[NetConst.SIZE_USER_TRASNFORM];
		public byte m_ucUserData1;
		public byte m_ucUserData2;
		public byte m_ucUserData3;
		public byte m_ucUserData4;

		override public void ReadBin(BinaryReader br)
		{
			base.ReadBin(br);

			for (int i = 0; i < NetConst.SIZE_USER_TRASNFORM; i++)
			{
				m_userTransform[i] = new NetVector3();
				m_userTransform[i].ReadBin(br);
			}
			m_ucUserData1 = br.ReadByte();
			m_ucUserData2 = br.ReadByte();
			m_ucUserData3 = br.ReadByte();
			m_ucUserData4 = br.ReadByte();
		}

		override public void WriteBin(BinaryWriter bw)
		{
			base.WriteBin(bw);

			for(int i = 0; i < NetConst.SIZE_USER_TRASNFORM; i++)
				m_userTransform[i].WriteBin(bw);

			bw.Write((byte)m_ucUserData1);
			bw.Write((byte)m_ucUserData2);
			bw.Write((byte)m_ucUserData3);
			bw.Write((byte)m_ucUserData4);
		}
	}

	public class UserHandle : UserBase
	{
		override public void ReadBin(BinaryReader br)
		{
			base.ReadBin(br);
		}

		override public void WriteBin(BinaryWriter bw)
		{

		}
	}
}
