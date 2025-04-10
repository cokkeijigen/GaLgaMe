#pragma once
#define _xtsr_
#include <xstrbuff.hpp>
#include <optional>
#include <charconv>

namespace xstr {

	using namespace utils::xstr;

	using buffer_x = string_buffer;
	using buffer_w = string_buffer;
	
	template<class T = int64_t>
	requires std::is_integral<T>::value
	auto to_integer(std::string_view str, int radix = 10) -> std::optional<T> 
	{
		T number{};
		auto beg{ str.data() };
		auto end{ beg + str.size() };
		std::from_chars_result result
		{ 
			std::from_chars(beg, end, number, radix)
		};

		if (result.ec == std::errc())
		{
			return { number };
		}
		else
		{
			return { std::nullopt };
		}
	}
}

