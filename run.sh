#!/usr/bin/env sh
# Play in the terminal:   ./run.sh
# Run the self-tests:     ./run.sh test
# Needs the .NET 8 SDK (https://dotnet.microsoft.com/download).
cd "$(dirname "$0")"
DOTNET="${DOTNET:-dotnet}"
if [ "$1" = "test" ]; then exec "$DOTNET" run -- --selftest; fi
exec "$DOTNET" run
