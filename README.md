# Simple .NET Core Container Demo

This is a basic set of .NET Core containers that help demonstrate container orchestration. As containers are deployed to different platforms, they will show configuration details to help show service discovery, upgrade, etc.

## ConfigAPI

* .NET Core web API
* docker build -t api .
* docker run -d --name api -p 5001:5001 api

## WebApp

* Basic web application 
* Requires 2 environment variables: BACKEND_IP and BACKEND_PORT
* docker build -t web .
* docker run -d -e BACKEND_IP="192.168.99.100" -e BACKEND_PORT="5001" --name web -p 5000:5000 web

## Deployment

### DCOS / Marathon

* Point Azure LB to service port (1000), update NSG
* Connect to ACS cluster: 
```
dcos package install marathon-lb
dcos marathon app add core-simpleapi-dcos.json
dcos marathon app add core-simpleweb-dcos.json
```

### Service Fabric


### Kubernetes