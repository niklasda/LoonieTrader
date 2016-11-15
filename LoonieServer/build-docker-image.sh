#!/bin/bash
if test "$OS" = "Windows_NT"
then
else
  mono .paket/paket.exe restore
  exit_code=$?
  if [ $exit_code -ne 0 ]; then
  	exit $exit_code
  fi

  docker build -t LoonieServer .
  exit_code=$?
  if [ $exit_code -ne 0 ]; then
    exit $exit_code
  fi
fi