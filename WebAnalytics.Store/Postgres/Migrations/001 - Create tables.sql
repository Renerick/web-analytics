create table session
(
    session_id        text not null
        constraint session_pk
            primary key,
    site_id         text not null,
    visitor_id      text not null,
    last_action_id  text not null,
    start           timestamp with time zone,
    device_info     jsonb
);

create table action
(
    action_id  bigserial                not null
        constraint action_pk
            primary key,
    session_id   text                     not null,
    visitor_id text                     not null,
    site_id    text                     not null,
    time       timestamp with time zone not null,
    type       text                     not null,
    url        text,
    data       jsonb
);

create table site
(
    site_id uuid not null
        constraint site_pk
            primary key,
    name    text,
    url     text
);
