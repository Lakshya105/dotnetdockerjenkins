pipeline {
    agent any

    environment {
        DOCKER_HUB_CREDENTIALS = 'docker-id'
        DOCKER_HUB_USERNAME = 'lakshya105'
        IMAGE_NAME = 'lakshya105/dotnet'
    }

    stages {
        stage('Clone Code') {
            steps {
                git branch: 'main', url: 'https://github.com/sairam-swamy/dotnetdockerjenkins'
            }
        }

        stage('Build Docker Image') {
            steps {
                script {
                    dockerImage = docker.build("${IMAGE_NAME}")
                }
            }
        }

        stage('Push to Docker Hub') {
            steps {
                script {
                    docker.withRegistry('https://index.docker.io/v1/', "${DOCKER_HUB_CREDENTIALS}") {
                        dockerImage.push('latest')
                    }
                }
            }
        }
    }
}


