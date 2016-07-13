#
# Start_AARunbook.ps1
#
# Start Azure Automation Runbook

Param(
[string]$RBAID
)
$TypePath = (Get-ItemProperty "hklm:\software\microsoft\system center\2010\service manager\setup").InstallDirectory + "scsm.azureautomation.wpf.dll"
Add-Type -Path $TypePath

$SMObj = Get-SCSMObject -Class (Get-SCSMClass -Name SCSM.AzureAutomation.Runbook.Activity$ ) -Filter "ID -eq $RBAID"
$ConnectorID = $SMObj.ConnectorID


$SMObject = Get-SCSMObject -Class (Get-SCSMClass -Name SCSM.AzureAutomation.Connector$) -Filter "ConnectorID -eq $ConnectorID"
$SubscriptionID = $SMObject.SubscriptionID
$AutomationAccountName = $SMObject.AutomationAccount
$username = $SMObject.RunAsAccountName
$encryptedPassword = $SMObject.RunAsAccountPassword
$secpassword = ConvertTo-SecureString([SCSM.AzureAutomation.WPF.Connector.StringCipher]::Decrypt($encryptedPassword,$username)) -AsPlainText -Force
$ResourceGroup = $SMObject.ResourceGroup

$Creds = New-Object System.Management.Automation.PSCredential ($username, $secpassword)

if($SMObj.AAPropertyMapping -ne $null)
{
	## Build Params
	$Params = $SMObject.AAPropertyMapping | ConvertFrom-Json
}
Login-AzureRmAccount -Credential $Creds -SubscriptionId $SubscriptionID

If($HBRW)
{
	if($Params)
	{
		$JobID = Start-AzureRmAutomationRunbook -Name $RunbookName -AutomationAccountName $AccountName -ResourceGroupName $ResourceGroup -Parameters $Params -RunOn $HBRW
	}
	else
	{
		$JobID = Start-AzureRmAutomationRunbook -Name $RunbookName -AutomationAccountName $AccountName -ResourceGroupName $ResourceGroup -RunOn $HBRW
	}
}
else
{
	if($Params)
	{
		$JobID = Start-AzureRmAutomationRunbook -Name $RunbookName -AutomationAccountName $AccountName -ResourceGroupName $ResourceGroup -Parameters $Params
	}
	else
	{
		$JobID = Start-AzureRmAutomationRunbook -Name $RunbookName -AutomationAccountName $AccountName -ResourceGroupName $ResourceGroup 
	}
}

Set-SCSMObject -SMObject $SMObj -Property JobID -Value $JobID