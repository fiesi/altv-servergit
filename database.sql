drop table if exists vehicle_configuration;
drop table if exists vehicles;
drop table if exists character_clothing;
drop table if exists character_customization;
drop table if exists characters;
drop table if exists accounts;

CREATE TABLE accounts (
  id bigserial primary key,
  discord_id varchar(150) not null,
  admin int not null default 0,
  premium int not null default 0,
  created_on timestamp not null default now(),
  updated_on timestamp not null default now()
);

CREATE TABLE characters (
  id bigserial primary key,
  account_id bigserial not null,
  firstname varchar(15) not null,
  lastname varchar(15) not null,
  money int not null default 0,
  bank int not null default 0,
  bank_pin int not null default 0,
  wanteds int not null default 0,
  organization int not null default 0,
  organization_rank int not null default 0,
  family int not null default 0,
  family_rank int not null default 0,
  created_on timestamp not null default now(),
  updated_on timestamp not null default now(),
  CONSTRAINT fk_account_id
  	FOREIGN KEY(account_id)
  	references accounts(id)
);

CREATE TABLE character_customization (
  id bigserial primary key,
  character_id bigserial not null,
  sex int not null default 0,
  father int not null default 0,
  father_skin int not null default 0,
  mother int not null default 0,
  mother_skin int not null default 0,
  face_mix float4 not null default 0,
  skin_mix float4 not null default 0,
  created_on timestamp not null default now(),
  updated_on timestamp not null default now(),
  CONSTRAINT fk_character_id
  	FOREIGN KEY(character_id)
  	references characters(id)
);

CREATE TABLE character_clothing (
  id bigserial primary key,
  character_id bigserial not null,
  hair int not null default 0,
  hair_color_primary int not null default 0,
  hair_color_secondary int not null default 0,
  created_on timestamp not null default now(),
  updated_on timestamp not null default now(),
  CONSTRAINT fk_character_id
  	FOREIGN KEY(character_id)
  	references characters(id)
);

CREATE TABLE vehicles (
  id bigserial primary key,
  owner bigserial not null,
  model int not null default 0,
  body_health int not null default 1000,
  engine_health int not null default 1000,
  pos_x float8 not null default 0,
  pos_y float8 not null default 0,
  pos_z float8 not null default 0,
  rot_r float8 not null default 0,
  rot_p float8 not null default 0,
  rot_y float8 not null default 0,
  fuel float4 not null default 0,
  created_on timestamp not null default now(),
  updated_on timestamp not null default now(),
  CONSTRAINT fk_owner
  	FOREIGN KEY(owner)
  	references characters(id)
);

CREATE TABLE vehicle_configuration (
  id bigserial primary key,
  model int not null default 0 unique,
  price int not null default 0,
  fuel_tank float4 not null default 0,
  fuel_consumption float4 not null default 0
);
