import * as alt from "alt-client";
import * as natives from "natives";
var isInNoclip = false;
alt.onServer('admin:noclip', () => {
    isInNoclip = !isInNoclip;
    if (isInNoclip === true) {
        noclipTick = alt.everyTick(() => keyHandlerTick());
    }
    else {
        alt.clearEveryTick(noclipTick);
        noclipTick = null;
    }
});
function camVector(camRot, forward = false) {
    const rotInRad = {
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
function addSpeedToVector(vector1, vector2, speed, lr = false) {
    return new alt.Vector3(vector1.x + vector2.x * speed, vector1.y + vector2.y * speed, lr === true ? vector1.z : vector1.z + vector2.z * speed);
}
function disableAllControls() {
    natives.disableControlAction(0, 61, true);
    natives.disableControlAction(0, 32, true);
    natives.disableControlAction(0, 31, true);
    natives.disableControlAction(0, 34, true);
    natives.disableControlAction(0, 35, true);
}
var noclipTick = null;
function keyHandlerTick() {
    if (alt.gameControlsEnabled() === false) {
        return;
    }
    disableAllControls();
    const camRot = natives.getGameplayCamRot(2);
    let newPosition = null;
    let speed = 1.5;
    if (natives.isDisabledControlPressed(0, 61) === true) {
        speed = 3.0;
    }
    const local = alt.Player.local;
    if (natives.isDisabledControlPressed(0, 32) === true) {
        newPosition = addSpeedToVector(local.pos, camVector(camRot, true), speed);
    }
    else if (natives.isDisabledControlPressed(0, 31) === true) {
        newPosition = addSpeedToVector(local.pos, camVector(camRot, true), -speed);
    }
    else if (natives.isDisabledControlPressed(0, 34) === true) {
        newPosition = addSpeedToVector(local.pos, camVector(camRot), -speed, true);
    }
    else if (natives.isDisabledControlPressed(0, 35) === true) {
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
    const local = alt.Player.local;
    if (local.hasSyncedMeta('admin') === false || Number(local.getSyncedMeta('admin')) < 1) {
        return;
    }
    if (keycode === 120) {
        alt.emitServer('admin:noclip');
    }
});
