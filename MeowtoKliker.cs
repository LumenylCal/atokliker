using Newtonsoft.Json;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace MeowtoKliker
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

        #endregion

        #region save and laod shitter

        private static void Saveicouldbesomuchmorethananumberbaby()
        {
            Console.WriteLine(File.Exists("MeowtoKlikerSettings.json") ? "Settings saved to `MeowtoKlikerSettings.json`" : "Created settings file `MeowtoKlikerSettings.json`");
            MeowtoKlikerSettings jsonfiler = new() { HoldButton = currenthold, ToggleButton = currenttoggle, ClickType = kliktype, Delay = delay, MinDelay = minDelay, MaxDelay = maxDelay };
            File.WriteAllText("MeowtoKlikerSettings.json", JsonConvert.SerializeObject(jsonfiler, Formatting.Indented));
        }

        private static void ewithinkijusttouchedoneofmortysloads()
        {
            if (File.Exists("MeowtoKlikerSettings.json"))
            {
                string json = File.ReadAllText("MeowtoKlikerSettings.json");
                var jsonfiler = JsonConvert.DeserializeObject<MeowtoKlikerSettings>(json);
                if (jsonfiler != null)
                {
                    currenthold = jsonfiler.HoldButton;
                    currenttoggle = jsonfiler.ToggleButton;
                    kliktype = jsonfiler.ClickType;
                    delay = jsonfiler.Delay;
                    minDelay = jsonfiler.MinDelay;
                    maxDelay = jsonfiler.MaxDelay;

                    Console.WriteLine("Settings loaded from `MeowtoKlikerSettings.json`");
                }
                else Console.WriteLine("ruh oh something doesnt... mb");
            }
        }

        #endregion

        #region help info
        private static void helpformeandnoobs()
        {
            Console.WriteLine("\n:3 Available Commands:\nL_Toggle(Button num/predefined)    - Sets the button for toggling auto-clicking.\nL_Hold(Button num/predefined)      - Sets the button for holding down to click.\nL_PrimaryClick                     - Sets the click type to primary (Left Mouse Button).\nL_SecondaryClick                   - Sets the click type to secondary (Right Mouse Button).\nL_Delay(Min, Max)                  - Sets the time between each click. Setting the max time will make it randomize between the 2 values. For non-random, only set Min.\nL_Reset                            - Resets all settings to defaults.\nL_Code(Button Input)               - Outputs the number for the specified Button.\n\nhelp                               - Displays this help information.\nhelpbind                           - Displays the keybind help information.\nhelplist                           - Displays the keybind list for binds that you cant input normally.\ncurrent                            - Displays all current binds and settings.\n\nL_Save                             - Saves your settings to a json file in the same directory as the .exe\nL_Load                             - Loads your settings From a json file in the same directory as the .exe named MeowtoKlikerSettings.json\n\nFor any other help or bug reporting go to discord.gg/FwmGf6vBu2\n");
        }

        private static void keybindhelpersingerest()
        {
            Console.WriteLine("\nMeowww!!! hi chat!!!1 :3\nif youre like me then you have no clue how to use this!!!\neither because you have dementia like me OR because youre new here :3 hi\n\nso to get started\nfirst type L_Code(the button you want to bind)\nthen type L_Hold(The outputted numba) or L_Toggle(The outputted numba) depending on what you want to bind\n\nokay so theres some buttons that you cant bind because im lazy so to bind them to special buttons just type like\nL_Hold(Mouse5) or lajk L_Toggle(Alt)\nfor a full list type `bindlist`\n\nthen uhhhh thats it i think dont @ me if it doesnt work for you\n\ndont forgoren to use L_Save!11!11\n\nHAVE FUNN!!111!!1111!\n");
        }

        private static void listallpredefinedshit()
        {
            Console.WriteLine("\nhi chat!!1111!!!! :3\nheres a list off all predefined characters that you usually cant type out into the L_Code() command!!!\nto use there you just type L_Hold or L_Toggle then in the () :3 type the button from the list for example L_Hold(Mouse1) this will bind it to primary mouse!!! (left click) :3:3:3:3:3\n\nList:\nMouse list: Mouse1, Mouse2, Mouse3, Mouse4, Mouse5\nFunction list: F1, F2, F3, F4, F5, F6, F7, F8, F9, F10, F11, F12, F13, F14, F15, F16, F 17, F18, F19, F20, F21, F22, F23, F24\nHAVE FUNN!!111!!1111!\n");
        }

        private static void Current()
        {
            Console.WriteLine($"\nhi again chat\n\nHold: {currenthold}\nToggle: {currenttoggle}\nKlik type: {kliktype}\nDelay: {delay}\nMinDelay: {minDelay}\nMaxDelay: {maxDelay}\n");
        }
        #endregion

        #region clicking
        private static void KlikMuoseRightFuckingMeow()
        {
            mouse_event(kliktype == primary ? primarydown : secondarydown, 0, 0, 0, 0);
            mouse_event(kliktype == primary ? primaryup : secondaryup, 0, 0, 0, 0);
        }

        private static bool iskayprassed(int key) => (GetAsyncKeyState(key) & 0x8000) != 0;

        #endregion

        #region commands
        private static readonly Dictionary<string, uint> uinters = new()
        {
            { "F1", 0x70 }, { "F2", 0x71 }, { "F3", 0x72 }, { "F4", 0x73 }, { "F5", 0x74 }, { "F6", 0x75 }, { "F7", 0x76 }, { "F8", 0x77 }, { "F9", 0x78 }, { "F10", 0x79 }, { "F11", 0x7A }, { "F12", 0x7B }, { "F13", 0x7C }, { "F14", 0x7D }, { "F15", 0x7E }, { "F16", 0x7F }, { "F17", 0x80 }, { "F18", 0x81 }, { "F19", 0x82 }, { "F20", 0x83 }, { "F21", 0x84 }, { "F22", 0x85 }, { "F23", 0x86 }, { "F24", 0x87 }, { "Mouse1", 0x01 }, { "Mouse2", 0x02 }, { "Mouse3", 0x04 }, { "Mouse4", 0x05 }, { "Mouse5", 0x06 }
        };
        private static uint predefuint(string button) => uinters.TryGetValue(button, out uint code) ? code : uint.MaxValue;

        //------------------------------------------------ real gamer shit

        private static void L_PrimaryClick() { kliktype = primary; Console.WriteLine("ok now primary"); }

        private static void L_SecondaryClick() { kliktype = secondary; Console.WriteLine("ok now secondary"); }

        private static void L_Code(string keyInput)
        {
            if (keyInput.Length == 1)
            {
                int virtualKeyCode = char.ToUpper(keyInput[0]);
                Console.WriteLine($"Code for {keyInput} = {virtualKeyCode}. hope that helps!111!!!!11 :3 :3 :3 :3 mrow");
            }
            else
            {
                Console.WriteLine($"whar is {keyInput}... can you just input like one character or something not as weird??? thanks!111!!!!1 :3");
            }
        }

        private static void L_Hold(int buttonnum)
        {
            currenthold = buttonnum;
            string HumanSymbole = buttonCodeToString(buttonnum);
            Console.WriteLine($"Hold button set to: {buttonnum} aka {HumanSymbole}");
        }

        private static string buttonCodeToString(int buttonCode) => uinters.FirstOrDefault(x => x.Value == (uint)buttonCode).Key ?? ((char)buttonCode).ToString();

        private static void L_Toggle(int buttonnum)
        {
            currenttoggle = buttonnum;
            string HumanSymbole = buttonCodeToString(buttonnum);
            Console.WriteLine($"Toggle button set to: {buttonnum} aka {HumanSymbole} :3");
        }

        private static void L_Delay(int minDelayValue, int maxDelayValue = -1)
        {
            if (maxDelayValue == -1)
            {
                delay = minDelayValue;
                minDelay = minDelayValue;
                maxDelay = minDelayValue;
                Console.WriteLine($"Delay set to: {delay} ms :3");
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
            Console.WriteLine("Settings reset to defaults: Hold on Mouse4, Toggle on Mouse5, kliktype to Primary (Mouse1). :3c meow");
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
                            Console.WriteLine("wharrrr???? did you forgoren to capitalize one of the letters??????");
                            break;
                    }
                }
                else
                {
                    uint predefinedCode = predefuint(buttonStr);
                    if (predefinedCode != uint.MaxValue)
                    {
                        switch (command)
                        {
                            case "L_Toggle":
                                L_Toggle((int)predefinedCode);
                                break;

                            case "L_Hold":
                                L_Hold((int)predefinedCode);
                                break;

                            default:
                                Console.WriteLine("wharrrr???? did you forgoren to capitalize one of the letters??????");
                                break;
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Buh??? Whar you mean {command}({buttonStr})!? I dont understand that key... type help or helpbind for valid options.");
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

        private static void randomfunnyresponseaha_aha_aha_aha_aha_aha_aha(string input)
        {
            Console.WriteLine(new[]
            {
                $"`{input}` isnt something i understand.... whar mean?? type help or helpbind ok??? then you the thing",
                $"Buh??? Whar you mean `{input}`!?1 you dont understand???+ type help or helpbind before doing whatever you do ok?",
                $"Wharrrrr????? Huhhhhhh??????? wha is `{input}`??? me not get... type help or helpbind for to know what do ok?"
            }
            [new Random().Next(3)]
           );
        }

        private static void CuteCatLanguageResponseMeowMeow()
        {
            Console.WriteLine(new[]
            {
                "Meowww :3333",
                "Meow meow!! :3 mrow",
                "Mrrrp? meow :3",
                "meeeowwwwwwwww :3 mrrrrrow mrrp meow meow",
                "MEEEAAAOOOWWWWWWWW",
                "Miau :3 -w- prrrrrrrrrrrr",
                "mrrow? Ma! meow meow miau meow!!! :3",
                "Mrowww meowwww prrrrrr mrrrowww -w- prrrrrr",
                "Mrrrrrrr mrowwwwww ~~~~",
                "Mewwwwwwwwwwwwwwwwww~~~",
                "Prrrrrr meow~",
                "Mrowwwww mrrrow mrrr~",
                "Meooow mrrrrrrrrrrr~",
                "Miaowwwwww~~~~",
                "Mrrrrowwwwwwww :3",
                "Mrrrowr mrrrrrr~",
                "Meowwwwwwwwwwww!!!",
                "Meeeowwwwwwwwwww :333",
                "Mrrrrowwwwww~",
                "Mreeeowwwwwww :3",
                "Prrrreeeeow~",
                "Mew mrrrrowwwww~~~",
                "Nyaaa~~",
                "Meowmew~",
                "Mewwwwww~",
                "Nyaa~ miau~",
                "Purrpurr mrow~",
                "Mew mrrr mrrrrr~",
                "Mewwwwwwwwwwwwww~~~",
                "Mrrroooowwwww~",
                "Mew meow meow~",
                "Nyaa~ nyaa~",
                "Mrrrow meow meow mrrr~",
                "Prrr~ purr~",
                "Mew mrrrrowwww~",
                "Mreeeow mrrrr~",
                "Meeeooowwwww~",
                "Mowwww mrowwww~",
                "Meeeowwwwwwwwww~~~",
                "Mew mrrrp~",
                "Prrrow~",
                "Mroooowwwwwww~",
                "Nyaa~~ meow~",
                "Mewwwwwwwwwwwww~",
                "Mrawr~",
                "Meow meow meow~",
                "Mew mrrrowww~",
                "Nya mrow~",
                "Mrrrrowwwww~",
                "Mowwwwww~~~",
                "Mewwww~",
                "Mrowwwwwww~",
                "Mewwwwwwwww~~~",
                "Mowwwwww~~",
            }[new Random().Next(50)]);
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
                randomfunnyresponseaha_aha_aha_aha_aha_aha_aha(input);
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
            Console.WindowWidth = 170;
            if (File.Exists("MeowtoKlikerSettings.json")) ewithinkijusttouchedoneofmortysloads(); else Saveicouldbesomuchmorethananumberbaby();
            Console.Title = new[] {
                "ato kliker!!!1",
                "gayming free from all punjabi viruses!!! (WORKING 2023) (NO SCAM) (NO VIRUS) (FREE RUBOX)",
                "made because logitech GHub was too slow for me :3"
            }[new Random().Next(3)];

            Console.WriteLine("\ntype help for a list of the uhhh commands yea... :3\ntype helpbind for rebinfhelp!111\ntype bindlist for a list of cool stuff you can bind to\ntype current for the current settings111\nHAVE FUNN ;3");

            while (true)
            {
                string? input = Console.ReadLine();

                if (Console.KeyAvailable)
                {
                    if (input != null)
                    {
                        if (input.StartsWith("L_Code(")) L_Code(input[7..^1]);
                        else commandreading(input);
                    }
                }

                if (input != null)
                {
                    string normalizedInput = input.ToLower();
                    bool isCatSound = Regex.IsMatch(normalizedInput,
                    @"(m+e+o+w+|m+iau+|m+r+o+w+|m*r*rp+|p+r+r+|n+y+a+|p+u+r*r+|m+ew+|:3|;3|-w-|UwU|OwO|:c|3:)",
                    RegexOptions.IgnoreCase);
                    if (isCatSound)
                    {
                        CuteCatLanguageResponseMeowMeow();
                    }
                    else commandreading(input);
                }

                fuckingtoggleorelse();

                if (iskayprassed(currenthold))
                {
                    KlikMuoseRightFuckingMeow();
                    AntiAntiAutoClicker();
                    Thread.Sleep(delay);
                }

                if (isgamertoggled)
                {
                    KlikMuoseRightFuckingMeow();
                    AntiAntiAutoClicker();
                    Thread.Sleep(delay);
                }
            }
        }
        #endregion
    }
}

