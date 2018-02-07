# Fast

## Overview

The goal of Fast is to be able to common tasks fast.  It's designed to support repeatability, best practice and consistency whilst remaining extensible for other domains.

It's highly configurable for your own workflow and requirements through templates, domain plugin and tool extensions.

## High Level Architecture

TODO: Insert Image here

## Drivers

Interfaces enable the Fast framework to be executed in application architectures.  Initially Fast provides a Command Line Interafce (CLI) that can be used manually or integrated into automation scripts.  

It is envisaged that future drivers will be developed (e.g. Protobuf, REST, Mobile)

## Templates

The following templates are available:

1. Local Template

More details can be found below.

## Tools

Fast purpose is *not* to replace best of breed commercial and open source tools, it is intended to facilitate their integration and adoption through providing a higher level abstraction which is simple to adopt.

The following extensions are available:

1. GitHub
2. CircleCI
3. Hashicorp Packer 
4. Hashicorp Terraform
5. Docker, Compose & Swarm
6. Digital Ocean

More details can be found below.