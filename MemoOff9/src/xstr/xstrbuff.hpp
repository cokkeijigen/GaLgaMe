#pragma once
#include <string_buffer.hpp>

namespace xstr {

	using namespace utils::xstr;

	template<class T>
	class buffer : public base_xstring_buffer<buffer<T>, T>
	{
	public:
		using base_xstring_buffer<buffer<T>, T>::base_xstring_buffer;
	};

	template<>
	class buffer<char> : public string_buffer
	{
	public:
		string_buffer::string_buffer;
	};

	template<>
	class buffer<unsigned char> : public string_buffer
	{
	public:
		string_buffer::string_buffer;
	};

	template<>
	class buffer<char8_t> : public string_buffer
	{
	public:
		string_buffer::string_buffer;
	};

	template<>
	class buffer<char16_t> : public wstring_buffer
	{
	public:
		wstring_buffer::wstring_buffer;
	};

	template<>
	class buffer<wchar_t> : public wstring_buffer
	{
	public:
		wstring_buffer::wstring_buffer;
	};
}