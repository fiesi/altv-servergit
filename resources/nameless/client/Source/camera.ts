import * as alt from 'alt-client';
import * as natives from 'natives';

import { Camera as CameraEvent } from './events.js';

var camera: number = null;

alt.onServer(CameraEvent.Create, (posX: number, posY: number, posZ: number, rotX: number, rotY: number, rotZ: number, rotO: number, fov: number) => {
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