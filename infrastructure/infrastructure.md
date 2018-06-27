# Infrastrucutre
Well, here you have two options to create Northwind environment. First you can use this step-by-step or you can change [infrastructure.sh](infrastructure.sh) according your user, password and subscription.

## Set up your account
```
az login --username <account> --password <password>
az account view
az account set --subscription=<subscription id or name>
```
If you are using multifactor authentication proably this will fail, so just type *az login*.

## Creating a resource group
```
az group create --name nwdstore-australiaeast-prd-rg --location australiaeast
```

## Creating AKS cluster
```
az aks create --resource-group nwdstore-australiaeast-prd-rg  --name nwdstore-aks --node-count 1 --generate-ssh-keys
```

## Install Kubernetes command line tool
```
az aks install-cli
```

# Download AKS Cluster keys
```
az aks get-credentials --resource-group nwdstore-australiaeast-prd-rg --name nwdstore-aks
```

# Environment testing
If every thing goes well you be able to browse Kubernetes interface. Open your browse and navigate to http://localhost:8001 after type the command below:
```
az aks browse --resource-group nwdstore-australiaeast-prd-rg --name nwdstore-aks
```