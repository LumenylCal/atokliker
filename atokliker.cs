using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Threading;

class AutoShitter
{
    [DllImport("user32.dll", SetLastError = true)]
    private static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint dwData, int dwExtraInfo);

    [DllImport("user32.dll", SetLastError = true)]
    private static extern short GetAsyncKeyState(int vKey);

    private const uint prumridown = 0x02;
    private const uint primryup = 0x04;
    private const uint secondraydown = 0x08;
    private const uint secondrymusknappupp = 0x10;

    private const int defalttogle = 0x05;
    private const int defuldgold = 0x06;
    private const int primarkiliker = 0x01;
    private const int seconfklik = 0x02;

    private static bool yestoggle;
    private static int holdingbuttonisthisthingy = defuldgold;
    private static int andthetogglebuttonisthisthingy = defalttogle;
    private static int klikkatyper = primarkiliker;

    private static bool isgamertoggled = false;

    private static void helpformeandnoobs()
    {
        Console.WriteLine("Available Commands:");
        Console.WriteLine("L_Toggle(button)    - Set the button for toggling auto-clicking.");
        Console.WriteLine("L_Hold(button)      - Set the button for holding down to click.");
        Console.WriteLine("L_PrimaryClick      - Set the click type to primary (Left Mouse Button).");
        Console.WriteLine("L_SecondaryClick    - Set the click type to secondary (Right Mouse Button).");
        Console.WriteLine("L_Reset             - Reset all settings to defaults.");
        Console.WriteLine("L_Code(character)   - Output the hex code for the specified character.");
        Console.WriteLine("help                - Display this help information.");
        Console.WriteLine("helpbind            - Display the keybind help information.");
        Console.WriteLine(" ");
        Console.WriteLine("discord.gg/FwmGf6vBu2");
    }

    private static void keybindhelpersingerest()
    {
        Console.WriteLine("hi chat!!!1");
        Console.WriteLine("if youre like me then you have no clue how to use this!!!");
        Console.WriteLine("either because you have dementia like me OR because youre new here");
        Console.WriteLine(" ");
        Console.WriteLine("so to get started");
        Console.WriteLine("first type `L_Code(the button you want to bind)`");
        Console.WriteLine("then type `L_Hold(The outputted code) or L_Toggle(The outputted code) depending on what you want to bind`");
        Console.WriteLine(" ");
        Console.WriteLine("okay so theres some buttons that you  cant bind because im lazy so to bind them to mouse buttons just type like");
        Console.WriteLine("L_Hold(Mouse5) or lajk L_Toggle(Mouse4)");
        Console.WriteLine("heres a list of the mouse binds");
        Console.WriteLine("Mouse1, Mouse2, Mouse3, Mouse4, Mouse5");
        Console.WriteLine(" ");
        Console.WriteLine("then uhhhh thats it i think dont @ me if it doesnt work for you ");
        Console.WriteLine(" ");
        Console.WriteLine("HAVE FUNN!!111!!1111!");
    }

    private static void nowclickwiththeleftbutton()
    {
        mouse_event(prumridown, 0, 0, 0, 0);
        mouse_event(primryup, 0, 0, 0, 0);
    }

    private static void klikwiththerightbutton()
    {
        mouse_event(secondraydown, 0, 0, 0, 0);
        mouse_event(secondrymusknappupp, 0, 0, 0, 0);
    }

    private static bool istehthingpressedyslashn(int key)
    {
        return (GetAsyncKeyState(key) & 0x8000) != 0;
    }

    private static int GetButtonCode(string button)
    {
        if (button.StartsWith("0x"))
        {
            if (int.TryParse(button.Substring(2), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out int hexCode))
            {
                return hexCode;
            }
            else
            {
                return -1;
            }
        }

        switch (button)
        {
            case "Mouse1": return primarkiliker;
            case "Mouse2": return seconfklik;
            case "Mouse4": return defuldgold;
            case "Mouse5": return defalttogle;
            default: return -1;
        }
    }

    private static void L_Toggle(int button)
    {
        andthetogglebuttonisthisthingy = button;
        Console.WriteLine("Toggle button set to: " + button);
    }

    private static void L_Hold(int button)
    {
        holdingbuttonisthisthingy = button;
        Console.WriteLine("Hold button set to: " + button);
    }

    private static void L_PrimaryClick()
    {
        klikkatyper = primarkiliker;
        Console.WriteLine("Click type set to: Primary (Mouse1)");
    }

    private static void L_SecondaryClick()
    {
        klikkatyper = seconfklik;
        Console.WriteLine("Click type set to: Secondary (Mouse2)");
    }

    private static void L_Reset()
    {
        holdingbuttonisthisthingy = defuldgold;
        andthetogglebuttonisthisthingy = defalttogle;
        klikkatyper = primarkiliker;
        yestoggle = false;
        isgamertoggled = false;
        Console.WriteLine("Settings reset to defaults: Hold on Mouse4, Toggle on Mouse5, klikkatyper to Primary (Mouse1).");
    }

    private static void ProcessCommand(string input)
    {
        string[] parts = input.Split('(');

        if (input.Equals("help", StringComparison.OrdinalIgnoreCase))
        {
            helpformeandnoobs();
            return;
        }

        if (input.Equals("helpbind", StringComparison.OrdinalIgnoreCase))
        {
            keybindhelpersingerest();
            return;
        }

        if (parts.Length == 2)
        {
            string command = parts[0].Trim();
            string buttonStr = parts[1].Trim(')');
            int buttonCode = GetButtonCode(buttonStr);

            if (buttonCode != -1)
            {
                switch (command)
                {
                    case "L_Toggle":
                        L_Toggle(buttonCode);
                        break;
                    case "L_Hold":
                        L_Hold(buttonCode);
                        break;
                    default:
                        Console.WriteLine("Invalid command.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Invalid button: " + buttonStr);
            }
        }
        else if (input == "L_PrimaryClick")
        {
            L_PrimaryClick();
        }
        else if (input == "L_SecondaryClick")
        {
            L_SecondaryClick();
        }
        else if (input == "L_Reset")
        {
            L_Reset();
        }
        else
        {
            Console.WriteLine("Invalid command format.");
        }
    }

    private static void L_Code(string keyInput)
    {
        if (keyInput.Length == 1)
        {
            char keyChar = keyInput[0];
            int virtualKeyCode = (int)keyChar;
            Console.WriteLine($"Code for {keyInput} = 0x{virtualKeyCode:X2}");
        }
        else
        {
            Console.WriteLine($"Invalid input: {keyInput}. Please provide a valid single character.");
        }
    }

    static void Main()
    {
        Console.WriteLine("type `help` for a list of the uhhh commands yea...\nTYPE `helpbind` TO LEARN HOW TO REBIND");
        while (true)
        {
            if (Console.KeyAvailable)
            {
                string input = Console.ReadLine();

                if (input.StartsWith("L_Code("))
                {
                    string keyInput = input.Substring(7, input.Length - 8);
                    L_Code(keyInput);
                }
                else
                {
                    ProcessCommand(input);
                }
            }

            if (istehthingpressedyslashn(holdingbuttonisthisthingy))
            {
                if (klikkatyper == primarkiliker)
                {
                    nowclickwiththeleftbutton();
                }
                else if (klikkatyper == seconfklik)
                {
                    klikwiththerightbutton();
                }
            }

            if (istehthingpressedyslashn(andthetogglebuttonisthisthingy))
            {
                if (!isgamertoggled)
                {
                    isgamertoggled = true;
                    yestoggle = !yestoggle;
                    Thread.Sleep(200);
                }
            }
            else
            {
                if (isgamertoggled)
                {
                    isgamertoggled = false;
                }
            }

            if (yestoggle)
            {
                if (klikkatyper == primarkiliker)
                {
                    nowclickwiththeleftbutton();
                }
                else if (klikkatyper == seconfklik)
                {
                    klikwiththerightbutton();
                }
            }
            Thread.Sleep(1);
        }
    }
}
