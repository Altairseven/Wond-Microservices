#!/bin/bash

echo "Building Shared Lib Image..."
docker build -t altairseven/wondshared ./Shared || exit
echo "Building Auth Service Image..."
docker build -t altairseven/wondauth ./Auth || exit
echo "Building Notifications Service Image..."
docker build -t altairseven/wondnotifications ./Notifications || exit
echo "Building Params Service Image..."
docker build -t altairseven/wondparams ./Params || exit
echo "Building Sells Service Image..."
docker build -t altairseven/wondsells ./Sells || exit
echo "Building Stock Service Image..."
docker build -t altairseven/wondstock ./Stock || exit

echo "All images were built successfuly";