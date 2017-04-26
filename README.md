# Simple .NET Core Container Demo

This is a basic set of .NET Core containers that help demonstrate container orchestration. As containers are deployed to different platforms, they will show configuration details to help show service discovery, upgrade, etc.

## ConfigAPI

* .NET Core web API
* docker build -t api .
* docker run -d --name api -p 5001:5001 api

## WebApp

* Basic web application 
* Requires 3 environment variables: BACKEND_IP, BACKEND_PORT, and ORCHESTRATOR.
* docker build -t web .
* docker run -d -e BACKEND_IP="192.168.99.100" -e BACKEND_PORT="5001" -e ORCHESTRATOR="Docker" --name web -p 5000:5000 web

## SQL on Linux

* Coming soon

## Deployment

### Kubernetes

* Deploy with kubectl
```
kubectl run core-api --image chzbrgr71/core-api:kube --replicas=4
kubectl expose deployment core-api --port=8080 --target-port=5001 --cluster-ip=10.0.176.131

kubectl run core-web --image chzbrgr71/core-web:kube --replicas=5 --env="BACKEND_PORT=8080" --env="BACKEND_IP=10.0.176.131"
kubectl expose deployment core-web --port=80 --target-port=5000 --type="LoadBalancer"
```

* Deploy with YAML
```
kubectl create -f ./kube-deploy.yaml
```

### Docker Swarm

```
docker-compose up
```

### DCOS / Marathon

* Point Azure LB to service port (1000), update NSG
* Connect to ACS cluster: 
```
dcos package install marathon-lb
dcos marathon app add core-simpleapi-dcos.json
dcos marathon app add core-simpleweb-dcos.json
```

### Service Fabric

Azure CLI required for this deployment. Must open port 5001 for the backend nodes and port 80 for the frontend.

```
azure servicefabric cluster connect http://yourcluster.cloudapp.azure.com:19080
azure servicefabric application package copy core-simple-package fabric:ImageStore
azure servicefabric application type register core-simple-package
azure servicefabric application create fabric:/core-simple core-simple-type 1.0
azure servicefabric service create --application-name fabric:/core-simple --service-name fabric:/core-simple/api --service-type-name CoreAPIService --instance-count 2 --service-kind Stateless --partition-scheme Singleton --placement-constraints "NodeType == backend"
azure servicefabric service create --application-name fabric:/core-simple --service-name fabric:/core-simple/web --service-type-name WebAppService --instance-count 4 --service-kind Stateless --partition-scheme Singleton --placement-constraints "NodeType == frontend"
```