#!/bin/bash
if which inotifywait >/dev/null; then
  while true; do
    change=$(inotifywait -e close_write,moved_to,create,delete -q -r .)
    change=${change#./ * }
    echo Changes detected:
    echo $change
    echo Triggering build:
    ./build.sh $@
  done
else
  echo inotifywait is not installed
fi