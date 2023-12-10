import * as alt from "alt-client";
var view = null;
alt.onServer('chat:create', () => {
    if (view !== null) {
        return;
    }
    view = new alt.WebView('http://resource/client/webviews/chat/index.html');
    view.on('chat:message', (text) => alt.emitServer('chat:message', text));
});
alt.onServer('chat:message', (type, from, text) => {
    if (view !== null) {
        view.emit('chat:message', { type, from, text });
    }
});
alt.on("keyup", (keycode) => {
    if (view === null) {
        return;
    }
    if (view.focused === false) {
        if (alt.gameControlsEnabled() === false) {
            return;
        }
        if (keycode === 84) {
            view.focus();
            view.emit('chat:open');
            alt.toggleGameControls(false);
            view.focus();
        }
    }
    else if (view.focused === true && keycode === 27) {
        view.unfocus();
        view.emit('chat:close');
        alt.toggleGameControls(true);
        view.unfocus();
    }
});
