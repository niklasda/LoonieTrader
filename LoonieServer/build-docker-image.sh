#!/bin/bash
if test "$OS" = "Windows_NT"
then
  .paket/paket.exe restore

  docker build -t loonie-trader-server .
else
  mono .paket/paket.exe restore
  exit_code=$?
  if [ $exit_code -ne 0 ]; then
  	exit $exit_code
  fi

  docker build -t loonie-trader-server .
  exit_code=$?
  if [ $exit_code -ne 0 ]; then
    exit $exit_code
  fi
fi