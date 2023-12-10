import * as alt from 'alt-client';
import * as natives from 'natives';
var lightsStateTimeouts = [];
export function lightsState(veh, light, time) {
    if (veh === null) {
        return;
    }
    const [_, alreadyLightsOn, alreadyHighbeamsOn] = natives.getVehicleLightsState(veh.scriptID);
    if (alreadyLightsOn === true || alreadyHighbeamsOn === true) {
        return;
    }
    natives.setVehicleLights(veh.scriptID, light);
    if (time !== -1) {
        const idx = lightsStateTimeouts.findIndex((el) => el.vehId === veh.scriptID);
        if (idx !== -1) {
            alt.clearTimeout(lightsStateTimeouts[idx].timeoutId);
            lightsStateTimeouts.splice(idx, 1);
        }
        lightsStateTimeouts.push({
            vehId: veh.scriptID,
            timeoutId: alt.setTimeout(() => {
                natives.setVehicleLights(veh.scriptID, 0);
                const idx = lightsStateTimeouts.findIndex((el) => el.vehId === veh.scriptID);
                lightsStateTimeouts.splice(idx, 1);
            }, time)
        });
    }
}
