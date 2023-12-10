import * as alt from 'alt-client';
import * as natives from 'natives';
import { Hud as HudEvent } from '../events.js';
import { NameByHash as WeaponNameByHash } from '../weapon/list.js';
var hudView = null;
var hudInterval = null;
alt.onServer(HudEvent.Create, (id, sid) => {
    hudView = new alt.WebView('http://resource/client/webviews/hud/index.html', true);
    hudView.emit(HudEvent.Create, id, sid);
    const local = alt.Player.local;
    if (hudInterval === null) {
        hudInterval = alt.setInterval(() => {
            const [_, ammoInClip] = natives.getAmmoInClip(local, local.currentWeapon);
            const ammoInWeapon = natives.getAmmoInPedWeapon(local, local.currentWeapon);
            hudView.emit(HudEvent.Update, {
                money: local.getSyncedMeta('money'),
                bank: local.getSyncedMeta('bank'),
                wanteds: local.getSyncedMeta('wanteds')
            }, {
                name: WeaponNameByHash.get(local.currentWeapon),
                ammo: ammoInClip,
                clipSize: ((ammoInWeapon - ammoInClip) <= 0 ? 0 : ammoInWeapon)
            }, local.isTalking);
        }, 50);
    }
});
alt.on(HudEvent.EnterVehicle, () => {
    if (hudView !== null) {
        hudView.emit(HudEvent.EnterVehicle);
    }
});
alt.on(HudEvent.UpdateVehicle, (speed, engineOn, lockState, fuel) => {
    if (hudView !== null) {
        hudView.emit(HudEvent.UpdateVehicle, speed, engineOn, lockState, fuel);
    }
});
alt.on(HudEvent.LeaveVehicle, () => {
    if (hudView !== null) {
        hudView.emit(HudEvent.LeaveVehicle);
    }
});
