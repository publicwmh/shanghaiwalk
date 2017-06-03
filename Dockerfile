FROM microsoft/aspnetcore:2.0.0-preview1
RUN apt-get update
RUN apt-get install -y libgdiplus
WORKDIR /app
COPY shanghaiwalk/bin/Release/netcoreapp2.0/publish /app
ENV ASPNETCORE_URLS http://*:5000
EXPOSE 5000
ENTRYPOINT /bin/bash -c "dotnet shanghaiwalk.dll"