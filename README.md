# Vulnerable ASP.NET Core App for Testing

## Setup

1. Get a `TrendAppProtect-x64-*.zip`

2. `unzip TrendAppProtect-x64-*.zip -d TrendAppProtect/`

3. `cp TrendAppProtect.example.config TrendAppProtect/TrendAppProtect.config`

4. Edit `TrendAppProtect/TrendAppProtect.config` with your key and secret

## Running

For Ubuntu:

    docker-compose up --build

For Alpine:

    docker-compose -f docker-compose.alpine.yml up --build
