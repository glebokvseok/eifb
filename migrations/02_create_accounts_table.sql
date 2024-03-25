CREATE TABLE IF NOT EXISTS accounts(
   login TEXT PRIMARY KEY REFERENCES users(login),
   password TEXT NOT NULL
);
