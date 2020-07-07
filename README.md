# Kubernetes CI/CD with .NET Core Seed Web API - Template Project

This is very basic API template api project; which gets hosted in Kubernetes Cluster

  a) As first step it gets latest version from the master
  
  b) Builds and Pushes Docker image to Docker Registry; Build Number is used to tag the Docker Image
  
  c) As part of build pipeline step; replaces the Container Image number from Kubernetes Manifest yaml file
  
  d) Finally, the Kubernetes manifest yaml file is applied to the cluster
  
  # Step 1: Create Kubernetes Cluster - Using following script
  
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
  
# Step 2: Create Azure DevOps Pipeline using azure-pipelines.yml defined in the project
    
    AzureDevOps pipeline azure-pipelines.yml defined in this project uses following three service connections
    
    a) vramanavar1 : Connection to GitHub Code Repository
    b) vramanavarDockerRegistry: Connection to Docker Hub Registry
    c) kubeConnection: Connection to Kubernetes Cluster
    
    Pipeline is structured as below
    
    1. Uses "ubuntu-16.04" Hosted-Agent 
    
    2. Points to origin "master" of vramanavar1/seed-webapi project
    
    3. Docker@2: This step uses multi-stage build Dockerfile. The image is built and pushed to DockerHub Registry via the service connection
    
    4. replacetokens@3: This is extension added to AzureDevOps Organization. It is used to replace the tag of the Docker Image present in kubernetes 
                        manifest yml (SeedWebApi/kube-deploy.yml) file. Build Number is used to tag to newly created docker images.
    
    5. KubernetesManifest@0: This steps connects to Kubernetes via the "kubeConnection" service connection. By now SeedWebApi/kube-deploy.yml would be pointing to
                        newly created Docker Image (Essentially, by Docker Image tag getting replaced by token replacement task in previous step)
    
# Release Azure Resources  (To avoid unnecessary billing)
    # Finally, when you are done working with Kubernetes commandlets; you can delete the resource group as below
    
    Remove-AzResourceGroup -Name $resourceGroupName
  
