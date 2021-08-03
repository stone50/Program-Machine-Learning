﻿using System.Collections.Generic;

namespace WindowsFormsApp1
{
    public class KeyCodes
    {
        public static Dictionary<string, int> Codes = new Dictionary<string, int>(){
            { "Left Mouse Button", 0x01 },
            { "Right Mouse Button", 0x02 },
            { "Control-Break Processing", 0x03 },
            { "Middle Mouse Button", 0x04 },
            { "X1 Mouse Button", 0x05 },
            { "X2 Mouse Button", 0x06 },
            { "BACKSPACE Key", 0x08 },
            { "TAB Key", 0x09 },
            { "CLEAR Key", 0x0C },
            { "ENTER Key", 0x0D },
            { "SHIFT Key", 0x10 },
            { "CTRL Key", 0x11 },
            { "ALT Key", 0x12 },
            { "PAUSE Key", 0x13 },
            { "CAPS LOCK Key", 0x14 },
            { "ESC Key", 0x1B },
            { "SPACEBAR", 0x20 },
            { "PAGE UP Key", 0x21 },
            { "PAGE DOWN Key", 0x22 },
            { "END Key", 0x23 },
            { "HOME Key", 0x24 },
            { "LEFT ARROW Key", 0x25 },
            { "UP ARROW Key", 0x26 },
            { "RIGHT ARROW Key", 0x27 },
            { "DOWN ARROW Key", 0x28 },
            { "SELECT Key", 0x29 },
            { "PRINT Key", 0x2A },
            { "EXECUTE Key", 0x2B },
            { "PRINT SCREEN Key", 0x2C },
            { "INS Key", 0x2D },
            { "DEL Key", 0x2E },
            { "HELP Key", 0x2F },
            { "0 Key", 0x30 },
            { "1 Key", 0x31 },
            { "2 Key", 0x32 },
            { "3 Key", 0x33 },
            { "4 Key", 0x34 },
            { "5 Key", 0x35 },
            { "6 Key", 0x36 },
            { "7 Key", 0x37 },
            { "8 Key", 0x38 },
            { "9 Key", 0x39 },
            { "A Key", 0x41 },
            { "B Key", 0x42 },
            { "C Key", 0x43 },
            { "D Key", 0x44 },
            { "E Key", 0x45 },
            { "F Key", 0x46 },
            { "G Key", 0x47 },
            { "H Key", 0x48 },
            { "I Key", 0x49 },
            { "J Key", 0x4A },
            { "K Key", 0x4B },
            { "L Key", 0x4C },
            { "M Key", 0x4D },
            { "N Key", 0x4E },
            { "O Key", 0x4F },
            { "P Key", 0x50 },
            { "Q Key", 0x51 },
            { "R Key", 0x52 },
            { "S Key", 0x53 },
            { "T Key", 0x54 },
            { "U Key", 0x55 },
            { "V Key", 0x56 },
            { "W Key", 0x57 },
            { "X Key", 0x58 },
            { "Y Key", 0x59 },
            { "Z Key", 0x5A },
            { "Left Windows Key", 0x5B },
            { "Right Windows Key", 0x5C },
            { "Applications Key", 0x5D },
            { "Computer Sleep Key", 0x5F },
            { "Numeric Keypad 0 Key", 0x60 },
            { "Numeric Keypad 1 Key", 0x61 },
            { "Numeric Keypad 2 Key", 0x62 },
            { "Numeric Keypad 3 Key", 0x63 },
            { "Numeric Keypad 4 Key", 0x64 },
            { "Numeric Keypad 5 Key", 0x65 },
            { "Numeric Keypad 6 Key", 0x66 },
            { "Numeric Keypad 7 Key", 0x67 },
            { "Numeric Keypad 8 Key", 0x68 },
            { "Numeric Keypad 9 Key", 0x69 },
            { "Numeric Keypad Multiply Key", 0x6A },
            { "Numeric Keypad Add Key", 0x6B },
            { "Numeric Keypad Separator Key", 0x6C },
            { "Numeric Keypad Subtract Key", 0x6D },
            { "Numeric Keypad Decimal Key", 0x6E },
            { "Numeric Keypad Divide Key", 0x6F },
            { "F1 Key", 0x70 },
            { "F2 Key", 0x71 },
            { "F3 Key", 0x72 },
            { "F4 Key", 0x73 },
            { "F5 Key", 0x74 },
            { "F6 Key", 0x75 },
            { "F7 Key", 0x76 },
            { "F8 Key", 0x77 },
            { "F9 Key", 0x78 },
            { "F10 Key", 0x79 },
            { "F11 Key", 0x7A },
            { "F12 Key", 0x7B },
            { "F13 Key", 0x7C },
            { "F14 Key", 0x7D },
            { "F15 Key", 0x7E },
            { "F16 Key", 0x7F },
            { "F17 Key", 0x80 },
            { "F18 Key", 0x81 },
            { "F19 Key", 0x82 },
            { "F20 Key", 0x83 },
            { "F21 Key", 0x84 },
            { "F22 Key", 0x85 },
            { "F23 Key", 0x86 },
            { "F24 Key", 0x87 },
            { "NUM LOCK Key", 0x90 },
            { "SCROLL LOCK Key", 0x91 },
            { "Left SHIFT Key", 0xA0 },
            { "Right SHIFT Key", 0xA1 },
            { "Left CONTROL Key", 0xA2 },
            { "Right CONTROL Key", 0xA3 },
            { "Left MENU Key", 0xA4 },
            { "Right MENU Key", 0xA5 },
            { "Browser Back Key", 0xA6 },
            { "Browser Forward Key", 0xA7 },
            { "Browser Refresh Key", 0xA8 },
            { "Browser Stop Key", 0xA9 },
            { "Browser Search Key", 0xAA },
            { "Browser Favorites Key", 0xAB },
            { "Browser Start and Home Key", 0xAC },
            { "Volume Mute Key", 0xAD },
            { "Volume Down Key", 0xAE },
            { "Volume Up Key", 0xAF },
            { "Next Track Key", 0xB0 },
            { "Previous Track Key", 0xB1 },
            { "Stop Media Key", 0xB2 },
            { "Play/Pause Media Key", 0xB3 },
            { "Start Mail Key", 0xB4 },
            { "Select Media Key", 0xB5 },
            { "Start Application 1 Key", 0xB6 },
            { "Start Application 2 Key", 0xB7 },
            { "Semicolon Key", 0xBA },
            { "Plus Key", 0xBB },
            { "Comma Key", 0xBC },
            { "Dash Key", 0xBD },
            { "Period Key", 0xBE },
            { "Forward Slash Key", 0xBF },
            { "Backtick Key", 0xC0 },
            { "Left Bracket Key", 0xDB },
            { "Back Slash Key", 0xDC },
            { "Right Bracket Key", 0xDD },
            { "Single Quote Key", 0xDE },
            { "Attn Key", 0xF6 },
            { "CrSel Key", 0xF7 },
            { "ExSel Key", 0xF8 },
            { "Erase EOF Key", 0xF9 },
            { "Play Key", 0xFA },
            { "Zoom Key", 0xFB },
            { "PA1 Key", 0xFD },
            { "Clear Key", 0xFE }
        };

        public static string[] Names = new string[] {
            "Left Mouse Button",
            "Right Mouse Button",
            "Control-Break Processing",
            "Middle Mouse Button",
            "X1 Mouse Button",
            "X2 Mouse Button",
            "BACKSPACE Key",
            "TAB Key",
            "CLEAR Key",
            "ENTER Key",
            "SHIFT Key",
            "CTRL Key",
            "ALT Key",
            "PAUSE Key",
            "CAPS LOCK Key",
            "ESC Key",
            "SPACEBAR",
            "PAGE UP Key",
            "PAGE DOWN Key",
            "END Key",
            "HOME Key",
            "LEFT ARROW Key",
            "UP ARROW Key",
            "RIGHT ARROW Key",
            "DOWN ARROW Key",
            "SELECT Key",
            "PRINT Key",
            "EXECUTE Key",
            "PRINT SCREEN Key",
            "INS Key",
            "DEL Key",
            "HELP Key",
            "0 Key",
            "1 Key",
            "2 Key",
            "3 Key",
            "4 Key",
            "5 Key",
            "6 Key",
            "7 Key",
            "8 Key",
            "9 Key",
            "A Key",
            "B Key",
            "C Key",
            "D Key",
            "E Key",
            "F Key",
            "G Key",
            "H Key",
            "I Key",
            "J Key",
            "K Key",
            "L Key",
            "M Key",
            "N Key",
            "O Key",
            "P Key",
            "Q Key",
            "R Key",
            "S Key",
            "T Key",
            "U Key",
            "V Key",
            "W Key",
            "X Key",
            "Y Key",
            "Z Key",
            "Left Windows Key",
            "Right Windows Key",
            "Applications Key",
            "Computer Sleep Key",
            "Numeric Keypad 0 Key",
            "Numeric Keypad 1 Key",
            "Numeric Keypad 2 Key",
            "Numeric Keypad 3 Key",
            "Numeric Keypad 4 Key",
            "Numeric Keypad 5 Key",
            "Numeric Keypad 6 Key",
            "Numeric Keypad 7 Key",
            "Numeric Keypad 8 Key",
            "Numeric Keypad 9 Key",
            "Numeric Keypad Multiply Key",
            "Numeric Keypad Add Key",
            "Numeric Keypad Separator Key",
            "Numeric Keypad Subtract Key",
            "Numeric Keypad Decimal Key",
            "Numeric Keypad Divide Key",
            "F1 Key",
            "F2 Key",
            "F3 Key",
            "F4 Key",
            "F5 Key",
            "F6 Key",
            "F7 Key",
            "F8 Key",
            "F9 Key",
            "F10 Key",
            "F11 Key",
            "F12 Key",
            "F13 Key",
            "F14 Key",
            "F15 Key",
            "F16 Key",
            "F17 Key",
            "F18 Key",
            "F19 Key",
            "F20 Key",
            "F21 Key",
            "F22 Key",
            "F23 Key",
            "F24 Key",
            "NUM LOCK Key",
            "SCROLL LOCK Key",
            "Left SHIFT Key",
            "Right SHIFT Key",
            "Left CONTROL Key",
            "Right CONTROL Key",
            "Left MENU Key",
            "Right MENU Key",
            "Browser Back Key",
            "Browser Forward Key",
            "Browser Refresh Key",
            "Browser Stop Key",
            "Browser Search Key",
            "Browser Favorites Key",
            "Browser Start and Home Key",
            "Volume Mute Key",
            "Volume Down Key",
            "Volume Up Key",
            "Next Track Key",
            "Previous Track Key",
            "Stop Media Key",
            "Play/Pause Media Key",
            "Start Mail Key",
            "Select Media Key",
            "Start Application 1 Key",
            "Start Application 2 Key",
            "Semicolon Key",
            "Plus Key",
            "Comma Key",
            "Dash Key",
            "Period Key",
            "Forward Slash Key",
            "Backtick Key",
            "Left Bracket Key",
            "Back Slash Key",
            "Right Bracket Key",
            "Single Quote Key",
            "Attn Key",
            "CrSel Key",
            "ExSel Key",
            "Erase EOF Key",
            "Play Key",
            "Zoom Key",
            "PA1 Key",
            "Clear Key"
        };
    }
}