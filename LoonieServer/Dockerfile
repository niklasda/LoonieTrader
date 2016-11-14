FROM mono
MAINTAINER niklasda
ADD . /src
RUN cd /src && chmod +x ./build.sh && ./build.sh && mkdir /app
RUN cp /src/build/app/* /app
WORKDIR /app
CMD [ "mono", "LoonieServer.exe" ]
