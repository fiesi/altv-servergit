import * as alt from 'alt-client';
import * as natives from 'natives';

alt.onServer('radar', (val: boolean) => {
    natives.displayRadar(val);
});

alt.onServer('freeze', (val: boolean) => {
    const player: number = alt.Player.local.scriptID;
    natives.freezeEntityPosition(player, val);
});

alt.onServer('visible', (val: boolean) => {
    const player: number = alt.Player.local.scriptID;
    natives.setEntityVisible(player, val, false);
});

// Set the alt:V watermark to bottom center
alt.setWatermarkPosition(alt.WatermarkPosition.BottomCenter);

import { playerTags as renderPlayerTags, vehicleTags as renderVehicleTags } from './renderer.js';

alt.everyTick(() => {
    natives.displayAmmoThisFrame(false);
    natives.hideHudComponentThisFrame(1); // Wanted stars
    natives.hideHudComponentThisFrame(6); // Vehicle names
    natives.hideHudComponentThisFrame(7); // Area names
    natives.hideHudComponentThisFrame(8); // Vehicle classes
    natives.hideHudComponentThisFrame(9); // Street names

    renderPlayerTags();
    renderVehicleTags();
});
