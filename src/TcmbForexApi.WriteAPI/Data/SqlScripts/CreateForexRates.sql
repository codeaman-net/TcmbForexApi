CREATE TABLE IF NOT EXISTS forex_rates
(
    id SERIAL PRIMARY KEY,

    rate_date DATE NOT NULL,

    code VARCHAR(10) NOT NULL,

    unit INTEGER NOT NULL,

    name VARCHAR(200) NOT NULL,

    forex_buying NUMERIC(18,6) NOT NULL,

    forex_selling NUMERIC(18,6) NOT NULL,

    banknote_buying NUMERIC(18,6) NOT NULL,

    banknote_selling NUMERIC(18,6) NOT NULL,

    cross_rate_usd NUMERIC(18,6),

    CONSTRAINT uq_forex_rates_date_code
        UNIQUE(rate_date, code)
);