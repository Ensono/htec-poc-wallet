# Script to get the data from the Terraform outputs and expose them
# as ADO variables

# Get the TF outputs
$tfoutputs = Invoke-Terraform -Output | ConvertFrom-Json

# Iterate around the keys and set the ADO variables
$objMembers = $tfoutputs.psobject.Members | Where-Object MemberType -like 'noteproperty'

foreach ($member in $objMembers) {

    # Output variables for ADO
    Write-Host ("##vso[task.setvariable variable={0};]{1}" -f $member.name, $member.value)
}