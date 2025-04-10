#define _CRT_SECURE_NO_WARNINGS
#include <iostream>
#include <windows.h>
#include <xstr.hpp>

extern "C" {

    __declspec(dllexport) auto _patch_by_iTsukezigen_(void) -> void
    {
    }

    static auto MemWrite(LPVOID Addr, LPVOID Buf, size_t Size) -> bool
    {
        DWORD  Protect{ NULL };
        SIZE_T Written{ NULL };
        if (::VirtualProtect(Addr, Size, PAGE_EXECUTE_READWRITE, &Protect))
        {
            static_cast<void>(WriteProcessMemory(INVALID_HANDLE_VALUE, Addr, Buf, Size, &Written));
            static_cast<void>(VirtualProtect(Addr, Size, Protect, &Protect));
            return Size == Written;
        }
        return false;
    }

    static auto InitTargetLanguage(void) -> void
    {
        auto hModule = HMODULE{ ::GetModuleHandleW(NULL) };

        auto tarPatch{ reinterpret_cast<intptr_t>(hModule) + 0xE0378 };

        utils::string::buffer buffer{ 1024 };
        auto get_module_file_name = DWORD
        {
            ::GetModuleFileNameA(hModule, buffer.data(), buffer.size())
        };

        if (get_module_file_name)
        {
            buffer.recount();
            auto splits{ buffer.to_lower().split_of(".", "_", "-") };
            for (auto& arg : std::ranges::reverse_view(splits))
            {
                if (arg == "chs")
                {
                    auto flag = int{ 0x0000000B };
                    MemWrite(reinterpret_cast<LPVOID>(tarPatch), &flag, 0x04);
                    return;
                }
                if (arg == "cht")
                {
                    auto flag = int{ 0x0000000A };
                    MemWrite(reinterpret_cast<LPVOID>(tarPatch), &flag, 0x04);
                    return;
                }
            }
        }
        auto flag = int{ 0x00000000 };
        MemWrite(reinterpret_cast<LPVOID>(tarPatch), &flag, 0x04);
    }

    auto APIENTRY DllMain(HMODULE, DWORD ul_reason_for_call, LPVOID) -> BOOL
    {
        if (DLL_PROCESS_ATTACH == ul_reason_for_call)
        {
            InitTargetLanguage();
        }
        return TRUE;
    }
}
