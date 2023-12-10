export var Camera;
(function (Camera) {
    Camera["Create"] = "camera:create";
    Camera["Destroy"] = "camera:destroy";
})(Camera || (Camera = {}));
;
export var Login;
(function (Login) {
    Login["Pressed"] = "login:pressed";
    Login["Error"] = "login:error";
    Login["Destroy"] = "login:destroy";
    Login["Token"] = "login:token";
})(Login || (Login = {}));
;
export var Hud;
(function (Hud) {
    Hud["Create"] = "hud:create";
    Hud["Update"] = "hud:update";
    Hud["EnterVehicle"] = "hud:vehicle:enter";
    Hud["UpdateVehicle"] = "hud:vehicle:update";
    Hud["LeaveVehicle"] = "hud:vehicle:leave";
})(Hud || (Hud = {}));
;
export var Character;
(function (Character) {
    Character["SelectionCreate"] = "character:selection:create";
    Character["SelectionDestroy"] = "character:selection:destroy";
    Character["Select"] = "character:select";
    Character["CreationCreate"] = "character:creation:create";
    Character["CreationDestroy"] = "character:creation:destroy";
    Character["Create"] = "character:create";
    Character["CreationError"] = "character:creation:error";
    Character["CreationSave"] = "character:creation:save";
    Character["CreationCancel"] = "character:creation:cancel";
})(Character || (Character = {}));
;
export var Voice;
(function (Voice) {
    Voice["Range"] = "voice:range";
})(Voice || (Voice = {}));
;
export var Vehicle;
(function (Vehicle) {
    Vehicle["Engine"] = "vehicle:engine";
    Vehicle["Lock"] = "vehicle:lock";
    Vehicle["IndicatorLights"] = "vehicle:indicatorLights";
    Vehicle["Update"] = "vehicle:update";
})(Vehicle || (Vehicle = {}));
;
