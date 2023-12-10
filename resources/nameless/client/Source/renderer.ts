import * as alt from 'alt-client';
import * as natives from 'natives';

import { drawText3d, distance2d } from './helper.js';

export function playerTags() {
    const local = alt.Player.local;
    const players = alt.Player.all;

    for (let i = 0; i < players.length; i++) {
        // Some metas are required
        if (players[i].hasSyncedMeta('id') === false) {
            continue;
        }

        // Skip own entity
        if (players[i].scriptID === local.scriptID) {
            continue;
        }

        // Skip invisible entities
        if (natives.isEntityVisible(players[i].scriptID) === false) {
            return;
        }

        // Skip entities behind walls
        if (!natives.hasEntityClearLosToEntity(local.scriptID, players[i].scriptID, 17)) {
            continue;
        }

        const dist = distance2d(local.pos, players[i].pos);
        if (dist > 10) {
            continue;
        }
        const scale = 1 - dist / 20;
        const fontSize = 0.4 * scale;

        let headPos = { ...natives.getPedBoneCoords(players[i].scriptID, 12844, 0, 0, 0) };
        headPos.z += 0.5;
        const entity = players[i].vehicle ? players[i].vehicle.scriptID : players[i].scriptID;
        const vector = natives.getEntityVelocity(entity);
        const frameTime = natives.getFrameTime();
        
        drawText3d(`Fremder\n[${players[i].getSyncedMeta('id')}]`, headPos.x + vector.x * frameTime, headPos.y + vector.y * frameTime, headPos.z + vector.z * frameTime, fontSize, 4, 255, 255, 255, 255);
    }
}

export function vehicleTags() {
    const local = alt.Player.local;
    const vehicles = alt.Vehicle.all;

    for (let i = 0; i < vehicles.length; i++) {
        // Some metas are required
        if (vehicles[i].hasSyncedMeta('id') === false) {
            continue;
        }

        // Skip entities behind walls
        if (!natives.hasEntityClearLosToEntity(local.scriptID, vehicles[i].scriptID, 17)) {
            continue;
        }

        const dist = distance2d(local.pos, vehicles[i].pos);
        if (dist > 10) {
            continue;
        }
        const scale = 1 - dist / 20;
        const fontSize = 0.4 * scale;

        const vector = natives.getEntityVelocity(vehicles[i].scriptID);
        const frameTime = natives.getFrameTime();
        
        drawText3d(`ID: ${vehicles[i].remoteID}\n[${vehicles[i].getSyncedMeta('id')}]`, vehicles[i].pos.x + vector.x * frameTime, vehicles[i].pos.y + vector.y * frameTime, vehicles[i].pos.z + vector.z * frameTime, fontSize, 4, 255, 255, 255, 255);
    }
}