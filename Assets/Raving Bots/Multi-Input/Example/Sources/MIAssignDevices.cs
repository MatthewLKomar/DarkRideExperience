using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using RavingBots.MultiInput;

using UnityEngine;
using UnityEngine.UI;

// this class is responsible for assigning mice devices to players
// and updating the UI
public class MIAssignDevices : MonoBehaviour {
    private static readonly IList<InputCode> InterestingAxes;

    public GameObject assignDevices;
    public Text availableDevices;

    public string[] contains;   // in order, in devices that the device name contains this string -- this is important when there are multiple devices with the same name
    public int[] assignTo;      // in order, assign devices to the player number -- use value -1 to skip this device, again for when there are multiple devices with the same name
    private IDevice[] foundDevices;

    //public GameObject PlayerPrefab;
    //public int numPlayers = 4;

    public MIDeviceListener[] players;
    private InputState _inputState;
    private string _devicesPrefix;
    [SerializeField] private bool printDetails = false;     //RCC do you want extra details printed for each device
    [SerializeField] private GameObject notObj;             //RCC tell the game conreller that devices have been assigned

    //private int _currentPlayer;

    // we want to check all axes *but* few analog ones
    // that might trigger false positives or be otherwise
    // inconvenient (like mouse movement or gamepad sticks)
    static MIAssignDevices() {
        InterestingAxes = InputStateExt
            .AllAxes
            .Where(axis => !IsUninteresting(axis))
            .ToList();
    }

    private static bool IsUninteresting(InputCode axis) {
        switch (axis) {
            case InputCode.MouseX:
            case InputCode.MouseY:
            case InputCode.MouseXLeft:
            case InputCode.MouseXRight:
            case InputCode.MouseYUp:
            case InputCode.MouseYDown:
            case InputCode.PadLeftStickY:
            case InputCode.PadLeftStickX:
            case InputCode.PadLeftStickDown:
            case InputCode.PadLeftStickUp:
            case InputCode.PadLeftStickLeft:
            case InputCode.PadLeftStickRight:
            case InputCode.PadRightStickY:
            case InputCode.PadRightStickX:
            case InputCode.PadRightStickDown:
            case InputCode.PadRightStickUp:
            case InputCode.PadRightStickLeft:
            case InputCode.PadRightStickRight:
                return true;
            default:
                return false;
        }
    }

    private void Awake() {
        //_players = null;
        _inputState = GetComponent<InputState>();
        _devicesPrefix = availableDevices.text;

        assignDevices.SetActive(true);

        // we don't need the arguments here
        _inputState.DeviceStateChanged.AddListener((d, e) => UpdateDeviceList());
        _inputState.DevicesEnumerated.AddListener(UpdateDeviceList);

        //SetPlayerCount(numPlayers);
        foundDevices = new IDevice[contains.Length];
        Invoke("AssignDevices", 1f);    // give everything time to load
    }

    // ReSharper disable once UnusedMember.Local
    private void Update() {
        //if (_players == null) {
            //return;
        //}
        /*
        var player = _players[_currentPlayer];

        IDevice device;
        InputCode input;
        if (!FindInput(out device, out input)) {
            return;
        }

        player.Assign(device, input);
        UpdateDeviceList();

        if (player.Ready) {
            var next = _currentPlayer + 1;

            if (next < _players.Length) {
                SetCurrentPlayer(next);
            } else {
                StartGame();
            }
        } else {
            SetInstructions(true);
        }*/
    }

    private void AssignDevices() {
        // Assign a spinner mouse to each player 
        Debug.Log("assigning devices");
        int p = 0;
        int c = 0;
        // build device list 
        foreach (var device in _inputState.Devices) {
            if (!device.IsUsable) {
                continue;
            }

            print(string.Format("Device ID : {0}, Device Name : {1}, Device Axis : {2}", device.Id, device.Name, device.SupportedAxes));
            print("Is this a Mouse: " + DeviceOfType("Mouse", device));
            if (printDetails) foreach (var thing in device.SupportedAxes) print(thing);            

            if (c < contains.Length && device.Name.Contains(contains[c]))
            {
                foundDevices[c] = device;
                c += 1;
            }
        }

        // assign devices 
        for (int i = 0; i < foundDevices.Length; i += 1)
        {
            print(foundDevices[i]);
            if (assignTo[i] != -1)  //RCC Ignore a -1 value as that device is not used for this game
            {
                players[assignTo[i]].Device = foundDevices[i];
            }
        }

        // we do not want to see the canvas with the listed devices anymore
        assignDevices.SetActive(false);

        if (notObj != null) notObj.SendMessage("DevicesAssigned");

        enabled = false;
    }

    private bool DeviceOfType(string contains, IDevice device) {
        foreach (var thing in device.SupportedAxes) {
            //print(thing);
            if (thing.ToString().Contains(contains)) {
                return true;
            }
        }
        return false;
    }

    private bool FindInput(out IDevice outDevice, out InputCode outCode) {
        // this returns first unassigned device with non-zero
        // input on one of the axes that we're interested in
        //
        // this is how you can implement key/button mapping, too:
        // simply run through supported axes and see which
        // one reports input (see InputStateExt.FindFirst documentation
        // for details)
        //
        // we also grab the InputCode so we can tell whether the device
        // is (likely) a keyboard, a mouse or a gamepad
       // var player = players[_currentPlayer];

        Func<IDevice, IVirtualAxis, bool> predicate = (device, axis) => {
            if (IsAssigned(device)) {
                return false;
            }
            /*
            // ReSharper disable once InvertIf
            if (player.KeyboardMouse) {
                // if player wants to use keyboard/mouse then we check which one of the
                // two has already been assigned and only check the other one
                if (player.HasKeyboard && !axis.Code.IsMouse()) {
                    return false;
                }

                if (!player.HasKeyboard && !axis.Code.IsKeyboard()) {
                    return false;
                }

                if (axis.Code.IsGamepad()) {
                    return false;
                }
            }*/

            return axis.Code.IsMouse();
        };

        // see IsUninteresting above
        IVirtualAxis outAxis;
        if (_inputState.FindFirst(out outDevice, out outAxis, predicate, InterestingAxes)) {
            outCode = outAxis.Code;
            return true;
        }

        outDevice = null;
        outCode = InputCode.None;
        return false;
    }

    public void SetPlayerCount(int count) {
        // this is called from MIDemoSelectPlayerCount after user
        // presses 'start' button; here we create all of the player
        // objects (without pawns) and show the assignment instructions UI
        //_players = new MIPlayerMouse[count];

        for (var idx = 0; idx < count; ++idx) {
            var player = idx + 1;
            //var playerObj = Instantiate(PlayerPrefab);
            //playerObj.name = string.Format("Player {0}", player);
            //_players[idx] = playerObj.GetComponent<MIPlayerMouse>();
            //_players[idx].Player = player;
        }

        SetCurrentPlayer(0);
        //AssignDevices.SetActive(true);

        // we need to discard all current input, otherwise
        // UI actions (e.g. the LMB/Enter/A button press on
        // the Start button) would cause that device to immediately
        // become assigned to first player
        _inputState.Reset();
    }

    private void SetCurrentPlayer(int player) {
        //_currentPlayer = player;
        //SetInstructions();
    }
    /*
    public void SetInstructions(bool partial = false) {
        // this updates the on-screen instructions to tell the player
        // that we're expecting a button press to assign a device to them
        //
        // because we expect every player to either have both keyboard and mouse,
        // or a gamepad, we need to handle the situation where player has either a keyboard
        // or a mouse, but not both -- in that case 'partial' will be true, and we'll check
        // what device has already been assigned and update the text accordingly
        var instructions = new StringBuilder();
        Instructions1.text = string.Format("<b>Selecting devices for player #{0}</b>", _players[_currentPlayer].Player);

        if (!partial) {
            instructions.AppendLine("Please select your device (keyboard, mouse, or pad) by pressing any button.");
        } else {
            // ReSharper disable once ConvertIfStatementToConditionalTernaryExpression
            if (_players[_currentPlayer].HasKeyboard) {
                instructions.AppendLine("Please select a mouse by pressing any button on it.");
            } else {
                instructions.AppendLine("Please select a keyboard by pressing any key on it.");
            }
        }

        Instructions2.text = instructions.ToString();
    }*/

    private void UpdateDeviceList() {
        var names = new StringBuilder();
        names.AppendLine(_devicesPrefix);
        names.AppendLine();

        foreach (var device in _inputState.Devices) {
            if (!device.IsUsable) {
                continue;
            }

            var color = GetDeviceColor(device);
            var label = GetDeviceLabel(device);
            names.AppendFormat("<color={0}>[#{1}] \"{2}\" {3}</color>\n", color, device.Id, device.Name, label);
        }

        availableDevices.text = names.ToString();
    }

    private string GetDeviceColor(IDevice device) {
        return IsAssigned(device) ? "green" : "#343434FF";
    }

    private string GetDeviceLabel(IDevice device) {
        if (!IsAssigned(device))
            return "";
        return "";
        //var player = _players.First(p => p.HasDevice(device));
        //return string.Format("[assigned to player {0}]", player.Player);
    }

    private bool IsAssigned(IDevice device) {
        return players != null ;//&& players.Any(player => player.HasDevice(device));
    }
}
