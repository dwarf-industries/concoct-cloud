#!/bin/bash

cd $1 
mkdir $2
cd $2
ls -l
git init --bare --shared 
cd ..
sudo groupadd $2Contribute
sudo groupadd $2Read

sudo chgrp $1 $2Contribute
sudo chgrp $1 $2Read
sudo chmod -R 777 $2Contribute
sudo chmod -R 444 $2Contribute
