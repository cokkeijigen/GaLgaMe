#include <iostream>
#include <windows.h>
#include <string_buffer.hpp>

namespace utils::xstr {

	auto string_buffer::wstring(uint32_t cdpg) -> std::wstring 
	{
		std::wstring result(::MultiByteToWideChar(cdpg, 0, this->m_Buffer.data(), -1, 0, 0) - 1, 0);
		::MultiByteToWideChar(cdpg, 0, this->m_Buffer.data(), -1, const_cast<wchar_t*>(result.c_str()), result.size());
		return result;
	}

	auto string_buffer::convert_encoding(uint32_t o_cdpg, uint32_t n_cdpg) -> string_buffer& 
	{
		auto&& w_str = std::wstring(::MultiByteToWideChar(o_cdpg, 0, this->m_Buffer.data(), -1, 0, 0) - 1, 0);
		::MultiByteToWideChar(o_cdpg, 0, this->m_Buffer.data(), -1, const_cast<wchar_t*>(w_str.c_str()), w_str.size());
		auto length = ::WideCharToMultiByte(n_cdpg, 0, w_str.c_str(), -1, 0, 0, 0, 0) - 1;
		if (size_t(length) > this->m_CharCount)
		{
			this->check(length - this->m_CharCount);
		}
		::WideCharToMultiByte(n_cdpg, 0, w_str.c_str(), -1, this->m_Buffer.data(), this->m_Buffer.size(), 0, 0);
		this->m_CharCount = length;
		return *this;
	}

	auto string_buffer::convert_to_utf8(uint32_t o_cdpg) -> string_buffer& 
	{
		return this->convert_encoding(o_cdpg, CP_UTF8);
	}

	auto string_buffer::wstring_buffer(uint32_t cdpg) -> xstr::wstring_buffer
	{
		xstr::wstring_buffer result(::MultiByteToWideChar(cdpg, 0, this->m_Buffer.data(), -1, 0, 0));
		::MultiByteToWideChar(cdpg, 0, this->m_Buffer.data(), -1, result.data(), result.size());
		result.recount();
		return result;
	}

	auto wstring_buffer::string(uint32_t cdpg) -> std::string
	{
		std::string result(::WideCharToMultiByte(cdpg, 0, this->m_Buffer.data(), -1, 0, 0, 0, 0) -  1, 0);
		::WideCharToMultiByte(cdpg, 0, this->m_Buffer.data(), -1, const_cast<char*>(result.data()), result.size(), 0, 0);
		return result;
	}

	auto wstring_buffer::string_buffer(uint32_t cdpg) -> xstr::string_buffer
	{
		xstr::string_buffer result(::WideCharToMultiByte(cdpg, 0, this->m_Buffer.data(), -1, 0, 0, 0, 0));
		::WideCharToMultiByte(cdpg, 0, this->m_Buffer.data(), -1, const_cast<char*>(result.data()), result.size(), 0, 0);
		result.recount();
		return result;
	}

};