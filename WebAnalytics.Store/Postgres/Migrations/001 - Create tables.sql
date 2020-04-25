create table session
(
    session_id        uuid not null
        constraint session_pk
            primary key,
    site_id         uuid not null,
    visitor_id      uuid not null,
    last_action_id  uuid not null,
    start           timestamp with time zone,
    device_info     jsonb
);

create table action
(
    action_id  bigserial                not null
        constraint action_pk
            primary key,
    session_id   uuid                     not null,
    visitor_id uuid                     not null,
    site_id    uuid                     not null,
    time       timestamp with time zone not null,
    type       text                     not null,
    name       text                     not null,
    url        text,
    data       jsonb
);

create table error
(
    error_id       uuid      not null
        constraint error_pk
            primary key,
    session_id       uuid,
    last_action_id bigserial not null,
    error_data     jsonb     not null
);

create table site
(
    site_id uuid not null
        constraint site_pk
            primary key,
    name    text,
    url     text
);
