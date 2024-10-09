using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;

class AutoShitter
{
    // if youre here to read the code im so sorry... its actual brainrot... ANYWAYS HAVE FUNNN BAIIII

    #region dumb shit

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
    private static int delay = 1;

    private static bool isgamertoggled = false;

    #endregion

    #region dumb shit PART 2 (EVIL) (DARK) (TWISTED)
    private static void helpformeandnoobs()
    {
        Console.WriteLine(" ");
        Console.WriteLine("Available Commands:");
        Console.WriteLine("L_Toggle(Button Code)    - Sets the button for toggling auto-clicking.");
        Console.WriteLine("L_Hold(Button Code)      - Sets the button for holding down to click.");
        Console.WriteLine("L_PrimaryClick           - Sets the click type to primary (Left Mouse Button).");
        Console.WriteLine("L_SecondaryClick         - Sets the click type to secondary (Right Mouse Button).");
        Console.WriteLine("L_Delay(Miliseconds)     - Sets the time between each click.");
        Console.WriteLine("L_Reset                  - Reset all settings to defaults.");
        Console.WriteLine("L_Code(Button Input)     - Output the hex code for the specified Button.");
        Console.WriteLine("help                     - Display this help information.");
        Console.WriteLine("helpbind                 - Display the keybind help information.");
        Console.WriteLine(" ");
        Console.WriteLine("For any other help or bug reporting go to discord.gg/FwmGf6vBu2");
    }

    private static void keybindhelpersingerest()
    {
        Console.WriteLine(" ");
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

    private static void nowclickwiththeleftbutton() // press prinmy
    {
        mouse_event(prumridown, 0, 0, 0, 0);
        mouse_event(primryup, 0, 0, 0, 0);
    }

    private static void klikwiththerightbutton() // press secondaty
    {
        mouse_event(secondraydown, 0, 0, 0, 0);
        mouse_event(secondrymusknappupp, 0, 0, 0, 0);
    }

    private static bool istehthingpressedyslashn(int key) // check if you pressing or holding or whaterev the thing FOR FREE
    {
        return (GetAsyncKeyState(key) & 0x8000) != 0;
    }

    private static int GetButtonCode(string button) //used for the settitng for the thing
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
            case "Mouse1": 
                return primarkiliker;
            case "Mouse2": 
                return seconfklik;
            case "Mouse4": 
                return defuldgold;
            case "Mouse5": 
                return defalttogle;
            default: return -1;
        }
    }

    private static void L_Toggle(int button) // yes this button set for toggle yes yes
    {
        andthetogglebuttonisthisthingy = button;
        Console.WriteLine($"Toggle button set to: {button}");
    }

    private static void L_Hold(int button) // now the setting for the uhhhhhh hold button yesssss thank you so much thats just what i need cz i need to BUZZ
    {
        holdingbuttonisthisthingy = button;
        Console.WriteLine($"Hold button set to: {button}");
    } 
    
    private static void L_Delay(int delayer) // for the delay commndeand
    {
        delay = delayer;
        Console.WriteLine($"Delay between kliks set to: {delayer}");
    }

    private static void L_PrimaryClick() // u wanna primary or secondary click_? this is primary
    {
        klikkatyper = primarkiliker;
        Console.WriteLine("Click type set to: Primary (Mouse1)");
    }

    private static void L_SecondaryClick() // and this is secondaryty
    {
        klikkatyper = seconfklik;
        Console.WriteLine("Click type set to: Secondary (Mouse2)");
    }

    private static void L_Reset() // reset to my defaultsts
    {
        holdingbuttonisthisthingy = defuldgold;
        andthetogglebuttonisthisthingy = defalttogle;
        klikkatyper = primarkiliker;
        yestoggle = false;
        isgamertoggled = false;
        Console.WriteLine("Settings reset to defaults: Hold on Mouse4, Toggle on Mouse5, klikkatyper to Primary (Mouse1).");
    }

    private static void ProcessCommand(string input) // read your stupid shit
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

            if (command.Equals("L_Delay", StringComparison.OrdinalIgnoreCase))
            {
                if (int.TryParse(buttonStr, out int delayer))
                {
                    L_Delay(delayer);
                    if (delayer < 50)
                    Console.WriteLine("just saying this rn.. low delay values COULD make ur pc run it so fast it doesnt register the kliks properly (1 has worked for me)");
                }
                else
                {
                    Console.WriteLine("sorry mane but i dont know what the sigma that i supposed to mean.. thats not a number i recognise");
                }
                return;
            }

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
                    case "L_Delay":
                        L_Hold(buttonCode);
                        break;
                    default:
                        Console.WriteLine("Wharrrr????");
                        break;
                }
            }
            else
            {
                int ARGH = new Random().Next(1, 4);
                switch (ARGH)
                {
                    case 1:
                        Console.WriteLine($"`{input}` isnt something i understand.... whar mean?? type `help` or `helpbind` ok??? then you the thing");
                        break;
                    case 2:
                        Console.WriteLine($"Buh??? Whar you mean `{input}`!?1 you dont understand???+ type `help` or `helpbind` before doing whatever you do ok?");
                        break;
                    case 3:
                        Console.WriteLine($"Wharrrrr????? Huhhhhhh??????? wha is `{input}`??? me not get... type `help` or `helpbind` for to know what do ok?");
                        break;
                }
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
            int ARGH = new Random().Next(1, 4);
            switch (ARGH)
            {
                case 1:
                    Console.WriteLine($"`{input}` isnt something i understand.... whar mean?? type `help` or `helpbind` ok??? then you the thing");
                    break;
                case 2:
                    Console.WriteLine($"Buh??? Whar you mean `{input}`!?1 you dont understand???+ type `help` or `helpbind` before doing whatever you do ok?");
                    break;
                case 3:
                    Console.WriteLine($"Wharrrrr????? Huhhhhhh??????? wha is `{input}`??? me not get... type `help` or `helpbind` for to know what do ok?");
                    break;
            }
        }
    }

    private static void L_Code(string keyInput) // here uyou get the code for the thing you want to binding
    {
        if (keyInput.Length == 1)
        {
            char keyChar = char.ToUpper(keyInput[0]);
            int virtualKeyCode = (int)keyChar;
            Console.WriteLine($"Code for {keyInput} = 0x{virtualKeyCode:X2}. hope that helps!111!!!!11");
        }
        else
        {
            Console.WriteLine($"whar is {keyInput}... can you just input like one character or something not as weird??? thanks!111!!!!1");
        }
    }

    public static void thingyasyncnc() // i should delete this but im too lazy
    {
        if (!isgamertoggled)
        {
            isgamertoggled = true;
            yestoggle = !yestoggle;
        }
    }

    #endregion

    #region mian? mnai? niam or however you spell it
    static void Main()
    {
        int ARGH = new Random().Next(1, 4);
        switch (ARGH)
        {
            case 1:
                Console.Title = "ato kliker!!!1";
                break;
            case 2:
                Console.Title = "gayming free from all punjabi viruses!!! (WORKING 2023) (NO SCAM) (NO VIRUS) (FREE RUBOX)";
                break;
            case 3:
                Console.Title = "made because logitech GHub was too slow for me :3";
                break;
        }

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
                Thread.Sleep(delay);
            }

            if (istehthingpressedyslashn(andthetogglebuttonisthisthingy))
            {
                thingyasyncnc();
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
                Thread.Sleep(delay);
            }
        }
        #endregion
    }
}
