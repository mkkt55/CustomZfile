#include "pch.h"
#include "ClrLibCustomZfile.h"

using namespace ClrLibCustomZfile;

bool LocalFileManager::MoveFileOrDir(String^ origin, String^ target) {
	return RenameFileOrDir(origin, target);
}

bool LocalFileManager::CopyFile(String^ origin, String^ target) {
	String^ cmdStr = "copy \"" + origin + "\" \"" + target + "\"";
	cmdStr = cmdStr->Replace("/", "\\");
	const char* cmd = CLRStringToChar(cmdStr);
	if (system(cmd) == 0) {
		return true;
	}
	else {
		return false;
	}
}

bool LocalFileManager::CopyDir(String^ origin, String^ target) {
	String^ cmdStr = "robocopy \"" + origin + "\" \"" + target + "\"";
	cmdStr = cmdStr->Replace("/", "\\");
	cmdStr += " /e";
	//std::cout << cmd << std::endl;
	const char* cmd = CLRStringToChar(cmdStr);
	if (system(cmd) == 0) {
		return true;
	}
	else {
		return false;
	}
}

bool LocalFileManager::DelFile(String^ filePath) {
	const char* path = CLRStringToChar(filePath);
	if (remove(path) == 0)
	{
		return true;
	}
	else
	{
		return false;
	}
}

bool LocalFileManager::DelDir(String^ dirPath) {
	String^ cmdStr = "rmdir /s /q \"" + dirPath->Replace("/", "\\") + "\"";
	const char* cmd = CLRStringToChar(cmdStr);
	//std::cout << cmd << std::endl;
	if (system(cmd) == 0) {
		return true;
	}
	else {
		return false;
	}
}

bool LocalFileManager::CreateDir(String^ dirPath) {
	if (_mkdir(CLRStringToChar(dirPath)) == 0)
	{
		return true;
	}
	else {
		return false;
	}
}

bool LocalFileManager::RenameFileOrDir(String^ oldPath, String^ newPath) {
	if (rename(CLRStringToChar(oldPath), CLRStringToChar(newPath)) == 0) {
		return true;
	}
	else
	{
		return false;
	}
}
