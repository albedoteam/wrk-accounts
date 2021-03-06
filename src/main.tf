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
    name = var.src_name
  }
}

resource "kubernetes_deployment" "accounts" {
  metadata {
    name      = var.src_name
    namespace = kubernetes_namespace.accounts.metadata.0.name
    labels = {
      app = var.deployment_label
    }
  }

  spec {
    replicas = var.replicas_count
    selector {
      match_labels = {
        app = var.src_name
      }
    }
    template {
      metadata {
        labels = {
          app = var.src_name
        }
      }
      spec {
        container {
          image             = "${var.src_name}:latest"
          name              = "${var.src_name}-container"
          image_pull_policy = "IfNotPresent"
          resources {
            limits = {
              cpu    = "0.5"
              memory = "512Mi"
            }
          }
          port {
            container_port = 80
            protocol       = "TCP"
          }
          env {
            name  = "Broker_Host"
            value = var.broker_connection_string
          }
          env {
            name  = "DatabaseSettings_ConnectionString"
            value = var.db_connection_string
          }
          env {
            name  = "DatabaseSettings_DatabaseName"
            value = var.db_name
          }
          
        }
      }
    }
  }
}