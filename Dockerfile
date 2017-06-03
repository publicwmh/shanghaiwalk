FROM microsoft/2.0.0-preview1
WORKDIR /app
COPY shanghaiwalk/bin/Release/netcoreapp2.0/publish /app
ENV ASPNETCORE_URLS http://*:5000
EXPOSE 5000
ENTRYPOINT /bin/bash -c "dotnet shanghaiwalk.dll"