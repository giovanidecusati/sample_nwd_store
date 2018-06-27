az login --username <account> --password <password>
az account set --subscription=<subscription id or name>
az group create --name nwdstore-australiaeast-prd-rg --location australiaeast
az aks create --resource-group nwdstore-australiaeast-prd-rg  --name nwdstore-aks --node-count 1 --generate-ssh-keys
az aks install-cli
az aks get-credentials --resource-group nwdstore-australiaeast-prd-rg --name nwdstore-aks
az aks browse --resource-group nwdstore-australiaeast-prd-rg --name nwdstore-aks