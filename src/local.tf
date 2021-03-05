terraform {
  required_providers {
    kubernetes = {
      source  = "hashicorp/kubernetes"
      version = ">= 2.0.0"
    }
  }
}

provider "kubernetes" {
  config_path = "~/.kube/config"
}

resource "kubernetes_namespace" "accounts" {
  metadata {
    name = "accounts-business"
  }
}

resource "kubernetes_deployment" "accounts" {
  metadata {
    name = "accounts-business"
    namespace = kubernetes_namespace.accounts.metadata.0.name
    labels = {
      app = "AccountsBusiness"
    }
  }

  spec {
    replicas = 2
    selector {
      match_labels = {
        app = "accounts-business"
      }
    }
    template {
      metadata {
        labels = {
          app = "accounts-business"
        }
      }
      spec {
        container {
          image = "accounts-business:latest"
          name = "accounts-business-container"
          image_pull_policy = "IfNotPresent"
          port {
            container_port = 80
            protocol = "TCP"
          }
        }
      }
    }
  }
}