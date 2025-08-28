# TodoApi

A sample **ASP.NET Core Web API** (minimal APIs) demonstrating how to containerize a .NET app with **Docker**, and deploy it to **Kubernetes** using **Helm**.  

This project was built as a learning exercise in modern cloud-native development: building, packaging, deploying, and scaling applications with Kubernetes.  

---

## Features
- ✅ Minimal ASP.NET Core Web API with a **Todo** endpoint and Swagger UI  
- ✅ Dockerized via a `Dockerfile`  
- ✅ Helm chart for Kubernetes deployment (`./todoapi/`)  
- ✅ Configurations injected via **ConfigMap** and **Secret**  
- ✅ Ingress routing with **NGINX Ingress Controller**  
- ✅ **Horizontal Pod Autoscaler (HPA)** scaling based on CPU usage  
- ✅ GitHub Actions CI workflow to build and push container images to **GitHub Container Registry (GHCR)**  

---

## Getting Started

### Run locally (Visual Studio / .NET CLI)
```bash
dotnet run --project TodoApi/TodoApi.csproj
