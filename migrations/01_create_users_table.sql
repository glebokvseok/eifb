CREATE TABLE IF NOT EXISTS users(
    id SERIAL,
    login TEXT PRIMARY KEY,
    name TEXT NOT NULL,
    surname TEXT NOT NULL
);
