#requires -RunAsAdministrator
#requires -Modules AzureRM

Write-Host "`n `n AZURE BLUEPRINT MULTI-TIER WEB APPLICATION SOLUTION FOR FEDRAMP: Pre-Deployment Script `n" -foregroundcolor green
Write-Host "This script can be used for creating the necessary preliminary resources to deploy a multi-tier web application architecture with pre-configured security controls to help customers achieve compliance with FedRAMP requirements. See https://github.com/AppliedIS/azure-blueprint#pre-deployment for more information. `n " -foregroundcolor yellow

Write-Host "Press any key to continue ..."

$x = $host.UI.RawUI.ReadKey("NoEcho,IncludeKeyDown")

Write-Host "`n LOGIN TO AZURE `n" -foregroundcolor green
$global:azureUserName = $null
$global:azurePassword = $null

function loginToAzure{
	Param(
			[Parameter(Mandatory=$true)]
			[int]$lginCount
		)

	$global:azureUserName = Read-Host "Enter your Azure username"
	$global:azurePassword = Read-Host -assecurestring "Enter your Azure password"


	$AzureAuthCreds = New-Object System.Management.Automation.PSCredential -ArgumentList @($global:azureUserName,$global:azurePassword)
	$azureEnv = Get-AzureRmEnvironment -Name $EnvironmentName
	Login-AzureRmAccount -EnvironmentName "AzureCloud" -Credential $AzureAuthCreds

	if($?) {
		Write-Host "Login successful!"
	} else {
		if($lginCount -lt 3){
			$lginCount = $lginCount + 1

			Write-Host "Invalid Credentials! Try Logging in again"

			loginToAzure -lginCount $lginCount
		} else{

			Throw "Your credentials are incorrect or invalid exceeding maximum retries. Make sure you are using your Azure Government account information"

		}
	}
}


function orchestration
{
	Param(
		[string]$environmentName = "AzureCloud",
		[string]$location = "East US",
		[Parameter(Mandatory=$true)]
		[string]$subscriptionId,
		[Parameter(Mandatory=$true)]
		[string]$azureUserName,
		[Parameter(Mandatory=$true)]
		[SecureString]$azurePassword,
		[Parameter(Mandatory=$true)]
		[string]$environmentPrefix
	)

	$errorActionPreference = 'stop'

	try
	{
		$Exists = Get-AzureRmSubscription  -SubscriptionId $SubscriptionId
		Write-Host "Using existing authentication"
	}
	catch {

	}

	if (-not $Exists)
	{
		Write-Host "Authenticate to Azure subscription"
		Add-AzureRmAccount -EnvironmentName $EnvironmentName | Out-String | Write-Verbose
	}

	Write-Host "Selecting subscription as default"
	Select-AzureRmSubscription -SubscriptionId $SubscriptionId | Out-String | Write-Verbose

  New-AzureRmResourceGroup -Name $environmentPrefix -Location $location

  New-AzureRmResourceGroupDeployment -Name "DOL-WHD-14c-VNET" -ResourceGroupName $environmentPrefix -TemplateFile ".\deployVNET.json" -environmentPrefix $environmentPrefix -location $location

  $vmNameSuffix = Read-Host "Enter the name of your VM suffix (api, ui, db etc)"
  deployVM -location $location -environmentPrefix $environmentPrefix -vmName $vmNameSuffix

}

function deployVM
{
Param(

  [string]$environmentName = "AzureCloud",
  [Parameter(Mandatory=$true)]
  [string]$vmName,
  [Parameter(Mandatory=$true)]
  [string]$location,
  [Parameter(Mandatory=$true)]
  [string]$environmentPrefix
)
New-AzureRmResourceGroupDeployment -Name $vmName -ResourceGroupName $environmentPrefix -TemplateFile ".\azuredeploy.json" -vmNameSuffix $vmName -virtualNetworkName "$($environmentPrefix)-vnet" -subnetName "defaultSubnet" -environmentPrefix $environmentPrefix
$nextDeployment = Read-Host "do you want to create another vm? (enter 'yes' to create another vm)" 
if($nextDeployment -eq "yes") {
  $vmNameSuffix = Read-Host "Enter the name of your VM suffix (api, ui, db etc)"
  deployVM -location $location -environmentPrefix $environmentPrefix -vmName $vmNameSuffix
}

}

try{

  loginToAzure -lginCount 1

  orchestration -azureUsername $global:azureUsername -azurePassword $global:azurePassword

}
catch{
Write-Host $PSItem.Exception.Message
Write-Host "Thank You"
}
