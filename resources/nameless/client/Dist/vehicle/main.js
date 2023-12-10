import * as alt from 'alt-client';
import * as natives from 'natives';
import './driver.js';
import { Vehicle as VehicleEvent } from './../events.js';
import { indicatorLights } from './indicatorLights.js';
import { lightsState } from './lightsState.js';
alt.onServer(VehicleEvent.Lock, (vehId, light, time) => {
    const veh = alt.Vehicle.getByRemoteID(vehId);
    if (veh === null) {
        return;
    }
    natives.playSoundFromCoord(-1, "BUTTON", veh.pos.x, veh.pos.y, veh.pos.z, "MP_PROPERTIES_ELEVATOR_DOORS", true, 10, false);
    indicatorLights(veh, light, time);
    lightsState(veh, 2, time);
});
alt.on('keyup', (keycode) => {
    if (alt.gameControlsEnabled() === false) {
        return;
    }
    const local = alt.Player.local;
    switch (keycode) {
        case 17:
            const localVeh = local.vehicle;
            if (localVeh === null || local.seat !== 1) {
                return;
            }
            alt.emitServer(VehicleEvent.Engine);
            break;
        case 76:
            alt.emitServer(VehicleEvent.Lock);
            break;
    }
});
