  # Kubernetes CI/CD with .NET Core Seed Web API - Template Project

    This is very basic API template project; which gets hosted in Kubernetes Cluster. API Project is structured as below

    a) SeedWebApi: Host shell; which exposes api endpoints
    b) ServiceLayer: All the business logic; processed using Domain objects 
    c) PersistenceLayer: Uses In-Memory database to store data
  
  # Prerequisites
    
    a) Installed Azure Powershell CLI (This is required to connect to Azure and execute the scripts from "Step 1" below)
    b) Docker (Containerizing the application and executing locally)
  
  # Step 1: Create Kubernetes Cluster - Using following script
  
    NOTE: For simplicity; the below resource names reflect as they are in my environment; please rename as per your environment
    
    #Note: Logon to your Azure Portal
    Login-AzAccount
    
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
    
    NOTE: Point the above three connections to your sources (Code Repository, Docker Registry and Kubernetes Cluster). If you rename the service connection; 
          then have the same names used in azure-pipelines.yml file.
    
    Pipeline is structured as below
    
    1. Uses "ubuntu-16.04" Hosted-Agent 
    
    2. Points to origin "master" of vramanavar1/seed-webapi project
    
    3. Docker@2: This step uses multi-stage build Dockerfile. The image is built and pushed to DockerHub Registry via the service connection
    
    4. replacetokens@3: This is extension added to AzureDevOps Organization. It is used to replace the tag of the Docker Image present in kubernetes 
                        manifest yml (SeedWebApi/kube-deploy.yml) file. Build Number is used to tag to newly created docker images.
    
    5. KubernetesManifest@0: This steps connects to Kubernetes via the "kubeConnection" service connection. By now SeedWebApi/kube-deploy.yml would be pointing to
                        newly created Docker Image (Essentially, by Docker Image tag getting replaced by token replacement task in previous step)
    

# Step 3: Trigger the pipeline
    
    Manually kicks-off the build job for the first time; which deploys the code to Kubernetes Cluster
    
# Step 4: Get the LoadBalancer Public IP
    
    Use the following commandlet to fetch the hosted public ip
    
    kubectl get svc label-seedwebapi -w 
    
    NOTE: Use "External-IP" column's IP from the output of previous commandlet. Use Postman and call http://<<External-IP>>/api/values. This should return 200 OK response
    
# Step 5: Demonstrate CI/CD - Make some code change 
    
    Perhaps, add some more values to api/values endpoint and push the code to the repo. 
    This action should automatically trigger the build pipeline. 
    Once the build succeeds. Hit the http://<<External-IP>>/api/values endpoint again; response should return new values (200 OK Response)

# Step 6: Release Azure Resources  (To avoid unnecessary billing)

    # Finally, when you are done working with Kubernetes commandlets; you can delete the resource group as below
    
    Remove-AzResourceGroup -Name $resourceGroupName
  
