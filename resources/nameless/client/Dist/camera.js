import * as alt from 'alt-client';
import * as natives from 'natives';
import { Camera as CameraEvent } from './events.js';
var camera = null;
alt.onServer(CameraEvent.Create, (posX, posY, posZ, rotX, rotY, rotZ, rotO, fov) => {
    if (camera === null) {
        camera = natives.createCam('DEFAULT_SCRIPTED_CAMERA', true);
        natives.setCamCoord(camera, posX, posY, posZ);
        natives.setCamRot(camera, rotX, rotY, rotZ, rotO);
        natives.setCamFov(camera, fov);
        natives.setCamActive(camera, true);
        natives.renderScriptCams(true, false, 0, true, false, 0);
    }
});
alt.onServer(CameraEvent.Destroy, () => {
    if (camera !== null) {
        natives.renderScriptCams(false, false, 0, true, false, 0);
        natives.setCamActive(camera, false);
        natives.destroyCam(camera, true);
        camera = null;
    }
});
