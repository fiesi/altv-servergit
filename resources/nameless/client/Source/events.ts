export enum Camera {
    Create    = 'camera:create',
    Destroy   = 'camera:destroy',
};

export enum Login {
    Pressed = 'login:pressed',
    Error   = 'login:error',
    Destroy = 'login:destroy',
    Token   = 'login:token',
};

export enum Hud {
    Create        = 'hud:create',
    Update        = 'hud:update',
    EnterVehicle  = 'hud:vehicle:enter',
    UpdateVehicle = 'hud:vehicle:update',
    LeaveVehicle  = 'hud:vehicle:leave',
};

export enum Character {
    SelectionCreate  = 'character:selection:create',
    SelectionDestroy = 'character:selection:destroy',
    Select           = 'character:select',
    CreationCreate   = 'character:creation:create',
    CreationDestroy  = 'character:creation:destroy',
    Create           = 'character:create',
    CreationError    = 'character:creation:error',
    CreationSave     = 'character:creation:save',
    CreationCancel   = 'character:creation:cancel',
};

export enum Voice {
    Range  = 'voice:range',
};

export enum Vehicle {
    Engine           = 'vehicle:engine',
    Lock             = 'vehicle:lock',
    IndicatorLights  = 'vehicle:indicatorLights',
    Update           = 'vehicle:update',
};