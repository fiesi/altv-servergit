import * as alt from 'alt-client';

import { Vehicle as VehicleEvent } from './../events.js';

var indicatorLightsTimeouts: { vehId: number, timeoutId: number }[] = [];

export function indicatorLights(veh: alt.Vehicle, light: number, time: number) {
    if (veh === null) {
        return;
    }

    veh.indicatorLights = light;

    if (time !== -1) {
        const idx = indicatorLightsTimeouts.findIndex((el) => el.vehId === veh.scriptID);
        if (idx !== -1) {
            alt.clearTimeout(indicatorLightsTimeouts[idx].timeoutId);
            indicatorLightsTimeouts.splice(idx, 1);
        }
        
        indicatorLightsTimeouts.push({
            vehId: veh.scriptID,
            timeoutId: alt.setTimeout(() => {
                veh.indicatorLights = alt.VehicleIndicatorLights.None;

                const idx = indicatorLightsTimeouts.findIndex((el) => el.vehId === veh.scriptID);
                indicatorLightsTimeouts.splice(idx, 1);
            }, time)
        });
    }
}

alt.onServer(VehicleEvent.IndicatorLights, (vehId: number, light: number, time: number) => indicatorLights(alt.Vehicle.getByRemoteID(vehId), light, time));