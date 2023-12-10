import * as alt from "alt-client";
var tempObject = null;
function destroyTempObject() {
    if (tempObject !== null) {
        tempObject.destroy();
        tempObject = null;
        return true;
    }
    return false;
}
alt.on("keyup", (keycode) => {
    if (alt.gameControlsEnabled() === false) {
        return;
    }
    if (keycode === 13) {
        if (tempObject !== null) {
            alt.emitServer('object:create', tempObject.model, tempObject.pos, tempObject.rot);
            destroyTempObject();
        }
    }
    else if (keycode === 66) {
        if (destroyTempObject() === true) {
            return;
        }
        const local = alt.Player.local;
        tempObject = new alt.LocalObject("prop_barrier_work05", local.pos, new alt.Vector3(0, 0, 0));
        tempObject.attachToEntity(local.scriptID, -1, new alt.Vector3(0, 1, -1), new alt.Vector3(0, 0, 0), false, false, true);
    }
});
