FROM mcr.microsoft.com/dotnet/sdk:8.0

RUN apt-get update && \
    apt-get install -qq -y --no-install-recommends && \
    apt-get install -y procps && \
    apt install unzip && \
    curl -sSL https://aka.ms/getvsdbgsh | /bin/sh /dev/stdin -v latest -l ~/vsdbg

ENV INSTALL_PATH /AwesomeShop.Services.Customers

RUN mkdir $INSTALL_PATH

WORKDIR $INSTALL_PATH

COPY . .

RUN dotnet restore AwesomeShop.Services.Customers.Api/AwesomeShop.Services.Customers.Api.csproj

ENV PATH="$PATH:/root/.dotnet/tools"
