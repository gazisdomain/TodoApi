#TodoApi

A sample ASP.NET Core Web API (minimal APIs) demonstrating how to containerize a .NET app with Docker, and deploy it to Kubernetes using Helm.

This project was built as a learning exercise in modern cloud-native development: building, packaging, deploying, and scaling applications with Kubernetes.

##Features

✅ Minimal ASP.NET Core Web API with a Todo endpoint and Swagger UI

✅ Dockerized via a Dockerfile

✅ Helm chart for Kubernetes deployment (./todoapi/)

✅ Configurations injected via ConfigMap and Secret

✅ Ingress routing with NGINX Ingress Controller

✅ Horizontal Pod Autoscaler (HPA) scaling based on CPU usage

✅ GitHub Actions CI workflow to build and push container images to GitHub Container Registry (GHCR)

##Getting Started
Run locally (Visual Studio / .NET CLI)
dotnet run --project TodoApi/TodoApi.csproj


Then open:

http://localhost:5259/swagger

##Run with Docker
# build
docker build -t todoapi:local ./TodoApi

# run
docker run -it --rm -p 8080:80 todoapi:local


Visit:

http://localhost:8080/swagger

##Deploy to Kubernetes (local cluster e.g., Rancher Desktop, minikube, Docker Desktop)

Install dependencies:

kubectl

helm

ingress-nginx (for ingress routing)

Install/upgrade the chart:

helm upgrade --install todo ./todoapi \
  --set image.repository=ghcr.io/<your-username>/todoapi \
  --set image.tag=latest


Port-forward ingress controller:

kubectl -n ingress-nginx port-forward svc/ingress-nginx-controller 8080:80


Open:

http://todo.localtest.me:8080/swagger

##Autoscaling Demo

Ensure HPA is enabled in values.yaml.

Generate load inside the cluster:

kubectl create deployment loadgen --image=busybox -- \
  /bin/sh -c "while true; do wget -q -O- http://todoapi.default.svc.cluster.local/work?ms=200 >/dev/null; done"


Watch scaling:

kubectl get hpa -w
kubectl get pods -l app=todoapi -w

##GitHub Actions CI/CD

A workflow at .github/workflows/ci.yml:

Builds the .NET app

Builds a Docker image

Pushes to GHCR under your account:

ghcr.io/<your-username>/todoapi:latest
ghcr.io/<your-username>/todoapi:<commit-sha>

##Next Steps

Deploy to a cloud Kubernetes cluster (AKS, EKS, GKE, etc.)

Add persistence (e.g., SQL database via StatefulSet)

Implement CI/CD pipeline that deploys automatically on merge to main