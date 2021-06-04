import {
    InteractionConfig,
    PhysicalConfig,
    HoverAndHoldInteractionSettings
} from "./ConfigurationTypes";
import {
    ActionCode,
    CommunicationWrapper,
    ConfigState,
    ResponseCallback,
    WebSocketResponse
} from '../Connection/TouchFreeServiceTypes';
import { ConnectionManager } from '../Connection/ConnectionManager';
import { Guid } from "guid-typescript";

// Class: ConfigurationManager
// This class provides a method for changing the configuration of the TouchFree
// Service. Makes use of the static <ConnectionManager> for communication with the Service.
export class ConfigurationManager {

    // Function: RequestConfigChange
    // Optionally takes in an <InteractionConfig> or a <PhysicalConfig> and sends them through the <ConnectionManager>
    // 
    // Provide a _callBack if you require confirmation that your settings were used correctly.
    // If your _callBack requires context it should be bound to that context via .bind().
    //
    // WARNING!
    // If a user changes ANY values via the TouchFree Service Settings UI,
    // values set from the Tooling via this function will be discarded.
    public static RequestConfigChange(
        _interaction: Partial<InteractionConfig> | null,
        _physical: Partial<PhysicalConfig> | null,
        _callback: (detail: WebSocketResponse) => void): void {

        let action = ActionCode.SET_CONFIGURATION_STATE;
        let requestID = Guid.create().toString();

        let content = new ConfigState(requestID, _interaction, _physical);
        let request = new CommunicationWrapper(action, content);

        let jsonContent = JSON.stringify(request);

        ConnectionManager.serviceConnection()?.SendMessage(jsonContent, requestID, _callback);
    }

    // Function: RequestConfigState
    // Used to request information from the Service via the <ConnectionManager>. Provides an asynchronous
    // <ConfigState> via the _callback parameter.
    //
    // If your _callBack requires context it should be bound to that context via .bind()
    public static RequestConfigState(_callback: (detail: ConfigState) => void): void {
        if (_callback === null) {
            console.error("Request failed. This is due to a missing callback");
            return;
        }

        ConnectionManager.serviceConnection()?.RequestConfigState(_callback);
    }
}