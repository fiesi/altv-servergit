import * as alt from 'alt-client';
import { Voice as VoiceEvent } from './events.js';
const defaultPosition = new alt.Vector3(0, 0, 0);
var checkpoint = null;
var checkpointTimeout = 0;
alt.everyTick(() => {
    if (checkpoint === null) {
        return;
    }
    checkpoint.pos = alt.Player.local.pos.sub(0, 0, 1);
});
alt.on('keyup', (keycode) => {
    if (alt.gameControlsEnabled() === false) {
        return;
    }
    if (keycode !== 114) {
        return;
    }
    alt.emitServer(VoiceEvent.Range);
    if (checkpointTimeout !== 0) {
        alt.clearTimeout(checkpointTimeout);
    }
    if (checkpoint !== null) {
        checkpoint.destroy();
    }
    const local = alt.Player.local;
    let range = 3;
    switch (local.getSyncedMeta('voice:range')) {
        case 0:
            range = 6;
            break;
        case 1:
            range = 10;
            break;
    }
    checkpoint = new alt.Checkpoint(44, local.pos.sub(0, 0, 1), defaultPosition, range, 2, new alt.RGBA(252, 186, 3, 100), new alt.RGBA(0, 0, 0, 0), 100);
    checkpointTimeout = alt.setTimeout(() => {
        checkpoint.destroy();
        checkpoint = null;
        checkpointTimeout = 0;
    }, 1000);
});
