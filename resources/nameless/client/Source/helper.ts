import * as native from 'natives';

export function drawText2d(
    msg:           string,
    x:             number,
    y:             number,
    scale:         number,
    fontType:      number,
    r:             number,
    g:             number,
    b:             number,
    a:             number,
    useOutline:    boolean = true,
    useDropShadow: boolean = true,
    align:         number  = 0
) {
    native.beginTextCommandDisplayText('STRING');
    native.addTextComponentSubstringPlayerName(msg);
    native.setTextFont(fontType);
    native.setTextScale(1, scale);
    native.setTextWrap(0.0, 1.0);
    native.setTextCentre(true);
    native.setTextColour(r, g, b, a);
    native.setTextJustification(align);

    if (useOutline) {
        native.setTextOutline();
    }

    if (useDropShadow) {
        native.setTextDropShadow();
    }

    native.endTextCommandDisplayText(x, y, 0);
}

export function drawText3d(
    msg:           string,
    x:             number,
    y:             number,
    z:             number,
    scale:         number,
    fontType:      number,
    r:             number,
    g:             number,
    b:             number,
    a:             number,
    useOutline:    boolean = true,
    useDropShadow: boolean = true
) {
    native.setDrawOrigin(x, y, z, false);
    native.beginTextCommandDisplayText('STRING');
    native.addTextComponentSubstringPlayerName(msg);
    native.setTextFont(fontType);
    native.setTextScale(1, scale);
    native.setTextWrap(0.0, 1.0);
    native.setTextCentre(true);
    native.setTextColour(r, g, b, a);

    if (useOutline) {
        native.setTextOutline();
    }

    if (useDropShadow) {
        native.setTextDropShadow();
    }

    native.endTextCommandDisplayText(0, 0, 0);
    native.clearDrawOrigin();
}

export function distance2d(vector1: any, vector2: any) {
    return Math.sqrt(Math.pow(vector1.x - vector2.x, 2) + Math.pow(vector1.y - vector2.y, 2));
}