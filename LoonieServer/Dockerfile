FROM microsoft/dotnet:1.0.1-core
MAINTAINER niklasda

WORKDIR build/a/app/LoonieTrader.Server/bin/Release
COPY . /app

WORKDIR /app
EXPOSE 8080
ENV ASPNETCORE_URLS=http://*:8080
ENTRYPOINT [ "dotnet", "LoonieTrader.Server.dll" ]
