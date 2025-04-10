#pragma once
#include <base_string_buffer.hpp>
#define _string_buffer_

namespace utils::xstr {

	class string_buffer;
	class wstring_buffer;

	class string_buffer : public base_xstring_buffer<string_buffer, char>
	{
		friend wstring_buffer;
	public:
		using base_xstring_buffer::base_xstring_buffer;

		inline auto string() -> std::string
		{
			return this->substr(0);
		}
		
		auto wstring(uint32_t cdpg = 0) -> std::wstring;

		auto convert_encoding(uint32_t o_cdpg, uint32_t n_cdpg) -> string_buffer&;

		auto convert_to_utf8(uint32_t o_cdpg) -> string_buffer&;

		auto wstring_buffer(uint32_t cdpg = 0) -> wstring_buffer;
	};

	class wstring_buffer : public base_xstring_buffer<string_buffer, wchar_t>
	{
		friend string_buffer;
	public:
		using base_xstring_buffer::base_xstring_buffer;

		inline auto wstring() -> std::wstring
		{
			return this->substr(0);
		}

		auto string(uint32_t cdpg = 0) -> std::string;

		auto string_buffer(uint32_t cdpg = 0) -> string_buffer;
	};

}

namespace utils::string {
	using buffer = utils::xstr::string_buffer;
}

namespace utils::wstring {
	using buffer = utils::xstr::wstring_buffer;
}