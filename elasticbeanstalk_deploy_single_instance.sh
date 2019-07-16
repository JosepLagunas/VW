#!/bin/bash -e

#AWS_REGION="eu-west-1"
#AWS_ACCOUNT_ID="AKIA5ZBDVSMBDSOXPCXG"
#AWS_SECRET_ID="Ykv/0YdiIgVddntbeQqdJpX7Qk/FccjglvUom865"
#DOCKER_REPO="webapp-images"
#TRAVIS_TAG=""
#TRAVIS_BRANCH="development"
#TRAVIS_BUILD_NUMBER="1"
#TRAVIS_COMMIT="4123fe4"
#ELB_DEV_ENVIRONMENT="LaklpPlatform-env"
#S3_BUCKET="webplatform-ci-cd"
#APPLICATION_NAME="laklp-platform"

REGISTRY_URL=${AWS_ACCOUNT_ID}.dkr.ecr.${AWS_REGION}.amazonaws.com
SOURCE_IMAGE="${DOCKER_REPO}"
# using it as there will be 2 versions published
TARGET_IMAGE="${REGISTRY_URL}/${DOCKER_REPO}"

TAG_VERSION_IF_EXISTS=""

if [ "${TRAVIS_TAG}" != "" ]; then 
	echo "entra"
	TAG_VERSION_IF_EXISTS="-tag-${TRAVIS_TAG}"
fi

VERSION=${TRAVIS_BRANCH}-${TRAVIS_BUILD_NUMBER}-${TRAVIS_COMMIT}${TAG_VERSION_IF_EXISTS}
TARGET_IMAGE_VERSIONED="${TARGET_IMAGE}:${VERSION}"

ELB_VERSION=${VERSION}

ELB_ENVIRONMENT=""

if [ "${TRAVIS_BRANCH}" == "development" ]; then
	ELB_ENVIRONMENT=${ELB_DEV_ENVIRONMENT}
elif [ ${TRAVIS_BRANCH} == "staging" ]; then
	ELB_ENVIRONMENT=${ELB_DEV_ENVIRONMENT}
elif [ ${TRAVIS_BRANCH} == "master" ]; then
	ELB_ENVIRONMENT=${ELB_PRO_ENVIRONMENT}
else
	#ends deployment execution
	echo "Invalid branh [${TRAVIS_BRANCH}] to be deployed."
	echo "exited with 1"
	exit 1
fi

CONTENT="{\t\n
  \t\"AWSEBDockerrunVersion\": \"1\",\n
  \t\"Image\": {\n
    \t\"Name\": \"${TARGET_IMAGE_VERSIONED}\",\n
    \t\"Update\": \"true\"\n
  \t},\n
  \t\"Ports\": [\n
    \t\t{\n
      \t\t\"ContainerPort\": \"80\"\n
    \t\t}\n
  \t]\n
}"

FILENAME="Dockerrun.aws.json"

echo "Creating deployment zip folder"
echo cat ./${FILENAME}
echo -e $CONTENT > ${FILENAME}
echo zip ./$VERSION.zip ./.ebextensions/*.config ./$FILENAME
zip ./$VERSION.zip ./.ebextensions/*.config ./$FILENAME
echo "Uploading deployment ZIP file to S3 bucket: ${S3_BUCKET}"
echo aws s3 cp $VERSION.zip s3://$S3_BUCKET --region $AWS_REGION
aws s3 cp $VERSION.zip s3://$S3_BUCKET --region $AWS_REGION
echo "Deploying $VERSION to $ELB_ENVIRONMENT"
echo "Creating application version"
echo aws elasticbeanstalk create-application-version --application-name "${APPLICATION_NAME}" --version-label "${ELB_VERSION}" --source-bundle S3Bucket="${S3_BUCKET}",S3Key="${FILENAME}" --region "${AWS_REGION}"
aws elasticbeanstalk create-application-version --application-name "${APPLICATION_NAME}" --version-label "${ELB_VERSION}" --source-bundle S3Bucket="${S3_BUCKET}",S3Key="${FILENAME}" --region "${AWS_REGION}"
echo "Updating ${ELB_ENVIRONMENT} environment"
echo aws elasticbeanstalk update-environment --application-name "${APPLICATION_NAME}" --environment-name "${ELB_ENVIRONMENT}" --version-label "${ELB_VERSION}" --region "${AWS_REGION}"
aws elasticbeanstalk update-environment --application-name "${APPLICATION_NAME}" --environment-name "${ELB_ENVIRONMENT}" --version-label "${ELB_VERSION}" --region "${AWS_REGION}"
echo cleaning temporary files...
rm "${FILENAME}"
rm ./$VERSION.zip
echo "done."
