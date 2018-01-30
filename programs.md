# Commands

## General Commands

fast info

fast diagnose # diagnoses current environment 
fast validate # validates current product 

## Configuration Commands

fast init

- scm: 1=github
- pipeline: 2=circleci
- provider: 3=digital_ocean
- orchestration: 1=swarm

fast pipeline add --overwrite [name]
# Adds a new pipeline

fast pipeline remove --force [name]
fast pipeline visualise 
fast provider add -o [name]
fast provider remove -f [name]

## VM Commands

fast vm list # Lists the vms
* Lists out key normalised attributes
* ID, Name, Base, Size and Region

fast vm add -o [name] # Adds a vm
* If VM exists and -o not present then 
* If VM does not exist then creates template and startup script

fast vm remove -f [name] # Removes a vm
* If VM exists and -f is set replace VM with template
* If VM does not exist then creates
* RESULT: Print and no error code
* ERRORS:
    * If VM exists and -f not set it errors
    * If template is not found
    * Invalid name characters 
    * Unexpected error

fast vm apply [name] # Applies a vm
* If VM exists then applys
* RESULT: Print and no error
* ERRORS:
    * VM does not exist
    * Validation error
    * Unexpected error

fast vm apply -a # Applies all vms
* Applies all errors
* RESULT: Augmented file with with no error
* ERRORS:
    * VM does not exist
    * Validation error
    * Unexpected error

fast vm destroy [name] # Destroys the named vms image
fast vm destroy -a -f  # Destroys all vms 

## Infrastructure Layer Commands

fast layer add -o [name] # Applies an infra layer 
fast layer remove -f [name] # Removes an infra layer
fast layer apply [name] [env] # Applies an infarstructure layer
fast layer destroy [name] [env] # Applies an infarstructure layer

fast stack up
fast stack down 
fast stack deploy

# Health Commands

fast health ping -n [addr]
fast health scan -p -s [addr]
fast health http [addr]
fast health https [addr]

Application Folder Structure

\bin
    ...
\templates
    \pipeline
        \circleci
        \travis
    \provider
        \digital_ocean
            \vm
                ...
            \layers
                ...
        \gcp
            \vm
                ...
            \layers
                ...

config:
    cloud
        - digital_ocean
    pipelines 
        - circleci
    orchestration
        - swarm

vms:
    vm: master
    vm: node

layers:
    layer: network
    layer: masters
    layer: nodes