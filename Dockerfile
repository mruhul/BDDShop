FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-alpine AS alpine-runtime
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT false
RUN apk add --no-cache icu-libs

ENV LC_ALL en_AU.UTF-8
ENV LANG en_AU.UTF-8


FROM mcr.microsoft.com/dotnet/core/sdk:3.0-alpine AS build
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY *.sln ./
COPY */*.csproj ./
RUN for file in $(ls *.csproj); do mkdir -p ${file%.*}/ && mv $file ${file%.*}/; done

RUN dotnet restore

# Copy whole solution
COPY . ./
RUN dotnet build

FROM build AS testrunner
RUN dotnet test

FROM build as publish
RUN dotnet publish BddShop -c Release -o ./out

FROM alpine-runtime
WORKDIR /app
COPY --from=publish /app/out ./
EXPOSE 80
ENTRYPOINT ["dotnet", "BddShop.dll"]