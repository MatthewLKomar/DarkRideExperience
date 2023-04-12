using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using RavingBots.MultiInput;

using UnityEngine;
using UnityEngine.UI;


public class MIShowDevices : MonoBehaviour {
    private static readonly IList<InputCode> InterestingAxes;

    public GameObject AssignDevices;
    public Text AvailableDevices;

    private InputState _inputState;
    private string _devicesPrefix;
    private IDevice currentDevice;  //RCC


    static MIShowDevices() {
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

    // ReSharper disable once UnusedMember.Local
    private void Awake() {
        //_players = null;
        _inputState = GetComponent<InputState>();
        _devicesPrefix = AvailableDevices.text;

        AssignDevices.SetActive(false);

        // we don't need the arguments here
        _inputState.DeviceStateChanged.AddListener((d, e) => UpdateDeviceList());
        _inputState.DevicesEnumerated.AddListener(UpdateDeviceList);
    }

    private void Start()
    {
        AssignDevices.SetActive(true);
        UpdateDeviceList();
    }

    // ReSharper disable once UnusedMember.Local
    private void Update() {

        IDevice device;
        InputCode input;
        if (!FindInput(out device, out input)) {
            return;
        }
        print(device.Id);
        print(device.Name);
        currentDevice = device;
        UpdateDeviceList();
    }

    private bool FindInput(out IDevice outDevice, out InputCode outCode) {

        Func<IDevice, IVirtualAxis, bool> predicate = (device, axis) => {
            if (IsAssigned(device)) {
                return false;
            }
            
            return axis.IsUp;
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
    
    private void UpdateDeviceList() {
        var names = new StringBuilder();
        names.AppendLine(_devicesPrefix);
        names.AppendLine();

        foreach (var device in _inputState.Devices) {
            if (!device.IsUsable) {
                continue;
            }

            var color = GetDeviceColor(device);
            names.AppendFormat("<color={0}>[#{1}] \"{2}\" </color>\n", color, device.Id, device.Name);
        }

        AvailableDevices.text = names.ToString();
    }

    private string GetDeviceColor(IDevice device) {
        //return IsAssigned(device) ? "green" : "#343434FF";
        if (currentDevice == null)
            return "#343434FF";
        else if (currentDevice.Id == device.Id)
            return "green";
            else return "#343434FF";
    }

    private bool IsAssigned(IDevice device) {
        //return _players != null && _players.Any(player => player.HasDevice(device));
        return false;
    }
}
