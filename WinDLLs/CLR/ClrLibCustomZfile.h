#pragma once
#include <direct.h>
#include <iostream>
#include <cstdio>
#include <msclr/marshal.h>
#include <string>

using namespace System;
using namespace msclr::interop;

namespace ClrLibCustomZfile {
	public ref class LocalFileManager
	{
		static marshal_context^ context = gcnew marshal_context();

		// TODO: 在此处为此类添加方法。
	public:
		static bool MoveFileOrDir(String^ origin, String^ target);
		static bool CopyFile(String^ origin, String^ target);
		static bool CopyDir(String^ origin, String^ target);
		static bool DelFile(String^ filePath);
		static bool DelDir(String^ dirPath);
		static bool CreateDir(String^ path);
		static bool RenameFileOrDir(String^ oldPath, String^ newPath);

		static void Say(String^ input) {
			std::cout << CLRStringToChar(input) << std::endl;
		}


	private:

		LocalFileManager(){}

		static const char* CLRStringToChar(String^ input) {
			
			const char* output = context->marshal_as<const char*>(input);
			return output;
		}
	};
}
