import * as alt from 'alt-client';
import * as natives from 'natives';

import { Character as CharacterEvent } from '../../events.js';
import { Data as TempPedData, Clothing as TempPedClothing } from './interfaces.js';

var view: alt.WebView = null;
var tempPedId: number = 0;

function updatePedHair(data: TempPedClothing) {
    if (tempPedId === 0) {
        return;
    }

    natives.setPedComponentVariation(tempPedId, 2, data.hair, 0, 0); // hair
    natives.setPedHairTint(tempPedId, Number(data.hairColorPrimary), Number(data.hairColorSecondary)); // color
}

async function updatePed(data: TempPedData | null, clothing: TempPedClothing | null) {
    if (tempPedId === 0) {
        await createPed(data);
        return;
    }

    if (data !== null) {
        natives.setPedHeadBlendData(
            tempPedId,
            Number(data.father),
            Number(data.mother),
            0,
            Number(data.fatherSkin),
            Number(data.motherSkin),
            0,
            Number(data.faceMix),
            Number(data.skinMix),
            0,
            false
        );
    }

    if (clothing !== null) {
        updatePedHair(clothing);
    }
}

async function createPed(data: TempPedData) {
    if (tempPedId !== 0) {
        natives.deletePed(tempPedId);
    }

    const modelHash: number = alt.hash(data.sex === 0 ? "mp_m_freemode_01" : "mp_f_freemode_01");
    await alt.Utils.requestModel(modelHash);
    tempPedId = natives.createPed(data.sex === 0 ? 4 : 5, modelHash, 341.3341, -997.8612, -100.19622, -110, false, true);

    if (tempPedId !== 0) {
        natives.setEntityRotation(tempPedId, 0, 0, -110, 0, true);
        natives.freezeEntityPosition(tempPedId, true);
    }

    updatePed(data, null);
}

alt.onServer(CharacterEvent.CreationCreate, () => {
    view = new alt.WebView('http://resource/client/webviews/character/index.html', false);
    view.on(CharacterEvent.CreationSave, (firstname, lastname, customization, clothing) => alt.emitServer(CharacterEvent.CreationSave, firstname, lastname, customization, clothing));
    view.on(CharacterEvent.CreationCancel, () => alt.emitServer(CharacterEvent.CreationCancel));
    view.on('character:creation:create:ped', async (data: TempPedData) => await createPed(data));
    view.on('character:creation:update:ped', async (data: TempPedData | null, clothing: TempPedClothing | null) => await updatePed(data, clothing));
    view.focus();
    view.emit('character:route', '/create');
    
    alt.showCursor(true);
    alt.toggleGameControls(false);
    alt.toggleVoiceControls(false);

    createPed({ sex: 0, father: 0, fatherSkin: 0, mother: 0, motherSkin: 0, faceMix: 0, skinMix: 0 });
});

alt.onServer(CharacterEvent.CreationError, (msg: string) => view.emit(CharacterEvent.CreationError, msg));

alt.onServer(CharacterEvent.CreationDestroy, () => {
    if (tempPedId !== 0) {
        natives.deletePed(tempPedId);
    }

    alt.toggleVoiceControls(true);
    alt.toggleGameControls(true);
    alt.showCursor(false);
    
    if (view !== null) {
        view.unfocus();
        view.destroy();
        view = null;
    }
});