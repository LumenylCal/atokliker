using Newtonsoft.Json;
using System.Runtime.InteropServices;

namespace AtoKliker
{
    class AutoShitter
    {
        // if youre here to read the code im so sorry... its actual brainrot... ANYWAYS HAVE FUNNN BAIIII

        #region stupid stuff here (fields mostly)

        [DllImport("user32.dll", SetLastError = true)] private static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint dwData, int dwExtraInfo);
        [DllImport("user32.dll", SetLastError = true)] private static extern short GetAsyncKeyState(int vKey);

        private const uint primarydown = 0x02, primaryup = 0x04, secondarydown = 0x08, secondaryup = 0x10;
        private const int togglebutton = 5, holdbutton = 6, primary = 1, secondary = 2;

        private static int delay = 1, minDelay = 1, maxDelay = 1, kliktype = primary, currenthold = holdbutton, currenttoggle = togglebutton;
        private static bool isgamertoggled, wasToggleKeyPressed = false;
        private static char HumanSymbole;

        #endregion

        #region save and laod shitter

        private static void Saveicouldbesomuchmorethananumberbaby()
        {
            Console.WriteLine(File.Exists("AtoKlikerSettings.json") ? "Settings saved to `AtoKlikerSettings.json`" : "Created settings file `AtoKlikerSettings.json`");
            AtoKlikerSettings jsonfiler = new() { HoldButton = currenthold, ToggleButton = currenttoggle, ClickType = kliktype, Delay = delay, MinDelay = minDelay, MaxDelay = maxDelay };
            File.WriteAllText("AtoKlikerSettings.json", JsonConvert.SerializeObject(jsonfiler, Formatting.Indented));
        }

        private static void ewithinkijusttouchedoneofmortysloads()
        {
            if (File.Exists("AtoKlikerSettings.json"))
            {
                string json = File.ReadAllText("AtoKlikerSettings.json");
                var jsonfiler = JsonConvert.DeserializeObject<AtoKlikerSettings>(json);
                if (jsonfiler != null)
                {
                    currenthold = jsonfiler.HoldButton;
                    currenttoggle = jsonfiler.ToggleButton;
                    kliktype = jsonfiler.ClickType;
                    delay = jsonfiler.Delay;
                    minDelay = jsonfiler.MinDelay;
                    maxDelay = jsonfiler.MaxDelay;

                    Console.WriteLine("Settings loaded from `AtoKlikerSettings.json`");
                }
                else Console.WriteLine("ruh oh something doesnt... mb");
            }
        }

        #endregion

        #region help info
        private static void helpformeandnoobs()
        {
            Console.WriteLine("\nAvailable Commands:\n" +
                              "L_Toggle(Button Code)    - Sets the button for toggling auto-clicking.\n" +
                              "L_Hold(Button Code)      - Sets the button for holding down to click.\n" +
                              "L_PrimaryClick           - Sets the click type to primary (Left Mouse Button).\n" +
                              "L_SecondaryClick         - Sets the click type to secondary (Right Mouse Button).\n" +
                              "L_Delay(Min, Max)        - Sets the time between each click. Setting the max time will make it randomize between the 2 values. For non-random only set Min.\n" +
                              "L_Reset                  - Resets all settings to defaults.\n" +
                              "L_Code(Button Input)     - Outputs the hex code for the specified Button.\n\n" +
                              "help                     - Displays this help information.\n" +
                              "helpbind                 - Displays the keybind help information.\n" +
                              "helplist                 - Displays the keybind list for binds that you cant input normally.\n" +
                              "current                  - Displays all current binds and settings.\n\n" +
                              "L_Save                   - Saves your settings to a json file in the same directory as the .exe\n" +
                              "L_Load                   - Loads your settings From a json file in the same directory as the .exe named AtoKlikerSettings.json\n\n" +
                              "For any other help or bug reporting go to discord.gg/FwmGf6vBu2\n");
        }

        private static void keybindhelpersingerest()
        {
            Console.WriteLine("\nhi chat!!!1\nif youre like me then you have no clue how to use this!!!\n" +
                              "either because you have dementia like me OR because youre new here\n" +
                              " \nso to get started\nfirst type L_Code(the button you want to bind)\n" +
                              "then type L_Hold(The outputted code) or L_Toggle(The outputted code) depending on what you want to bind\n" +
                              " \nokay so theres some buttons that you cant bind because im lazy so to bind them to special buttons just type like\n" +
                              "L_Hold(Mouse5) or lajk L_Toggle(Alt)\n" +
                              "for a full list type `bindlist`\n" +
                              " \nthen uhhhh thats it i think dont @ me if it doesnt work for you\n" +
                              "dont forgoren to use L_Save!11!11\n" +
                              " \nHAVE FUNN!!111!!1111!\n");
        }

        private static void listallpredefinedshit()
        {
            Console.WriteLine("\nhi chat!!1111!!!!\n" +
                              "heres a list off all predefined characters that you usually cant type out into the L_Code() command!!!\n" +
                              "to use there you just type L_Hold or L_Toggle then in the () type the button from the list for example L_Hold(Mouse1) this will bind it to primary mouse!!! (left click)\n" +
                              " \nList:\n" +
                              "Mouse list: Mouse1, Mouse2, Mouse3, Mouse4, Mouse5\n" +
                              "HAVE FUNN!!111!!1111!\n");
        }

        private static void Current()
        {
            Console.WriteLine($"\nhi again chat\n\n" +
                              $"Hold: {currenthold}\n" +
                              $"Toggle: {currenttoggle}\n" +
                              $"Klik type: {kliktype}\n" +
                              $"Delay: {delay}\n" +
                              $"MinDelay: {minDelay}\n" +
                              $"MaxDelay: {maxDelay}\n");
        }
        #endregion

        #region clicking
        private static void KlikMuose()
        {
            mouse_event((kliktype == primary ? primarydown : secondarydown), 0, 0, 0, 0);
            mouse_event(kliktype == primary ? primaryup : secondaryup, 0, 0, 0, 0);
        }

        private static bool iskayprassed(int key) => (GetAsyncKeyState(key) & 0x8000) != 0;

        #endregion

        #region commands
        private static readonly Dictionary<string, int> keyMappings = new()
        {
            { "Mouse1", 1 }, { "Mouse2", 2 }, { "Mouse3", 4 },
            { "Mouse4", 5 }, { "Mouse5", 6 }
        };
        private static int predefined(string button) => keyMappings.TryGetValue(button, out int keyCode) ? keyCode : -1;

        private static int getcode(string button) => predefined(button);

        private static char getthecharacterfromurstupidnumba(int keyNumber)
        {
            return (char)keyNumber;
        }

        //------------------------------------------------ real gamer shit

        private static void L_PrimaryClick() { kliktype = primary; Console.WriteLine("ok now primary"); }

        private static void L_SecondaryClick() { kliktype = secondary; Console.WriteLine("ok now secondary"); }

        private static void L_Code(string keyInput)
        {
            if (keyInput.Length == 1)
            {
                int virtualKeyCode = char.ToUpper(keyInput[0]);
                Console.WriteLine($"Code for {keyInput} = {virtualKeyCode}. hope that helps!111!!!!11");
            }
            else
            {
                Console.WriteLine($"whar is {keyInput}... can you just input like one character or something not as weird??? thanks!111!!!!1");
            }
        }

        private static void L_Hold(int buttonnum)
        {
            currenthold = buttonnum;
            HumanSymbole = getthecharacterfromurstupidnumba(buttonnum);
            Console.WriteLine($"Hold button set to: {buttonnum} aka {HumanSymbole}");
        }

        private static void L_Toggle(int buttonnum)
        {
            currenttoggle = buttonnum;
            HumanSymbole = getthecharacterfromurstupidnumba(buttonnum);
            Console.WriteLine($"Toggle button set to: {buttonnum} aka {HumanSymbole}");
        }

        private static void L_Delay(int minDelayValue, int maxDelayValue = -1)
        {
            if (maxDelayValue == -1)
            {
                delay = minDelayValue;
                minDelay = minDelayValue;
                maxDelay = minDelayValue;
                Console.WriteLine($"Delay set to: {delay} ms");
            }
            else
            {
                minDelay = minDelayValue;
                maxDelay = maxDelayValue;
                AntiAntiAutoClicker();
            }
        }

        private static void L_Reset()
        {
            currenthold = holdbutton;
            currenttoggle = togglebutton;
            kliktype = primary;
            isgamertoggled = false;
            Console.WriteLine("Settings reset to defaults: Hold on Mouse4, Toggle on Mouse5, kliktype to Primary (Mouse1).");
        }
        
        #endregion

        #region handling of your shit
        private static void commandreading(string input)
        {
            string[] parts = input.Split('(');

            if (input.Equals("L_Save", StringComparison.OrdinalIgnoreCase)) { Saveicouldbesomuchmorethananumberbaby(); return; }
            if (input.Equals("L_Load", StringComparison.OrdinalIgnoreCase)) { ewithinkijusttouchedoneofmortysloads(); return; }
            if (input.Equals("help", StringComparison.OrdinalIgnoreCase)) { helpformeandnoobs(); return; }
            if (input.Equals("helpbind", StringComparison.OrdinalIgnoreCase)) { keybindhelpersingerest(); return; }
            if (input.Equals("helplist", StringComparison.OrdinalIgnoreCase)) { listallpredefinedshit(); return; }
            if (input.Equals("current", StringComparison.OrdinalIgnoreCase)) { Current(); return; }

            if (parts.Length == 2)
            {
                string command = parts[0].Trim();
                string buttonStr = parts[1].Trim(')');

                if (command.Equals("L_Delay", StringComparison.OrdinalIgnoreCase))
                {
                    string[] delayParts = buttonStr.Split(',');
                    if (delayParts.Length == 1 && int.TryParse(delayParts[0], out int delayer))
                    {
                        L_Delay(delayer);
                        if (delayer < 50)
                            Console.WriteLine("just saying this rn.. low delay values COULD make ur pc run it so fast it doesnt register the kliks (in games mostly or other apps) properly (1 has worked for me)");
                    }
                    else if (delayParts.Length == 2 &&
                             int.TryParse(delayParts[0], out int minDelay) &&
                             int.TryParse(delayParts[1], out int maxDelay))
                    {
                        L_Delay(minDelay, maxDelay);
                    }
                    else
                    {
                        Console.WriteLine("sorry mane but i dont know what the sigma that i supposed to mean.. thats not a number i recognise");
                    }
                    return;
                }
                if (int.TryParse(buttonStr, out int buttonCode))
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
                            Console.WriteLine("Wharrrr????");
                            break;
                    }
                }
                else
                {
                    int predefinedCode = predefined(buttonStr);
                    if (predefinedCode != -1)
                    {
                        switch (command)
                        {
                            case "L_Toggle":
                                L_Toggle(predefinedCode);
                                break;

                            case "L_Hold":
                                L_Hold(predefinedCode);
                                break;

                            default:
                                Console.WriteLine("Wharrrr???? did you forgoren to capitalize one of the letters??????");
                                break;
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Buh??? Whar you mean {command}({buttonStr})!?1 I dont understand that key... type help or helpbind for valid options.");
                    }
                }
            }
            else
            {
                simplecommands(input);
            }
        }

        private static void fuckingtoggleorelse()
        {
            bool isPressedNow = iskayprassed(currenttoggle);

            if (isPressedNow && !wasToggleKeyPressed)
            {
                isgamertoggled = !isgamertoggled;
                Thread.Sleep(200);
            }

            wasToggleKeyPressed = isPressedNow;
        }

        private static void randomfunnyresponse(string input)
        {
            switch (new Random().Next(1, 4))
            {
                case 1:
                    Console.WriteLine($"`{input}` isnt something i understand.... whar mean?? type help or helpbind ok??? then you the thing");
                    break;

                case 2:
                    Console.WriteLine($"Buh??? Whar you mean `{input}`!?1 you dont understand???+ type help or helpbind before doing whatever you do ok?");
                    break;
                case 3:
                    Console.WriteLine($"Wharrrrr????? Huhhhhhh??????? wha is `{input}`??? me not get... type help or helpbind for to know what do ok?");
                    break;
            }
        }

        private static void simplecommands(string input)
        {
            if (input == "L_PrimaryClick")
                L_PrimaryClick();
            else if (input == "L_SecondaryClick")
                L_SecondaryClick();
            else if (input == "L_Reset")
                L_Reset();
            else
                randomfunnyresponse(input);
        }

        private static void AntiAntiAutoClicker()
        {
            if (maxDelay > minDelay)
            {
                delay = new Random().Next(minDelay, maxDelay + 1);
            }
            else
            {
                delay = minDelay;
            }
        }

        #endregion

        #region main
        static void Main()
        {
            Console.WindowHeight = 30; 
            Console.WindowWidth = 160;
            if (File.Exists("AtoKlikerSettings.json")) ewithinkijusttouchedoneofmortysloads(); else Saveicouldbesomuchmorethananumberbaby();
            Console.Title = new[] {
                "ato kliker!!!1",
                "gayming free from all punjabi viruses!!! (WORKING 2023) (NO SCAM) (NO VIRUS) (FREE RUBOX)",
                "made because logitech GHub was too slow for me :3"
            }[new Random().Next(3)];

            Console.WriteLine("type help for a list of the uhhh commands yea...\nTYPE helpbind TO LEARN HOW TO REBIND");

            while (true)
            {
                if (Console.KeyAvailable)
                {
                    string? input = Console.ReadLine();
                    if (input != null)
                    {
                        if (input.StartsWith("L_Code(")) L_Code(input[7..^1]);
                        else commandreading(input);
                    }
                }

                fuckingtoggleorelse();

                if (iskayprassed(currenthold))
                {
                    KlikMuose();
                    Thread.Sleep(delay);
                }

                if (isgamertoggled)
                {
                    KlikMuose();
                    Thread.Sleep(delay);
                }
            }
        }
        #endregion
    }
}

