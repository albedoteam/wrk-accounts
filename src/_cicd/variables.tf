// main/common variables

variable "namespace" {
  description = "Albedo Team product's namespace"
  type        = string
  default     = "albedoteam-products"
}

variable "do_registry_name" {
  description = "Digital Ocean registry name"
  type        = string
  default     = "registry.digitalocean.com/albedoteam"
}

// project variables
variable "project_secrets_name" {
  description = "Secrets name"
  type        = string
  default     = "accounts-business-secrets"
}

variable "project_name" {
  description = "Source name"
  type        = string
  default     = "accounts-business"
}

variable "project_label" {
  description = "Deployment Label / Container Name"
  type        = string
  default     = "accountsBusiness"
}

variable "project_image_tag" {
  description = "Image tag to be pulled from registry"
  type        = string
  default     = "latest"
}

variable "project_replicas_count" {
  description = "Number of container replicas to provision."
  type        = number
}

variable "environment_prefix" {
  description = "Host environment to naming pattern"
  type        = string
}

// project settings variables
variable "settings_broker_connection_string" {
  description = "Broker Connection String"
  type        = string
  sensitive   = true
  default     = ""
}

variable "settings_db_connection_string" {
  description = "Db Connection String"
  type        = string
  sensitive   = true
  default     = ""
}

variable "settings_db_name" {
  description = "Db Name"
  type        = string
  default     = ""
}