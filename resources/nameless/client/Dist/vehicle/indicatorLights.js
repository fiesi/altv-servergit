import * as alt from 'alt-client';
import { Vehicle as VehicleEvent } from './../events.js';
var indicatorLightsTimeouts = [];
export function indicatorLights(veh, light, time) {
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
                veh.indicatorLights = 0;
                const idx = indicatorLightsTimeouts.findIndex((el) => el.vehId === veh.scriptID);
                indicatorLightsTimeouts.splice(idx, 1);
            }, time)
        });
    }
}
alt.onServer(VehicleEvent.IndicatorLights, (vehId, light, time) => indicatorLights(alt.Vehicle.getByRemoteID(vehId), light, time));
