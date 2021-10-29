#!/bin/bash
sudo rm logs/appsec.log logs/defence.log

# Ubuntu image
docker-compose up --build

# Alpine image
#docker-compose -f docker-compose.alpine.yml up --build