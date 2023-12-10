import * as alt from 'alt-client';
import * as natives from 'natives';
import { Login as LoginEvent } from '../events.js';
var view = null;
alt.on('connectionComplete', () => {
    natives.pauseClock(true);
    natives.triggerScreenblurFadeIn(0);
    view = new alt.WebView('http://resource/client/webviews/login/index.html', false);
    view.on(LoginEvent.Pressed, async () => {
        try {
            const token = await alt.Discord.requestOAuth2Token('1173352842531192963');
            alt.emitServer(LoginEvent.Token, token);
        }
        catch {
            view.emit(LoginEvent.Error, 'Bitte stelle sicher, dass du Discord im Hintergrund geöffnet hast!');
        }
    });
    view.focus();
    alt.showCursor(true);
    alt.toggleGameControls(false);
    alt.toggleVoiceControls(false);
});
alt.onServer(LoginEvent.Error, (msg) => {
    if (view !== null) {
        view.emit(LoginEvent.Error, msg);
    }
});
alt.onServer(LoginEvent.Destroy, () => {
    natives.triggerScreenblurFadeOut(200);
    alt.toggleVoiceControls(true);
    alt.toggleGameControls(true);
    alt.showCursor(false);
    if (view !== null) {
        view.unfocus();
        view.destroy();
        view = null;
    }
});
