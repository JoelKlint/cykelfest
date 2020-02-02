FROM mcr.microsoft.com/dotnet/core/sdk:3.1

WORKDIR /workspace

ADD . .

RUN dotnet publish --configuration Release --output /build_artifacts

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1

COPY --from=0 /build_artifacts cykelfest

CMD ASPNETCORE_URLS=http://*:$PORT dotnet /cykelfest/cykelfest.dll
