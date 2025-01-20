#!/bin/bash

namespace=$1
localPort=$2
appName=$3
errored=false

if [ -z "$namespace" ]; then
    echo $'\nK8s namespace not informed!'
    echo $'\nHere\'s a list of namespaces for the current K8s context:\n\n'
    kubectl get namespaces
    errored=true
fi

if [ -z "$localPort" ]; then
    echo $'\nLocal port not informed!'
   errored=true
fi

if [ -z "$appName" ]; then
    echo $'\nApp name not informed!'
    if [ -z "$namespace" ]; then
        echo $'\nHere\'s a list of deployments for the current K8s namespace:\n\n'
        kubectl get deployments
    else
        echo $'\nHere\'s a list of deployments for the entered K8s namespace:\n\n'
        kubectl get deployments --namespace $namespace
   fi 
    errored=true
fi

if [ "$errored" = true ]; then
    echo $'\nCommand usage: `./port-forward.sh <k8s-namespace> <localPort> <appName>`\n\n'
    exit 1
fi

echo -e "\nForwarding local port $localPort for app $appName in namespace $namespace...\n"

export POD_NAME=$(kubectl get pods --namespace $namespace -l "app=$appName" -o jsonpath="{.items[0].metadata.name}")

export CONTAINER_PORT=$(kubectl get pod --namespace $namespace $POD_NAME -o jsonpath="{.spec.containers[0].ports[0].containerPort}")

kubectl --namespace $namespace port-forward $POD_NAME $localPort:$CONTAINER_PORT

echo -e "\n\nEnded port forwarding from local port $localPort to container port $CONTAINER_PORT on pod $POD_NAME.\n"