echo 'ELB'
aws elbv2 create-load-balancer --name corewebapi-nlb --scheme internet-facing --type network --subnets subnet-05c25ea8b5376adac subnet-0f343fefb6cfed5db
aws elbv2 describe-load-balancers --query LoadBalancers[].LoadBalancerArn[] --output text --names corewebapi-nlb >temp
set /p elb_arn=<temp

echo 'Target group'
aws elbv2 create-target-group --name corewebapi-tg --port 8080 --protocol TCP --target-type ip --vpc-id vpc-0352eb5dcafe265e5 --health-check-path /actuator/health --health-check-protocol HTTP --healthy-threshold-count 3 --unhealthy-threshold-count 3
aws elbv2 describe-target-groups --query TargetGroups[].TargetGroupArn[] --output text --names corewebapi-tg >temp
set /p tg_arn=<temp

echo %elb_arn%
echo %tg_arn%

echo 'ELB listener'
aws elbv2 create-listener --default-actions TargetGroupArn=%tg_arn%,Type=forward --load-balancer-arn %elb_arn% --port 80 --protocol TCP