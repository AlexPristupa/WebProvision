#!/bin/bash
cp ./mentolprovisionweb.service /etc/systemd/system/
systemctl daemon-reload
systemctl enable mentolprovisionweb.service
systemctl start mentolprovisionweb
