import * as alt from 'alt-client';
import * as natives from 'natives';
import { Vehicle as VehicleEvent, Hud as HudEvent } from './../events.js';
;
var vehicleData = null;
function enteredVehicle(veh, seat) {
    if (seat !== 1) {
        return;
    }
    const local = alt.Player.local;
    natives.setPedConfigFlag(local, 429, true);
    natives.setPedConfigFlag(local, 184, true);
    natives.setPedConfigFlag(local, 241, true);
    natives.setPedConfigFlag(local, 35, false);
    vehicleData = {
        fuel: null,
        tick: null,
    };
    if (veh.hasSyncedMeta('fuel') === true && veh.hasSyncedMeta('fuel_tank') === true && veh.hasSyncedMeta('fuel_consumption') === true) {
        vehicleData.fuel = {
            value: Number(veh.getSyncedMeta('fuel')),
            tank: Number(veh.getSyncedMeta('fuel_tank')),
            consumption: Number(veh.getSyncedMeta('fuel_consumption')),
            interval: null,
        };
        vehicleData.fuel.interval = alt.setInterval(() => {
            if (veh.engineOn === true) {
                const trip = veh.speed / 1000;
                vehicleData.fuel.value -= vehicleData.fuel.consumption * trip;
                if (vehicleData.fuel.value < 0) {
                    vehicleData.fuel.value = 0;
                    alt.emitServer(VehicleEvent.Update, { fuel: vehicleData.fuel.value });
                    alt.emitServer(VehicleEvent.Engine);
                }
                else {
                    alt.emitServerUnreliable(VehicleEvent.Update, { fuel: vehicleData.fuel.value });
                }
                const fuelLevel = (vehicleData.fuel.value / vehicleData.fuel.tank) * 100.0;
                veh.fuelLevel = fuelLevel < 6.0 ? 6.0 : fuelLevel;
            }
            else {
                vehicleData.fuel.value = Number(veh.getSyncedMeta('fuel'));
            }
        }, 1000);
    }
    alt.emit(HudEvent.EnterVehicle);
    vehicleData.tick = alt.everyTick(() => {
        alt.emit(HudEvent.UpdateVehicle, veh.speed, veh.engineOn, veh.lockState, (vehicleData.fuel === null ? 0 : (vehicleData.fuel.value / vehicleData.fuel.tank * 100)).toFixed(2));
    });
}
alt.on('enteredVehicle', (veh, seat) => enteredVehicle(veh, seat));
function leftVehicle(seat) {
    if (seat !== 1) {
        return;
    }
    if (vehicleData !== null) {
        if (vehicleData.fuel !== null) {
            alt.emitServer(VehicleEvent.Update, { fuel: vehicleData.fuel.value });
            if (vehicleData.fuel.interval !== null) {
                alt.clearInterval(vehicleData.fuel.interval);
                vehicleData.fuel.interval = null;
            }
        }
        if (vehicleData.tick !== null) {
            alt.clearEveryTick(vehicleData.tick);
            vehicleData.tick = null;
        }
        vehicleData = null;
    }
    alt.emit(HudEvent.LeaveVehicle);
}
alt.on('leftVehicle', (_, seat) => leftVehicle(seat));
alt.on('changedVehicleSeat', (veh, oldSeat, newSeat) => {
    if (newSeat === 1) {
        enteredVehicle(veh, newSeat);
    }
    else if (oldSeat === 1) {
        leftVehicle(oldSeat);
    }
});
