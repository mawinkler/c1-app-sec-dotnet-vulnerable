#!/bin/bash
docker login
DOCKER_BUILDKIT=1 docker build -t c1-app-sec-dotnet2 .
docker tag c1-app-sec-dotnet2 ${DOCKER_USERNAME}/c1-app-sec-dotnet2:latest
docker push ${DOCKER_USERNAME}/c1-app-sec-dotnet2:latest

cat <<EOF | kubectl apply -f -
apiVersion: v1
kind: Service
metadata:
  annotations:
    service.alpha.kubernetes.io/tolerate-unready-endpoints: "true"
  name: c1-app-sec-dotnet2
  labels:
    app: c1-app-sec-dotnet2
spec:
  type: LoadBalancer
  ports:
  - port: 5000
    name: c1-app-sec-dotnet2
    targetPort: 5000
  selector:
    app: c1-app-sec-dotnet2
---
apiVersion: apps/v1
kind: Deployment
metadata:
  labels:
    app: c1-app-sec-dotnet2
  name: c1-app-sec-dotnet2
spec:
  replicas: 1
  selector:
    matchLabels:
      app: c1-app-sec-dotnet2
  strategy:
    type: RollingUpdate
    rollingUpdate:
      maxSurge: 1
      maxUnavailable: 1
  template:
    metadata:
      labels:
        app: c1-app-sec-dotnet2
    spec:
      containers:
      - name: c1-app-sec-dotnet2
        image: ${DOCKER_USERNAME}/c1-app-sec-dotnet2:latest
        imagePullPolicy: Always
        env:
        - name: TREND_AP_KEY
          value: ${APPSEC_KEY}
        - name: TREND_AP_SECRET
          value: ${APPSEC_SECRET}
        - name: CONNECTION_STRING
          value: Server=mysql;Database=test_app_dotnetcore;User=root;Password=;
        ports:
        - containerPort: 5000
      - name: mysql
        image: mysql:5.7
        env:
        - name: MYSQL_ALLOW_EMPTY_PASSWORD
          value: "yes"
        - name: MYSQL_DATABASE
          value: test_app_dotnetcore
EOF
