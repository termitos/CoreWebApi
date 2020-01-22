aws sts get-caller-identity --query Account --output text > temp
set /p account_id=<temp
set s3_bucket_name=corewebapi-artifacts-%account_id%
aws s3 mb s3://%s3_bucket_name%
aws s3api put-bucket-policy --bucket %s3_bucket_name% --policy file://../Infrastructure/artifacts-bucket-policy.json
