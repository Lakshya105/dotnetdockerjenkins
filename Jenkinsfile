pipeline {
    agent any

    environment {
        ACR_NAME = "capgeminidemotr.azurecr.io"
        IMAGE_NAME = "productapi"
        BUILD_TAGGED = "${IMAGE_NAME}:${BUILD_NUMBER}"
        AZURE_RG = "capgemini_training"
        ACI_NAME = "capgeminidemotr"
        LOCATION = "CentralIndia"
    }

    stages {
        stage('Checkout') {
            steps {
                git 'https://github.com/sairam-swamy/dotnetdockerjenkins.git'
            }
        }

        stage('Build .NET Project') {
            steps {
                sh 'dotnet publish -c Release -o out'
            }
        }

        stage('Build Docker Image') {
            steps {
                sh """
                docker build -t ${IMAGE_NAME}:${BUILD_NUMBER} .
                docker tag ${IMAGE_NAME}:${BUILD_NUMBER} ${ACR_NAME}/${BUILD_TAGGED}
                """
            }
        }

        stage('Push to ACR') {
            steps {
                withCredentials([usernamePassword(credentialsId: 'acr-creds', usernameVariable: 'Username', passwordVariable: 'Password')]) {
                    sh """
                    echo $Password | docker login ${ACR_NAME} -u $Username --password-stdin
                    docker push ${ACR_NAME}/${BUILD_TAGGED}
                    """
                }
            }
        }

        stage('Deploy to ACI') {
            steps {
                withCredentials([usernamePassword(credentialsId: 'acr-creds', usernameVariable: 'Username', passwordVariable: 'Password')]) {
                    sh """
                    az container delete --name ${ACI_NAME} --resource-group ${AZURE_RG} --yes || true

                    az container create \
                      --resource-group ${AZURE_RG} \
                      --name ${ACI_NAME} \
                      --image ${ACR_NAME}/${BUILD_TAGGED} \
                      --registry-login-server ${ACR_NAME} \
                      --registry-username $Username \
                      --registry-password $Password \
                      --dns-name-label product-api-${BUILD_NUMBER} \
                      --ports 80 \
                      --location ${LOCATION}
                    """
                }
            }
        }
    }
}

