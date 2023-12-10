import * as alt from "alt-client";
import * as natives from "natives";

var isInNoclip: boolean = false;
alt.onServer('admin:noclip', () => {
    isInNoclip = !isInNoclip;

    if (isInNoclip === true) {
        noclipTick = alt.everyTick(() => keyHandlerTick());
    } else {
        alt.clearEveryTick(noclipTick);
        noclipTick = null;
    }
});

interface CVector { x: number, y: number, z: number }

function camVector(camRot: alt.Vector3, forward: boolean = false) {
    const rotInRad: CVector = {
        x: camRot.x * (Math.PI / 180),
        y: camRot.y * (Math.PI / 180),
        z: camRot.z * (Math.PI / 180) + (forward === true ? (Math.PI / 2) : 0),
    };

    return {
        x: Math.cos(rotInRad.z),
        y: Math.sin(rotInRad.z),
        z: Math.sin(rotInRad.x),
    };
}

function addSpeedToVector(vector1: alt.Vector3, vector2: CVector, speed: number, lr = false) {
    return new alt.Vector3(
        vector1.x + vector2.x * speed,
        vector1.y + vector2.y * speed,
        lr === true ? vector1.z : vector1.z + vector2.z * speed
    );
}

function disableAllControls() {
    natives.disableControlAction(0, 61, true); // LEFT SHIFT
    natives.disableControlAction(0, 32, true); // W
    natives.disableControlAction(0, 31, true); // S
    natives.disableControlAction(0, 34, true); // A
    natives.disableControlAction(0, 35, true); // D
}

var noclipTick: number = null;
function keyHandlerTick() {
    if (alt.gameControlsEnabled() === false) {
        return;
    }

    disableAllControls();

    const camRot: alt.Vector3 = natives.getGameplayCamRot(2);
    let newPosition: alt.Vector3 = null;

    let speed: number = 1.5;
    if (natives.isDisabledControlPressed(0, 61) === true) { // LEFT SHIFT
        speed = 3.0;
    }

    const local: alt.LocalPlayer = alt.Player.local;
    if (natives.isDisabledControlPressed(0, 32) === true) { // W
        newPosition = addSpeedToVector(local.pos, camVector(camRot, true), speed);
    } else if (natives.isDisabledControlPressed(0, 31) === true) { // S
        newPosition = addSpeedToVector(local.pos, camVector(camRot, true), -speed);
    } else if (natives.isDisabledControlPressed(0, 34) === true) { // A
        newPosition = addSpeedToVector(local.pos, camVector(camRot), -speed, true);
    } else if (natives.isDisabledControlPressed(0, 35) === true) { // D
        newPosition = addSpeedToVector(local.pos, camVector(camRot), speed, true);
    }

    if (newPosition !== null) {
        alt.emitServerUnreliable('admin:noclip:position', newPosition);
    }
}

alt.on('keyup', (keycode) => {
    if (alt.gameControlsEnabled() === false) {
        return;
    }

    const local: alt.LocalPlayer = alt.Player.local;
    if (local.hasSyncedMeta('admin') === false || Number(local.getSyncedMeta('admin')) < 1) {
        return;
    }

    if (keycode === 120) { // F9
        alt.emitServer('admin:noclip');
    }
});