import * as alt from "alt-client";
import * as natives from "natives";

alt.onServer('notification:show', (imageName, headerMsg, detailsMsg, message) => draw(imageName, headerMsg, detailsMsg, message));

export function draw(imageName: string, headerMsg: string, detailsMsg: string, message: string) {
    natives.beginTextCommandThefeedPost('STRING');
    natives.addTextComponentSubstringPlayerName(message);
    natives.endTextCommandThefeedPostMessagetextTu(
        imageName.toUpperCase(),
        imageName.toUpperCase(),
        false,
        4,
        headerMsg,
        detailsMsg,
        1.0
    );
    natives.endTextCommandThefeedPostTicker(false, false);
}