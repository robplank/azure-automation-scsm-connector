#
# Get_JobStatus.ps1
#
# Get Job Status for Running Jobs

# This logic could be improved to removed activities from active status if a AA Job was not created.


$TypePath = (Get-ItemProperty "hklm:\software\microsoft\system center\2010\service manager\setup").InstallDirectory + "scsm.azureautomation.wpf.dll"
Add-Type -Path $TypePath

$AAActivities = Get-SCSMObject -Class (Get-SCSMClass -Name SCSM.AzureAutomation.Runbook.Activity$) -Filter "Status -eq '11fc3cef-15e5-bca4-dee0-9c1155ec8d83'"
if($AAActivities -ne $null)
{
	$AAConnectors = Get-SCSMObject -Class (Get-SCSMClass -Name SCSM.AzureAutomation.Connector$)
	foreach($connector in $AAConnectors)
	{
		$ConnectorConfig = Get-SCSMconnector -ID ($connector.ID)
		if($connectorConfig.Enabled -eq $true)
		{
			$SubscriptionID = $connector.SubscriptionID
			$AutomationAccountName = $connector.AutomationAccount
			$username = $connector.RunAsAccountName
			$encryptedPassword = $connector.RunAsAccountPassword
			$secpassword = ConvertTo-SecureString([SCSM.AzureAutomation.WPF.Connector.StringCipher]::Decrypt($encryptedPassword,$username)) -AsPlainText -Force
			$ResourceGroup = $connector.ResourceGroup

			$Creds = New-Object System.Management.Automation.PSCredential ($username, $secpassword)
			Login-AzureRmAccount -Credential $Creds -SubscriptionId $SubscriptionID
			$Activities = $AAActivities | ? {$_.ConnectorID -eq $connector.ID}
			foreach($Activity in $Activities)
			{
				$job = Get-AzureRmAutomationJob -ResourceGroupName $ResourceGroup -AutomationAccountName $AutomationAccountName -Id $Activity.JobID
				if($job -eq $null)
				{
					#Add Code here to either change the status of the activity or retry (questions: how many retires, what if internet goes down)
				}
				else
				{
					if ($job.Status -match "Completed")
					{ 
						if ($ReturnJobOutput) 
						{ 
							# Output 
							$jobout = Get-AzureRmAutomationJobOutput ` 
										-Id $job.Id ` 
										-AutomationAccountName $AutomationAccountName `
										-ResourceGroupName $ResourceGroup ` 
										-Stream Output 
							if ($jobout) 
							{
						$HT = @{
						Output = $jobout.Text
						Status = "Completed"
						}
						Set-SCSMObject -SMObject $Activity -PropertyHashtable $HT
					}
							else
							{
						Set-SCSMObject -SMObject $Activity -Property Status -Value "Completed"
					}

						}
					}
				}

			}
		}
	}
}

##################################3
#end of script code below saved for future versions










    foreach($Activity in $ActiveJobs)
    {
	    $job = Get-AzureRmAutomationJob -ResourceGroupName $ResourceGroup -AutomationAccountName $AutomationAccountName -Id $Activity.JobID
    if ($job -eq $null) { 
            # No Job was created
		    # throw an exception
		    # Need to Add Code here
    } 
    else 
    { 
        
    # Monitor the job until finish or timeout limit has been reached 
    $maxDateTimeout = (Get-Date).AddSeconds($JobPollingTimeoutInSeconds)
             
    $doLoop = $true 
             
    while($doLoop) { 
        Start-Sleep -s $JobPollingIntervalInSeconds 
                 
        $job = Get-AzureRmAutomationJob ` 
            -Id $job.Id ` 
            -AutomationAccountName $AutomationAccountName
		    -ResourceGroupName $ResourceGroup 
                 
        if ($maxDateTimeout -lt (Get-Date)) { 
            # timeout limit reached so exception 
                
        } 
                 
        $doLoop = (($job.Status -notmatch "Completed") -and ($job.Status -notmatch "Failed") -and ($job.Status -notmatch "Suspended") -and ($job.Status -notmatch "Stopped")) 
    } 
             
        if ($job.Status -match "Completed") { 
            if ($ReturnJobOutput) { 
                # Output 
                $jobout = Get-AzureRmAutomationJobOutput ` 
                                -Id $job.Id ` 
                                -AutomationAccountName $AutomationAccountName `
							    -ResourceGroupName $ResourceGroup ` 
                                -Stream Output 
                if ($jobout) {Write-Output $jobout.Text} 
                     
                # Error 
                $jobout = Get-AzureRmAutomationJobOutput ` 
                                -Id $job.Id ` 
                                -AutomationAccountName $AutomationAccountName `
							    -ResourceGroupName $ResourceGroup ` 
                                -Stream Error 
                if ($jobout) {Write-Error $jobout.Text} 
                     
                # Warning 
                $jobout = Get-AzureRmAutomationJobOutput ` 
                                -Id $job.Id ` 
                                -AutomationAccountName $AutomationAccountName ` 
							    -ResourceGroupName $ResourceGroup `
                                -Stream Warning 
                if ($jobout) {Write-Warning $jobout.Text} 
                     
                # Verbose 
                $jobout = Get-AzureRmAutomationJobOutput ` 
                                -Id $job.Id ` 
                                -AutomationAccountName $AutomationAccountName `
							    -ResourceGroupName $ResourceGroup ` 
                                -Stream Verbose 
                if ($jobout) {Write-Verbose $jobout.Text} 
            } 
            else { 
                # Return the job id 
                    
            } 
        } 
        else { 
            # The job did not complete successfully, so throw an exception 
               
        } 
    }

    # Add Code to Update the SCSM Runbook Activity        
    # this needs to be updated to reflect the real results from Azure Automation
    Set-SCSMObject -SMObject $Activity -Property Status -Value "Completed"
    }
}


