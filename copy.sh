#!/bin/bash

    DIR=bin/debug/netcoreapp2.0    
    mkdir -p console/$DIR/commands

# Copies file 
    cp -R cloud/$DIR/cloud.dll console/$DIR/commands/cloud.dll
    cp -R cloud/$DIR/cloud.pdb console/$DIR/commands/cloud.pdb
    cp -R templates console/$DIR/