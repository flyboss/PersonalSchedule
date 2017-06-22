// ������ DLL �ļ���

#include "stdafx.h"

#include "EncryptLib.h"

namespace EncryptLib
{
	int myMD5::Add(int a, int b)
	{
		return a + b;
	}
	void myMD5::mainLoop(cli::array<unsigned int> ^M)
	{
		unsigned int f, g;
		unsigned int a = atemp;
		unsigned int b = btemp;
		unsigned int c = ctemp;
		unsigned int d = dtemp;
		for (unsigned int i = 0; i < 64; i++)
		{
			if (i<16) {
				f = F(b, c, d);
				g = i;
			}
			else if (i<32)
			{
				f = G(b, c, d);
				g = (5 * i + 1) % 16;
			}
			else if (i<48) {
				f = H(b, c, d);
				g = (3 * i + 5) % 16;
			}
			else {
				f = I(b, c, d);
				g = (7 * i) % 16;
			}
			unsigned int tmp = d;
			d = c;
			c = b;
			b = b + shift((a + f + k[i] + M[g]), s[i]);
			a = tmp;
		}
		atemp = a + atemp;
		btemp = b + btemp;
		ctemp = c + ctemp;
		dtemp = d + dtemp;
	}
	cli::array<unsigned int> ^ myMD5::add(System::String ^ str)
	{
		unsigned int num = ((str->Length + 8) / 64) + 1;//��512λ,64���ֽ�Ϊһ��
		cli::array<unsigned int> ^strByte = gcnew cli::array<unsigned int>(num * 16);//64/4=16,������16������	   
		strlength = num * 16;
		for (unsigned int i = 0; i < num * 16; i++)
			strByte[i] = 0;
		for (unsigned int i = 0; i <str->Length; i++)
		{
			strByte[i >> 2] |= (str[i]) << ((i % 4) * 8);//һ�������洢�ĸ��ֽڣ�i>>2��ʾi/4 һ��unsigned int��Ӧ4���ֽڣ�����4���ַ���Ϣ
		}
		strByte[str->Length >> 2] |= 0x80 << (((str->Length % 4)) * 8);//β�����1 һ��unsigned int����4���ַ���Ϣ,������128����
																		 /*
																		 *���ԭ���ȣ�����ָλ�ĳ��ȣ�����Ҫ��8��Ȼ����С�������Է��ڵ����ڶ���,���ﳤ��ֻ����32λ
																		 */
		strByte[num * 16 - 2] = str->Length * 8;
		return strByte;
	}
	System::String ^ myMD5::changeHex(int a) {
		int b;

		System::String ^ str = gcnew System::String("");;
		for (int i = 0; i<4; i++)
		{
			System::String ^ str1 = gcnew System::String("");
			b = ((a >> i * 8) % (1 << 8)) & 0xff;   //������ÿ���ֽ�
			for (int j = 0; j < 2; j++)
			{
				str1=str1->Insert(0, str16->Substring(b % 16, 1));
				b = b / 16;
			}
			str=str->Insert(str->Length, str1);
		}
		return str;
	}
	System::String ^ myMD5::getMD5(System::String ^ source) 
	{
		atemp = A;    //��ʼ��
		btemp = B;
		ctemp = C;
		dtemp = D;
		cli::array<unsigned int> ^strByte = add(source);
		for (unsigned int i = 0; i<strlength / 16; i++)
		{
			cli::array<unsigned int> ^num=gcnew cli::array<unsigned int>(16);
			for (unsigned int j = 0; j<16; j++)
				num[j] = strByte[i * 16 + j];
			mainLoop(num);
		}
		System::String ^ str=changeHex(atemp);
		str=str->Insert(str->Length, changeHex(btemp));
		str=str->Insert(str->Length, changeHex(ctemp));
		str=str->Insert(str->Length, changeHex(dtemp));
		return str;
	}
}

