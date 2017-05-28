FROM microsoft/dotnet:1.1.2-runtime
WORKDIR /app
COPY bin/Release/netcoreapp1.1 /app
ENV ASPNETCORE_URLS http://*:5000
EXPOSE 5000
ENTRYPOINT /bin/bash -c "dotnet shanghaiwalk.dll"