# .NET Core Seed Web API - Template Project

This is very basic API template api project; which gets hosted in Kubernetes Cluster

  a) As first step it gets latest version from the master
  
  b) Builds and Pushes Docker image to Docker Registry; Build Number is used to tag the Docker Image
  
  c) As part of build pipeline step; replaces the Container Image number from Kubernetes Manifest yaml file
  
  d) Finally, the Kubernetes manifest yaml file is applied to the cluster
  
  
  # Kubernetes Cluster Creation Script
  
    $subscriptionId = (Get-AzSubscription -SubscriptionName Polyglot-Development-PayAsYouGo).Id
    $resourceGroupName = "pgt-seedwebapi-aue-rg"
    $aksClusterName = "kube-seedwebapi-cluster"
    $sku = "Standard_A2"
    $region = "Australia East"
    $kubernetesVersion = "1.16.10"
  
    Set-AzContext -SubscriptionId $subscriptionId
    New-AzResourceGroup -Name $resourceGroupName -Location $region
    
    #NOTE: Switched to az commands as Powershell kubernetes "Az" commands are not supported in Australia East Region
    # Using Kubernetes 1.16.10 version in below command; which is important; since this version defines how the kube-deploy.yml tempate 
    # should be structured.
    az aks create -g $resourceGroupName -n $aksClusterName --node-count 1 --node-vm-size $sku --kubernetes-version $kubernetesVersion
    
    # Connect to the created Kubernetes Cluster
    az aks get-credentials -n $aksClusterName -g $resourceGroupName
    
    # Make sure the Kubernetes Cluster is indeed created and you are getting the desired number of nodes
    kubectl get nodes
  
  
# Release Azure Resources  (To avoid unnecessary billing)
    # Finally, when you are done working with Kubernetes commandlets; you can delete the resource group as below
    
    Remove-AzResourceGroup -Name $resourceGroupName
  
