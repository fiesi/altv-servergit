import * as alt from 'alt-client';
import * as natives from 'natives';
alt.onServer('radar', (val) => {
    natives.displayRadar(val);
});
alt.onServer('freeze', (val) => {
    const player = alt.Player.local.scriptID;
    natives.freezeEntityPosition(player, val);
});
alt.onServer('visible', (val) => {
    const player = alt.Player.local.scriptID;
    natives.setEntityVisible(player, val, false);
});
alt.setWatermarkPosition(4);
import { playerTags as renderPlayerTags, vehicleTags as renderVehicleTags } from './renderer.js';
alt.everyTick(() => {
    natives.displayAmmoThisFrame(false);
    natives.hideHudComponentThisFrame(1);
    natives.hideHudComponentThisFrame(6);
    natives.hideHudComponentThisFrame(7);
    natives.hideHudComponentThisFrame(8);
    natives.hideHudComponentThisFrame(9);
    renderPlayerTags();
    renderVehicleTags();
});
