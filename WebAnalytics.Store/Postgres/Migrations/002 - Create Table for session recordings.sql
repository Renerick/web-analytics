create table session_recording
(
    recording_id bigserial not null
        constraint session_recording_pk
            primary key,
    session_id text not null,
--         constraint session_recording_session_session_id_fk
--             references session,
    time timestamp with time zone not null,
    url text not null,
    recording_data text not null
);

