# Dockefile to create an image for either of the functions
#
# To build it, a function name needs to be passed in as an argument to the build.
# For example:
#
# docker build -t stacks-dotnet-cqrs-events --build-arg function=Htec.Poc.Listener .\src\functions

FROM mcr.microsoft.com/dotnet/sdk:6.0 as build

ARG function
ARG functionPath
ARG outdir=out

WORKDIR /app

# Copy the worker project and build
COPY src/ src/
RUN cd ${functionPath} && dotnet publish -c Release -o ${outdir} ${function}

# Build the runtime image
FROM mcr.microsoft.com/azure-functions/dotnet:4 as runtime
EXPOSE 80
WORKDIR /home/site/wwwroot
COPY --from=build /app/${outdir} .
